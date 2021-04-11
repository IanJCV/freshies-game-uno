using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBase : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;

    [SerializeField]
    private int _enemyHealth;
    [SerializeField]
    private int _maxEnemyHealth = 10;

    [SerializeField]
    public EnemyHealthBar healthbar;  

    //shooting
    [Header("shooting, only ranged and flying")]
    [Range(0.0f, 20.0f)]
    [SerializeField] private float _attackdistance;
    [Range(0.0f, 3f)]
    [SerializeField] private float _timeBetweenAttacks = 1;
    [SerializeField] private float _attacktimer = 0;

    //flying
    [Header("Flying Variables")]
    [Range(0.0f, 10.0f)]
    public float yAmplitude = 1;
    [Range(0.0f, 10.0f)]
    public float YFrequency = 1;

    [Range(0.0f, 10.0f)]
    public float xAmplitude = 1;
    [Range(0.0f, 10.0f)]
    public float xFrequency = 1;

    //Jumping
    [SerializeField]
    private float Jumptimer = 0;
    [SerializeField]
    private float timebetweenjumps = 10;

    [Header("make character stand still (ranged elee)")]
    [SerializeField] private bool _CantMove;


    [Header("other shit")]
    [SerializeField] public GameObject deathVFX;
    [SerializeField] private float explosionDuration = 1;

    public GameObject _player = null;
    public Transform _gunarm = null;
    public GameObject _enemyBullet = null;
    public AudioSource _audioSource = null;
    public SpriteRenderer _mySpriteRenderer;

    public Color StartColor;
    public enum EnemyType { flyingEnemy, RangedEnemy, MeleeEnemy}
    [SerializeField]
    EnemyType enemyType;

    private void Awake()
    {
        //Registers the enemy in the Game Controller
        GameController.Instance.RegisterEnemy(this);

        myRigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        StartColor = _mySpriteRenderer.color;
        _enemyHealth = _maxEnemyHealth;
        healthbar.SetMaxHealth(_maxEnemyHealth);
    }   

    private void FixedUpdate()
    {
        if (enemyType != EnemyType.flyingEnemy)
        {
            if (!_CantMove)
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
        }

        //Jump();
    }   

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);       
    }

    private void Update()
    {
        _attacktimer += Time.deltaTime;
        Jumptimer += Time.deltaTime;

        EnemyShooting();
        FlyingMovement();      
    }

    //private void Jump()
    //{
    //    if (Jumptimer>timebetweenjumps && enemyType != EnemyType.flyingEnemy) 
    //    {
    //        timebetweenjumps = 0;            
    //        StartCoroutine(Jump(50));
    //    }
    //}

    private void FlyingMovement()
    {
        if (enemyType == EnemyType.flyingEnemy)
        {
            float yBeat = yAmplitude * (Mathf.Sin(2 * Mathf.PI * YFrequency * Time.time));
            float xBeat = xAmplitude * (Mathf.Sin(2 * Mathf.PI * xFrequency * Time.time));

            ;
            transform.Translate(xBeat * Time.deltaTime, yBeat * Time.deltaTime, 0);
        }
    }

    private void EnemyShooting()
    {
        if (enemyType == EnemyType.RangedEnemy || enemyType == EnemyType.flyingEnemy)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            _gunarm.LookAt(_player.transform.position);
            if (_attacktimer > _timeBetweenAttacks && distance < 10)
            {
                Instantiate(_enemyBullet, _gunarm.transform.position, _gunarm.transform.rotation * Quaternion.Euler(0, -90, 0));
                _audioSource.Play();
                _attacktimer = 0;
            }
        }
    }

    //public void TakeDamage()
    //{
    //    if (_enemyHealth > 0)
    //    {
    //        --_enemyHealth;
    //    }
    //    else
    //    {                       
    //        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
    //        Destroy(explosion, 10);
            
    //    }

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            default:
                break;
            case "Projectile/Bullet":
                _enemyHealth--;
                healthbar.SetHealth(_enemyHealth);
                StartCoroutine(TurnRed());
                break;
            case "Projectile/Rocket":
                GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
                Destroy(explosion, 1);
                Destroy(gameObject);
                break;
        }

        if (_enemyHealth <= 0)
        {
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, 1);
            Destroy(gameObject);
        }

    }

    //public IEnumerator Jump(float jumpheight)
    //{
    //    _CantMove = true;
    //    myRigidBody.velocity = Vector2.zero;
    //    myRigidBody.AddForce(new Vector2(0, jumpheight * Time.deltaTime), ForceMode2D.Impulse);      

    //    yield return new WaitForSeconds(5);       

    //    _CantMove = false;
    //    yield return null;


    //}
    public IEnumerator TurnRed()
    {
        _mySpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        Debug.Log("afterroutine");
        _mySpriteRenderer.color = StartColor;
    }

}
