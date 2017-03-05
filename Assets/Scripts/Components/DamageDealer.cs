using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField]
    float damage;

    public void applyDamageOnce(GameObject receiver)
    {
        receiver.GetComponent<Health>().changeHealth(-damage);
    }

    public void applyDamageOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Health>().changeHealth(-damage);
    }

    public void applyDamageOnce(GameObject receiver, float damage)
    {
        receiver.GetComponent<Health>().changeHealth(-damage);
    }

    public void applyDamageOnce(string receiver, float damage)
    {
        GameObject.Find(receiver).GetComponent<Health>().changeHealth(-damage);
    }
}
