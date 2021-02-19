using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool cambioV = true;
    public AudioSource sonido;

    public Canvas creditos;

    void Start() 
    {   
        Cursor.lockState = CursorLockMode.None;
    }

    void Update() {

        if(cambioV == true)
        {
            SubirVelocidadAudio();
        }
        else
        {
            BajarVelocidadAudio();
        }
    }

    public void cambiarVelocidadAudio(bool cambio)
    {
        this.cambioV = cambio;
    }

    public void cambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    public void IniciarJuego()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movimiento>().enabled = true;
        GameObject.FindGameObjectWithTag("Lantern").gameObject.SetActive(true);
        sonido.Stop();
        ControlUI.controlUI.StartGame();
    }
    
    public void cerrarApp()
    {
        Application.Quit();
    }

    public void creditosM(bool estado)
    {
        creditos.gameObject.SetActive(estado);
    }    
    
    void SubirVelocidadAudio()
    {
        float valorPitch = sonido.pitch + 0.02f;
        sonido.pitch = Mathf.Clamp(valorPitch,0.7f,1);
    }

    void BajarVelocidadAudio()
    {
        float valorPitch = sonido.pitch - 0.005f;
        sonido.pitch = Mathf.Clamp(valorPitch,0.7f,1);
    }
}
