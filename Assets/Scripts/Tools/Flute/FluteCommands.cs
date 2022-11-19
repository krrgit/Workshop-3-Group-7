using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteCommands : MonoBehaviour
{
    public void DoCommand(string command)
    {
        if (command == "controlCat")
        {
            SwitchPlayerController.Instance.Switch(false);
        }
    }
}
