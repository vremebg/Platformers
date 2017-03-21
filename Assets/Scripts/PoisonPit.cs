using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPit : MonoBehaviour {

    private class Receiver
    {
        public float timeCounter;
        public GameObject obj;
    }

    private List<Receiver> receivers = new List<Receiver>();
    private List<Receiver> itemsToRemove = new List<Receiver>();

    [SerializeField]
    private float dps = 20;

    [SerializeField]
    private float secondsBetweenDamage = 0.1f;

    [SerializeField]
    private string targetTags;

    private string[] tags;

    // Use this for initialization
    void Start () {
        tags = targetTags.Split(',');
    }

    void Update()
    {
        if (receivers != null && receivers.Count > 0)
        {
            foreach (Receiver receiver in receivers)
                if (Time.time - receiver.timeCounter >= secondsBetweenDamage)
                {
                    if (receiver.obj != null)
                    {
                        gameObject.GetComponent<DamageDealer>().ApplyDamageOnce(receiver.obj, dps * (Time.time - receiver.timeCounter));
                        receiver.timeCounter = Time.time;
                    }
                    else
                        itemsToRemove.Add(receiver);
                }
            if (itemsToRemove.Count != 0)
            {
                foreach (Receiver temp in itemsToRemove)
                {
                    receivers.Remove(temp);
                }
                itemsToRemove.Clear();
            }
        }
    }
	
    void OnTriggerEnter2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag) && !collider.isTrigger)
            {
                Receiver temp = new Receiver();
                temp.obj = collider.gameObject;
                temp.timeCounter = Time.time;
                receivers.Add(temp);
            }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag) && !collider.isTrigger)
            {
                foreach (Receiver receiver in receivers)
                    if (receiver.obj.Equals(collider.gameObject))
                        itemsToRemove.Add(receiver);
            }
        if (itemsToRemove.Count != 0)
        {
            foreach (Receiver temp in itemsToRemove)
            {
                receivers.Remove(temp);
            }
            itemsToRemove.Clear();
        }
    }
}