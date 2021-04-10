using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ItemSpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject modulePrefab;

    [SerializeField]
    private List<SpawnBox> spawnBoxes = new List<SpawnBox>();

    [SerializeField]
    private List<ModuleBehaviour> behaviours = new List<ModuleBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        ModuleSpawn module = new ModuleSpawn();

        foreach (SpawnBox box in spawnBoxes)
        {
            if (box.modulesToSpawn <= 0)
                return;

            for (int i = 0; i < box.modulesToSpawn; i++)
            {
                ModuleBehaviour behaviourToSpawn = behaviours[Random.Range(0, behaviours.Count)];

                module.Spawn(box.collider.bounds, behaviourToSpawn, modulePrefab);
            }
        }
    }
}

[System.Serializable]
internal class SpawnBox
{
    [SerializeField]
    internal Collider2D collider;

    [Range(0, 32),
        Tooltip("The amount of modules that can be spawned in each of the colliders."),
        SerializeField]
    internal int modulesToSpawn;
}