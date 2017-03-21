using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGiver : MonoBehaviour {

    [SerializeField]
    private int health;

    public void GivehealthOnce(GameObject receiver)
    {
        receiver.GetComponent<Health>().ChangeHealth(health);
    }

    public void GivehealthOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Health>().ChangeHealth(health);
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