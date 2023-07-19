using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float coolDownAtaque; 
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!puedeAtacar)
        {
            return;
        }

        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

        puedeAtacar = false;

        if(other.gameObject.CompareTag("Player")){
            GameManager.Instace.PerderVida();
            other.gameObject.GetComponent<ControladorPersonaje>().aplicarGolpe();
        }
        Invoke("ReactivarAtaque",coolDownAtaque);
    }
    void ReactivarAtaque()
    {   
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
        puedeAtacar = true;
    }
}
