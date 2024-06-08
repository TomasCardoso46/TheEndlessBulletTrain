using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strikes : MonoBehaviour
{
    public Sprite threeHealth;
    public Sprite twoHealth;
    public Sprite oneHealth;
    public Sprite zeroHealth;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateStrikeSprite(GameManager.instance.strikes);
    }
    void Update()
    {
        UpdateStrikeSprite(GameManager.instance.strikes);
    }

    // Update the health sprite based on the number of strikes
    public void UpdateStrikeSprite(int strikes)
    {
        switch (strikes)
        {
            case 3:
                spriteRenderer.sprite = threeHealth;
                break;
            case 2:
                spriteRenderer.sprite = twoHealth;
                break;
            case 1:
                spriteRenderer.sprite = oneHealth;
                break;
            default:
                spriteRenderer.sprite = zeroHealth;
                break;
        }
    }
}
