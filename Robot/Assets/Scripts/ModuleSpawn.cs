using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSpawn
{
    private GameObject _modulePrefab;

    public ModuleBehaviour behaviour;

    public ModuleSpawn(ModuleBehaviour behaviour, GameObject modulePrefab)
    {
        this.behaviour = behaviour;
        this._modulePrefab = modulePrefab;
    }

    public void Spawn(Bounds bounds)
    {
        GameObject prefabOobject = GameObject.Instantiate(_modulePrefab, RandomPointInBounds(bounds), _modulePrefab.transform.rotation);

        prefabOobject.GetComponent<ModuleObject>().behaviour = behaviour;
    }

    public void Use()
    {
        behaviour.OnPress();
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
