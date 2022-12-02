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
    [SerializeField] float speed = 2;

    // Update is called once per frame
    void Start()
    {
        fish.gameObject.SetActive(false);
    }
    
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
        StartCoroutine(animateCatch());

    }

    IEnumerator animateCatch()
    {
        fish.gameObject.SetActive(true);
        fish.position = bobberPos.position;

        float distance = Vector3.Distance(fish.position, PlayerMovement.Instance.transform.position); 
        
        while(distance > 0.1f)
        {
            fish.position = Vector3.Lerp(fish.position, PlayerMovement.Instance.transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(fish.position, PlayerMovement.Instance.transform.position); 
            yield return new WaitForEndOfFrame();
        }

        fish.position = PlayerMovement.Instance.transform.position + Vector3.up * 1.5f;
        yield return new WaitForSeconds(1f);
        fish.gameObject.SetActive(false);

    }
}
