using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private int endSceneIndex;

    [SerializeField]
    private int startSceneIndex;

    public int CurrentSceneIndex
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(CurrentSceneIndex + 1);
        SceneManager.UnloadSceneAsync(CurrentSceneIndex);
    }

    public void LoadEndScreen()
    {

    }

}
