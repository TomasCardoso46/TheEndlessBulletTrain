using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    public Sprite healthBar0;  // Full health
    public Sprite healthBar1;  // 2/3 health
    public Sprite healthBar2;  // 1/3 health
    public Sprite healthBar3;  // 0 health or dead
    private SpriteRenderer spriteRenderer;
    public PlayerBody playerBodyScript;
    public GameManager gameManagerBody;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthSprite()
    {
        switch (gameManagerBody.health)
        {
            case 3:
                spriteRenderer.sprite = healthBar3;
                break;

            case 2:
                spriteRenderer.sprite = healthBar2;
                break;

            case 1:
                spriteRenderer.sprite = healthBar1;
                break;

            case 0:
            default:
                spriteRenderer.sprite = healthBar0;
                if (gameManagerBody.health <= 0)
                {
                    DestroyPlayer();
                }
                break;
        }
    }

    private void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
    }
}
