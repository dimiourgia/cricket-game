using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;

    public GameManager Instance {
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
    }
}
