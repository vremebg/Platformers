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
    float jumpVelocity = 5;

    [SerializeField]
    float walkVelocityPerSecond = 250;

    Rigidbody2D characterRigidBody;
    Collider2D charBoxCollider;
    Animator characterAnimator;

    characterState charState;
    characterFacing charFacing = characterFacing.left;
    bool isMoving = false;
    bool jumpedInThisFrame = false;

    // Use this for initialization
    void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        characterAnimator = gameObject.GetComponent<Animator>();
        charBoxCollider = gameObject.GetComponent<Collider2D>();
    }

    void HandleInput()
    {
        Vector3 localScale = gameObject.transform.localScale;
        if (Input.GetKey(KeyCode.W) && charState == characterState.onTheGround)
        {
            characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, jumpVelocity);
            charState = characterState.jumping;
            jumpedInThisFrame = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterRigidBody.velocity = new Vector2(-walkVelocityPerSecond * Time.deltaTime, characterRigidBody.velocity.y);
            if (charFacing == characterFacing.right)
            {
                charFacing = characterFacing.left;
                ChangeScaleFacing(localScale);
            }
            if (charState != characterState.jumping || charState != characterState.falling)
                isMoving = true;
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            characterRigidBody.velocity = new Vector2(walkVelocityPerSecond * Time.deltaTime, characterRigidBody.velocity.y);
            if (charFacing == characterFacing.left)
            {
                charFacing = characterFacing.right;
                ChangeScaleFacing(localScale);
            }
            if (charState != characterState.jumping || charState != characterState.falling)
                isMoving = true;
        }
        else
        {
            characterRigidBody.velocity = new Vector2(0, characterRigidBody.velocity.y);
        }
    }

    void SetAnimations()
    {
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

    void ChangeScaleFacing(Vector3 localScale)
    {
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
	
	// Update is called once per frame
	void Update () {
        CollisionHandling();
        HandleInput();
        SetAnimations();
        ResetBoolAnimValues();
        GameObject temp = GameObject.Find("Velocity");
        temp.GetComponent<Text>().text = "Velocity.x " + characterRigidBody.velocity.x.ToString();
	}

    private void CollisionHandling()
    {
        if (characterRigidBody.velocity.y <= 0)
        {
            bool isFalling = true;
            Collider2D[] check = Physics2D.OverlapAreaAll(new Vector2(charBoxCollider.bounds.min.x + charBoxCollider.bounds.size.x / 2, charBoxCollider.bounds.min.y),
new Vector2(charBoxCollider.bounds.min.x + charBoxCollider.bounds.size.x/2, charBoxCollider.bounds.min.y - 0.001f));
            if (check.Length != 0)
                foreach (Collider2D temp in check)
                {
                    if (temp.gameObject.CompareTag("Ground") || temp.gameObject.CompareTag("Platform") && !temp.isTrigger)
                    {
                        isFalling = false;
                        break;
                    }
                }
            if (isFalling)
                charState = characterState.falling;
            else
                charState = characterState.onTheGround;
        }
    }
}
