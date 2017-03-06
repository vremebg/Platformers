using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    bool objectsFollowPlatformMovement = true;

    PathFollower pathFollower;

	// Use this for initialization
	void Start () {
        pathFollower = gameObject.GetComponent<PathFollower>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (objectsFollowPlatformMovement)
        {
            if (pathFollower.platform.GetComponent<Platform>().offendersOnPlatform.Count != 0)
                foreach (Collider2D offender in pathFollower.platform.GetComponent<Platform>().offendersOnPlatform)
                    offender.transform.position = new Vector2(offender.transform.position.x + pathFollower.dx, offender.transform.position.y + pathFollower.dy);
        }
	}
}
