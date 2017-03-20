using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public enum characterState
    {
        jumping=0, falling, onTheGround
    }

    public enum characterFacing
    {
        right=0, left
    }

    [SerializeField]
    float maxJumpAngle = 30;

    [SerializeField]
    Transform[] groundPoints;

    float walkVelocityPerSecond;
    float xForce;
    Rigidbody2D characterRigidBody;
    Animator characterAnimator;
    VelocityController velocity;

    characterState charState;
    characterFacing charFacing = characterFacing.right;

    public bool up, left, right = false;
    bool isMoving = false;
    bool jumpedInThisFrame = false;
    bool triggered = false;
    bool canJump = false;
    float lastFrameCharacterY;
    float deviationYToCountForProperOnPlatform = 0.05f; //the ground points should be in 0.05f distance from top of the platform collider
    bool onMovingPlatform = false;

    // Use this for initialization
    void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        characterAnimator = gameObject.GetComponent<Animator>();
        velocity = gameObject.GetComponent<VelocityController>();
        lastFrameCharacterY = transform.position.y;
        xForce = velocity.getXForce();
    }

    void HandleMovement()
    {
        Vector3 localScale = gameObject.transform.localScale;
        if (up && charState == characterState.onTheGround && canJump)
        {
            characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, 0);
            characterRigidBody.AddForce(new Vector2(0, velocity.getJumpVelocity()), ForceMode2D.Impulse);
            charState = characterState.jumping;
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

    void SetAnimations()
    {
        if (up && charState == characterState.onTheGround && canJump)
            jumpedInThisFrame = true;
        if (left || right)
            if (charState != characterState.jumping || charState != characterState.falling)
                isMoving = true;
        characterAnimator.SetBool("Moving", isMoving);
        characterAnimator.SetBool("OnTheGround", (charState == characterState.onTheGround || jumpedInThisFrame));
        characterAnimator.SetBool("Jumping", (charState == characterState.jumping));
        characterAnimator.SetBool("Falling", (charState == characterState.falling));
    }

    void ResetBoolAnimValues()
    {
        isMoving = false;
        jumpedInThisFrame = false;
    }

    void ResetPhysicsValues()
    {
        triggered = false;
        canJump = false;
        lastFrameCharacterY = transform.position.y;
    }

    void ChangeScaleFacing(Vector3 localScale)
    {
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckGroundPoints();
        if (characterRigidBody.velocity.y < 0 && !triggered && charState != characterState.onTheGround)
        {
            charState = characterState.falling;
        }
        if (charState != characterState.onTheGround)
            walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();
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
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(groundPoints[1].position, 0.15f,LayerMask.GetMask("Platforms")))
        {
            if (collider.gameObject.CompareTag("MovingPlatform") && !triggered && characterRigidBody.velocity.y <= 0)
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
            foreach (Collider2D collider in Physics2D.OverlapPointAll(point.position))
            {
                if (collider.gameObject.CompareTag("Ground") && !collider.isTrigger)
                {
                    float angle = collider.gameObject.transform.rotation.eulerAngles.z;
                    if (angle > 180) angle = 360 - angle;
                    if (angle <= maxJumpAngle)
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
                        if (angle <= maxJumpAngle)
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
}
