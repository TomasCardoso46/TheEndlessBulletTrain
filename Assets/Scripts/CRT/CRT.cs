using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float followDistance = 0.5f;




    private Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {

            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;

                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
            }
            else
            {
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
            }

        }

        
    }





    
}