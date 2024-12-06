using UnityEngine;

public class Bowler : MonoBehaviour
{
    public int ballsPlayed { get; private set; }
    public int ballsHit { get; private set; }

    private int rollCount = 0;
    private readonly int maxRolls = 10;

    public Transform pointA;
    public Transform pointB;

    private void Start()
    {
        ballsPlayed = 0;
        ballsHit = 0;
    }

    public bool CanRoll()
    {
        return rollCount < maxRolls;
    }

    public void BallRolled()
    {
        if (!CanRoll())
        {
            Debug.Log("Maximum rolls reached. Cannot roll further.");
            return;
        }

        rollCount++;
        ballsPlayed++;
        Debug.Log($"Ball rolled. Current roll count: {rollCount}, Balls Played: {ballsPlayed}");
    }

    public void BallHit()
    {
        ballsHit++;
        Debug.Log($"Ball hit confirmed. Total Hits: {ballsHit}");
    }

    public int GetRollCount()
    {
        return rollCount;
    }
}
