using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module
{
    [SerializeField] private GameObject _modulePrefab;
    [SerializeField] private int _durabilityMin;
    [SerializeField] private int _durabilityMax;

    private int _durability;

    public int Durability 
    { 
        get => _durability; 
        set => _durability = value; 
    }

    public ModuleBehaviour behaviour;

    public void Spawn(Vector2 pos)
    {
        GameObject prefabOobject = GameObject.Instantiate(_modulePrefab, pos, _modulePrefab.transform.rotation);

        prefabOobject.GetComponent<SpriteRenderer>().sprite = behaviour.sprite;
    }

    public void Use()
    {
        behaviour.Activate();
    }

    
}
