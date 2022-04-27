using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorPortal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animatorPortal.SetBool("portalStart", true);
    }
}
