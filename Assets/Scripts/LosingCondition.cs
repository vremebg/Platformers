using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosingCondition : MonoBehaviour {

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private bool pointsObjective;

    [SerializeField]
    private int pointsCount;

    [SerializeField]
    private string pointsStr = "Not enough points!";

    [SerializeField]
    private bool healthObjective;

    [SerializeField]
    private int healthTreshold;

    [SerializeField]
    private string healthStr = "You died!";

    [SerializeField]
    private bool timeObjective;

    [SerializeField]
    private int timeInSeconds;

    [SerializeField]
    private string timeStr = "Time is up!";

    [SerializeField]
    private GameObject lossMenu;

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
            if (healthComponent.getHealth() <= healthTreshold)
            {
                lossMenu.transform.parent.gameObject.SetActive(true);
                lossMenu.SetActive(true);
                lossMenu.GetComponentInChildren<Text>().text = healthStr;
                Time.timeScale = 0;
            }
        if (pointsObjective)
            if (pointsComponent.getPoints() <= pointsCount)
            {
                lossMenu.transform.parent.gameObject.SetActive(true);
                lossMenu.SetActive(true);
                lossMenu.GetComponentInChildren<Text>().text = pointsStr;
                Time.timeScale = 0;
            }
        if (timeObjective)
            if (Time.timeSinceLevelLoad >= timeInSeconds)
            {
                lossMenu.transform.parent.gameObject.SetActive(true);
                lossMenu.SetActive(true);
                lossMenu.GetComponentInChildren<Text>().text = timeStr;
                Time.timeScale = 0;
            }
    }
}
