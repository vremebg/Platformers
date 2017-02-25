using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGiver : MonoBehaviour {

    [SerializeField]
    int health;

    public void givehealthOnce(GameObject receiver)
    {
        receiver.GetComponent<Health>().changeHealth(health);
    }

    public void givehealthOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Health>().changeHealth(health);
    }

    public bool isHealthMaxed(GameObject receiver)
    {
        return receiver.GetComponent<Health>().isHealthMaxed();
    }

    public bool isHealthMaxed(string receiver)
    {
        return GameObject.Find(receiver).GetComponent<Health>().isHealthMaxed();
    }
}