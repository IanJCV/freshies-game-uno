using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modules/Test Module")]
public class TestModule : ModuleBehaviour
{
    public override void Activate()
    {
        Debug.Log("Activated!");
    }

    public override void Deactivate()
    {
        Debug.Log("Deactivated!");
    }
}
