using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public float cronometro;
    public int rutina;
    public Vector3 destino;
    public float grado;
    [SerializeField] private loadLevel obj;
    [SerializeField] private int maxDistance = 5;
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float velocidadRotacion = 5f;
    int healthNormal = 1;
    int healthJefe = 3;
    public void TakeDamage(int amount)
    {
        if (gameObject.tag.Equals("Jefe"))
        {
            healthJefe += amount;
            if (healthJefe <= 0)
            {
                obj.CargarLobby();
                Destroy(gameObject);
            }
        }
        else
        {
            healthNormal += amount;
            if (healthNormal <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Update()
    {
        if (rutina == 2)
        {
            // Rotar hacia destino
            Vector3 direccion = (destino - transform.position).normalized;
            if (direccion != Vector3.zero)
            {
                Quaternion angulo = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, velocidadRotacion);
            }

            // Mover hacia destino
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

            // Lleg� al destino
            if (Vector3.Distance(transform.position, destino) < 0.1f)
                rutina = 0;

            return;
        }

        cronometro += Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }

        switch (rutina)
        {
            case 0:
                break;

            case 1:
                destino = new Vector3(
                    transform.position.x + Random.Range(-maxDistance, maxDistance),
                    transform.position.y,
                    transform.position.z + Random.Range(-maxDistance, maxDistance)
                );
                rutina++;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(-1);
        }
    }
}