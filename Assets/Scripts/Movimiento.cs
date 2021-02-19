using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad;
    private float xAxis, zAxis;

    public Camera camaraPrincipal;
    public float velocidadRotacion;
    public float posicionArriba;
    public float posicionAbajo;
    Vector3 rotacion;

    public AudioSource audio;
    public AudioSource extra;
    public AudioClip walkSFX; 
    public AudioClip runSFX;
    public AudioClip tiredSFX;

    private bool tired = false;
    private bool running = false;
    private float runningTime = 0;
/* 
    #region singleton
    public static Movimiento movimiento = new Movimiento();
    void Awake()
    {
        if (movimiento == null)
        {
            movimiento = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion
*/
    void Start()
    {
        //Datos iniciales de la rotación de la cámara
        rotacion = camaraPrincipal.transform.eulerAngles;

        //Bloquea cursor en el juego
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        //Correr
        if (!tired && Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            StartRunning();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            StopRunning();
        }

        //Movimiento personaje
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        Vector3 caminar = new Vector3(xAxis, 0, zAxis);
        caminar.Normalize();
        if (running)
        {
            caminar *= 2;
            runningTime += Time.deltaTime;
            print(runningTime);
            if (runningTime >= 5)
            {
                StartCoroutine(TimeResting());
                tired = true;
                extra.Play();
                running = false;
                StopRunning();
            }
        }
        else if (tired) caminar /= 2;

        if (caminar != Vector3.zero)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
        this.transform.Translate(caminar * velocidad * Time.deltaTime);

        //Rotación personaje
        rotacion.x = rotacion.x - (Input.GetAxis("Mouse Y") * velocidadRotacion);
        rotacion.x = Mathf.Clamp(rotacion.x, posicionArriba, posicionAbajo);
        camaraPrincipal.transform.localEulerAngles = rotacion;
        this.transform.Rotate(0, Input.GetAxis("Mouse X") * velocidadRotacion, 0);
    }

    public void StartRunning()
    {
        if (audio.clip == walkSFX)
        {
            audio.clip = runSFX;
        }
    }

    public void StopRunning()
    {
        if (audio.clip == runSFX)
        {
            audio.clip = walkSFX;
        }
        runningTime = 0;
    }

    IEnumerator TimeRunning()
    {
        yield return new WaitForSeconds(5);
        
    }

    IEnumerator TimeResting()
    {
        yield return new WaitForSeconds(10);
        tired = false;
    }
}
