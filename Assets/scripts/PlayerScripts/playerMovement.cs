using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public float fuerza;
    public bool grounded;
    public Transform cameraTransform;
    public Transform groundCheck;
    float contadorAtaque;
    float Ataquecooldown = 3;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    float threshold = -30f;

    void Start()
    {
        fuerza = 5f;
        speed = 12f;
        contadorAtaque = Ataquecooldown;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        contadorAtaque -= Time.deltaTime;

        CheckGround();
    }

    void FixedUpdate()
    {
        manageInput();
        if (transform.position.y < threshold)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = new Vector3(16.5f, 2.8f, 0.20f);
        }
    }

    private void CheckGround()
    {
        // Lanza una esfera hacia abajo desde los pies del jugador
        grounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);
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

        Vector3 moveDir = (forward * v + right * h).normalized;
        Vector3 velocity = moveDir * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerza, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        enemy enemigo = collision.gameObject.GetComponent<enemy>();
        if (enemigo == null) return;
        if (collision.gameObject.tag == "Enemigo" && !grounded)
        {
            enemigo.TakeDamage(-1);
            return;
        }

        if (contadorAtaque <= 0 && health.getImmunity())
        {
            enemigo.TakeDamage(-1);
            contadorAtaque = Ataquecooldown;
        }
    }
}