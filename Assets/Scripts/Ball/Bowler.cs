using UnityEngine;

public class Bowler : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;

    public bool CanRoll()
    {
        return GameManager.Instance.TotalBallsRolled < GameManager.Instance.MaxBallsToRoll;
    }

    public void BallRolled()
    {
        if (!CanRoll())
        {
            Debug.Log("Maximum rolls reached. Cannot roll further.");
            return;
        }

        GameManager.Instance.TotalBallsRolled++;
        //UIManager.Instance.updateTimingMeter()
        //UIManager.Instance.updateTotalCount();
    }
}
