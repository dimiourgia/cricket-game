using UnityEngine;

public class UITestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.UpdateTimingMeter(1.0f);
        //UIManager.Instance.FlashBallMissedMessage();
        //UIManager.Instance.UpdateTotalCount();
        //UIManager.Instance.UpdateHitCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
