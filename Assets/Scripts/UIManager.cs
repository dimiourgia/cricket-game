//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


///*
// Exposed Functions: 

// */

//public class UIManager : MonoBehaviour
//{
//    private static UIManager _instance;

//    public static UIManager Instance
//    {
//        get
//        {
//            if(_instance == null)
//            {
//                Debug.LogError("UIManager is null");
//            }

//            return _instance;
//        }
//    }

//    public TMP_Text HitBallsCountText;
//    public TMP_Text TotalBallsCountText;
//    //timing meter images
//    public Image TM_upperImage;
//    public Image TM_lowerImage;
//    public Image TM_fillerImage;


//    public void Awake()
//    {
//        _instance = this;
//    }

//    //should be called when player hits ball successfully...  UIManager.Instance.updateHitCount()
//    public void updateHitCount()
//    {
//        HitBallsCountText.text = "" + GameManager.Instance.ballsHit;
//    }

//    //should be called when a ball is played...  UIManager.Instance.updateTotalCount();
//    public void updateTotalCount()
//    {
//        TotalBallsCountText.text = "/ " + GameManager.Instance.ballsPlayed;
//    }


//    //this will update the timingMeter thingy... should be called whenever a new ball is instantiated
//    public void updateTimingMeter(float ballVelocity)
//    {
//        //no idea right now
//        //assuming a few fixed parameters for now... they should be defined somewhere as constants.. or a way to access them instead of hardcoding here
//        float batLength = 1.0f;
//        float pitchLenght = 5.0f;

//        TM_fillerImage.fillAmount = 0.4f;


//    }

//}
