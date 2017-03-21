using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour {

    [SerializeField]
    private GameObject character;

    private Character script;

    public bool interactionX = false;
    public bool interactionUp = false;

    // Use this for initialization
    void Start()
    {
        script = character.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            HandleInput();
    }

    private void HandleInput()
    {
        if (!interactionUp)
            if (Input.GetKey(KeyCode.W)) script.up = true;
            else script.up = false;

        if (!interactionX)
            if (Input.GetKey(KeyCode.A)) script.left = true;
            else
            {
                script.left = false;
                if (Input.GetKey(KeyCode.D)) script.right = true;
                else script.right = false;
            }
    }
}
