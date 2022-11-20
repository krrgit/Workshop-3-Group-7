using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
    public virtual bool CanUnequip()
    {
        return true;
    }
    public virtual bool Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        return true;
    }

    public virtual bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        return false;

    }
}
