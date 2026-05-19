using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Transform puntoA; 
    public Transform puntoB; 
    public float velocidad = 2f;
    
    private Vector3 destino;
    private bool jugadorEncima = false; // Booleano para detectar al cerdo

    void Start()
    {
        destino = puntoB.position;
    }

    void FixedUpdate()
    {
        //  Solo se mueve si el jugador está encima
        if (jugadorEncima)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

            // Cambiar de dirección al llegar a los puntos
            if (Vector3.Distance(transform.position, puntoB.position) < 0.1f) destino = puntoA.position;
            if (Vector3.Distance(transform.position, puntoA.position) < 0.1f) destino = puntoB.position;
        }
    }

  private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        // Primero lo hacemos hijo
        collision.transform.SetParent(transform);
        
        // FÓRMULA MÁGICA: Dividimos 1 entre la escala del elevador.
        // Esto hace que si el elevador mide 2, el cerdo mida 1/2 (0.5), 
        // y 2 * 0.5 vuelve a ser 1 (su tamaño normal).
        collision.transform.localScale = new Vector3(
            1 / transform.localScale.x, 
            1 / transform.localScale.y, 
            1 / transform.localScale.z
        );
        
        jugadorEncima = true; 
    }
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            jugadorEncima = false; // El cerdo bajó, el elevador se para
        }
    }
}


