﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehaviour : ScriptableObject
{
    public Sprite sprite;
    public int _maxDurability;
    public GameObject parentObject;

    public ModuleType type;

    public AudioClip sound;
    public AudioSource source;

    public abstract void OnPress();
    public abstract void OnRelease();
    public abstract void OnHold();
    public abstract void Update();
}

public enum ModuleType
{
    Leg,
    Arm,
    Head,
    Back
}