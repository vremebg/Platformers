using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public enum characterState
    {
        inAir=0, idle
    }

    public enum characterFacing
    {
        right=0, left
    }

    [SerializeField]
    float jumpVelocity = 5;

    [SerializeField]
    float walkVelocity = 5;

    Rigidbody2D characterRigidBody;
    Animator characterAnimator;

    characterState charState;
    characterFacing charFacing = characterFacing.left;
    bool isMoving = false;
    bool isJumping = false;

	// Use this for initialization
	void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        characterAnimator = gameObject.GetComponent<Animator>();
    }

    void HandleInput()
    {
        Vector3 position = gameObject.transform.position;
        Vector3 localScale = gameObject.transform.localScale;

        if (Input.GetKey(KeyCode.W) && charState != characterState.inAir)
        {
            characterRigidBody.velocity = new Vector2(0, jumpVelocity);
            charState = characterState.inAir;
            isJumping = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector3(position.x - walkVelocity, position.y, position.z);
            if (charFacing == characterFacing.right)
            {
                charFacing = characterFacing.left;
                ChangeScaleFacing(localScale);
            }
            if (charState != characterState.inAir)
                isMoving = true;
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector3(position.x + walkVelocity, position.y, position.z);
            if (charFacing == characterFacing.left)
            {
                charFacing = characterFacing.right;
                ChangeScaleFacing(localScale);
            }
            if (charState != characterState.inAir)
                isMoving = true;
        }
    }

    void SetAnimations()
    {
        characterAnimator.SetBool("Moving", isMoving);
        characterAnimator.SetBool("Jumping", isJumping);
    }

    void ResetBoolAnimValues()
    {
        isMoving = false;
        if (isJumping && characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("CharJumping"))
            isJumping = false;
    }

    void ChangeScaleFacing(Vector3 localScale)
    {
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();
        SetAnimations();
        ResetBoolAnimValues();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground") || collision.collider.gameObject.CompareTag("Platform") && charState == characterState.inAir)
        {
            charState = characterState.idle;
        }
    }
}
