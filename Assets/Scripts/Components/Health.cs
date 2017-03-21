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

    [SerializeField]
    private bool showDamageNumbersOnChange = false;

    [SerializeField]
    private bool showHealth = false;

    [SerializeField]
    private GameObject healthSpriteObj;

    [SerializeField]
    private bool hasInvincibilityAfterHit = false;

    [SerializeField]
    private float invincibilityInSeconds;

    private bool isInvincible = false;
    private float invicStartTime;
    private SpriteRenderer objSpriteRenderer;
    private float alpha;
    private float alphaStep = -5;

    void Start()
    {
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = ((int)health).ToString();
        objSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (isInvincible)
        {
            if (Time.time - invincibilityInSeconds > invicStartTime)
            {
                isInvincible = false;
                alpha = 255;
                objSpriteRenderer.color = new Color(255, 255, 255, alpha);
            }
            else
            {
                alpha += alphaStep;
                if (alpha < 0)
                {
                    alpha = 0;
                    alphaStep = 0.05f;
                }else
                    if (alpha > 1)
                {
                    alpha = 1;
                    alphaStep = -0.05f;
                }
                objSpriteRenderer.color = new Color(255, 255, 255, alpha);
            }
        }
    }

    public float getHealth()
    {
        return health;
    }

    public void changeHealth(float change)
    {
        if (!hasInvincibilityAfterHit)
        {
            health += change;
            if (health > maxHealth)
                health = maxHealth;
            if (health < 0)
                health = 0;
            if (isOnHud)
                hudComponent.GetComponent<Text>().text = ((int)health).ToString();

            if (showDamageNumbersOnChange)
                gameObject.GetComponent<DamageNumbers>().addNumberToDisplay((int)change);
            if (showHealth)
                healthSpriteObj.transform.localScale = new Vector2(health / maxHealth, healthSpriteObj.transform.localScale.y);
        }
        else
        {
            if (!isInvincible)
            {
                isInvincible = true;
                invicStartTime = Time.time;
                health += change;
                if (health > maxHealth)
                    health = maxHealth;
                if (health < 0)
                    health = 0;
                if (isOnHud)
                    hudComponent.GetComponent<Text>().text = ((int)health).ToString();

                if (showDamageNumbersOnChange)
                    gameObject.GetComponent<DamageNumbers>().addNumberToDisplay((int)change);
                if (showHealth)
                    healthSpriteObj.transform.localScale = new Vector2(health / maxHealth, healthSpriteObj.transform.localScale.y);
            }
        }
    }

    public bool healthDepleted()
    {
        if (health<=0) return true;
        else return false;
    }

    public bool isHealthMaxed()
    {
        if (health == maxHealth) return true;
        else return false;
    }
}
