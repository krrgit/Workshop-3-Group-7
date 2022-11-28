using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleTool : Tool
{
    [SerializeField] FishingPoleAnimator anim;
    [SerializeField] float min = 1;
    [SerializeField] float max = 8;
    [SerializeField] GameObject miniGame;

    public override bool Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        print("Use FishingPole!");
        anim.UpdateSprite(PlayerMovement.Instance.FacingDir);
        StartCoroutine(StartFishing());
        return true;
    }
    
    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using FishingPole");
        return false;
    }

    IEnumerator StartFishing()
    {
        float waitTime = Random.Range(min, max);

        yield return new WaitForSeconds(waitTime); 
        
        // Start FishingMiniGame animation
        StartMiniGame();
    }

    void StartMiniGame()
    {
        miniGame.SetActive(true);
        miniGame.transform.position = transform.position + Vector3.right * 5;
    }
}
