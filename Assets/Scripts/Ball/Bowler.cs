using UnityEngine;
using System.Collections;

public class Bowler : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float ballSpeed = 1f; 
    public float speedIncrement = 2f; 

    private GameObject ballPrefab; 
    private GameObject currentBall;

    private void Start()
    {

        BallThrow ballThrow = GetComponentInChildren<BallThrow>(true); 
        if (ballThrow != null)
        {
            ballPrefab = ballThrow.gameObject;
        }

        if (ballPrefab == null)
        {
            Debug.LogError("Ball prefab (Mball) is not found as a child of the Bowler GameObject. Ensure the Ball prefab exists in the hierarchy.");
            return;
        }

        ballPrefab.SetActive(false); 
        SpawnBall();
    }

    public void StartGame()
    {
        if (CanRoll())
        {
            SpawnBall();
        }
        else
        {
            Debug.Log("Max Rolls Exceeded. Restarting the game...");
        }
    }



    public bool CanRoll()
    {
        return GameManager.Instance.TotalBallsRolled < GameManager.Instance.MaxBallsToRoll;
    }

    public void BallHit()
    {
        StartCoroutine(HandleBallHit());
    }

    //private IEnumerator HandleBallHit()
    //{
    //    Debug.Log("Ball hit! Waiting for 1 second before destroying...");
    //    yield return new WaitForSeconds(1f);

    //    if (currentBall != null)
    //    {
    //        Destroy(currentBall);
    //    }

    //    GameManager.Instance.BallsHit++;
    //    Debug.Log($"Balls Hit: {GameManager.Instance.BallsHit}");
    //    UIManager.Instance.UpdateHitCount();

    //    if (CanRoll())
    //    {
    //        if (GameManager.Instance.TotalBallsRolled % 3 == 0)
    //        {
    //            ballSpeed += speedIncrement;
    //            Debug.Log($"Ball speed increased to: {ballSpeed}");
    //        }

    //        Invoke(nameof(SpawnBall), 2f);
    //    }
    //    else
    //    {
    //        Debug.Log("Maximum rolls reached. No more balls.");
    //    }
    //}

    private IEnumerator HandleBallHit()
    {
        Debug.Log("Ball hit! Reversing ball direction...");
        AudioManager.Instance.PlayBallHitSound();

        if (currentBall != null)
        {
            if (currentBall.TryGetComponent<Rigidbody>(out var ballRigidbody))
            {
                Debug.Log("Velcoity change");
                Vector3 currentVelocity = ballRigidbody.linearVelocity;
                ballRigidbody.linearVelocity = -currentVelocity; 
            }
        }

        yield return new WaitForSeconds(1f); 

        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        GameManager.Instance.BallsHit++;
        Debug.Log($"Balls Hit: {GameManager.Instance.BallsHit}");
        UIManager.Instance.UpdateHitCount();

        if (CanRoll())
        {
            if (GameManager.Instance.TotalBallsRolled % 3 == 0)
            {
                ballSpeed += speedIncrement;
                Debug.Log($"Ball speed increased to: {ballSpeed}");
            }

            Invoke(nameof(SpawnBall), 2f);
        }
        else
        {
            Debug.Log("Maximum rolls reached. No more balls.");
        }
    }

    public void BallMissed()
    {
        Debug.Log("Ball missed!");

        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        GameManager.Instance.BallsMissed++;
        Debug.Log($"Balls Missed: {GameManager.Instance.BallsMissed}");
        UIManager.Instance.UpdateMissCount();
        UIManager.Instance.FlashBallMissedMessage();

        if (CanRoll())
        {
            
            if (GameManager.Instance.TotalBallsRolled % 3 == 0)
            {
                ballSpeed += speedIncrement;
                Debug.Log($"Ball speed increased to: {ballSpeed}");
            }

            Invoke(nameof(SpawnBall), 2f); 
        }
        else
        {
            Debug.Log("Maximum rolls reached. No more balls.");
        }
    }

    private void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Cannot spawn ball. Ball prefab is null.");
            return;
        }

        GameManager.Instance.TotalBallsRolled++;
        UIManager.Instance.UpdateTimingMeter(ballSpeed);
        UIManager.Instance.UpdateTotalCount();

        currentBall = Instantiate(ballPrefab, pointA.position, Quaternion.identity, transform);
        currentBall.SetActive(true);

        BallThrow ballThrow = currentBall.GetComponent<BallThrow>();

        if (ballThrow != null)
        {
            ballThrow.Initialize(this, pointA, pointB, ballSpeed);
            UIManager.Instance.UpdateTimingMeter(ballSpeed);
        }
        else
        {
            Debug.LogError("Spawned ball does not have a BallThrow script attached.");
        }
    }
}
