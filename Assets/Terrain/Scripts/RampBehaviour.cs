using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampBehaviour : MonoBehaviour
{
    public float jumpForce;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
