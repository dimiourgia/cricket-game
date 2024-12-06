using UnityEngine;

public class BallThrow : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPoint;
    private bool movingToPointB = true;
    private bool isMoving = false;

    public bool isHit = false;
    public float ballSpeed = 10f;
    public float speedIncrement = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetBall();
    }

    void Update()
    {
        if (GameManager.Instance.CanRoll())
        {
            if (!isMoving)
            {
                Debug.Log($"Ball started moving towards {(movingToPointB ? "Point B" : "Point A")}");
                isMoving = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, ballSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                Debug.Log($"Ball reached target: {(movingToPointB ? "Point B" : "Point A")}. isHit = {isHit}");
                isMoving = false;

                if (movingToPointB)
                {
                    HandlePointB();
                }
                else
                {
                    HandlePointA();
                }
            }
        }
    }

    private void ResetBall()
    {
        transform.position = GameManager.Instance.pointA.position;
        targetPoint = GameManager.Instance.pointB.position;
        movingToPointB = true;
        isMoving = false;
        Debug.Log("Ball reset to Point A.");
    }

    private void HandlePointB()
    {
        if (isHit)
        {
            Debug.Log("Hit confirmed at Point B. Incrementing ballsHit and rolling back to Point A.");
            GameManager.Instance.BallHit();
            RollBackToPointA();
        }
        else
        {
            Debug.Log("Missed at Point B. Incrementing ballsPlayed and respawning ball.");
            GameManager.Instance.BallRolled(); // Increment roll count as the ball is missed
            StopMovement();
            VanishBall();
        }
    }

    private void HandlePointA()
    {
        if (isHit)
        {
            Debug.Log("Hit confirmed at Point A. Respawning ball.");
            StopMovement();
            VanishBall();
        }
        else
        {
            Debug.Log("Missed at Point A. Incrementing roll count and rolling to Point B.");
            IncrementRollCount();
            RollToPointB();
        }
    }

    private void RollBackToPointA()
    {
        targetPoint = GameManager.Instance.pointA.position;
        movingToPointB = false;
        Debug.Log("Ball is now moving back to Point A.");
    }

    private void RollToPointB()
    {
        targetPoint = GameManager.Instance.pointB.position;
        movingToPointB = true;
        Debug.Log("Ball is now moving to Point B.");
    }

    private void IncrementRollCount()
    {
        GameManager.Instance.BallRolled();

        if (GameManager.Instance.GetRollCount() % 3 == 0)
        {
            ballSpeed += speedIncrement;
            Debug.Log($"Ball speed increased to: {ballSpeed}");
        }
    }

    private void StopMovement()
    {
        targetPoint = transform.position;
        Debug.Log("Ball movement stopped.");
    }

    public void VanishBall()
    {
        Debug.Log("Ball vanishing...");
        gameObject.SetActive(false);

        GameManager.Instance.BallRolled();

        if (GameManager.Instance.CanRoll())
        {
            Invoke(nameof(RespawnBall), 1f);
        }
        else
        {
            Debug.Log("Maximum rolls reached. No respawn.");
        }
    }

    private void RespawnBall()
    {
        Debug.Log("Respawning ball...");
        gameObject.SetActive(true);
        ResetBall();
    }
}
