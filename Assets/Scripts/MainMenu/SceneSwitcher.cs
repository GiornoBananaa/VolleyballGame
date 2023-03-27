using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void NextScene(int _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}