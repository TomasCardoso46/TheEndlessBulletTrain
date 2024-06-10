using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    public float rollSpeedMultiplier = 2f; // Speed multiplier during roll
    private Animator animator;
    public Rigidbody2D playerRb;
    public Audio audioScript;
    private bool isFacingRight = true; // Start facing right by default
    
    private bool canRoll = true;
    private bool isRolling;
    private float rollTime = 0.5f;
    private float rollCooldown = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Handle sprite flipping
        FlipSprite(horizontalInput);

        // Handle walking animation
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.SetFloat("isWalking", 1);
        }
        else
        {
            animator.SetFloat("isWalking", 0);
        }

        // Handle rolling
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
        {
            StartCoroutine(Roll(horizontalInput));
        }

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        
        // Move the player if not rolling or in knockback
        if (!isRolling)
        {
            transform.Translate(movement);
        }

        // Set rolling animation state
        animator.SetBool("isRolling", isRolling);
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

    private IEnumerator Roll(float horizontalInput)
    {
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        canRoll = false;
        isRolling = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        
        // Maintain the direction and apply the roll speed multiplier
        float rollDirection = isFacingRight ? 1f : -1f;
        playerRb.velocity = new Vector2(rollDirection * speed * rollSpeedMultiplier, playerRb.velocity.y);

        // Allow for movement adjustments during roll
        float elapsedTime = 0f;
        while (elapsedTime < rollTime)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            playerRb.velocity = new Vector2(horizontalInput * speed * rollSpeedMultiplier, playerRb.velocity.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

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
