using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour {

    [SerializeField]
    private bool onPlatform;

    [SerializeField]
    private bool betweenPoints;

    [SerializeField]
    private GameObject platform;

    [SerializeField]
    private Transform leftPoint;

    [SerializeField]
    private Transform rightPoint;

    private Rigidbody2D thisRigidBody;
    private Collider2D thisCollider;

    private float maxX;
    private float minX;

    private float patrollerMinX;
    private float patrollerMaxX;
    private bool leftHit = false;
    private bool rightHit = false;

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
        if (onPlatform)
        {
            if (platform.activeInHierarchy)
                Calculations();
        }
        else
            Calculations();
    }

    private void Calculations()
    {
        patrollerMinX = thisCollider.bounds.min.x + thisRigidBody.velocity.x * Time.fixedDeltaTime;
        patrollerMaxX = thisCollider.bounds.max.x + thisRigidBody.velocity.x * Time.fixedDeltaTime;
        if (patrollerMinX <= minX && !leftHit)
        {
            leftHit = true;
            rightHit = false;
            thisRigidBody.velocity = new Vector2(Mathf.Abs(thisRigidBody.velocity.x), thisRigidBody.velocity.y);
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
        else if (patrollerMaxX >= maxX && !rightHit)
        {
            leftHit = false;
            rightHit = true;
            thisRigidBody.velocity = new Vector2(-Mathf.Abs(thisRigidBody.velocity.x), thisRigidBody.velocity.y);
            gameObject.transform.localScale = new Vector2(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
    }
}
