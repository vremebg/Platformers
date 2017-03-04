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
    Animator characterAnimator;

    characterState charState;
    characterFacing charFacing = characterFacing.left;
    bool isMoving = false;
    bool jumpedInThisFrame = false;
    bool triggered = false;

    // Use this for initialization
    void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        characterAnimator = gameObject.GetComponent<Animator>();
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
        triggered = false;
    }

    void ChangeScaleFacing(Vector3 localScale)
    {
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (characterRigidBody.velocity.y < 0 && !triggered)
        {
            charState = characterState.falling;
        }
        HandleInput();
        SetAnimations();
        ResetBoolAnimValues();
        GameObject temp = GameObject.Find("Velocity");
        temp.GetComponent<Text>().text = "Velocity.x " + characterRigidBody.velocity.x.ToString();
	}

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.gameObject.CompareTag("Ground") || collider.gameObject.CompareTag("Platform")) && !collider.isTrigger)
        {
            charState = characterState.onTheGround;
            triggered = true;
        }
    }
}
