using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    [SerializeField]
    private float speedAroundThePathPerSecond = 1.5f;

    [SerializeField]
    private GameObject platform;

    [SerializeField]
    private GameObject[] points;

    private int currentPoint = 0;
    private int nextPoint = 1;

    public float dx = 0;
    public float dy = 0;

    private float x;
    private float y;
    private float predictedX;
    private float predictedY;
    private float timedSpeedAroundThePath;

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

    private void CalcCurrentAndNextPoint()
    {
        currentPoint++;
        if (currentPoint >= points.Length)
            currentPoint = 0;
        nextPoint = currentPoint + 1;
        if (currentPoint + 1 >= points.Length)
            nextPoint = 0;
    }

    private void CalcPredictedXY()
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
            CalcCurrentAndNextPoint();
        else
        {
            if (temp.x > 0 && predictedX > points[nextPoint].transform.position.x)
                PredictToNextPointAndCalculate();
            if (temp.x < 0 && predictedX < points[nextPoint].transform.position.x)
                PredictToNextPointAndCalculate();
            if (temp.x == 0)
            {
                if (temp.y > 0 && predictedY > points[nextPoint].transform.position.y)
                    PredictToNextPointAndCalculate();
                if (temp.y < 0 && predictedY < points[nextPoint].transform.position.y)
                    PredictToNextPointAndCalculate();
            }
        }

        if (temp.x == 0 && temp.y == 0)
        {
            CalcCurrentAndNextPoint();
        }

        x = predictedX;
        y = predictedY;

        dx = x - platform.transform.position.x;
        dy = y - platform.transform.position.y;
        platform.transform.position = new Vector2(x, y);
    }

    private void PredictToNextPointAndCalculate()
    {
        predictedX = points[nextPoint].transform.position.x;
        predictedY = points[nextPoint].transform.position.y;
        CalcCurrentAndNextPoint();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timedSpeedAroundThePath = speedAroundThePathPerSecond * Time.fixedDeltaTime;
        if (points.Length >= 2)
            CalcPredictedXY();
    }
}
