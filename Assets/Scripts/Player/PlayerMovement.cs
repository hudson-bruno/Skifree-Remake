using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public LayerMask groundLayer;
    public float maxBuildupSpeed;

    private Transform cameraTransform;
    private RaycastHit hit;
    private Vector3 normal;
     

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        cameraTransform = Camera.main.transform; 
    }

    private void Update()
    {
        CalculateNormal();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void CalculateNormal()
    {
        Debug.DrawRay(transform.position - transform.up, -transform.up * 3, Color.yellow);
        if (Physics.Raycast(transform.position - transform.up, -transform.up, out hit, 10f, groundLayer))
        {
            normal = hit.normal;
        }
        else
        {
            normal = Vector3.up;
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        Vector3 forwardDirection = Vector3.Cross(cameraTransform.right, normal);
        Vector3 rightDirection = cameraTransform.right;

        float forwardVelocity = Vector3.Dot(rb.velocity, forwardDirection);
        float rightVelocity = Vector3.Dot(rb.velocity, cameraTransform.right);
        if ((forwardVelocity > maxBuildupSpeed && movement.z > 0) || (forwardVelocity < -maxBuildupSpeed && movement.z < 0))
        {
            forwardDirection = Vector3.zero;
        }
        if ((rightVelocity > maxBuildupSpeed && movement.x > 0) || (rightVelocity < -maxBuildupSpeed && movement.x < 0))
        {
            rightDirection = Vector3.zero;
        }

        Vector3 result = (forwardDirection * movement.z) + (rightDirection * movement.x);
        rb.AddForce(result * speed * Time.deltaTime);

        Debug.DrawRay(transform.position, forwardDirection * 2, Color.blue);
        Debug.DrawRay(transform.position, rightDirection * 2, Color.red);
        Debug.DrawRay(transform.position, normal * 2, Color.green);
    }

    private void HandleJump()
    {
        if (hit.collider == null) return;

        bool isCloseToGround = hit.distance < .2f;
        bool isFallingToGround = Vector3.Dot(hit.normal, rb.velocity.normalized) <= 0;

        bool canJump = isCloseToGround && isFallingToGround;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}