using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGiver : MonoBehaviour {

    public void giveSpeedBoost(GameObject receiver)
    {
        receiver.GetComponent<VelocityController>().giveSpeedBoost();
    }

    public void giveSpeedBoost(string receiver)
    {
        GameObject.Find(receiver).GetComponent<VelocityController>().giveSpeedBoost();
    }

    public void giveJumpBoost(GameObject receiver)
    {
        receiver.GetComponent<VelocityController>().giveJumpBoost();
    }

    public void giveJumpBoost(string receiver)
    {
        GameObject.Find(receiver).GetComponent<VelocityController>().giveJumpBoost();
    }

    public bool hasSpeedBoost(GameObject receiver)
    {
        return receiver.GetComponent<VelocityController>().hasSpeedBoost();
    }

    public bool hasSpeedBoost(string receiver)
    {
        return GameObject.Find(receiver).GetComponent<VelocityController>().hasSpeedBoost();
    }

    public bool hasJumpBoost(GameObject receiver)
    {
        return receiver.GetComponent<VelocityController>().hasJumpBoost();
    }

    public bool hasJumpBoost(string receiver)
    {
        return GameObject.Find(receiver).GetComponent<VelocityController>().hasJumpBoost();
    }
}