
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTarget;

    [SerializeField]
    public float smoothspeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = PlayerTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);


        transform.position = smoothedPosition;
    }  


}
