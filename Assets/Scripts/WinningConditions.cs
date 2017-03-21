using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningConditions : MonoBehaviour {

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private bool pointsObjective;

    [SerializeField]
    private int pointsCount;

    [SerializeField]
    private string pointsStr = "Congrats! You collected enough points!";

    [SerializeField]
    private bool healthObjective;

    [SerializeField]
    private int healthTreshold;

    [SerializeField]
    private string healthStr = "Congrats! You are invincible!";

    [SerializeField]
    private bool timeObjective;

    [SerializeField]
    private int timeInSeconds;

    [SerializeField]
    private string timeStr = "Victory! Time is up!";

    [SerializeField]
    private GameObject victoryMenu;

    private Health healthComponent;
    private Points pointsComponent;

    // Use this for initialization
    void Start()
    {
        healthComponent = character.GetComponent<Health>();
        pointsComponent = character.GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthObjective)
            if (healthComponent.getHealth() >= healthTreshold)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = healthStr;
                Time.timeScale = 0;
            }
        if (pointsObjective)
            if (pointsComponent.getPoints() >= pointsCount)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = pointsStr;
                Time.timeScale = 0;
            }
        if (timeObjective)
            if (Time.timeSinceLevelLoad <= timeInSeconds)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = timeStr;
                Time.timeScale = 0;
            }
    }
}
