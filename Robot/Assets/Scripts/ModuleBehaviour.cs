using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehaviour : ScriptableObject
{
    public Sprite sprite;
    public int _durabilityMin;
    public int _durabilityMax;

    public abstract void Activate();
    public abstract void Deactivate();
}
