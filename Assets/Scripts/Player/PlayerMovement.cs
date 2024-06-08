using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    private Animator animator;
    public Rigidbody2D playerRb;
    private bool isFacingRight = true; // Start facing right by default
    
    private bool canRoll = true;
    private bool isRolling;
    private float rollPower = 5f;
    private float rollTime = 0.5f;
    private float rollCooldown = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isRolling)
        {
            return;
        }

        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite(horizontalInput);

        // Check if the player is walking
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.SetFloat("isWalking", 1);
        }
        else
        {
            animator.SetFloat("isWalking", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
        {
            StartCoroutine(Roll());
        }

        // Check if the player is not getting knockbacked
        if (!isRolling && playerRb.velocity.magnitude < 0.1f)
        {
            // Calculate movement vector
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;

            transform.Translate(movement);
        }
    }


    void FlipSprite(float horizontalInput)
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private IEnumerator Roll()
    {
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        canRoll = false;
        isRolling = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(transform.localScale.x * rollPower, 0f);
        yield return new WaitForSeconds(rollTime);
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        playerRb.gravityScale = originalGravity;
        isRolling = false;
        yield return new WaitForSeconds(rollCooldown);    
        canRoll = true;
    }

    public void ApplyKnockback(Vector2 knockbackForce)
    {
        playerRb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
