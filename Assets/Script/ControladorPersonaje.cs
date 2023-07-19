using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public int saltoMaximos; // 
    public AudioClip sonidoSalto;
    public float fuerzaGolpe;

    public LayerMask capaSuelo;
    private Rigidbody2D rigidbody2;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true; // cuando mire a la derecha va hacer true sino false
    private int saltoRestantes; // -
    private Animator animator;
    private bool puedeMoverse = true;
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        saltoRestantes =saltoMaximos;
    }

    /*
        GameObject: son elementos de unity como personaje ,monedas,camara
        componentes: define los comportamientos de los gameObject
    */
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    // --------
    //  Salto
    // --------
    bool estaEnSuelo()

    {
        // 1 punto de origen
        // 2 vector tamaño de la caja
        // 3 angulo de la caja
        // 4 direccion de la box cast
        // 5 distancia de la box cast
        // 6 layer mask: layer en unity define que puede interactuar entre si y con diferentes caracteristicas
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center,new Vector2(boxCollider.bounds. size.x,boxCollider.bounds.size.y) , 0f,Vector2.down,0.2f,capaSuelo);

        return raycastHit2D.collider != null;
    }

    void ProcesarSalto()
    {

        if(estaEnSuelo()){
            saltoRestantes = saltoMaximos;
        }

        if(Input.GetKeyDown(KeyCode.Space) && saltoRestantes > 0)
        {
            saltoRestantes--;
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x,0f);
            rigidbody2.AddForce(Vector2.up * fuerzaSalto , ForceMode2D.Impulse);
            Audio.Instance.ReproducirSonido(sonidoSalto);
        }

    }

    // --------------
    //   movimiento
    // --------------

    void ProcesarMovimiento()
    {
        if(!puedeMoverse)
        {
            return;
        }
        float inputMovimiento = Input.GetAxis("Horizontal");

        if(inputMovimiento != 0f)
        {
            animator.SetBool("isRunnig",true);
        } 
        else
        {
            animator.SetBool("isRunnig",false);
        }

        rigidbody2.velocity = new Vector2(inputMovimiento * velocidad , rigidbody2.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // personaje esta mirando a la derecha y jugador quiere ir a la izquierda 
        // personaje esta mirando a la  izquierda y jugador quiere ir a la derecha
        if( (mirandoDerecha == true && inputMovimiento < 0) ||  
        (mirandoDerecha == false && inputMovimiento > 0) )
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
        }
    }
    
    public void aplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe;
        if(rigidbody2.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1,1);
        }
        else
        {
            direccionGolpe = new Vector2(1,1);
        }
        rigidbody2.AddForce(direccionGolpe * fuerzaGolpe);

        StartCoroutine(esperarYActivarMovimiento());
    }

    IEnumerator esperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);

        while(!estaEnSuelo())
        {
            yield return null;
        }
        puedeMoverse = true;
    }
}
