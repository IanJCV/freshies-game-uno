using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBase : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;

    [SerializeField]
    private int _enemyHealth;

    //shooting
    [SerializeField] private float _attackdistance;
    [SerializeField] private float _timeBetweenAttacks = 1;
    [SerializeField] private float _attacktimer = 0;

    //flying
    public float yAmplitude = 1;
    public float YFrequency = 1;

    public float xAmplitude = 1;
    public float xFrequency = 1;


    public GameObject _player = null;
    public Transform _gunarm = null;
    public GameObject _enemyBullet = null;
    public enum EnemyType { flyingEnemy, RangedEnemy, MeleeEnemy}
    [SerializeField]
    EnemyType enemyType;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");      
    }   

    private void FixedUpdate()
    {
        if (enemyType != EnemyType.flyingEnemy)
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

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    private void Update()
    {
        _attacktimer += Time.deltaTime;

        if (enemyType == EnemyType.RangedEnemy || enemyType == EnemyType.flyingEnemy)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            _gunarm.LookAt(_player.transform.position);
            if (_attacktimer > _timeBetweenAttacks && distance < 10)
            {
                Instantiate(_enemyBullet, _gunarm.transform.position, _gunarm.transform.rotation * Quaternion.Euler(0, -90, 0));
                _attacktimer = 0;
            }
        }

        if (enemyType == EnemyType.flyingEnemy)
        {
            float yBeat = yAmplitude * (Mathf.Sin(2 * Mathf.PI * YFrequency * Time.time));
            float xBeat = xAmplitude * (Mathf.Sin(2 * Mathf.PI * xFrequency * Time.time));

            ;
            transform.Translate(xBeat * Time.deltaTime, yBeat * Time.deltaTime, 0);
        }
       

    }

    public void TakeDamage()
    {
        if (_enemyHealth > 0)
        {
            --_enemyHealth;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);

            if (_enemyHealth > 0)
            {
                --_enemyHealth;
            }
            else
            {
                Destroy(gameObject);
            }
        }                     

    }
}
