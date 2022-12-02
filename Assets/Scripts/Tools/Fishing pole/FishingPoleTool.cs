using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleTool : Tool
{
    [SerializeField] FishingPoleAnimator anim;
    [SerializeField] float min = 1;
    [SerializeField] float max = 8;
    [SerializeField] GameObject miniGame;
    [SerializeField] ListPieceCounter list;

    bool canExit;
    bool correctSpot;
    bool isFishingSpot;


    public override bool Use()
    {
        if(!isFishingSpot) return false;

        PlayerMovement.Instance.ToggleMove(false);
        print("Use FishingPole!");
        anim.UpdateSprite(PlayerMovement.Instance.FacingDir);
        StartCoroutine(StartFishing());
        SoundManager.Instance.PlaycastHook();
        SoundManager.Instance.PlaywaterSplash();
        return true;
    }
    
    public override bool Stop()
    {
        if (anim.AnimActive) return true;
        StopAllCoroutines();
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using FishingPole");
        anim.UpdateSprite(Vector2.zero);
        return false;
    }

    IEnumerator StartFishing()
    {
        float waitTime = Random.Range(min, max);

        yield return new WaitForSeconds(waitTime); 
        
        // "Catch!" animation
        
        // Start FishingMiniGame animation
        StartMiniGame();
        SoundManager.Instance.PlaypullHook();
    }

    void StartMiniGame()
    {
        UpdateExitStatus(false);
        miniGame.SetActive(true);
        miniGame.transform.position = transform.position + Vector3.right * 5;
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
        anim.playCatchAnim(correctSpot);
        if(correctSpot)
        {
            if(FishingSpotManager.Instance)
            {
                FishingSpotManager.Instance.SpotFished(true);
            }
            ProgressTracker.Instance.UpdateFishCaught();
            list.UpdateListCount();
        }

    }
    
    public void SetCorrectSpot(bool state)
    {
        correctSpot = state;
    }

    public void SetInFishingSpot(bool state)
    {
        isFishingSpot = state;
    }
}
