using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Vector3 direccion;
    private float velocidad;
    private int daño;
    private float tiempoVida = 5f;
    
    public void Inicializar(Vector3 dir, float vel, int dmg)
    {
        direccion = dir;
        velocidad = vel;
        daño = dmg;
        Destroy(gameObject, tiempoVida);
    }
    
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth vidaPlayer = collision.gameObject.GetComponent<PlayerHealth>();
            if (vidaPlayer != null)
            {
                vidaPlayer.TakeDamage(-daño);
            }
        }
        
        Destroy(gameObject);
    }
}