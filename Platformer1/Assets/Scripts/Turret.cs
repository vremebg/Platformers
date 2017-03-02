using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField]
    float range = 5;

    [SerializeField]
    int framesPerShot = 10;

    [SerializeField]
    string targetTags;

    [SerializeField]
    Transform ammo;

    [SerializeField]
    Transform shootingPoint;

    [SerializeField]
    Transform cannon;

    //when too close
    [SerializeField]
    float disableRange = 2;

    Animator turretAnimator;

    string[] tags;

    int counter = 10;

    void Start()
    {
        tags = targetTags.Split(',');
        turretAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        counter++;
        if (counter >= framesPerShot)
            counter = framesPerShot;

        if (counter >= framesPerShot)
        {
            Collider2D[] targetsInArea = Physics2D.OverlapCircleAll(this.transform.position, range);
            if (targetsInArea.Length != 0)
            {
                bool stopCheck = false;
                foreach (Collider2D collidingObject in targetsInArea)
                {
                    foreach (string tag in tags)
                        if (collidingObject.CompareTag(tag))
                        {
                            float dx = collidingObject.gameObject.transform.position.x - shootingPoint.position.x;
                            float dy = collidingObject.gameObject.transform.position.y - shootingPoint.position.y;
                            if (Mathf.Sqrt(Mathf.Pow(cannon.transform.position.x - collidingObject.gameObject.transform.position.x, 2) 
                                + Mathf.Pow(cannon.transform.position.y - collidingObject.gameObject.transform.position.y, 2)) >= disableRange)
                            {
                                if (cannon.position.x - collidingObject.gameObject.transform.position.x < 0 && this.transform.localScale.x >= 0)
                                    this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
                                if (cannon.position.x - collidingObject.gameObject.transform.position.x > 0 && this.transform.localScale.x <= 0)
                                    this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
                                cannon.rotation = Quaternion.Euler(0, 0, Mathf.Atan((cannon.position.y - collidingObject.gameObject.transform.position.y) / (cannon.position.x - collidingObject.gameObject.transform.position.x)) * 180 / Mathf.PI);
                                Instantiate(ammo, shootingPoint.position, Quaternion.Euler(0, 0, Mathf.Atan(dy / dx) * 180 / Mathf.PI));
                                GameObject[] ammos = GameObject.FindGameObjectsWithTag("Turret_ammo");
                                ammos[ammos.Length - 1].GetComponent<TurretAmmo>().setInitialVelocity(dx, dy);
                                if (dx < 0)
                                    ammos[ammos.Length - 1].transform.localScale = new Vector2(ammos[ammos.Length - 1].transform.localScale.x * -1, ammos[ammos.Length - 1].transform.localScale.y);
                                counter = 0;
                                stopCheck = true;
                                break;
                            }

                        }
                    if (stopCheck)
                        break;
                }
            }
        }
        turretAnimator.SetBool("TurretShoot", counter == 0);
    }
}
