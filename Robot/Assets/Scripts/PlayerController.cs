using System;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float health = 10f;

    

    private bool isAlive = true;
    
    private bool _jump = false;
    private float _horzontalMovement;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isAlive) { return; }
        {
            Run();            
            Jump();
        }
    }

    private void Update()
    {
        FlipSprite();
        InputControll();
    }

    private void InputControll()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }
        _horzontalMovement = Input.GetAxis("Horizontal");
    }
        

    private void Die()
    {
        Vector2 explosion = new Vector2(UnityEngine.Random.Range(0.0f, 10.0f), UnityEngine.Random.Range(0.0f, 10.0f));

        GetComponent<Rigidbody2D>().velocity = explosion;
        isAlive = false;
    }

    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        //if (!myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (_jump)
        {            
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed * Time.deltaTime);
            myRigidBody.velocity += jumpVelocityToAdd;
            _jump = false;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Run()
    {        
        Vector2 playerVelocity = new Vector2(_horzontalMovement * runSpeed * Time.deltaTime, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy"))) //other types of dmg, spikes etc
        {
            if (health > 0)
            {
                --health;

                CinemachineController.Instance.ShakeCamera(2f, 0.1f);

                ////dropcomponent() ?
                //myRigidBody.isKinematic = true;


                //if (collision.gameObject.transform.localScale.x > 0)
                //{
                //    Vector2 knockback = new Vector2(UnityEngine.Random.Range(3, 5), 1);
                //    myRigidBody.AddForce(knockback * 100, ForceMode2D.Impulse);
                //}
                //else
                //{
                //    Vector2 knockback = new Vector2(UnityEngine.Random.Range(-5, 3), 1);
                //    myRigidBody.AddForce(knockback *100, ForceMode2D.Impulse);

                //}

                //myRigidBody.isKinematic = false;
            }
            else
            {
                Die();
            }
            
        }
    }
}
