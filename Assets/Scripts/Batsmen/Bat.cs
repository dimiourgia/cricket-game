using UnityEngine;

void Start()
{
    Debug.Log("Ready to detect collision");
}

public class Bat : MonoBehaviour
{
    private void OnCollistionEnter(Collision other)
    {

        Debug.Log("collision detected with " + other.gameObject.name);
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Bat hit the ball!");

            // Apply force to the ball
            Rigidbody ballRb = other.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                Vector3 hitDirection = transform.forward;
                float hitForce = 10f;
                ballRb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
            }


            BallThrow ballThrow = other.gameObject.GetComponent<BallThrow>();
            if (ballThrow != null)
            {
                ballThrow.BallHit();
            }
            else
            {
                Debug.Log("No BallThrow component found on the collided ball");
            }
        }
    }
}
