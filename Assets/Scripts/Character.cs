using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    private enum characterState
    {
        jumping=0, falling, onTheGround
    }

    private enum characterFacing
    {
        right=0, left
    }

    [SerializeField]
    private float maxJumpAngle = 30;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private GameObject secondJumpParticleSystem;

    private string[] groundLayers = new string[] { "Platforms", "Barrels", "Ground" };
    private string[] enemyLayers = new string[] { "Dummy Robot" };
    private List<GameObject> hitTargets = new List<GameObject>();
    private float walkVelocityPerSecond;
    private float xForce;
    private Rigidbody2D characterRigidBody;
    private Collider2D charCollider;
    private Animator characterAnimator;
    private VelocityController velocity;
    private DamageDealer dmgDealer;
    private characterState charState;
    private characterFacing charFacing = characterFacing.right;
    private bool isMoving = false;
    private bool jumpedInThisFrame = false;
    private bool triggered = false;
    private bool canJump = false;
    private float lastFrameCharacterY;
    private float deviationYToCountForProperOnPlatform = 0.05f; //the ground points should be in 0.05f distance from top of the platform collider
    private float deviationYToCountForProperSteppingOnEnemies = 0.2f;
    private bool onMovingPlatform = false;
    private bool secondJump = false;

    public bool up, left, right = false;

    // Use this for initialization
    void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        charCollider = gameObject.GetComponent<Collider2D>();
        characterAnimator = gameObject.GetComponent<Animator>();
        velocity = gameObject.GetComponent<VelocityController>();
        lastFrameCharacterY = transform.position.y;
        xForce = velocity.getXForce();
        dmgDealer = gameObject.GetComponent<DamageDealer>();
    }

    private void HandleMovement()
    {
        Vector3 localScale = gameObject.transform.localScale;
        if (up)
            if (charState == characterState.onTheGround && canJump)
            {
                characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, 0);
                characterRigidBody.AddForce(new Vector2(0, velocity.getJumpVelocity()), ForceMode2D.Impulse);
                charState = characterState.jumping;
                jumpedInThisFrame = true;
                secondJump = false;
                up = false;
            }
            else
                if (!secondJump)
                {
                    characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, 0);
                    characterRigidBody.AddForce(new Vector2(0, velocity.getJumpVelocity()), ForceMode2D.Impulse);
                    charState = characterState.jumping;
                    Instantiate(secondJumpParticleSystem, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    secondJump = true;
                    up = false;
                }
        if (left)
        {
            if (characterRigidBody.velocity.x > -walkVelocityPerSecond)
                characterRigidBody.AddForce(new Vector2(-xForce, 0), ForceMode2D.Impulse);
            if (charFacing == characterFacing.right)
            {
                charFacing = characterFacing.left;
                ChangeScaleFacing(localScale);
            }
        }
        else
        if (right)
        {
            if (characterRigidBody.velocity.x < walkVelocityPerSecond)
                characterRigidBody.AddForce(new Vector2(xForce, 0), ForceMode2D.Impulse);
            if (charFacing == characterFacing.left)
            {
                charFacing = characterFacing.right;
                ChangeScaleFacing(localScale);
            }
        }
        else
            if (characterRigidBody.velocity.x > 0 || characterRigidBody.velocity.x < 0)
                characterRigidBody.AddForce(new Vector2(-characterRigidBody.velocity.x, 0), ForceMode2D.Impulse);
    }

    private void SetAnimations()
    {
        if (left || right)
            if (charState != characterState.jumping || charState != characterState.falling)
                isMoving = true;
        characterAnimator.SetBool("Moving", isMoving);
        characterAnimator.SetBool("OnTheGround", (charState == characterState.onTheGround || jumpedInThisFrame));
        characterAnimator.SetBool("Jumping", (charState == characterState.jumping));
        characterAnimator.SetBool("Falling", (charState == characterState.falling));
    }

    private void ResetBoolAnimValues()
    {
        isMoving = false;
        jumpedInThisFrame = false;
    }

    private void ResetPhysicsValues()
    {
        triggered = false;
        canJump = false;
        lastFrameCharacterY = transform.position.y;
    }

    private void ChangeScaleFacing(Vector3 localScale)
    {
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckGroundPoints();
        SteppingOnEnemies();
        if (characterRigidBody.velocity.y < 0 && !triggered && charState != characterState.onTheGround)
        {
            charState = characterState.falling;
        }
        if (charState != characterState.onTheGround)
            walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
        else
        {
            hitTargets.Clear();
            secondJump = false;
        }
        HandleMovement();
        ResetPhysicsValues();
    }

    void Update()
    {
        SetAnimations();
        ResetBoolAnimValues();
    }

    private void CheckGroundPoints()
    {
        onMovingPlatform = false;
        //this is a fix for the moving platform moving downwards because character looses connection with it
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(groundPoints[1].position, new Vector2(0, 0.15f),LayerMask.GetMask("Platforms")))
        {
            if (collider.gameObject.CompareTag("MovingPlatform") && !triggered && characterRigidBody.velocity.y <= 0
                && groundPoints[1].position.y > collider.bounds.max.y)
            {
                onMovingPlatform = true;
                float dx = collider.gameObject.GetComponentInParent<PathFollower>().dx;
                float dy = collider.gameObject.GetComponentInParent<PathFollower>().dy;
                transform.Translate(new Vector2(dx, dy));

                charState = characterState.onTheGround;
                walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
                canJump = true;
                triggered = true;
            }
        }

        foreach (Transform point in groundPoints)
            foreach (Collider2D collider in Physics2D.OverlapPointAll(point.position, LayerMask.GetMask(groundLayers)))
            {
                if (collider.gameObject.CompareTag("Ground") && !collider.isTrigger)
                {
                    float angle = collider.gameObject.transform.rotation.eulerAngles.z;
                    if (angle > 180) angle = 360 - angle;
                    //if (angle <= maxJumpAngle)
                        canJump = true;
                    charState = characterState.onTheGround;
                    if (angle != 0 && transform.position.y >= lastFrameCharacterY)
                        walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond() * (90 - angle) / 90;
                    else
                        walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
                    triggered = true;
                }
                if (characterRigidBody.velocity.y <= 0)
                    if (collider.gameObject.CompareTag("Platform") && !collider.isTrigger && point.position.y > collider.bounds.max.y - deviationYToCountForProperOnPlatform)
                    {
                        float angle = collider.gameObject.transform.rotation.eulerAngles.z;
                        if (angle > 180) angle = 360 - angle;
                        //if (angle <= maxJumpAngle)
                            canJump = true;
                        charState = characterState.onTheGround;
                        if (angle != 0 && transform.position.y >= lastFrameCharacterY)
                            walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond() * (90 - angle) / 90;
                        else
                            walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
                        triggered = true;
                    }
                if (collider.gameObject.CompareTag("Barrel"))
                {
                    charState = characterState.onTheGround;
                    walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
                    canJump = true;
                    triggered = true;
                }
                if (collider.gameObject.CompareTag("MovingPlatform") && !triggered && characterRigidBody.velocity.y <= 0 
                    && !onMovingPlatform && point.position.y > collider.bounds.max.y - deviationYToCountForProperOnPlatform)
                {
                    onMovingPlatform = true;
                    float dx = collider.gameObject.GetComponentInParent<PathFollower>().dx;
                    float dy = collider.gameObject.GetComponentInParent<PathFollower>().dy;
                    transform.Translate(new Vector2(dx,dy));

                    charState = characterState.onTheGround;
                    walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
                    canJump = true;
                    triggered = true;
                }
            }
    }

    private void SteppingOnEnemies()
    {
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(new Vector2(charCollider.bounds.min.x, charCollider.bounds.min.y), new Vector2(charCollider.bounds.size.x, -deviationYToCountForProperSteppingOnEnemies), LayerMask.GetMask(enemyLayers)))
        {
            if (!hitTargets.Contains(collider.gameObject) && collider.gameObject.CompareTag("Enemy") && !collider.isTrigger && characterRigidBody.velocity.y < 0
                && charCollider.bounds.min.y > collider.bounds.max.y - deviationYToCountForProperSteppingOnEnemies)
            {
                hitTargets.Add(collider.gameObject);
                dmgDealer.ApplyDamageOnce(collider.gameObject);
            }
        }
    }
}
