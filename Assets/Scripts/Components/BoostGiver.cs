using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGiver : MonoBehaviour {

    public void GiveSpeedBoost(GameObject receiver)
    {
        receiver.GetComponent<VelocityController>().GiveSpeedBoost();
    }

    public void GiveSpeedBoost(string receiver)
    {
        GameObject.Find(receiver).GetComponent<VelocityController>().GiveSpeedBoost();
    }

    public void GiveJumpBoost(GameObject receiver)
    {
        receiver.GetComponent<VelocityController>().GiveJumpBoost();
    }

    public void GiveJumpBoost(string receiver)
    {
        GameObject.Find(receiver).GetComponent<VelocityController>().GiveJumpBoost();
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