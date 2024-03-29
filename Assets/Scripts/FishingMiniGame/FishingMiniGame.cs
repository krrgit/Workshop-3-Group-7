using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingMiniGame : MonoBehaviour
{
    [Header("Fishing Area")]
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;

    [Header("Fish Settings")]
    [SerializeField] Transform fish;
    [SerializeField] float smoothMotion = 3f; //smooth fish movement
    [SerializeField] float fishTimeRandomizer = 3f; //how often fish moves
    
    float fishPosition;
    float fishSpeed;
    float fishTimer;
    float fishTargetPosition;

    [Header("Hook Settings")]
    [SerializeField] Transform hook;
    [SerializeField] float hookSize = .18f;
    [SerializeField] float hookSpeed = .1f;
    [SerializeField] float hookGravity = .05f;
    float hookPosition;
    float hookPullVelocity;

    [Header("Progress Bar Settings")]
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float hookPower;
    [SerializeField] float progressBarDecay;
   
    float catchProgress;

    bool pause = false;
    
    [SerializeField] float failTimer = 10f;

    [SerializeField] UnityEvent winEvent = new UnityEvent();
    [SerializeField] UnityEvent loseEvent = new UnityEvent();

    [SerializeField] FishingPoleTool pole;

    private void FixedUpdate()
    {
        if(pause) return;
        
        MoveFish();
        MoveHook();
        CheckProgress();
    }

    private void CheckProgress()
    {
        Vector3 progressBarScale = progressBarContainer.localScale;
        progressBarScale.y = catchProgress;
        progressBarContainer.localScale = progressBarScale;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if(min < fishPosition && fishPosition < max)
        {
            catchProgress += hookPower * Time.deltaTime;
            if(catchProgress >= 1)
            {
                pause = true;
                winCondition();
                winEvent.Invoke();
            }
        }
        else
        {
            catchProgress -= progressBarDecay * Time.deltaTime;
            failTimer -= Time.deltaTime;
            if(failTimer < 0)
            {
                pause = true;
                loseCondition();
                loseEvent.Invoke();
            }
        }
        catchProgress = Mathf.Clamp(catchProgress, 0, 1);
    }

    private void MoveHook()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            //increase our pull velocity
            hookPullVelocity += hookSpeed * Time.deltaTime; //raise hook
        }
        hookPullVelocity -= hookGravity * Time.deltaTime;

        hookPosition += hookPullVelocity;

        if(hookPosition - hookSize / 2 <= 0 && hookPullVelocity < 0)
        {
            hookPullVelocity = 0;
        }

        if(hookPosition + hookSize / 2 >= 1 && hookPullVelocity > 0)
        {
            hookPullVelocity = 0;
        }

        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomBounds.position, topBounds.position, hookPosition);
    }
    
    private void MoveFish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0)
        {
            fishTimer = Random.value * fishTimeRandomizer;
            fishTargetPosition = Random.value;
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishTargetPosition, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomBounds.position, topBounds.position, fishPosition);
    }
    
    void winCondition()
    {
        stopMiniGame();
        pole.CatchFish();
    }
    
    void loseCondition()
    {
        stopMiniGame();
        SoundManager.Instance.PlaywrongPuzzle();
    }

    void stopMiniGame()
    {
        
        gameObject.SetActive(false);
        pole.UpdateExitStatus(true);
        SoundManager.Instance.StoppullHook();
    }

    void OnEnable()
    {
        reset();
    }

    void reset()
    {
        catchProgress = 0.5f;
        progressBarContainer.localScale = new Vector3(1, 0.5f, 1);
        pause = false;
        fishPosition = Random.value;
        hookPosition = 0; 
        fishTimer = Random.value * fishTimeRandomizer;
        fishTargetPosition = Random.value;
        failTimer = 5;
    }
}
