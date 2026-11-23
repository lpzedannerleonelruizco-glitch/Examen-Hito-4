using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform jugador;
    public GameObject textoEndGame;
    public float alturaLimite = -6f;

    private bool terminado;

    void Update()
    {
        if (terminado || jugador == null) return;

        if (jugador.position.y < alturaLimite)
        {
            terminado = true;

            if (textoEndGame != null) textoEndGame.SetActive(true);

            Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }

            PlayerController control = jugador.GetComponent<PlayerController>();
            if (control != null) control.enabled = false;
        }
    }
}
