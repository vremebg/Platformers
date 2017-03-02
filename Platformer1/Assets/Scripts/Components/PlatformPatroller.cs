using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatroller : MonoBehaviour {

    [SerializeField]
    GameObject platform;

    Rigidbody2D thisRigidBody;
    Collider2D thisCollider;

    float maxX;
    float minX;

    float patrollerMinX;
    float patrollerMaxX;

	// Use this for initialization
	void Start () {
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
        thisCollider = gameObject.GetComponent<Collider2D>();
        minX = platform.GetComponent<Collider2D>().bounds.min.x;
        maxX = platform.GetComponent<Collider2D>().bounds.max.x;
    }

    // Update is called once per frame
    void FixedUpdate () {
        patrollerMinX = thisCollider.bounds.min.x;
        patrollerMaxX = thisCollider.bounds.max.x; //+ thisRigidBody.velocity.x * Time.deltaTime;
        if (patrollerMinX <= minX || patrollerMaxX >= maxX)
        {
            thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x * -1, thisRigidBody.velocity.y);
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
    }
}
