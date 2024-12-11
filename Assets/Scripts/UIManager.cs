using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


/*
 Exposed Functions: 
 
UpdateHitCount()
UpdateTotalCount()
UpdateTimingMeter(float ballSpeed);
FlashBallMissedMessage()

 */

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }

            return _instance;
        }
    }

    public Image[] fillers;
    public TMP_Text HitBallsCountText;
    public TMP_Text TotalBallsCountText;
    public TMP_Text BallMissedMessageText;
    //timing meter images
    public Image TM_upperImage;
    public Image TM_lowerImage;
    public Image TM_fillerImage;
    public Image GameCompletePanel;
    public GameObject Retry;
    public GameObject Congratulate;
    public Gradient fillColorGradient;

    GameObject bowler;
    


    private float flashMessageDuration = 2.0f;
    private float flashMessageMaxFontSize = 36.0f;
    private float fontSizeSpeed = 20.0f;
    public float scaleSpeed = 2f;





    public void Awake()
    {
        _instance = this;
    }

    //should be called when player hits ball successfully...  UIManager.Instance.updateHitCount()
    public void UpdateHitCount()
    {
        HitBallsCountText.text = "" + GameManager.Instance.BallsHit;

        if (GameManager.Instance.BallsHit + GameManager.Instance.BallsMissed == GameManager.Instance.MaxBallsToRoll)
        {
            ShowGameCompletePanel();
        }
    }

    public void UpdateMissCount()
    {
        if (GameManager.Instance.BallsHit + GameManager.Instance.BallsMissed == GameManager.Instance.MaxBallsToRoll)
        {
            ShowGameCompletePanel();
        }
    }

    //should be called when a ball is played...  UIManager.Instance.updateTotalCount();
    public void UpdateTotalCount()
    {
        TotalBallsCountText.text = "/ " + GameManager.Instance.TotalBallsRolled;
    }


    //this will update the timingMeter thingy... should be called whenever a new ball is instantiated
    public void UpdateTimingMeter(float ballVelocity)
    {
        //no idea right now
        //assuming a few fixed parameters for now... they should be defined somewhere as constants.. or a way to access them instead of hardcoding here
        float batLength = 1.0f;
        float pitchLenght = 6.37f;
        float timeToTravelPitch = (pitchLenght / ballVelocity)+(batLength/ballVelocity);   

        StartCoroutine(FillTimingMeterOverTime(timeToTravelPitch));
    }

    public void RestartGame()
    {
        bowler = GameObject.Find("Bowler");
       

        Debug.Log("restarting game");
        GameManager.Instance.BallsHit = 0;
        GameManager.Instance.BallsMissed = 0;
        GameManager.Instance.TotalBallsRolled = 0;
        UpdateHitCount();
        UpdateTotalCount();
        GameCompletePanel.gameObject.SetActive(false);

        bowler.GetComponent<Bowler>().StartGame();

    }

    //private IEnumerator FillTimingMeterOverTime(float timeToFill)
    //{
    //    float elapsedTime = 0f;
    //    while(elapsedTime < timeToFill)
    //    {
    //        TM_fillerImage.fillAmount = (elapsedTime / timeToFill);
    //        TM_fillerImage.color = fillColorGradient.Evaluate(elapsedTime / timeToFill);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    TM_fillerImage.fillAmount = 0f;

    //}

    private IEnumerator FillTimingMeterOverTime(float timeToFill)
    {
        float elapsedTime = 0f;
        int totalFillers = fillers.Length;

        while (elapsedTime < timeToFill)
        {
            // Calculate progress as a fraction of total time
            float progress = elapsedTime / timeToFill;

            // Determine how many fillers to activate
            int fillersToActivate = Mathf.FloorToInt(progress * totalFillers);

            // Enable appropriate fillers
            for (int i = 0; i < totalFillers; i++)
            {
                fillers[i].enabled = i < fillersToActivate;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure all fillers are enabled at the end
        foreach (Image filler in fillers)
        {
            filler.enabled = false;
        }
    }



public void FlashBallMissedMessage()
    {
        StartCoroutine(ShowMessageCoroutine());
    }

    public void FlashBallsHitEncouragementMessage()
    {

    }

    private void ShowGameCompletePanel()
    {
        GameCompletePanel.gameObject.SetActive(true);

        if(GameManager.Instance.BallsHit < 4)
        {
            Retry.gameObject.SetActive(true);
        }
        else
        {
            Congratulate.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowMessageCoroutine()
    {
        // Show the message
        BallMissedMessageText.gameObject.SetActive(true);

        yield return StartCoroutine(AnimateTextScale(0f, 1f));

        // Wait for the specified duration
        yield return new WaitForSeconds(flashMessageDuration);

        // Animate the text scale from 1 to 0 (shrink)
        yield return StartCoroutine(AnimateTextScale(1f, 0f));

        // Hide the message at the end
        BallMissedMessageText.gameObject.SetActive(false);
    }

    private IEnumerator AnimateTextScale(float startScale, float endScale)
    {
        float elapsedTime = 0f;

        // Set the initial scale
        BallMissedMessageText.transform.localScale = Vector3.one * startScale;

        // Animate scale from start to end
        while (elapsedTime < 1f)
        {
            // Interpolate the scale between start and end
            float scale = Mathf.Lerp(startScale, endScale, elapsedTime);
            BallMissedMessageText.transform.localScale = Vector3.one * scale;

            // Increment elapsed time based on the scale speed
            elapsedTime += Time.deltaTime * scaleSpeed;

            yield return null;  // Wait until next frame
        }

        // Ensure the final scale is set correctly (either 1 or 0)
        BallMissedMessageText.transform.localScale = Vector3.one * endScale;
    }
}
