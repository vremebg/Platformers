using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

    [SerializeField]
    GameObject barrelExplosion;

    Health barrelHealth;

    void Start()
    {
        barrelHealth = gameObject.GetComponent<Health>();
    }

    void FixedUpdate()
    {
        if (barrelHealth.healthDepleted())
        {
            Instantiate(barrelExplosion, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            DestroyObject(gameObject);
        }
    }
}
