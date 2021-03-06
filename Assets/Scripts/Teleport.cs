﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    [SerializeField]
    private bool twoWay = true;

    [SerializeField]
    private Transform destination;

    [SerializeField]
    private string targetTags;

    [SerializeField]
    private Transform teleportExitOneWay;

    private string[] tags;
    private bool ready = true;

    void Start()
    {
        tags = targetTags.Split(',');
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (ready)
            if (tags != null && tags.Length > 0 && !coll.isTrigger)
                foreach (string tag in tags)
                    if (coll.gameObject.CompareTag(tag))
                    {
                        if (twoWay)
                        {
                            destination.gameObject.SetActive(true);
                            destination.gameObject.GetComponent<Animator>().SetBool("Ready", false);
                            destination.GetComponent<Teleport>().ready = false;
                        }
                        if (!twoWay && teleportExitOneWay != null)
                        {
                            Instantiate(teleportExitOneWay, destination.position, Quaternion.Euler(0, 0, 0));
                        }
                        coll.gameObject.transform.position = destination.position;
                    }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (tags != null && tags.Length > 0 && !coll.isTrigger)
            foreach (string tag in tags)
                if (coll.gameObject.CompareTag(tag))
                {
                    gameObject.GetComponent<Animator>().SetBool("Ready", true);
                    ready = true;
                }
    }
}
