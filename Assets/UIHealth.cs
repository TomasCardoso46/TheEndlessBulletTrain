using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    public Sprite healthBar0;  // 0 health or dead
    public Sprite healthBar1;  // 1/3 health
    public Sprite healthBar2;  // 2/3 health
    public Sprite healthBar3;  // Full health
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthSprite(GameManager.instance.health);
    }

    // Update is called once per frame
    
   void Update()
    {
        UpdateHealthSprite(GameManager.instance.health);
    }

    public void UpdateHealthSprite( int health)
    {
        

        switch (health)
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
                if (health <= 0)
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
