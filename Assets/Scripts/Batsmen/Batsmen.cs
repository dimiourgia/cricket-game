using System.Collections;
using System.Collections.Specialized;

using UnityEngine;

public class Batsmen : MonoBehaviour
{
    [SerializeField]
    private Animator hitanim;
    private GameObject Bat; // Reference to the bat GameObject (child of the player)
    private Collider batcollider;
    private bool isSwing = false;
    

    void Start()
    {
        hitanim = GetComponent<Animator>();

            batcollider = GetComponentInChildren<Collider>();
        Debug.Log("collider" + batcollider);


        if(Bat != null)
        {
            if (batcollider == null)
            {
                Debug.LogError("Collider is missing on the bat!");
            }
            else
            {
                Debug.LogError("GameObject Bat is not assign");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSwing)
        {
            StartCoroutine(Batsmenshot());
        }
    }
    IEnumerator Batsmenshot()
    {
        isSwing = true;
        hitanim.SetBool("Shot", true);

        // Enable the collider during the swing for impact detection
        if (batcollider != null)
        {
            batcollider.enabled = true;
        }

        yield return new WaitForSeconds(0.5f);  // Assuming 0.5 seconds is the critical contact period

        // Then disable again if needed after the swing phase
        if (batcollider != null)
        {
            batcollider.enabled = false;
        }

        hitanim.SetBool("Shot", false);
        isSwing = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("bat hit the ball");

            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log($"Hit direction: {transform.forward}");

            if (ballRb != null)
            {
                Vector3 hitDirection = transform.forward;
                float hitforce = 10f;
                ballRb.AddForce(hitDirection * hitforce, ForceMode.Impulse);
            }
        }
    }

}
