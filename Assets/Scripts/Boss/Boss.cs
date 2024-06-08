using UnityEngine;
public class Boss : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float followDistance = 0.5f;
    public Rigidbody2D enemyRb;
    private Transform playerTransform;
    public PlayerMovement playerMovement;
    private PlayerBody PlayerBodyScript = null;
    public float contactTimeThreshold = 3.0f;
    [SerializeField]
    public float contactTimer = 0.0f;
    public float EKBForce;
    public float EKBCounter;
    public float EKBTotalTime;
    public bool EKnockFromRight;
    public bool isInContact = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null && EKBCounter <=0)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            if (EKnockFromRight == true)
            {
                enemyRb.velocity = new Vector2(-EKBForce, EKBForce);
            }
            if (EKnockFromRight == false)
            {
                enemyRb.velocity = new Vector2(EKBForce, EKBForce);
            }
            EKBCounter -= Time.deltaTime;
        }
    }

    void Update()
    {
        if (PlayerBodyScript == null)
        {
            PlayerBodyScript = FindObjectOfType<PlayerBody>();
            if (PlayerBodyScript == null)
            {
                return;
            }
        }
        if (PlayerBodyScript != null)
        { 
            if (isInContact)
            {
                contactTimer += Time.deltaTime;

                if (contactTimer >= contactTimeThreshold)
                {         
                    resetTimer();
                    PlayerBodyScript.loseHealth();
                    animator.SetBool("IsAttacking", false);
                    return;   
                }
                

            }
            else
            {
                resetTimer();
            }
        }

        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;

                if (animator != null)
                {
                    animator.SetBool("IsMoving", true);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerBodyScript != null)
            {
                inContact();
                Debug.Log("Contact with player started.");
                
            }
            else
            {
                Debug.LogError("PlayerBodyScript is null in OnTriggerEnter2D.");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        notInContact();
        resetTimer();
        return;
    }

    public void resetTimer()
    {
        contactTimer = 0.0f;
    }

    public void inContact()
    {
        isInContact = true;
    }

    public void notInContact()
    {
        isInContact = false;
    }
}
