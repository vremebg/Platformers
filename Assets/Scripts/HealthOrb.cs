using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour {

    [SerializeField]
    private bool desroyOnContact = false;

    [SerializeField]
    private int secondsBetweenHealthGive = 5;

    [SerializeField]
    private float maxDeviationY = 0.4f;

    [SerializeField]
    private float maxDeviationX = 0.1f;

    [SerializeField]
    private float angleStepPerSecond = 5f;


    private SpriteRenderer orbSpriteRenderer;

    private float startOfCooldown;
    private float initialTransformY;
    private float initialTransformX;
    private float angle = 0;

    void Start()
    {
        startOfCooldown = Time.time;
        orbSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        initialTransformY = transform.position.y;
        initialTransformX = transform.position.x;
    }

    void FixedUpdate()
    {
        orbSpriteRenderer.color = new Color(1, 1, 1, Mathf.Clamp((Time.time - startOfCooldown)/secondsBetweenHealthGive,0,1));
        if ((Time.time - startOfCooldown) < secondsBetweenHealthGive)
        {
            angle += angleStepPerSecond * Time.fixedDeltaTime;
            transform.position = new Vector2(initialTransformX + (maxDeviationX * Mathf.Cos(angle)),
                                            initialTransformY + (maxDeviationY * Mathf.Sin(angle)));
        }
        else
        {
            gameObject.transform.position = new Vector2(initialTransformX, initialTransformY);
            angle = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character") && !collider.isTrigger && ((Time.time - secondsBetweenHealthGive) >= startOfCooldown))
        {
            if (!gameObject.GetComponent<HealthGiver>().isHealthMaxed(collider.gameObject))
            {
                gameObject.GetComponent<HealthGiver>().GivehealthOnce(collider.gameObject);
                startOfCooldown = Time.time;
                if (desroyOnContact) DestroyObject(gameObject);
            }
        }
    }
}
