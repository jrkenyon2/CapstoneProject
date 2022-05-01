using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;

    public GameObject player;
    private Vector3 playerOffSet;

    //copied/edited code

    //Me: Serializefield allows Unity to create a visual
    //reference to the variable in the UI
    //but does not allow it to be edited
    [Range(0f, 180f)] [SerializeField] private float yRotationLimit;

    Vector2 rotation = Vector2.zero; //Me: .zero initializes all Vector2 values to 0
    //end copied code

    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 100f;
        yRotationLimit = 75f;

        playerOffSet = new Vector3(0f, 0f, 0f);

        //Me: hides the cursor and locks it to the application window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Me: follow player
        transform.position = player.transform.position + playerOffSet;

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        //copied/edited code
        rotation.x += Input.GetAxis("Mouse X") * Time.deltaTime; //Me: No sensitivty allows the player and camera to synchronize rotation
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //Me: clamp returns a value within a specified range
        //stops rotation beyond the specified range.
        //in this case it prevents the y camera rotation from extending beyond
        //the expected behavior of the head and eyes in real life
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit); 

        //Me: Quaternions are a Unity specific solution to preventing gimbal lock
        //Gimbal lock is when the traditional euler rotations behave unexpectedly when calculating small rotations
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        /*OP: Quaternions seem to rotate more consistently than EulerAngles. 
         Sensitivity seemed to change slightly at certain degrees using Euler. 
         transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0); */
        transform.localRotation = xQuat * yQuat;
        //end copied code
    }
}