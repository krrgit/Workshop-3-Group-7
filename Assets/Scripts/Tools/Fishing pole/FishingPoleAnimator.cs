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
    [SerializeField] private Transform listPiece;
    [SerializeField] private ParticleSystem fishParticles;
    [SerializeField] float speed = 2;
    [SerializeField] private FishingPoleTool pole;


    public bool AnimActive
    {
        get {    return fish.gameObject.activeSelf;}
    }

    // Update is called once per frame
    void Start()
    {
        if (fish) fish.gameObject.SetActive(false);
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

    public void playCatchAnim(bool hasPiece)
    {
        UpdateSprite(Vector2.zero);
        StartCoroutine(animateCatch(hasPiece));

    }

    IEnumerator animateCatch(bool hasPiece)
    {
        fish.gameObject.SetActive(true);
        fish.position = bobberPos.position;

        float distance = Vector3.Distance(fish.position, CatAnimController.Instance.transform.position); 
        var catHead = CatAnimController.Instance.transform.GetChild(0);
        // Move Fish to Cat Animation
        while(distance > 0.1f)
        {
            fish.position = Vector3.Lerp(fish.position, catHead.position - catHead.up * 1.2f, speed * Time.deltaTime);
            distance = Vector3.Distance(fish.position, catHead.position  - catHead.up * 1.2f); 
            yield return new WaitForEndOfFrame();
        }
        
        // Cat Eat Anim
        fish.position = catHead.position - catHead.up * 1.2f;
        fishParticles.Play();
        yield return new WaitForSeconds(1f);
        fish.gameObject.SetActive(false);

        if (hasPiece)
        {
            // List Piece Coming Out of Fish
            listPiece.gameObject.SetActive(true);
            listPiece.position = catHead.position - catHead.up * 1.2f;
            distance = Vector3.Distance(listPiece.position, PlayerMovement.Instance.transform.position);
            yield return new WaitForSeconds(0.1f);

            while(distance > 0.1f)
            {
                listPiece.position = Vector3.Lerp(listPiece.position, PlayerMovement.Instance.transform.position, speed * Time.deltaTime);
                distance = Vector3.Distance(listPiece.position, PlayerMovement.Instance.transform.position); 
                yield return new WaitForEndOfFrame();
            }
        
            listPiece.position = PlayerMovement.Instance.transform.position + Vector3.up * 1.2f;
            yield return new WaitForSeconds(1f);
            listPiece.gameObject.SetActive(false);
        }
        
        // List Piece Spit Out
        pole.Stop();
        ToolManager.Instance.SetInUse(false);

    }
}
