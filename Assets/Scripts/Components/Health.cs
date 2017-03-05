using System.Collections;
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

    void Start()
    {
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = "Health " + health.ToString();
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
        if (health < 0)
            health = 0;
        if (isOnHud)
            hudComponent.GetComponent<Text>().text = "Health " + health.ToString();

        if(showDamageNumbersOnChange)
            gameObject.GetComponent<DamageNumbers>().addNumberToDisplay((int)change);
        if (showHealth)
            healthSpriteObj.transform.localScale = new Vector2(health/maxHealth, healthSpriteObj.transform.localScale.y);
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
