using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Batsmen : MonoBehaviour
{
    [SerializeField]
    private Animator hitanim;   
    private Collider batcollider;
    private bool isSwing = false;
    public GameObject Bat;
    public float MoveSpeed = 5f;

    void Start()
    {
        hitanim = GetComponent<Animator>();

        batcollider = Bat.GetComponent<Collider>();
        Debug.Log("Collider found: " + batcollider);

        if (batcollider == null)
        {
            Debug.LogError("Collider is missing on the bat!");
        }
    }


    void Update()
    {
     

        if (Input.GetKeyDown(KeyCode.Space) && !isSwing)
        {
            StartCoroutine(Batsmenshot());
        }

        //float horizontalInput = Input.GetAxis("Horizontal");
        //Vector3 movement = new Vector3(0, 0, horizontalInput) * MoveSpeed * Time.deltaTime;
        //transform.Translate(movement);
    }

    IEnumerator Batsmenshot()
    {
        isSwing = true;
        hitanim.SetBool("Shot", true);


        // Critical period where the bat can hit the ball
        yield return new WaitForSeconds(0.5f);
        hitanim.SetBool("Shot", false);
        isSwing = false;
    }
}
