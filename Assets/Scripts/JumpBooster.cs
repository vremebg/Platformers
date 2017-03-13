using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBooster : MonoBehaviour {

    [SerializeField]
    bool desroyOnContact = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character") && !collider.isTrigger)
        {
            gameObject.GetComponent<BoostGiver>().giveJumpBoost(collider.gameObject);
            if (desroyOnContact)
                Destroy(gameObject);
        }
    }
}
