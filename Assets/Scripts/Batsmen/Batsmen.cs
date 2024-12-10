using System.Collections;
using UnityEngine;

public class Batsmen : MonoBehaviour
{
    [SerializeField]
    private Animator hitanim;   
    private Collider batcollider;
    private bool isSwing = false;
    public GameObject Bat;

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
