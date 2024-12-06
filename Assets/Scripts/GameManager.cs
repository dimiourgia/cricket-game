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

    public int TotalBallsRolled { get; set; }
    public int BallsHit { get; set; }
    public int BallsMissed { get; set; }
    public int MaxBallsToRoll = 10;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            TotalBallsRolled = 0;
            BallsHit = 0;
            BallsMissed = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

