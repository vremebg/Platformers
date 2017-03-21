using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHandler : MonoBehaviour {

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private GameObject controls;

    Character charScript;
    private KeyboardControls keyboard;

	// Use this for initialization
	void Start () {
        charScript = character.GetComponent<Character>();
        keyboard = controls.GetComponent<KeyboardControls>();
	}

    public void SetLeft()
    {
        charScript.right = false;
        charScript.left = true;
        if (keyboard != null)
            keyboard.interactionX = true;
    }

    public void SetRight()
    {
        charScript.right = true;
        charScript.left = false;
        if (keyboard != null)
            keyboard.interactionX = true;
    }

    public void ResetLeft()
    {
        charScript.left = false;
        if (keyboard != null)
            keyboard.interactionX = false;
    }

    public void ResetRight()
    {
        charScript.right = false;
        if (keyboard != null)
            keyboard.interactionX = false;
    }

    public void SetUp()
    {
        charScript.up = true;
        if (keyboard != null)
            keyboard.interactionUp = true;
    }

    public void ResetUp()
    {
        charScript.up = false;
        if (keyboard != null)
            keyboard.interactionUp = false;
    }
}
