using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Modules/Gun")]
public class GunModule : ModuleBehaviour
{
    //shooting
    Vector3 mousePosition;
    private bool _fire;
    public float Timebetweenshots = 0.2f;
    public float shotTimer = 0f;

    //bullettypes
    public GameObject bullet;

    public override void OnPress()
    {
        _fire = true;
    }

    public override void OnRelease()
    {
        _fire = false;
    }

    public override void OnHold()
    {

    }

    private void shoot()
    {
        if (shotTimer > Timebetweenshots && _fire)
        {            
            CinemachineController.Instance.ShakeCamera(3f, 0.1f);
            Instantiate(bullet, parentObject.transform.position, parentObject.transform.rotation * Quaternion.Euler(0, -90, 0));
            shotTimer = 0;
            source.Play();
        }
    }

    private void rotateArm()
    {
        parentObject.transform.LookAt(mousePosition, Vector3.up);
    }

    public override void Update()
    {
        shoot();
        rotateArm();

        shotTimer += Time.deltaTime;

        Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        mousePosition = Camera.main.ScreenToWorldPoint(vector);
        mousePosition.z = 0f;
    }
}
