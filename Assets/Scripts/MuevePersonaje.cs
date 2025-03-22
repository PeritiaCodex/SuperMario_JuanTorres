using UnityEngine;

public class MuevePersonaje : MonoBehaviour
{
    //Velocidad
    public float velocidadX;

    [SerializeField] // Permiso a Unity de acceder a la variable
    private float velocidadY;

    // Rigidbody para usar la física
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // FixedUpdate() is called 50 times per second
    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");
        if (movVertical > 0) {
            rb.linearVelocity = new Vector2(movHorizontal*velocidadX, movVertical*velocidadY);
        } else {
            rb.linearVelocity = new Vector2(movHorizontal*velocidadX, rb.linearVelocityY);
        }
    }
}