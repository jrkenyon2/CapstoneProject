using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    //The asset I was using originally has animations attached which would have been activated using this script
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Move and fire are two different methods to more easily control which one is prioritized
         * This asset was not built to aim and run at the same which is why overriding is necessary */
        if (Input.GetMouseButton(0)) { PlayShoot(); }
        else { PlayMove(); }
    }

    private void PlayMove()
    {
        if (Input.GetAxis("Vertical") == 1) { animator.Play("Run"); } 
        else if (Input.GetAxis("Vertical") == -1) { animator.Play("RunBack"); }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 1) { animator.Play("RunStrafeRight"); }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == -1) { animator.Play("RunStrafeLeft"); }
        else { animator.Play("RifleAim"); }
    }

    private void PlayShoot()
    {
        animator.Play("RifleFire");
    }
}
