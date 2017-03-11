using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosingCondition : MonoBehaviour {

    [SerializeField]
    GameObject character;

    [SerializeField]
    bool pointsObjective;

    [SerializeField]
    int pointsCount;

    [SerializeField]
    string pointsStr = "Not enough points!";

    [SerializeField]
    bool healthObjective;

    [SerializeField]
    int healthTreshold;

    [SerializeField]
    string healthStr = "You died!";

    [SerializeField]
    bool timeObjective;

    [SerializeField]
    int timeInSeconds;

    [SerializeField]
    string timeStr = "Time is up!";

    [SerializeField]
    GameObject lossMenu;

    Health healthComponent;
    Points pointsComponent;

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
