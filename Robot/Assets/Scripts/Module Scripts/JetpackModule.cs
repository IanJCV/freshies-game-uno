using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Modules/Jetpack")]
public class JetpackModule : ModuleBehaviour
{
    public int strength;
    public PlayerController player;

    public override void OnHold()
    {
        Float();
    }

    public override void OnInitialize()
    {
        player = GameController.Instance.player;

        player.OnJump.AddListener(Float);
    }

    void Float()
    {
        player.myRigidBody.velocity += new Vector2(0, strength);
        Heat++;
    }

    public override void OnOverheat()
    {
        player.OnJump.RemoveListener(Float);
        base.OnOverheat();
    }
}
