using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    // Variables para el movimiento
   [SerializeField] float vel;
    [SerializeField] float fuerzasalto;
    [SerializeField] float fuerzaGolpe;
    public LayerMask capaSuelo;
    public AudioClip SonidoSalto;
    public AudioClip SonidoCaminar;

    // Componentes
    private Rigidbody2D rb;
    private BoxCollider2D boxcollider;
    private Animator animator;
    private bool puedeMoverse = true;

    // Estado del personaje
    private bool mirandoderecha = true;
    private bool estaEnElAire = false; //Variable para rastrear si el personaje está en el aire o no

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.Dead == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("death", true);
        }
        else { rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("death", false);
        }
        // Llamada a los métodos de movimiento y salto
        ProcesarMovimiento();
        procesarSalto();
    }

    bool estaEnSuelo()
    {
        // Comprueba si el personaje está en contacto con el suelo
        RaycastHit2D hit = Physics2D.BoxCast(boxcollider.bounds.center, new Vector2(boxcollider.bounds.size.x, boxcollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return hit.collider != null;
    }

    void procesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo())
        {
            // Aplica fuerza hacia arriba para simular el salto
            rb.AddForce(Vector2.up * fuerzasalto, ForceMode2D.Impulse);
            estaEnElAire = true; // El personaje está en el aire después de saltar
            animator.SetBool("isjumping", true); // Activa la animación de salto
            AudioManager.Instance.ReproducirSonido(SonidoSalto);
        }
       
        // Comprueba si el personaje está en el aire y desactiva la animación de salto si no está en el suelo
        if (!estaEnSuelo())
        {
            animator.SetBool("isjumping", true);
            estaEnElAire = true;
        }
        else
        {
            estaEnElAire = false; // El personaje está en el suelo
            animator.SetBool("isjumping", false); // Desactiva la animación de salto si está en el suelo
        }
    }

    void ProcesarMovimiento()
    {
        if (!puedeMoverse) { return; }


        float inputMovimiento = Input.GetAxis("Horizontal");

        GestionarOrientacion(inputMovimiento);

        // Activa la animación de correr si el jugador se está moviendo horizontalmente y está en el suelo
        if (inputMovimiento != 0f && !estaEnElAire)
        {
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }

        // Aplica la velocidad horizontal al Rigidbody para mover al personaje
        rb.velocity = new Vector2(inputMovimiento * vel, rb.velocity.y);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoderecha && inputMovimiento < 0) || (!mirandoderecha && inputMovimiento > 0))
        {
            // Voltea el sprite del personaje si cambia de dirección
            mirandoderecha = !mirandoderecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    public void AplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe;
        if(rb.velocity.x > 0) 
        {
            direccionGolpe = new Vector2(-1, 1);
        } else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rb.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(EsperarYActivarMovimiento());
    }
    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);
        while (!estaEnSuelo())
        {
            animator.SetBool("hurt", true);
            animator.SetBool("isjumping", false);
            yield return null;
        }
        animator.SetBool("hurt", false);
        puedeMoverse = true;
    }
}