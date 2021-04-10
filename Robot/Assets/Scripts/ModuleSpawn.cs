using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSpawn
{
    public void Spawn(Bounds bounds, ModuleBehaviour behaviour, GameObject modulePrefab)
    {
        GameObject prefabObject = GameObject.Instantiate(modulePrefab, RandomPointInBounds(bounds), modulePrefab.transform.rotation);

        prefabObject.GetComponent<ModuleObject>().behaviour = behaviour;
        prefabObject.GetComponent<SpriteRenderer>().sprite = behaviour.sprite;
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
