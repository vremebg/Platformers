using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateItems : MonoBehaviour
{

    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private Canvas checkSize;

    private float x;
    private float y;
    private float objSizeX;
    private float objSizeY;

    void Start()
    {
        x = checkSize.GetComponent<RectTransform>().rect.width * checkSize.GetComponent<RectTransform>().localScale.x;
        y = checkSize.GetComponent<RectTransform>().rect.height * checkSize.GetComponent<RectTransform>().localScale.y;
    }

    void Update()
    {
        foreach (Transform temp in rootObject.GetComponentsInChildren<Transform>(true))
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
                if (temp.gameObject.GetComponent<Rigidbody2D>())
                    Check(temp, 0, 0);
                else
                    Check(temp, x / 4, y / 4);
            }
        }
    }

    private void Check(Transform temp, float deviationX, float deviationY)
    {
        if (temp.position.x + objSizeX + x + deviationX > gameObject.transform.position.x
        && temp.position.x - objSizeX - x - deviationX < gameObject.transform.position.x)
        {
            if (temp.position.y + objSizeY + y + deviationY > gameObject.transform.position.y
                && temp.position.y - objSizeY - y - deviationY < gameObject.transform.position.y)
                temp.gameObject.SetActive(true);
            else
                temp.gameObject.SetActive(false);
        }
        else
            temp.gameObject.SetActive(false);
    }
}