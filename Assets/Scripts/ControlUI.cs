using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlUI : MonoBehaviour
{
    
    public int segundos;
    int timer = 0;
    bool murio = false;
    bool gano = false;

    public TextMeshProUGUI texto;
    public Image imagen;

    #region singleton
    public static ControlUI controlUI = new ControlUI();
    void Awake()
    {
		if (controlUI == null)
		{
			controlUI = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
    }
    #endregion
 
    void Start() 
    {
        GameObject t = GameObject.FindGameObjectWithTag("Mensaje");        
        texto = t.GetComponent<TextMeshProUGUI>();
        texto.enabled = false;  
        t = GameObject.FindWithTag("Muerte");
        imagen = t.GetComponent<Image>();
        gameObject.GetComponent<Text>().enabled = false;
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        imagen.enabled = false;
        segundos = -2;
    }
    
    void Update() 
    {
        DeshabilitarMensaje();
        
        if(segundos == -1)
        {
            MostrarMensaje("Recoge las " + Control.control.cartasEnMundo + " cartas");
        }

        if (murio == true)
        {
            VolverMenu();
        }

        if (gano == true)
        {
            VolverMenu();
        }
    }

    void FixedUpdate()
    {
        time();
    }


    //tiempo del juego
 	public void time()
    {
        timer++;
        timer %= 50; 
        if (timer == 49)
        {
            segundos++;
        }
        
    }

    public void MostrarMensaje(string mensaje)
    {
        timer = 0;
        segundos = 0;        
        texto.text = mensaje;
        texto.enabled = true;

    }

    void DeshabilitarMensaje()
    {
        if (segundos == 6)
        {
            texto.enabled = false;
        }        
    }

    public void Morir()
    {
        timer = 0;
        segundos = 0;
        murio = true;      
        imagen.enabled = true;        
    }

    public void Ganar()
    {
        timer = 0;
        segundos = 0;
        gano = true;      
        imagen.enabled = true;
    }

    public void VolverMenu()
    {
        print(segundos);
        if(segundos == 3)
        {
            murio = false;
            gano = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    

    //Revisar viabilidad
    /* 
    public GameObject timerPrefab;
    public GameObject timer;
    public Timer time;

    void mostrarMensaje()
    {
        Instantiate(timerPrefab);
        timer = GameObject.FindGameObjectWithTag("Timer");
        time = timer.GetComponent<Timer>();
    }

    void deshabilitarMensaje()
    {
        print(time.GetSegundos());
         
        if (segundos == 6)
        {
            texto.enabled = false;
        }
        
    }
    */
}
