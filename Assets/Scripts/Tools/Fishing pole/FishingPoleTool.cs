using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleTool : Tool
{
    [SerializeField] FishingPoleAnimator anim;
    [SerializeField] float min = 1;
    [SerializeField] float max = 8;
    [SerializeField] GameObject miniGame;

    bool canExit;
    bool correctSpot;


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
        anim.UpdateSprite(Vector2.zero);
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

        UpdateExitStatus(false);
    }

    public void UpdateExitStatus(bool state)
    {
        canExit = state;
    }

    public override bool CanUnequip()
    {
        return canExit;
    }

    public void CatchFish()
    {
        anim.playCatchAnim();
        if(correctSpot)
        {
            ProgressTracker.Instance.UpdateFishCaught();
        }

    }
    
    public void SetCorrectSpot(bool state)
    {
        correctSpot = state;
    }
}
