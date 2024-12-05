using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private UIManager _instance;
    public UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is null");
            }

            return _instance;
        }
    }

    public TMP_Text HitBallsCountText;
    public TMP_Text totalBallsCountText;


    public void Awake()
    {
        _instance = this;
    }

    //should be called when player hits ball successfully
    public void updateHitCount(int count)
    {
        HitBallsCountText.text = "" + count;
    }

    //should be called when a ball is played
    public void updateTotalCount(int count)
    {
        totalBallsCountText.text = " / " + count;
    }


    //this will update the timingMeter thingy... should be called whenever a new ball is instantiated
    public void updateTimingMeter(int ballVelocity)
    {
        //no idea right now
    }

}
