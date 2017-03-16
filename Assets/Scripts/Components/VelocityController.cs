using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityController : MonoBehaviour {

    [SerializeField]
    float jumpVelocity = 380;

    [SerializeField]
    float maxWalkVelocityPerSecond = 4.5f;

    [SerializeField]
    float xForce = 25;

    [SerializeField]
    float speedBoostXTimes = 2;

    [SerializeField]
    int speedBoostSeconds = 10;

    [SerializeField]
    float JumpBoostXTimes = 1.5f;

    [SerializeField]
    int jumpBoostSeconds = 10;

    [SerializeField]
    GameObject hudBoostSpeedTime;

    [SerializeField]
    GameObject hudBoostSpeedIcon;


    [SerializeField]
    GameObject hudBoostJumpTime;

    [SerializeField]
    GameObject hudBoostJumpIcon;


    bool speedBoost = false;
    bool jumpBoost = false;

    float startTimeJump;
    float startTimeSpeed;

    void Update()
    {
        if (speedBoost)
            if (Time.time - startTimeSpeed > speedBoostSeconds)
                stopSpeedBoost();
            else
                showHudSpeed();
        if (jumpBoost)
            if (Time.time - startTimeJump > jumpBoostSeconds)
                stopJumpBoost();
            else
                showHudJump();
    }

    void showHudJump()
    {
        hudBoostJumpTime.GetComponent<Text>().text = ((int)(jumpBoostSeconds - (Time.time - startTimeJump))).ToString();
    }

    void showHudSpeed()
    {
        hudBoostSpeedTime.GetComponent<Text>().text = ((int)(speedBoostSeconds - (Time.time - startTimeSpeed))).ToString();
    }

    void stopJumpBoost()
    {
        jumpBoost = false;
        hudBoostJumpIcon.SetActive(false);
        hudBoostJumpTime.SetActive(false);
    }

    void stopSpeedBoost()
    {
        speedBoost = false;
        hudBoostSpeedIcon.SetActive(false);
        hudBoostSpeedTime.SetActive(false);
    }

    public float getMaxWalkVelocityPerSecond()
    {
        if (speedBoost)
            return maxWalkVelocityPerSecond * speedBoostXTimes;
        else
            return maxWalkVelocityPerSecond;
    }

    public float getXForce()
    {
        return xForce;
    }

    public float getJumpVelocity()
    {
        if (jumpBoost)
            return jumpVelocity * JumpBoostXTimes;
        else
            return jumpVelocity;
    }

    public void giveSpeedBoost()
    {
        startTimeSpeed = Time.time;
        speedBoost = true;
        hudBoostSpeedIcon.SetActive(true);
        hudBoostSpeedTime.SetActive(true);
    }

    public void giveJumpBoost()
    {
        startTimeJump = Time.time;
        jumpBoost = true;
        hudBoostJumpIcon.SetActive(true);
        hudBoostJumpTime.SetActive(true);
    }

    public bool hasSpeedBoost()
    {
        return speedBoost;
    }

    public bool hasJumpBoost()
    {
        return jumpBoost;
    }
}
