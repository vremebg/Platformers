using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDummyHook : MonoBehaviour {

    [SerializeField]
    private float xVelocity;

    [SerializeField]
    private Transform startRaycastPoint;

    [SerializeField]
    private Transform endRaycastPoint;

    [SerializeField]
    private float secondsBetweenHits = 2;

    [SerializeField]
    private string targetTags = "Character";

    [SerializeField]
    private GameObject deathExplosion;

    private Health robotHealth;
    private Rigidbody2D thisRigidBody;
    private SpriteRenderer thisSpriteRenderer;
    private DamageDealer dmgDealer;
    private string[] tags;
    private Animator thisAnimator;
    private bool hitting = false;
    private float timeCounter;

    void Start () {
        thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
        thisRigidBody.velocity = new Vector2(xVelocity, 0);
        thisAnimator = gameObject.GetComponent<Animator>();
        tags = targetTags.Split(',');
        timeCounter = Time.time;
        dmgDealer = gameObject.GetComponent<DamageDealer>();
        robotHealth = gameObject.GetComponent<Health>();
    }

    void FixedUpdate()
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
                                dmgDealer.ApplyDamageOnce(collider.gameObject);
                                thisRigidBody.velocity = new Vector2(0, 0);
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
        if (!hitting && !thisAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (Mathf.Abs(thisRigidBody.velocity.x) != xVelocity))
            if (transform.localScale.x >= 0)
                thisRigidBody.velocity = new Vector2(xVelocity, 0);
            else
                thisRigidBody.velocity = new Vector2(-xVelocity, 0);
        hitting = false;
    }
}
