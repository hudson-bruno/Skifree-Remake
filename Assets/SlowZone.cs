using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float targetSlowSpeed;
    public float deceleration;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null && rb.velocity.magnitude < targetSlowSpeed)
        {
            return;
        }

        Vector3 targetVelocity = rb.velocity.normalized * targetSlowSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, deceleration * Time.deltaTime);
    }
}
