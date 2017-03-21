using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    [SerializeField]
    private GameObject mineExplosion;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character") && !collider.isTrigger)
        {
            gameObject.GetComponent<DamageDealer>().ApplyDamageOnce(collider.gameObject);
            Instantiate(mineExplosion, gameObject.transform.position, Quaternion.Euler(0,0,0));
            DestroyObject(gameObject);
        }
    }
}