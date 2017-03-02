using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour {

    [SerializeField]
    bool onPlatform;

    [SerializeField]
    bool betweenPoints;

    [SerializeField]
    GameObject platform;

    [SerializeField]
    Transform leftPoint;

    [SerializeField]
    Transform rightPoint;

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
        if (betweenPoints &&  leftPoint != null && rightPoint != null)
        {
            minX = leftPoint.position.x;
            maxX = rightPoint.position.x;
        }
        if (onPlatform && platform)
        {
            minX = platform.GetComponent<Collider2D>().bounds.min.x;
            maxX = platform.GetComponent<Collider2D>().bounds.max.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        patrollerMinX = thisCollider.bounds.min.x;
        patrollerMaxX = thisCollider.bounds.max.x; //+ thisRigidBody.velocity.x * Time.deltaTime;
        if (patrollerMinX <= minX || patrollerMaxX >= maxX)
        {
            thisRigidBody.velocity = new Vector2(-thisRigidBody.velocity.x, thisRigidBody.velocity.y);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }
}
