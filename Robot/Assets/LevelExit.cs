using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float _levelLoadDelay = 2f;

    [SerializeField]
    private LevelManager _levelmanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(_levelLoadDelay);

        Time.timeScale = 1f;

        _levelmanager.GetComponent<LevelManager>().LoadNextLevel();
    }
}
