using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHandler : MonoBehaviour {

    [SerializeField]
    GameObject character;

    [SerializeField]
    GameObject controls;

    Character charScript;
    KeyboardControls keyboard;

	// Use this for initialization
	void Start () {
        charScript = character.GetComponent<Character>();
        keyboard = controls.GetComponent<KeyboardControls>();
	}
	
    public void setLeft()
    {
        charScript.right = false;
        charScript.left = true;
        keyboard.interactionX = true;
    }

    public void setRight()
    {
        charScript.right = true;
        charScript.left = false;
        keyboard.interactionX = true;
    }

    public void resetLeft()
    {
        charScript.left = false;
        keyboard.interactionX = false;
    }

    public void resetRight()
    {
        charScript.right = false;
        keyboard.interactionX = false;
    }

    public void setUp()
    {
        charScript.up = true;
        keyboard.interactionUp = true;
    }

    public void resetUp()
    {
        charScript.up = false;
        keyboard.interactionUp = false;
    }
}
