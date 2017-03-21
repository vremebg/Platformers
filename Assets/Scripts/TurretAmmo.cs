using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAmmo : MonoBehaviour {

    [SerializeField]
    private GameObject ammoExplosion;

    [SerializeField]
    private string tagsForExploding;

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float lifetimeInSeconds = 2;

    private string[] tags;

    void Start()
    {
        Destroy(gameObject, lifetimeInSeconds);
        tags = tagsForExploding.Split(',');
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
            gameObject.GetComponent<DamageDealer>().ApplyDamageOnce(collider.gameObject);
        if (tags != null && tags.Length != 0)
            foreach (string tag in tags)
                if (collider.gameObject.CompareTag(tag))
                {
                    Instantiate(ammoExplosion, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    DestroyObject(gameObject);
                }
    }

    //dx,dy are distance between shootingPoint and Target
    public void setInitialVelocity(float dx, float dy)
    {
        float yVel = speed * dy / Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
        float xVel = speed * dx / Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }
}