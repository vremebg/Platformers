using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    [SerializeField]
    GameObject mineExplosion;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
        {
            gameObject.GetComponent<DamageDealer>().applyDamageOnce(collider.gameObject);
            Instantiate(mineExplosion, gameObject.transform.position, Quaternion.Euler(0,0,0));
            DestroyObject(gameObject);
        }
    }
}