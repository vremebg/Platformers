using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    [SerializeField]
    string passingTags = "Character,Enemy";

    string[] tags;

    // Use this for initialization
    void Start(){
        tags = passingTags.Split(',');
        if (tags != null && tags.Length != 0)
            foreach (string tag in tags)
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
                if (objects != null && objects.Length != 0)
                    foreach (GameObject obj in objects)
                    {
                        Collider2D[] colliders = obj.GetComponents<Collider2D>();
                        if (colliders != null && colliders.Length != 0)
                            foreach (Collider2D objCollider in colliders)
                                Physics2D.IgnoreCollision(objCollider, gameObject.GetComponents<BoxCollider2D>()[1], true);
                    }
            }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (tags != null && tags.Length != 0)
            foreach (string tag in tags)
                if (collider.gameObject.CompareTag(tag)) //&& charState == characterState.inAir)
                {
                    float checkY = collider.gameObject.GetComponent<Collider2D>().bounds.min.y;
                    Vector3 max = gameObject.GetComponents<BoxCollider2D>()[1].bounds.max;
                    if (checkY >= max.y)
                    {
                        Physics2D.IgnoreCollision(collider, gameObject.GetComponents<BoxCollider2D>()[1], false);
                    }
                    else
                    {
                        Physics2D.IgnoreCollision(collider, gameObject.GetComponents<BoxCollider2D>()[1], true);
                    }
                }
    }
}
