using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPit : MonoBehaviour {

    [SerializeField]
    float dps = 25;

    [SerializeField]
    int framesBetweenDamage = 4;

    [SerializeField]
    string targetTags;

    bool start = false;
    int counter = 0;

    GameObject receiver;
    string[] tags;

    // Use this for initialization
    void Start () {
        tags = targetTags.Split(',');
    }

    void FixedUpdate()
    {
        counter++;
        if (counter > framesBetweenDamage) counter = framesBetweenDamage;
        if (start && receiver != null && counter == framesBetweenDamage)
        {
            gameObject.GetComponent<DamageDealer>().applyDamageOnce(receiver, framesBetweenDamage * dps * Time.deltaTime);
            counter = 0;
        }
    }
	
    private void OnTriggerEnter2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag))
            {
                start = true;
                receiver = collider.gameObject;
            }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag))
            start = false;
    }
}