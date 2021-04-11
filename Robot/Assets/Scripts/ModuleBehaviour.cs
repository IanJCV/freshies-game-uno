using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehaviour : ScriptableObject
{
    public Sprite sprite;

    public GameObject parentObject;

    public bool useDurability;

    private int _durability;
    public int maxDurability;

    public int Durability
    {
        get
        {
            return _durability;
        }

        set
        {
            _durability = value;
            if (_durability < 0 && useDurability)
            {
                _durability = 0;
                OnBreak();
            }
        }
    }

    public bool useHeat;

    private int _heat;
    public int maxHeat;

    public int Heat
    {
        get
        {
            return _heat;
        }

        set
        {
            _heat = value;
            if (useHeat)
            {
                if (_heat < 0)
                {
                    _heat = 0;
                }

                if (_heat >= maxHeat)
                {
                    _heat = 0;
                    OnOverheat();
                }
            }
            else
            {
                _heat = 0;
            }
        }
    }


    public ModuleType type;

    public AudioClip sound;
    public AudioSource source;

    public abstract void OnInitialize();

    public abstract void OnPress();
    public abstract void OnRelease();
    public abstract void OnHold();
    public abstract void Update();

    public abstract void OnOverheat();
    public abstract void OnBreak();
}

public enum ModuleType
{
    Leg,
    Arm,
    Head,
    Back
}