using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGiver : MonoBehaviour {

    [SerializeField]
    int points;

    public void givePointsOnce(GameObject receiver)
    {
        receiver.GetComponent<Points>().changePoints(points);
    }

    public void givePointsOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Points>().changePoints(points);
    }

    public bool isPointsPoweredUp(GameObject receiver)
    {
        return receiver.GetComponent<Points>().isPoweredUp();
    }

    public bool isPointsPoweredUp(string receiver)
    {
        return GameObject.Find(receiver).GetComponent<Points>().isPoweredUp();
    }
}