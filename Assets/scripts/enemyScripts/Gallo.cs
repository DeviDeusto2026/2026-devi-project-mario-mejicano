using UnityEngine;

public class Gallo : enemy
{
    [Header("Configuración de Ataques")]
    public GameObject proyectilPrefab;
    public GameObject enemigoInvocable;
    public Transform puntoDisparo;

    [Header("Tiempos")]
    public float tiempoEntreAtaques = 3f;
    public float tiempoPreparacion = 0.5f;

    [Header("Invocación")]
    public int cantidadEnemigos = 2;

    [Header("Proyectil")]
    public float velocidadProyectil = 10f;

    // Variables de tiempo
    private float temporizadorAtaque;
    private float temporizadorPreparacion;

    // Estados del boss
    private bool preparandoAtaque = false;
    private bool invocando = false;
    private int enemigosInvocados = 0;
    private GameObject jugador;

    // Sprite para efectos visuales
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");
        }

        // Estado: invocando enemigos
        if (invocando)
        {
            return; // No hace nada más mientras invoca
        }

        // Estado: preparando ataque
        if (preparandoAtaque)
        {
            temporizadorPreparacion -= Time.deltaTime;
            if (temporizadorPreparacion <= 0)
            {
                // Terminó la preparación, ejecutar el ataque
                preparandoAtaque = false;
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = colorOriginal;
                }

                // Elegir y ejecutar ataque
                float probabilidad = Random.Range(0f, 1f);
                if (probabilidad <= 0.8f)
                {
                    LanzarProyectil();
                }
                else
                {
                    EmpezarInvocacion();
                }

                // Reiniciar temporizador entre ataques
                temporizadorAtaque = tiempoEntreAtaques;
            }
            return;
        }

        // Estado normal: esperando para atacar
        temporizadorAtaque -= Time.deltaTime;
        if (temporizadorAtaque <= 0)
        {
            // Empezar preparación del ataque
            preparandoAtaque = true;
            temporizadorPreparacion = tiempoPreparacion;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
            }
        }
    }

    void LanzarProyectil()
    {
        if (proyectilPrefab == null) return;
        if (jugador == null) return;

        // Posición de disparo
        Vector3 posicionInicio = transform.position;
        if (puntoDisparo != null)
        {
            posicionInicio = puntoDisparo.position;
        }

        // Crear proyectil
        GameObject nuevoProyectil = Instantiate(proyectilPrefab, posicionInicio, Quaternion.identity);

        // Calcular dirección hacia el jugador
        Vector3 direccion = (jugador.transform.position - posicionInicio).normalized;

        // Mover el proyectil
        Proyectil scriptProyectil = nuevoProyectil.GetComponent<Proyectil>();
        if (scriptProyectil != null)
        {
            scriptProyectil.Inicializar(direccion, velocidadProyectil, 5);
        }
    }

    void EmpezarInvocacion()
    {
        invocando = true;
        enemigosInvocados = 0;
    }

    void FixedUpdate()
    {
        // Manejar la invocación del muro
        if (invocando)
        {
            if (jugador != null && enemigoInvocable != null)
            {
                // Calcular posición detrás del jugador
                Vector3 direccionJugador = jugador.transform.forward;
                Vector3 posicionDetras = jugador.transform.position - direccionJugador * 3f;
                posicionDetras.y = 0;

                // Crear el muro
                GameObject muro = Instantiate(enemigoInvocable, posicionDetras, Quaternion.identity);

                // Hacer que el muro se mueva hacia adelante (barrido)
                MuroBarrido scriptMuro = muro.GetComponent<MuroBarrido>();
                if (scriptMuro != null)
                {
                    scriptMuro.direccion = direccionJugador;
                    scriptMuro.velocidad = 5f;
                }

                invocando = false; // Solo invoca UN muro
            }
            else
            {
                invocando = false;
            }
        }
    }
}