using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private GameObject hudComponent;

    [SerializeField]
    private bool isOnHud = false;

    void Start()
    {
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = health.ToString();
    }

    public int getHealth()
    {
        return health;
    }

    public void changeHealth(int change)
    {
        health += change;
        if (health > maxHealth)
            health = maxHealth;
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = health.ToString();
    }

    public bool healthDepleted()
    {
        if (health<0) return true;
        else return false;
    }

    public bool isHealthMaxed()
    {
        if (health == maxHealth) return true;
        else return false;
    }
}
