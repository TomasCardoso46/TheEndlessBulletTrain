using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    public float speed = 5f; // Adjust the speed as needed
    private Animator animator;
    public Rigidbody2D playerRb;
    private bool isFacingRight = true; // Start facing right by default
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        // Check if the player is walking
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.SetFloat("isWalking", 1);
        }
        else
        {
            animator.SetFloat("isWalking", 0);
        }

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;

        // Move the player
        if (KBCounter <= 0)
        {
            transform.Translate(movement);
        }
        else
        {
            if (KnockFromRight == true)
            {
                playerRb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                playerRb.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }
    }

    void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }
}
