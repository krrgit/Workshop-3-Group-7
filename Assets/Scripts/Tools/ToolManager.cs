using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolInUse {
    None,
    FishingPole,
    Flute,
    Blacklight,
    Wrench
}

public class ToolManager : MonoBehaviour {
    [SerializeField] private float distance = 1;
    [SerializeField] private ToolInUse toolIndex;
    [SerializeField] private Tool fishingPole;
    [SerializeField] private Tool flute;
    [SerializeField] private Tool blacklight;
    [SerializeField] private Tool wrench;

    [SerializeField] private Tool current;


    private bool toolInUse;

    public Tool Equipped
    {
        get { return current; }
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CycleInput();
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (PlayerMovement.Instance.GetDir().magnitude == 0 || !PlayerMovement.Instance.canMove) return;

        transform.localPosition = PlayerMovement.Instance.GetDir() * distance;
    }

    void CycleInput()
    {
        if (Input.GetButtonDown("CycleTool"))
        {
            if (!toolInUse)
            {
                CycleTools();
            }
            
        }

        if (Input.GetButtonDown("UseTool"))
        {
            if (toolIndex != ToolInUse.None)
            {
                if (!toolInUse)
                {
                    Equipped.Use();
                    toolInUse = !toolInUse;
                }
                else
                {
                    Equipped.Stop();
                    toolInUse = !toolInUse;
                }
            }
        }
    }

    void CycleTools()
    {
        if (toolIndex != ToolInUse.None) Equipped.gameObject.SetActive(false);
        toolIndex = (ToolInUse)((int)toolIndex + 1 > 4 ? 0 : (int)toolIndex + 1);
        UpdateCurrentTool();
        Equipped.gameObject.SetActive(true);
        print("Current Tool: " + toolIndex);
    }

    void UpdateCurrentTool()
    {
        switch (toolIndex)
        {
            case ToolInUse.FishingPole:
                current = fishingPole;
                break;
            case ToolInUse.Flute:
                current = flute;
                break;
            case ToolInUse.Blacklight:
                current = blacklight;
                break;
            case ToolInUse.Wrench:
                current =  wrench;
                break;
            default:
                current =  null;
                break;
        }
    }
}
