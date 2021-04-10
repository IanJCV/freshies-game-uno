using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehaviour : ScriptableObject
{
    public Sprite sprite;
    public int _maxDurability;

    public ModuleType type;

    public abstract void Activate();
    public abstract void Deactivate();
}

public enum ModuleType
{
    Leg,
    Arm,
    Head,
    Back
}