using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

    [SerializeField]
    GameObject barrelExplosion;

    [SerializeField]
    float speedXTreshold = 0.2f;

    [SerializeField]
    float speedDecreaseXtimes = 3;

    [SerializeField]
    float secondsBetweenSpeedDecrease = 0.05f;

    public float currentVelocity;

    private Health barrelHealth;
    private Rigidbody2D barrelRigidBody;
    private float startTime;

    void Start()
    {
        barrelHealth = gameObject.GetComponent<Health>();
        barrelRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (barrelHealth.healthDepleted())
        {
            Instantiate(barrelExplosion, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            DestroyObject(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - startTime > secondsBetweenSpeedDecrease)
        {
            startTime = Time.time;
            currentVelocity = barrelRigidBody.velocity.x;
            if (collision.collider.gameObject.CompareTag("Ground") && !collision.collider.isTrigger)
            {
                /*float angle = collision.collider.gameObject.transform.rotation.eulerAngles.z;
                if (angle > 180) angle = 360 - angle;

                if (angle != 0 && transform.position.y >= lastFrameCharacterY)
                    walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond() * (90 - angle) / 90;
                else
                    walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();*/
                if (Mathf.Abs(barrelRigidBody.velocity.x) >= speedXTreshold)
                    barrelRigidBody.AddForce(new Vector2(-barrelRigidBody.velocity.x / speedDecreaseXtimes, 0),ForceMode2D.Force);
                else
                    barrelRigidBody.velocity = new Vector2(0, barrelRigidBody.velocity.y);
            }

            if (barrelRigidBody.velocity.y <= 0)
                if (collision.collider.gameObject.CompareTag("Platform") && !collision.collider.isTrigger)
                {
                    /*float angle = collision.collider.gameObject.transform.rotation.eulerAngles.z;
                    if (angle > 180) angle = 360 - angle;

                    if (angle != 0 && transform.position.y >= lastFrameCharacterY)
                        walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond() * (90 - angle) / 90;
                    else
                        walkVelocityPerSecond = velocity.getMaxWalkVelocityPerSecond();*/
                    if (Mathf.Abs(barrelRigidBody.velocity.x) >= speedXTreshold)
                        barrelRigidBody.AddForce(new Vector2(-barrelRigidBody.velocity.x / speedDecreaseXtimes, 0), ForceMode2D.Impulse);
                    else
                        barrelRigidBody.velocity = new Vector2(0, barrelRigidBody.velocity.y);
                }
        }
    }
}
