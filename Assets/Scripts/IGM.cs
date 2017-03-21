using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGM : MonoBehaviour {

    [SerializeField]
    private GameObject IGMenu;

    private void IGMPopUp()
    {
        IGMenu.transform.parent.gameObject.SetActive(true);
        IGMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void IGMResume()
    {
        Time.timeScale = 1;
        IGMenu.transform.parent.gameObject.SetActive(false);
        IGMenu.SetActive(false);
    }

    private void IGMRestart()
    {
        foreach (GameObject temp in SceneManager.GetActiveScene().GetRootGameObjects())
            temp.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IGMQuit()
    {
        Application.Quit();
    }
}
