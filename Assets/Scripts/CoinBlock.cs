using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public AudioClip sonidoMoneda;

    private AudioSource fuente;

    void Awake()
    {
        fuente = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D choque)
    {
        // Solo cuenta si el que toca el bloque es Pikachu...
        PlayerController jugador = choque.collider.GetComponent<PlayerController>();
        if (jugador == null) return;

        // ...y solo si lo toca mientras esta saltando.
        if (!jugador.EstaSaltando) return;

        if (fuente != null && sonidoMoneda != null)
        {
            fuente.PlayOneShot(sonidoMoneda);
        }
    }
}
