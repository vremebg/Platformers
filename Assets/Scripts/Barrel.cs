using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

    [SerializeField]
    private GameObject barrelExplosion;

    [SerializeField]
    private float speedXTreshold = 0.2f;

    [SerializeField]
    private float speedDecreaseXtimes = 3;

    [SerializeField]
    private float secondsBetweenSpeedDecrease = 0.05f;

    private float currentVelocity;
    
    private Rigidbody2D barrelRigidBody;
    private float startTime;

    void Start()
    {
        barrelRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - startTime > secondsBetweenSpeedDecrease)
        {
            startTime = Time.time;
            currentVelocity = barrelRigidBody.velocity.x;
            if (collision.collider.gameObject.CompareTag("Ground") && !collision.collider.isTrigger)
            {
                if (Mathf.Abs(barrelRigidBody.velocity.x) >= speedXTreshold)
                    barrelRigidBody.AddForce(new Vector2(-barrelRigidBody.velocity.x / speedDecreaseXtimes, 0),ForceMode2D.Force);
                else
                    barrelRigidBody.velocity = new Vector2(0, barrelRigidBody.velocity.y);
            }

            if (barrelRigidBody.velocity.y <= 0)
                if (collision.collider.gameObject.CompareTag("Platform") && !collision.collider.isTrigger)
                {
                    if (Mathf.Abs(barrelRigidBody.velocity.x) >= speedXTreshold)
                        barrelRigidBody.AddForce(new Vector2(-barrelRigidBody.velocity.x / speedDecreaseXtimes, 0), ForceMode2D.Impulse);
                    else
                        barrelRigidBody.velocity = new Vector2(0, barrelRigidBody.velocity.y);
                }
        }
    }
}
