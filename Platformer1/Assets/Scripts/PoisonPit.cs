using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPit : MonoBehaviour {

    [SerializeField]
    float dps = 25;

    bool start = false;

    GameObject receiver;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (start && receiver != null)
            gameObject.GetComponent<DamageDealer>().applyDamageOnce(receiver, dps * Time.deltaTime);
    }
	
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
        {
            start = true;
            receiver = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
            start = false;
    }
}