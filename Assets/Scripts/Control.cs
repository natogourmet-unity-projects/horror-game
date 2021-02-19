using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public int cartas = 0;
	public float cartasEnMundo;
    public GameObject enemy;

	//Singleton para el control del flujo de juego
    #region singleton
    public static Control control = new Control();
    void Awake()
    {
		if (control == null)
		{
			control = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
    }
    #endregion

	//Método ejecutado cada 0.02 segundos
    

	//Controla el número de cartas recogidas en el mundo
    public void SumarCarta()
    {
        if (cartas == 0)
        {
            enemy.SetActive(true);
        }
        control.cartas++;
		ControlUI.controlUI.MostrarMensaje(control.cartas + " de "+ control.cartasEnMundo + " cartas");
		if (!(cartas < cartasEnMundo))
		{
			Gano();
		}
	}

	//Devuelve el porcentaje de cartas recogidas
	public float GetPorcentajeJuego()
	{
		return ((float)(cartas + 1f) / cartasEnMundo);
	}

	//Indica que se ganó el juego
	public void Gano()
	{
		ControlUI.controlUI.MostrarMensaje("Ganó");
		ControlUI.controlUI.Ganar();
		GameObject enemy = GameObject.FindGameObjectWithTag("Enemigo");
		enemy.SetActive(false);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		AudioSource[] audio = player.GetComponents<AudioSource>();
		audio[0].enabled = false;
		audio[1].enabled = false;
	}

	//Suma una nueva carta en la escena
	public void AgregarCartaPosible()
	{
		control.cartasEnMundo++;
	}


}
