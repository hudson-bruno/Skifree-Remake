using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFirstPerson : MonoBehaviour
{
    public Transform CameraPosition;
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float XRotation;
    private float YRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovendoCameraComMouse();
    
        transform.position = CameraPosition.position;

    }

    public void MovendoCameraComMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        YRotation += mouseX;
        XRotation += mouseY;

        XRotation = Mathf.Clamp(XRotation, -90, 90f);
        transform.rotation = Quaternion.Euler(-XRotation, YRotation, 0);
        orientation.rotation = Quaternion.Euler(0, YRotation, 0);
    }
}
