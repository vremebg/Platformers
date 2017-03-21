using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField]
    private float damage;

    public void ApplyDamageOnce(GameObject receiver)
    {
        receiver.GetComponent<Health>().ChangeHealth(-damage);
    }

    public void ApplyDamageOnce(string receiver)
    {
        GameObject.Find(receiver).GetComponent<Health>().ChangeHealth(-damage);
    }

    public void ApplyDamageOnce(GameObject receiver, float damage)
    {
        receiver.GetComponent<Health>().ChangeHealth(-damage);
    }

    public void ApplyDamageOnce(string receiver, float damage)
    {
        GameObject.Find(receiver).GetComponent<Health>().ChangeHealth(-damage);
    }
}
