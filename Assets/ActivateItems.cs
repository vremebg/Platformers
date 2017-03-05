using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateItems : MonoBehaviour {

    [SerializeField]
    GameObject rootObject;

    [SerializeField]
    Canvas checkSize;

    float x;
    float y;

    void Start()
    {
        x = checkSize.GetComponent<RectTransform>().rect.width * checkSize.GetComponent<RectTransform>().localScale.x;
        y = checkSize.GetComponent<RectTransform>().rect.height * checkSize.GetComponent<RectTransform>().localScale.y;
    }

    void Update()
    {
        foreach(Transform temp in rootObject.GetComponentsInChildren<Transform>(true))
        {
            if (!temp.gameObject.CompareTag("Character") && !temp.gameObject.CompareTag("Root")
                && !temp.gameObject.CompareTag("Untagged") && !temp.gameObject.CompareTag("MainCamera"))
                if (temp.position.x + x> gameObject.transform.position.x && temp.position.x - x < gameObject.transform.position.x)
                {
                    if (temp.position.y + y > gameObject.transform.position.y && temp.position.y - y < gameObject.transform.position.y)
                        temp.gameObject.SetActive(true);
                    else
                        temp.gameObject.SetActive(false);
                }
                else
                    temp.gameObject.SetActive(false);
        }
    }
}
