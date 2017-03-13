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
    float objSizeX;
    float objSizeY;

    void Start()
    {
        x = checkSize.GetComponent<RectTransform>().rect.width * checkSize.GetComponent<RectTransform>().localScale.x;
        y = checkSize.GetComponent<RectTransform>().rect.height * checkSize.GetComponent<RectTransform>().localScale.y + 2; //hack for tall objects activated and platformer beneath them not
    }

    void Update()
    {
        foreach(Transform temp in rootObject.GetComponentsInChildren<Transform>(true))
        {
            if (!temp.gameObject.CompareTag("Character") && !temp.gameObject.CompareTag("Root")
                && !temp.gameObject.CompareTag("Untagged") && !temp.gameObject.CompareTag("MainCamera"))
            {
                if (temp.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    objSizeX = temp.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    objSizeY = temp.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                }
                else
                {
                    objSizeX = 0;
                    objSizeY = 0;
                }
                if (temp.position.x + objSizeX + x > gameObject.transform.position.x
                    && temp.position.x - objSizeX - x < gameObject.transform.position.x)
                {
                    if (temp.position.y + objSizeY + y > gameObject.transform.position.y
                        && temp.position.y - objSizeY - y < gameObject.transform.position.y)
                        temp.gameObject.SetActive(true);
                    else
                        temp.gameObject.SetActive(false);
                }
                else
                    temp.gameObject.SetActive(false);
            }
        }
    }
}
