using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0f);
        }
    }   

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
