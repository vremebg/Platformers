using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDummyHook : MonoBehaviour {

    [SerializeField]
    float xVelocity;

    [SerializeField]
    Transform startRaycastPoint;

    [SerializeField]
    Transform endRaycastPoint;

    [SerializeField]
    float secondsBetweenHits = 2;

    [SerializeField]
    string targetTags = "Character";

    Rigidbody2D thisRigidBody;
    SpriteRenderer thisSpriteRenderer;
    string[] tags;
    Animator thisAnimator;
    bool hitting = false;
    float timeCounter;

    void Start () {
        thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
        thisRigidBody.velocity = new Vector2(xVelocity, 0);
        thisAnimator = gameObject.GetComponent<Animator>();
        tags = targetTags.Split(',');
        if (tags != null && tags.Length != 0)
            foreach (string tag in tags)
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
                if (objects != null && objects.Length != 0)
                    foreach (GameObject obj in objects)
                    {
                        Collider2D[] colliders = obj.GetComponents<Collider2D>();
                        if (colliders != null && colliders.Length != 0)
                            foreach (Collider2D objCollider in colliders)
                                Physics2D.IgnoreCollision(objCollider, gameObject.GetComponent<BoxCollider2D>(), true);
                    }
            }
        timeCounter = Time.time;
    }

    void Update()
    {
        if (Time.time - timeCounter >= secondsBetweenHits)
        {
            bool breakAll = false;
            Collider2D[] colliders = Physics2D.OverlapAreaAll(startRaycastPoint.position, endRaycastPoint.position);
            if (colliders != null && colliders.Length != 0)
                foreach (Collider2D collider in colliders)
                {
                    if (tags != null & tags.Length != 0)
                        foreach (string tag in tags)
                            if (collider.CompareTag(tag))
                            {
                                hitting = true;
                                gameObject.GetComponent<DamageDealer>().applyDamageOnce(collider.gameObject);
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                                timeCounter = Time.time;
                                breakAll = true;
                                break;
                            }
                    if (breakAll) break;
                }
        }
        thisAnimator.SetBool("Hook", hitting);
        if (thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            thisSpriteRenderer.sortingLayerName = "Character";
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
            thisSpriteRenderer.sortingLayerName = "EnemyRobot";
        if (!hitting && !thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (Mathf.Abs(thisRigidBody.velocity.x) < xVelocity))
            if (transform.localScale.x >= 0)
                thisRigidBody.velocity = new Vector2(xVelocity, 0);
            else
                thisRigidBody.velocity = new Vector2(-xVelocity, 0);
        hitting = false;
    }
}
