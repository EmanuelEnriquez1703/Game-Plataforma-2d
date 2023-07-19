using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instace {get; private set;}
    public int PuntosTotales{ get {return puntosTotales;} }
    private int puntosTotales;

    private int vidas = 3;

    public HUD hud;
    void Awake() 
    {
        if(Instace == null)
        {
            Instace = this;
        }
        else
        {
            Debug.Log("cuidaaaooo");
        }
    }
    public void SumarPunto(int puntoASumar)
    {
        puntosTotales = puntosTotales + puntoASumar;
        hud.ActualizarPuntos(puntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 1;

        if(vidas == 0){
            SceneManager.LoadScene(0);
        }

        hud.DesactivarVida(vidas);
    }
    public bool Recuperarvida()
    {
        if(vidas == 3){
            return false;
        }
        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

}
