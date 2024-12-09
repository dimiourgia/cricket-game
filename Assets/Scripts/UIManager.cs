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

    public TMP_Text HitBallsCountText;
    public TMP_Text TotalBallsCountText;
    public TMP_Text BallMissedMessageText;
    //timing meter images
    public Image TM_upperImage;
    public Image TM_lowerImage;
    public Image TM_fillerImage;


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

    private IEnumerator FillTimingMeterOverTime(float timeToFill)
    {
        float elapsedTime = 0f;
        while(elapsedTime < timeToFill)
        {
            TM_fillerImage.fillAmount = (elapsedTime / timeToFill);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        TM_fillerImage.fillAmount = 0f;
    }


    public void FlashBallMissedMessage()
    {
        StartCoroutine(ShowMessageCoroutine());
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
