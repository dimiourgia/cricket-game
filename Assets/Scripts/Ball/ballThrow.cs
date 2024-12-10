using UnityEngine;

public class BallThrow : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPoint;
    private bool isHit = false;
    private Bowler bowler;
    private float ballSpeed;

    private Transform pointA;
    private Transform pointB;

    public void Initialize(Bowler bowler, Transform pointA, Transform pointB, float ballSpeed)
    {
        this.bowler = bowler;
        this.pointA = pointA;
        this.pointB = pointB;
        this.ballSpeed = ballSpeed;

        ResetBall();
    }



    private void Update()
    {
        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, ballSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                bowler.BallMissed();
            }
        }
    }

    public void BallHit()
    {
        if (!isHit)
        {
            isHit = true;
            Debug.Log("Ball hit!");
            bowler.BallHit();
        }
    }

    private void ResetBall()
    {
        transform.position = pointA.position;
        targetPoint = pointB.position;
        isHit = false;
        Debug.Log("Ball reset to Point A.");
    }
}
