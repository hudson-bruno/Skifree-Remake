using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float maxForwardWalkSpeed;

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        Transform directionTransfrom = Camera.main.transform;
        Vector3 result = (directionTransfrom.forward * movement.z) + (directionTransfrom.right * movement.x);
        rb.AddForce(result);
    }
}