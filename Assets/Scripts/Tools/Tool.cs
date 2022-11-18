using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public virtual bool CanUnequip()
    {
        return true;
    }
    public virtual void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
    }

    public virtual void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);

    }
}
