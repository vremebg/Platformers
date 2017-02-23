using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(GameObject.Find("Character").GetComponent<CapsuleCollider2D>(), gameObject.GetComponents<BoxCollider2D>()[1], true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character")) //&& charState == characterState.inAir)
        {
            Vector3 max = gameObject.GetComponents<BoxCollider2D>()[0].bounds.max;
            if (//collider.gameObject.GetComponentsInChildren<Transform>()[0].position.y >= max.y)
                collider.gameObject.transform.FindChild("contactPoint").transform.position.y >= max.y)
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
