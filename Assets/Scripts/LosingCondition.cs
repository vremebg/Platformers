using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingCondition : MonoBehaviour {

    [SerializeField]
    GameObject character;

    [SerializeField]
    bool pointsObjective;

    [SerializeField]
    int pointsCount;

    [SerializeField]
    bool healthObjective;

    [SerializeField]
    int healthTreshold;

    [SerializeField]
    bool timeObjective;

    [SerializeField]
    int timeInSeconds;

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
                lossMenu.SetActive(true);
            }
        if (pointsObjective)
            if (pointsComponent.getPoints() <= pointsCount)
            {
                lossMenu.SetActive(true);
            }
        if (timeObjective)
            if (Time.timeSinceLevelLoad >= timeInSeconds)
            {
                lossMenu.SetActive(true);
            }
    }
}
