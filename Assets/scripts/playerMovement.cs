using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public float fuerza;
    public bool grounded;
    public Transform cameraTransform; // <-- arrastra la cámara aquí en el Inspector

    void Start()
    {
        fuerza = 10f;
        speed = 12f;
    }

    void Update()
    {
        manageInput();
    }

    private void manageInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h);

        transform.position += moveDir * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * fuerza, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = false;
    }
}


