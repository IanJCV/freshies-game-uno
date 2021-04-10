using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float health = 10f;

    private bool isAlive = true;


    //move
    private bool _jump = false;
    private float _horzontalMovement;


    //this is me
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    Inventory inventory;


    void Start()
    {
        inventory = GetComponent<Inventory>();
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

        foreach (ModuleBehaviour module in inventory.modules)
        {
            module.Update();
        }
    }


    private void InputControll()
    {
        if (Input.GetButtonDown("Jump") && myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
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

                StartCoroutine(knockback(0.02f,100, transform.position));
            }
            else
            {
                Die();
            }
            
        }

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Module")))
        {
            if (inventory.modules.Count < 3)
            {
                inventory.AddModule(collision.gameObject);
            }
        }
    }

    public IEnumerator knockback(float knockdur, float KnockbackPwr, Vector3 KnockbackDir)
    {
        float timer = 0;

        while (knockdur > timer)
        {
            timer += Time.deltaTime;
            myRigidBody.AddForce(new Vector3 (KnockbackDir.x * -350, KnockbackDir.y * KnockbackPwr, transform.position.z));
        }


        yield return null;
    }
}
