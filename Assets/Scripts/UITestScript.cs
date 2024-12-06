using UnityEngine;

public class UITestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.updateTimingMeter(2.0f);
        UIManager.Instance.flashBallMissedMessage();
        UIManager.Instance.updateTotalCount();
        UIManager.Instance.updateHitCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
