using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField]
    int damage;

    public void applyDamageOnce(GameObject receiver)
    {
        receiver.GetComponent<Health>().changeHealth(-damage);
    }

    public void applyDamageOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Health>().changeHealth(-damage);
    }
}
