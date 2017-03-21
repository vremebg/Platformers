using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour {

    private class numToShow
    {
        public float deviationX;
        public float angle;
        public float number;
        public float initialX;
        public float initialY;
        public List<GameObject> sprites = new List<GameObject>();
    }

    [SerializeField]
    private float deviationXMax = 0.2f;

    [SerializeField]
    private float deviationY = 0.3f;

    [SerializeField]
    private float speedInSeconds = 5f;

    [SerializeField]
    private float spacingX = 0.4f;

    [SerializeField]
    private GameObject gameObj;

    [SerializeField]
    private float lifetimeSeconds = 0.5f;

    private List<numToShow> nums = new List<numToShow>();
    private List<numToShow> itemsToRemove = new List<numToShow>();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (nums.Count != 0)
        {
            foreach (numToShow temp in nums)
            {
                temp.angle += speedInSeconds * Time.deltaTime;
                if (temp.angle > 0)
                {
                    foreach (GameObject tempObj in temp.sprites)
                    {
                        Destroy(tempObj);
                    }
                    itemsToRemove.Add(temp);
                }
                else
                {
                    int count = 0;
                    foreach (GameObject tempObj in temp.sprites)
                    {
                        if (tempObj != null)
                        tempObj.transform.position = new Vector2(temp.initialX - count * spacingX  + (temp.deviationX * Mathf.Cos(temp.angle)),
                                temp.initialY + (deviationY * Mathf.Sin(temp.angle)));
                        count++;
                    }
                }
            }
            if (itemsToRemove.Count != 0)
            {
                foreach (numToShow temp in itemsToRemove)
                {
                    nums.Remove(temp);
                }
                itemsToRemove.Clear();
            }
        }
	}

    public void AddNumberToDisplay(int number)
    {
        bool negative = (number < 0);
        if (number == 0) number = 1;
        number = Mathf.Abs(number);
        numToShow temp = new numToShow();
        temp.deviationX = Random.Range(0, deviationXMax);
        temp.angle = -90*Mathf.PI/180;
        temp.number = number;
        int count = 0;
        while (number > 0)
        {
            int module = number % 10;
            number /= 10;
            GameObject sprGameObj = new GameObject();
            sprGameObj.name = "damageNumber";
            sprGameObj.AddComponent<SpriteRenderer>();
            SpriteRenderer sprRenderer = new SpriteRenderer();
            sprRenderer = sprGameObj.GetComponent<SpriteRenderer>();
            sprRenderer.sprite = Resources.LoadAll<Sprite>("Sprites/numbers")[module];
            if (negative)
                sprRenderer.color = new Color(0.6f, 0, 0, 1);
            else
                sprRenderer.color = new Color(0, 0.6f, 0, 1);
            sprRenderer.sortingLayerName = "Orb_Mines";
            sprGameObj.transform.position = new Vector2(gameObj.transform.position.x - count * spacingX, gameObj.GetComponent<Collider2D>().bounds.max.y + 1f);
            temp.initialX = sprGameObj.transform.position.x;
            temp.initialY = sprGameObj.transform.position.y;
            temp.sprites.Add(sprGameObj);
            Destroy(sprGameObj, lifetimeSeconds);
            count++;
        }
        nums.Add(temp);
    }
}
