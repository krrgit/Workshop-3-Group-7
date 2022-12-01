using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleAnimator : MonoBehaviour
{
    [SerializeField] Sprite horSprite;
    [SerializeField] Sprite verSprite;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite heldSprite;
    [SerializeField] Transform bobberPos;
    [SerializeField] Transform fish;

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
        else
        {
            rend.sprite = heldSprite;
            rend.flipX = false;
            rend.flipY = false;
        }
    }

    public void playCatchAnim()
    {
        UpdateSprite(Vector2.zero);

    }

    IEnumerator animateCatch()
    {
        float timer = 1f;
        Vector3 displacement = bobberPos.position - PlayerMovement.Instance.transform.position;


        fish.gameObject.SetActive(true);
        fish.position = bobberPos.position;
        while(timer > 0)
        {
            fish.position = PlayerMovement.Instance.transform.position + displacement * timer;
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
}
