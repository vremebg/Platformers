using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningConditions : MonoBehaviour {

    [SerializeField]
    GameObject character;

    [SerializeField]
    bool pointsObjective;

    [SerializeField]
    int pointsCount;

    [SerializeField]
    string pointsStr = "Congrats! You collected enough points!";

    [SerializeField]
    bool healthObjective;

    [SerializeField]
    int healthTreshold;

    [SerializeField]
    string healthStr = "Congrats! You are invincible!";

    [SerializeField]
    bool timeObjective;

    [SerializeField]
    int timeInSeconds;

    [SerializeField]
    string timeStr = "Victory! Time is up!";

    [SerializeField]
    GameObject victoryMenu;

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
            if (healthComponent.getHealth() >= healthTreshold)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = healthStr;
            }
        if (pointsObjective)
            if (pointsComponent.getPoints() >= pointsCount)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = pointsStr;
            }
        if (timeObjective)
            if (Time.timeSinceLevelLoad <= timeInSeconds)
            {
                victoryMenu.transform.parent.gameObject.SetActive(true);
                victoryMenu.SetActive(true);
                victoryMenu.GetComponentInChildren<Text>().text = timeStr;
            }
    }
}
