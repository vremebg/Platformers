using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public enum characterState
    {
        inAir=0, idle
    }

    [SerializeField]
    float jumpVelocity = 5;

    [SerializeField]
    float walkVelocity = 5;

    Rigidbody2D characterRigidBody;

    characterState charState;

	// Use this for initialization
	void Start () {
        characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void HandleInput()
    {
        Vector3 position = gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.W) && charState != characterState.inAir)
        {
            characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, jumpVelocity);
            charState = characterState.inAir;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector3(position.x - walkVelocity, position.y, position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector3(position.x + walkVelocity, position.y, position.z);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground") && charState == characterState.inAir)
        {
            charState = characterState.idle;
        }
    }
}
