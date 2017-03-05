using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    [SerializeField]
    private int points;

    [SerializeField]
    private int powerUpPoints;

    [SerializeField]
    private GameObject hudComponent;

    [SerializeField]
    private bool isOnHud = false;

    void Start()
    {
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = points.ToString();
    }

    public float getPoints()
    {
        return points;
    }

    public void changePoints(int change)
    {
        points += change;
        if (points < 0)
            points = 0;
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = points.ToString();
    }

    public bool pointsDepleted()
    {
        if (points<=0) return true;
        else return false;
    }

    public bool isPoweredUp()
    {
        if (points >= powerUpPoints) return true;
        else return false;
    }
}
