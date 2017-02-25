using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour {

    [SerializeField]
    bool desroyOnContact = false;

    [SerializeField]
    int secondsBetweenHealthGive = 5;

    [SerializeField]
    float maxDeviationY = 0.6f;

    [SerializeField]
    float maxDeviationX = 0.3f;

    [SerializeField]
    float angleStep = 0.1f;


    SpriteRenderer orbSpriteRenderer;
    Rigidbody2D orbRigidBody;

    float startOfCooldown;
    float initialTransformY;
    float initialTransformX;
    float angle = 0;

    void Start()
    {
        startOfCooldown = Time.time;
        orbSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        orbRigidBody = gameObject.GetComponent<Rigidbody2D>();
        initialTransformY = transform.position.y;
        initialTransformX = transform.position.x;
    }

    void FixedUpdate()
    {
        orbSpriteRenderer.color = new Color(1, 1, 1, Mathf.Clamp((Time.time - startOfCooldown)/secondsBetweenHealthGive,0,1));
        if ((Time.time - startOfCooldown) < secondsBetweenHealthGive)
        {
            angle += angleStep;
            transform.position = new Vector2(initialTransformX + (maxDeviationX * Mathf.Cos(angle)),
                                            initialTransformY + (maxDeviationY * Mathf.Sin(angle)));
        }
        else
        {
            gameObject.transform.position = new Vector2(initialTransformX, initialTransformY);
            angle = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character") && ((Time.time - secondsBetweenHealthGive) >= startOfCooldown))
        {
            if (!gameObject.GetComponent<HealthGiver>().isHealthMaxed(collider.gameObject))
            {
                gameObject.GetComponent<HealthGiver>().givehealthOnce(collider.gameObject);
                startOfCooldown = Time.time;
                if (desroyOnContact) DestroyObject(gameObject);
            }
        }
    }
}
