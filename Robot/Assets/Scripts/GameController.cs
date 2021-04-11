using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCamera;

    public List<EnemyBase> enemies = new List<EnemyBase>();

    public PlayerController player;
    public Inventory playerInventory;

    private static GameController _instance;

    public static GameController Instance()
    {
        if (_instance == null)
        {
            _instance = new GameController();
        }

        return _instance;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void RegisterEnemy(EnemyBase enemy)
    {
        enemies.Add(enemy);
    }


    //update position
    //update levels
    private void Update()
    {
        //foreach (EnemyBase enemy in enemies)
        //{
        //    //implement logic here!
        //}


    }
}
