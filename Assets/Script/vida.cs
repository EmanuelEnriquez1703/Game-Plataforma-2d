using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bool vidaRecuperada = GameManager.Instace.Recuperarvida();
            if(vidaRecuperada)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
