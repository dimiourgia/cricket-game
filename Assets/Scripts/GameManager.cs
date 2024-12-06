//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    private static GameManager _instance;

//    public static GameManager Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                Debug.LogError("GameManager is null");
//            }

//            return _instance;
//        }
//    }

//    public int ballsPlayed { get; set; }
//    public int ballsHit { get; set; }

//    public void Awake()
//    {
//        _instance = this;
//        ballsPlayed = 0;
//        ballsHit = 0;
//        UIManager.Instance.updateHitCount();
//        UIManager.Instance.updateTotalCount();
//        UIManager.Instance.updateTimingMeter(5.0f);
//    }
//}


using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    private int rollCount = 0;
    private readonly int maxRolls = 10;

    public int ballsPlayed { get; private set; }
    public int ballsHit { get; private set; }

    public Transform pointA;
    public Transform pointB;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ballsPlayed = 0;
        ballsHit = 0;
        UIManager.Instance.updateHitCount();
        UIManager.Instance.updateTotalCount();
        UIManager.Instance.updateTimingMeter(5.0f);
    }

    public bool CanRoll()
    {
        return rollCount < maxRolls;
    }

    public void BallRolled()
    {
        rollCount++;
        ballsPlayed++;
        UIManager.Instance.updateTotalCount();
        Debug.Log($"Roll count incremented. Current roll count: {rollCount}");

        if (rollCount >= maxRolls)
        {
            Debug.Log("Game over. Maximum rolls reached.");
        }
    }

    public void BallHit()
    {
        ballsHit++;
        UIManager.Instance.updateHitCount();
        Debug.Log($"Ball hit count incremented. Total hits: {ballsHit}");
    }

    public int GetRollCount()
    {
        return rollCount;
    }
}
