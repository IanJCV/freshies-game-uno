using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ModuleBehaviour> modules = new List<ModuleBehaviour>();

    public Transform
        armPoint,
        backPoint,
        legPoint,
        headPoint;

    const int playerLayer = 12;

    // Update is called once per frame
    void Update()
    {
        ApplyInput();
    }

    public void AddModule(GameObject module)
    {
        ModuleObject mObj = module.GetComponent<ModuleObject>();

        modules.Add(mObj.behaviour);

        mObj.durability = Random.Range(1, mObj.behaviour._maxDurability + 1);
        module.layer = playerLayer;

        switch (mObj.behaviour.type)
        {
            case ModuleType.Arm:
                module.transform.parent = armPoint;
                break;
            case ModuleType.Back:
                module.transform.parent = backPoint;
                break;
            case ModuleType.Head:
                module.transform.parent = headPoint;
                break;
            case ModuleType.Leg:
                module.transform.parent = legPoint;
                break;

            default:
                Debug.Log("Improper setup of module! Check your shit");
                break;
        }
        module.transform.localPosition = Vector3.zero;
        module.transform.localRotation = Quaternion.identity;
    }

    private void InstantiateModule(Transform mountPoint)
    {

    }

    private void ApplyInput()
    {
        int i = GetInput();

        if (i == -1)
            return;

        modules[i].Activate();
    }

    static int GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    return 3;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    return 4;
        //}
         
        return -1;
    }

}
