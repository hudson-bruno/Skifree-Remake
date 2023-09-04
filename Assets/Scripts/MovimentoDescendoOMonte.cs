using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public float speedHorizontal;
    public float jumpForce = 100;
    public Transform orientantion;
   

    private bool isGrounded;

    Vector3 point1, point2, point3, normal;

    void Start()
    {

    }
    void FixedUpdate()
    {
        normalAngle();
        MoveLeftRight();
        this.gameObject.GetComponent<Rigidbody>().AddForce(orientantion.forward * speed);

        Jump();



    }
    void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * speedHorizontal);
        }
        else if (Input.GetKey(KeyCode.D))
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * speedHorizontal);

    }

    void Jump()
    {
        float RayDistance = (transform.lossyScale.y) + 0.5f;

        Vector3 rayDown = transform.TransformDirection(-normal);

        if (Physics.Raycast(transform.position, rayDown, RayDistance))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;

        }

        if (isGrounded && ((Input.GetKeyDown(KeyCode.Space))))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(normal * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");

        }

    }
    void normalAngle(){

        RaycastHit hit;

        Vector3 ray1 = (transform.TransformDirection(new Vector3(-0.3f,-0.5f,0.5f)).normalized);
        Vector3 ray2 = (transform.TransformDirection(new Vector3(0.3f, -0.5f, 0.5f)).normalized);
        Vector3 ray3 = transform.TransformDirection(new Vector3(0, -1, 0));

        if (Physics.Raycast(transform.position, ray1, out hit))
        {
            point1 = hit.point;
        }

        if (Physics.Raycast(transform.position, ray2, out hit))
        {
            point2 = hit.point;
    
        }

        if (Physics.Raycast(transform.position, ray3, out hit))
        {
            point3 = hit.point;
        }
 
        normal = Vector3.Cross(point1 - point3, point2 - point3);
        Debug.DrawRay(transform.position, normal, Color.green);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
    }
  

}
