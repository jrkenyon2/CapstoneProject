using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    //This was a test script to check whether certain issues I was having were due to the script or the asset it was applied to.
    //I removed alot of the comments and condensed some of the code to see if I could detect errors in logic more easily
    private float horizInput, vertInput, jumpHeight, speed, mouseX;

    bool isGrounded;

    private Vector3 jumpVar;
    private Rigidbody mainRB;

    void Start()
    {
        jumpHeight = 4f;
        mainRB = GetComponent<Rigidbody>();
        jumpVar = new Vector3(0.0f, 2.0f, 0.0f);
        speed = 5f;
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        UpdatePlayerPos();
        Jump();

    }

    private void UpdatePlayerPos()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");

        Vector3 moved = new Vector3(horizInput, 0, vertInput);
        mainRB.MovePosition(transform.position + moved * Time.deltaTime * speed);
        mainRB.rotation = Quaternion.Euler(mainRB.rotation.eulerAngles + new Vector3(0f, mouseX, 0f));
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            mainRB.AddForce(jumpVar * jumpHeight, ForceMode.Impulse);
        }
    }
}
