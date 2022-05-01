using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizInput, zInput, speed, jumpForce, mouseX, bulletSpeed;
    bool isGrounded;

    private Vector3 jumpUp, moved, mouseRotate;
    private Rigidbody mainRB;
    private Camera mainCamera;
    Quaternion camRotateY;
    
    
    public GameObject bullets;

    // Start is called before the first frame update
    void Start()
    {

        speed = 15f;
        jumpForce = 4f;
        bulletSpeed = 700f;

        mainCamera = Camera.main;
        mainRB = GetComponent<Rigidbody>();
        jumpUp = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePlayerPos();
        Jump();
        Shoot();
    }

    private void UpdatePlayerPos()
    {
        //update position variables with current state of control/mouse inputs
        horizInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        moved = new Vector3(horizInput, 0, zInput);
        mouseX = Input.GetAxis("Mouse X");
        mouseRotate = new Vector3(0, mouseX, 0);

        //update with current camera rotation about the y axis
        camRotateY = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);

        //move player on the x and z axis (y axis would move into the sky)
        mainRB.position += camRotateY * moved * speed * Time.deltaTime;
        transform.Rotate(mouseRotate);
        //Time.deltaTime tracks the time between updates.
        //This eliminates discrepancies that occur when relying on framerate which occurs by default)

    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            //Apply vertical force. Rigidbody applies gravity to bring character back down.
            mainRB.AddForce(jumpUp * jumpForce, ForceMode.Impulse);
        }
    }

    private void Shoot(){
        if (Input.GetButtonDown("Fire1"))
        {
            //TransformPoint instantiates at a point in front of the object the script is to attached to (MainCharacter)
            //LookRotation defines the angle to shoot at, and the direction considered "Up" depending on the perspective of the camera (what will be objectively up)
            GameObject myBullet = Instantiate(bullets, transform.TransformPoint(Vector3.forward * 2), Quaternion.LookRotation(mainCamera.transform.forward, transform.up));
            
            //adds force relative to the object spawned along the z axis
            myBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletSpeed));
        }
    }
}
