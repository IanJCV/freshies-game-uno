using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float _levelLoadDelay = 1f;

    [SerializeField]
    private LevelManager _levelmanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(_levelLoadDelay);

        _levelmanager.GetComponent<LevelManager>().LoadNextLevel();
    }
}
