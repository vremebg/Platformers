using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

    public void RestartScene()
    {
        foreach (GameObject temp in SceneManager.GetActiveScene().GetRootGameObjects())
            temp.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
