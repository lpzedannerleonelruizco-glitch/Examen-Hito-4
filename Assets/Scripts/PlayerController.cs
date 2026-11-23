using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private bool enSuelo;

    // Verdadero mientras Pikachu esta en el aire despues de saltar.
    public bool EstaSaltando { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Keyboard teclado = Keyboard.current;
        if (teclado == null) return;

        float direccion = 0f;
        if (teclado.upArrowKey.isPressed) direccion -= 1f; // Flecha ARRIBA = caminar a la izquierda
        if (teclado.aKey.isPressed) direccion += 1f;       // Tecla A = caminar a la derecha

        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        if (teclado.vKey.wasPressedThisFrame && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            EstaSaltando = true;
        }

        if (anim != null)
        {
            anim.SetFloat("Velocidad", Mathf.Abs(direccion));
            anim.SetBool("EnSuelo", enSuelo);
        }
    }

    void FixedUpdate()
    {
        Bounds caja = col.bounds;
        Vector2 origen = new Vector2(caja.center.x, caja.min.y + 0.02f);
        RaycastHit2D[] golpes = Physics2D.BoxCastAll(origen, new Vector2(caja.size.x * 0.9f, 0.05f), 0f, Vector2.down, 0.12f);

        enSuelo = false;
        for (int i = 0; i < golpes.Length; i++)
        {
            if (golpes[i].collider != null && golpes[i].collider != col)
            {
                enSuelo = true;
                break;
            }
        }

        // Solo dejamos de considerar "saltando" cuando esta apoyado y ya no sube.
        if (enSuelo && rb.linearVelocity.y <= 0.01f) EstaSaltando = false;
    }
}
