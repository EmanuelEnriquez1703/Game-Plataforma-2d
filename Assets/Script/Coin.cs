using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int valor=1;
    public AudioClip sonidoMoneda;
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Player"))
        {
            GameManager.Instace.SumarPunto(valor);
            Destroy(this.gameObject);
            Audio.Instance.ReproducirSonido(sonidoMoneda);
        }
    }
}
