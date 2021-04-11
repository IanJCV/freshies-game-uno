using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Modules/Rocket Launcher")]
public class RocketModule : ModuleBehaviour
{
    public GameObject rocketPrefab;

    private Vector3 mousePosition;

    public override void Update()
    {

        Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        mousePosition = Camera.main.ScreenToWorldPoint(vector);
        mousePosition.z = 0f;

        Rotate();
    }

    private void Rotate()
    {
        parentObject.transform.LookAt(mousePosition, Vector3.up);
    }

    public override void OnHold()
    {

    }

    public override void OnPress()
    {
        Shoot();
    }

    private void Shoot()
    {
        CinemachineController.Instance.ShakeCamera(5f, 0.2f);
        Instantiate(rocketPrefab, parentObject.transform.position, parentObject.transform.rotation * Quaternion.Euler(0, -90, 0));
        source.Play();
    }

    public override void OnRelease()
    {

    }

    public override void OnBreak()
    {

    }

    public override void OnOverheat()
    {

    }

    public override void OnInitialize()
    {

    }
}
