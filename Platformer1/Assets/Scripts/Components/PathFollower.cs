using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    [SerializeField]
    float speedAroundThePathPerSecond = 1.5f;

    [SerializeField]
    public GameObject platform;

    [SerializeField]
    GameObject[] points;

    int currentPoint = 0;
    int nextPoint = 1;

    public float dx = 0;
    public float dy = 0;

    float x;
    float y;
    float predictedX;
    float predictedY;
    float timedSpeedAroundThePath;

    // Use this for initialization
    void Start () {
        if (points.Length != 0) platform.transform.position = points[0].transform.position;
        x = platform.transform.position.x;
        y = platform.transform.position.y;
	}

    private Vector2 directionVector()
    {
        return new Vector2(points[nextPoint].transform.position.x - x,
            points[nextPoint].transform.position.y - y);
    }

    private void calcCurrentAndNextPoint()
    {
        currentPoint++;
        if (currentPoint >= points.Length)
            currentPoint = 0;
        nextPoint = currentPoint + 1;
        if (currentPoint + 1 >= points.Length)
            nextPoint = 0;
    }

    private void calcPredictedXY()
    {
        predictedX = x;
        predictedY = y;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(points[nextPoint].transform.position.x - x, 2) + Mathf.Pow(points[nextPoint].transform.position.y - y, 2));
        if (hypotenuse != 0)
            predictedX = x + timedSpeedAroundThePath * (points[nextPoint].transform.position.x - x) / hypotenuse;
        if (hypotenuse != 0)
            predictedY = y + timedSpeedAroundThePath * (points[nextPoint].transform.position.y - y) / hypotenuse;

        Vector2 temp = directionVector();

        if (temp.x == 0)
            if (temp.y > 0)
                predictedY = y + timedSpeedAroundThePath;
            else if (temp.y < 0)
                predictedY = y - timedSpeedAroundThePath;

        if (temp.y == 0)
            if (temp.x > 0)
                predictedX = x + timedSpeedAroundThePath;
            else if (temp.x < 0)
                predictedX = x - timedSpeedAroundThePath;

        if (hypotenuse == 0 && temp.x != 0 && temp.y != 0)
            calcCurrentAndNextPoint();
        else
        {
            if (temp.x > 0 && predictedX > points[nextPoint].transform.position.x)
                predictToNextPointAndCalculate();
            if (temp.x < 0 && predictedX < points[nextPoint].transform.position.x)
                predictToNextPointAndCalculate();
            if (temp.x == 0)
            {
                if (temp.y > 0 && predictedY > points[nextPoint].transform.position.y)
                    predictToNextPointAndCalculate();
                if (temp.y < 0 && predictedY < points[nextPoint].transform.position.y)
                    predictToNextPointAndCalculate();
            }
        }

        if (temp.x == 0 && temp.y == 0)
        {
            calcCurrentAndNextPoint();
        }

        x = predictedX;
        y = predictedY;

        dx = x - platform.transform.position.x;
        dy = y - platform.transform.position.y;
        platform.transform.position = new Vector2(x, y);
    }

    private void predictToNextPointAndCalculate()
    {
        predictedX = points[nextPoint].transform.position.x;
        predictedY = points[nextPoint].transform.position.y;
        calcCurrentAndNextPoint();
    }
	
	// Update is called once per frame
	void Update () {
        timedSpeedAroundThePath = speedAroundThePathPerSecond * Time.deltaTime;
        if (points.Length >= 2)
            calcPredictedXY();
    }
}
