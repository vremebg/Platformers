using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGM : MonoBehaviour {

    [SerializeField]
    GameObject IGMenu;

    public void IGMPopUp()
    {
        IGMenu.transform.parent.gameObject.SetActive(true);
        IGMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void IGMResume()
    {
        Time.timeScale = 1;
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void IGMRestart()
    {
        foreach (GameObject temp in SceneManager.GetActiveScene().GetRootGameObjects())
            temp.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IGMQuit()
    {
        Application.Quit();
    }
}
