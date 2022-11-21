using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerController : MonoBehaviour {
    [SerializeField] private PlayerMovement player;
    [SerializeField] private CatAnimController cat;

    public static SwitchPlayerController Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Switch(bool switchToPlayer)
    {
        player.ToggleControl(switchToPlayer);
        cat.ToggleControl(switchToPlayer);
        CamAimAtTarget.Instance.SetTarget(switchToPlayer ? player.transform : cat.transform);
        UpdateButtonLabels.Instance.UpdateLabels(switchToPlayer ? "Default" : "Cat");
    }
}
