using UnityEngine;
using UnityEngine.InputSystem;

public class MueveAccionPersonaje : MonoBehaviour
{
    [SerializeField]
    private InputAction moveAction;
    
    [SerializeField]
    private InputAction jumpAction;

    [SerializeField]
    private Animator animator;

    private const float SPEED = 10.0f;
    private const float JUMP_FORCE = 15.0f;
    private const float RAYCAST_DISTANCE = 0.35f; // Distancia del raycast

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener Rigidbody2D
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(move.x * SPEED, rb.linearVelocity.y);

        // Obtener la velocidad absoluta para el Animator
        float movementSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("movement", movementSpeed);

        // 🔄 Voltear personaje según dirección
        if (move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (move.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Detectar si está en el suelo con Raycast
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, RAYCAST_DISTANCE, LayerMask.GetMask("Ground"));

        // Salto solo si está en el suelo
        if (jumpAction.triggered && isGrounded)
        {
            rb.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        // Dibuja la línea del Raycast en la escena para depuración
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * RAYCAST_DISTANCE);
    }
}
