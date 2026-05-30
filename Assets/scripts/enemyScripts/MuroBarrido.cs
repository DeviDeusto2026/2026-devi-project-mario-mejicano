using UnityEngine;

public class MuroBarrido : MonoBehaviour
{
    public Vector3 direccion;
    public float velocidad = 5f;
    public float tiempoVida = 3f;
    
    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
    
    void Update()
    {
        // El muro se mueve en la dirección del jugador
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth vidaPlayer = collision.gameObject.GetComponent<PlayerHealth>();
            if (vidaPlayer != null)
            {
                vidaPlayer.TakeDamage(-10); // Daño del muro
            }
            Destroy(gameObject);
        }
    }
}