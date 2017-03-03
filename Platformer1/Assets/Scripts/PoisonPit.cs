using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPit : MonoBehaviour {

    private class Receiver
    {
        public float timeCounter;
        public GameObject obj = new GameObject();
    }

    LinkedList<Receiver> receivers = new LinkedList<Receiver>();
    LinkedList<Receiver> itemsToRemove = new LinkedList<Receiver>();

    [SerializeField]
    float dps = 20;

    [SerializeField]
    float secondsBetweenDamage = 0.1f;

    [SerializeField]
    string targetTags;

    string[] tags;

    // Use this for initialization
    void Start () {
        tags = targetTags.Split(',');
    }

    void Update()
    {
        if (receivers != null)
        {
            foreach (Receiver receiver in receivers)
                if (Time.time - receiver.timeCounter >= secondsBetweenDamage)
                {
                    if (receiver.obj != null)
                    {
                        gameObject.GetComponent<DamageDealer>().applyDamageOnce(receiver.obj, dps * (Time.time - receiver.timeCounter));
                        receiver.timeCounter = Time.time;
                    }
                    else
                        itemsToRemove.AddLast(receiver);
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
	
    private void OnTriggerEnter2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag))
            {
                Receiver temp = new Receiver();
                temp.obj = collider.gameObject;
                temp.timeCounter = Time.time;
                receivers.AddLast(temp);
            }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        foreach (string tag in tags)
            if (collider.gameObject.CompareTag(tag))
            {
                foreach (Receiver receiver in receivers)
                    if (receiver.obj.Equals(collider.gameObject))
                        itemsToRemove.AddLast(receiver);
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