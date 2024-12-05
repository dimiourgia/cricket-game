using System.Collections;
using UnityEngine;

public class Batsmen : MonoBehaviour
{
    [SerializeField]
    private Animator hitanim;
    void Start()
    {
        hitanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Batsmenshot());
        }
    }
    IEnumerator Batsmenshot()
    {
        hitanim.SetBool("Shot", true);
        yield return new WaitForSeconds(1.0f);
        hitanim.SetBool("Shot", false);
    }
}
