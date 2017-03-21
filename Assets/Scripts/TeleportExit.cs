using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportExit : MonoBehaviour {

    [SerializeField]
    private float secondsBeforeDespawn = 1;

    [SerializeField]
    private float zAngleRotation = 0.5f;

    private float startTime;
    private Vector2 initialScale;

    void Start()
    {
        Destroy(gameObject, secondsBeforeDespawn);
        startTime = Time.time;
        initialScale = transform.localScale;
    }

    void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, zAngleRotation));
        transform.localScale = initialScale * (secondsBeforeDespawn - (Time.time - startTime)) / secondsBeforeDespawn;
	}
}
