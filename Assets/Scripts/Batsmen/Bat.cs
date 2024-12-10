

using UnityEngine;


public class Bat : MonoBehaviour
{

    public void OnCollisionEnter(Collision other)
    {

        Debug.Log("collision detected with " + other.gameObject.name);
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Bat hit the ball!");

        
            Rigidbody ballRb = other.gameObject.GetComponent<Rigidbody>();


            if (ballRb != null)
            {
                Debug.Log("Found the Rigidbody");

                // Define an angle for the hit
                float angle = 90f; // Change to desired angle in degrees
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up); // Rotate around the Y-axis
                Vector3 hitDirection = rotation * transform.forward; // Apply rotation to forward direction

                float hitForce = 4f;
                ballRb.AddForce(hitDirection.normalized * hitForce, ForceMode.Impulse);
            }


            BallThrow ballThrow = other.gameObject.transform.parent.GetComponent<BallThrow>();
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
