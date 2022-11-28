using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleAnimator : MonoBehaviour
{
    [SerializeField] Sprite horSprite;
    [SerializeField] Sprite verSprite;
    [SerializeField] SpriteRenderer rend;

    // Update is called once per frame
    public void UpdateSprite(Vector2 direction)
    {
        if (direction.y < 0)
        {
            rend.sprite = verSprite;
            rend.flipY = false;
            rend.flipX = false;
        }
        else if (direction.y > 0)
        {
            rend.sprite = verSprite;
            rend.flipY = false;
            rend.flipX = false;

        }
        else if (direction.x > 0)
        {
            rend.sprite = horSprite;
            rend.flipX = true;
            rend.flipY = false;

        }
        else if (direction.x < 0)
        {
            rend.sprite = horSprite;
            rend.flipX = false;
            rend.flipY = false;
        }
    }
}
