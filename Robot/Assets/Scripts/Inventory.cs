using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ModuleBehaviour> modules = new List<ModuleBehaviour>();

    private int slots;

    // Update is called once per frame
    void Update()
    {
        ApplyInput();
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
