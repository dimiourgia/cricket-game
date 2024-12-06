using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is null");
            }

            return _instance;
        }
    }

    public int ballsPlayed { get; set; }
    public int ballsHit { get; set; }

    public void Awake()
    {
        _instance = this;
        ballsPlayed = 0;
        ballsHit = 0;
        UIManager.Instance.updateHitCount();
        UIManager.Instance.updateTotalCount();
        UIManager.Instance.updateTimingMeter(5.0f);
    }
}
