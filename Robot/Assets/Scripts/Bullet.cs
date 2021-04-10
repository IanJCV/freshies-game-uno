using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 5;
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerController>().knockback(0.02f, 100, transform.position));
            CinemachineController.Instance.ShakeCamera(4f, 0.1f);
        }

        Destroy(gameObject);
    }
}
