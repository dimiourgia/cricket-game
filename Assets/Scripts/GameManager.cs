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

    private Bowler bowler;

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
    }

    private void Start()
    {
        bowler = FindObjectOfType<Bowler>();
        if (bowler == null)
        {
            Debug.LogError("Bowler is not found in the scene.");
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.updateHitCount(bowler.ballsHit); // Fetch ballsHit from Bowler
            UIManager.Instance.updateTotalCount(bowler.ballsPlayed); // Fetch ballsPlayed from Bowler
        }
        else
        {
            Debug.LogError("UIManager.Instance is null.");
        }
    }

    public void NotifyBallRolled()
    {
        bowler.BallRolled(); // Inform Bowler that a ball was rolled
        UpdateUI(); // Update the UI after the ball roll
    }

    public void NotifyBallHit()
    {
        bowler.BallHit(); // Inform Bowler that a ball was hit
        UpdateUI(); // Update the UI after the ball hit
    }
}
