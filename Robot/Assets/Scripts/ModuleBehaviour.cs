using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehaviour : ScriptableObject
{
    public Sprite sprite;

    public abstract void Activate();
    public abstract void Deactivate();
    public void Destroy()
    {

    }
}
