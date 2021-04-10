using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modules/Test Module")]
public class TestModule : ModuleBehaviour
{
    public override void OnHold()
    {
    }

    public override void OnPress()
    {
        Debug.Log("Activated!");
    }

    public override void OnRelease()
    {
        Debug.Log("Deactivated!");
    }

    public override void Update()
    {
    }
}
