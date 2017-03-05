using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    bool desroyOnContact = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character") && !collider.isTrigger)
        {
            gameObject.GetComponent<PointsGiver>().givePointsOnce(collider.gameObject);
            if (desroyOnContact)
                Destroy(gameObject);
        }
    }
}
