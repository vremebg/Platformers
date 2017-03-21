using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityController : MonoBehaviour {

    [SerializeField]
    private float jumpVelocity = 380;

    [SerializeField]
    private float maxWalkVelocityPerSecond = 4.5f;

    [SerializeField]
    private float xForce = 25;

    [SerializeField]
    private float speedBoostXTimes = 2;

    [SerializeField]
    private int speedBoostSeconds = 10;

    [SerializeField]
    private float JumpBoostXTimes = 1.5f;

    [SerializeField]
    private int jumpBoostSeconds = 10;

    [SerializeField]
    private GameObject hudBoostSpeedTime;

    [SerializeField]
    private GameObject hudBoostSpeedIcon;


    [SerializeField]
    private GameObject hudBoostJumpTime;

    [SerializeField]
    private GameObject hudBoostJumpIcon;


    private bool speedBoost = false;
    private bool jumpBoost = false;

    private float startTimeJump;
    private float startTimeSpeed;

    void Update()
    {
        if (speedBoost)
            if (Time.time - startTimeSpeed > speedBoostSeconds)
                StopSpeedBoost();
            else
                ShowHudSpeed();
        if (jumpBoost)
            if (Time.time - startTimeJump > jumpBoostSeconds)
                StopJumpBoost();
            else
                ShowHudJump();
    }

    private void ShowHudJump()
    {
        hudBoostJumpTime.GetComponent<Text>().text = ((int)(jumpBoostSeconds - (Time.time - startTimeJump))).ToString();
    }

    private void ShowHudSpeed()
    {
        hudBoostSpeedTime.GetComponent<Text>().text = ((int)(speedBoostSeconds - (Time.time - startTimeSpeed))).ToString();
    }

    private void StopJumpBoost()
    {
        jumpBoost = false;
        hudBoostJumpIcon.SetActive(false);
        hudBoostJumpTime.SetActive(false);
    }

    private void StopSpeedBoost()
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

    public void GiveSpeedBoost()
    {
        startTimeSpeed = Time.time;
        speedBoost = true;
        hudBoostSpeedIcon.SetActive(true);
        hudBoostSpeedTime.SetActive(true);
    }

    public void GiveJumpBoost()
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
