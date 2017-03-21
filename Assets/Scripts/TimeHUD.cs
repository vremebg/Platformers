using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHUD : MonoBehaviour {

    private Text textComp;

    void Start()
    {
        textComp = gameObject.GetComponent<Text>();
    }

	void Update () {
        textComp.text = ((int) Time.timeSinceLevelLoad).ToString();
	}
}
