﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject hudComponent;

    [SerializeField]
    private bool isOnHud = false;

    void Start()
    {
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = health.ToString();
    }

    public float getHealth()
    {
        return health;
    }

    public void changeHealth(float change)
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
