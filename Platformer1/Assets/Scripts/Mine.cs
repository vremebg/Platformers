﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
        {
            gameObject.GetComponent<DamageDealer>().applyDamageOnce(collider.gameObject);
            DestroyObject(gameObject);
        }
    }
}
