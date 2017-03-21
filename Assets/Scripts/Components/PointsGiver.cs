using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGiver : MonoBehaviour {

    [SerializeField]
    private int points;

    public void GivePointsOnce(GameObject receiver)
    {
        receiver.GetComponent<Points>().ChangePoints(points);
    }

    public void GivePointsOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Points>().ChangePoints(points);
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