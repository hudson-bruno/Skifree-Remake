using UnityEngine;

public class SkierBehaviour : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public Vector2 minMaxSideSpeed;
    public float frequency;
    public float targetSpeed;
    public float acceleration;
    public float initialSpeed;

    private void Start()
    {
        Vector3 raycastOrigin = new Vector3(0, GetComponent<CapsuleCollider>().bounds.min.y, 0);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit))
        {
            Vector3 direction = Quaternion.AngleAxis(90, Vector3.right) * hit.normal;
            rb.velocity = direction * initialSpeed;
            Debug.Log(rb.velocity);
        }
    }

    void Update()
    {
        float smoothRandomSpeed = lerp(
            minMaxSideSpeed.x,
            minMaxSideSpeed.y,
            Mathf.PerlinNoise(transform.position.x * frequency, transform.position.y * frequency)
        );

        rb.velocity = new Vector3(smoothRandomSpeed, rb.velocity.y, rb.velocity.z);
        if (rb.velocity.magnitude < targetSpeed)
        {
            rb.velocity += rb.velocity.normalized * acceleration * Time.deltaTime;
        }
    }

    float lerp(float a, float b, float f) 
    {
        return (a * (1.0f - f)) + (b * f);
    }
}
