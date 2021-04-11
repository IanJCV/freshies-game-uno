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

    public virtual void OnInitialize()
    {

    }
    public virtual void OnPress()
    {

    }
    public virtual void OnRelease()
    {

    }
    public virtual void OnHold()
    {

    }
    public virtual void Update()
    {

    }

    public virtual void OnOverheat()
    {
        RemoveItem();
    }
    public virtual void OnBreak()
    {
        RemoveItem();
    }

    public void RemoveItem()
    {
        GameController.Instance.playerInventory.RemoveModule(parentObject);
    }
}

public enum ModuleType
{
    Leg,
    Arm,
    Head,
    Back
}