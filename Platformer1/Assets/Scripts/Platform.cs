using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(GameObject.Find("Character").GetComponent<BoxCollider2D>(), gameObject.GetComponents<BoxCollider2D>()[1], true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character")) //&& charState == characterState.inAir)
        {
            float checkY = collider.gameObject.GetComponent<BoxCollider2D>().bounds.min.y;
            Vector3 max = gameObject.GetComponents<BoxCollider2D>()[1].bounds.max;
            if (checkY >= max.y)
            {
                Physics2D.IgnoreCollision(collider, gameObject.GetComponents<BoxCollider2D>()[1], false);
                //charState = characterState.idle;
            }
            else
            {
                Physics2D.IgnoreCollision(collider, gameObject.GetComponents<BoxCollider2D>()[1], true);
            }
        }
    }
}
