using UnityEngine;
using System.Collections;
using RTS;

public class UserInput : MonoBehaviour {

    private Player player;

    // Use this for initialization
    void Start () {
        player = transform.root.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {

        if(player.human) {
            MoveCamera();
            RotateCamera();
        }

    }

    private void MoveCamera(){
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0,0,0);

        // Horizontal camera movement in the x axis
        if(xpos >= 0 && xpos < ResourceManager.ScrollWidth){
            movement.x -= ResourceManager.ScrollSpeed;
        } else if(xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) {
            movement.x += ResourceManager.ScrollSpeed;
        }

        // Vertical camera movement in the y axis
        if(ypos >= 0 && ypos < ResourceManager.ScrollWidth) {
            movement.z -= ResourceManager.ScrollSpeed;
        } else if(ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) {
            movement.z += ResourceManager.ScrollSpeed;
        }

        // make sure movement is in the direction the camera is pointing
        // but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.mainCamera.transform.TransformDirection(movement);
        movement.y = 0;

        // Away from ground movement
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        // Calculate desired camera position based on received input
        Vector3 origin = Camera.mainCamera.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        // Limit distance from ground to be between minimum and maximum distance
        if(destination.y > ResourceManager.MaxCameraHeight) {
            destination.y = ResourceManager.MaxCameraHeight;
        } else if(destination.y < ResourceManager.MinCameraHeight) {
            destination.y = ResourceManager.MinCameraHeight;
        }

        // Check if the position has changed, if so, move towards
        if(destination != origin) {
            Camera.mainCamera.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void RotateCamera(){
        Vector3 origin = Camera.mainCamera.transform.eulerAngles;
        Vector3 destination = origin;

        // Detect rotation amount if ALT is being and the Right mouse button is down
        if((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)) {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }

        // Check if the rotation has changed, if so, rotate
        if(destination != origin) {
            Camera.mainCamera.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }
}
