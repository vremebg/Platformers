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

    characterState charState;
    characterFacing charFacing;

	// Use this for initialization
	void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void HandleInput()
    {
        Vector3 position = gameObject.transform.position;
        Vector3 localScale = gameObject.transform.localScale;

        if (Input.GetKey(KeyCode.W) && charState != characterState.inAir)
        {
            characterRigidBody.velocity = new Vector2(0, jumpVelocity);
            charState = characterState.inAir;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector3(position.x - walkVelocity, position.y, position.z);
            if (charFacing == characterFacing.right)
            {
                charFacing = characterFacing.left;
                localScale.x *= -1;
                gameObject.transform.localScale = localScale;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector3(position.x + walkVelocity, position.y, position.z);
            if (charFacing == characterFacing.left)
            {
                charFacing = characterFacing.right;
                localScale.x *= -1;
                gameObject.transform.localScale = localScale;
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground") || collision.collider.gameObject.CompareTag("Platform") && charState == characterState.inAir)
        {
            charState = characterState.idle;
        }
    }
}
