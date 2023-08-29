using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float horizontalSpeed;

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3 (horizontalInput * horizontalSpeed, rb.velocity.y, rb.velocity.z);
    }
}
