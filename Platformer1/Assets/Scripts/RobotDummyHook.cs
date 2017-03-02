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
    int framesBetweenHits = 50;

    [SerializeField]
    string targetTags = "Character";

    Rigidbody2D thisRigidBody;
    SpriteRenderer thisSpriteRenderer;
    string[] tags;
    Animator thisAnimator;
    bool hitting = false;
    int counter;
    bool facingRight = true;

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
        counter = framesBetweenHits;
    }

    void FixedUpdate()
    {
        if (counter + 1 <= framesBetweenHits)
        {
            if (!thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                counter++;
        }
        else
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
                                facingRight = (thisRigidBody.velocity.x > 0);
                                gameObject.GetComponent<DamageDealer>().applyDamageOnce(collider.gameObject);
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                                counter = 0;
                                breakAll = true;
                                break;
                            }
                    if (breakAll) break;
                }
        }
        thisAnimator.SetBool("Hook", hitting);
        if (thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            thisSpriteRenderer.sortingLayerName = "Character";
        else
            thisSpriteRenderer.sortingLayerName = "EnemyRobot";
        if (!hitting && !thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && thisRigidBody.velocity.x == 0)
            if (facingRight)
                thisRigidBody.velocity = new Vector2(xVelocity, 0);
            else
                thisRigidBody.velocity = new Vector2(-xVelocity, 0);
        hitting = false;
    }
}
