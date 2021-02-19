using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemigo : MonoBehaviour
{
	public float			distanciaMaxima;
	public Transform		objetivo;
	public float			rangoAtaque;
    public AudioSource      audio;
    public AudioClip[]      girlSounds;
	public Animator			miAnimator;
    public float            distanciaEscucha;

	private NavMeshAgent	miAgente;
	private float			cercania;
	private bool			moviendo;
	private float			_rangoAtaque;
	private float 			distanciaEnemigo;
	private Vector3			posAnterior;

	private bool ataco = false;
	private void Awake()
	{
		miAgente = GetComponent<NavMeshAgent>();
		if (objetivo == null)
		{
			GameObject g = GameObject.FindGameObjectWithTag("Player");
			if (g != null)
			{
				objetivo = g.transform;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	void Start()
	{
		StartCoroutine(DefinirObjetivo());
		_rangoAtaque = rangoAtaque * rangoAtaque;
		StartCoroutine(Anim());
		InvokeRepeating("PlaySound", 15, 15);
	}
	
    void FixedUpdate()
    {
		cercania = (1 - Control.control.GetPorcentajeJuego())*distanciaMaxima;
        float dm = cercania * cercania;
		float dob = (transform.position - objetivo.position).sqrMagnitude;
		moviendo = (dob > dm);
		if (dob<_rangoAtaque && ataco == false)
		{
			Atacar();
		}
    }

	void Update() 
	{
		Vector3 distancia = objetivo.position - this.transform.position;
		distanciaEnemigo = distancia.magnitude;
		audio.volume = 1 - Mathf.Clamp(distanciaEnemigo/distanciaEscucha, 0, 1);
	}

	void Atacar()
	{
        ControlUI.controlUI.MostrarMensaje("Muerto");
        //Movimiento.movimiento.velocidad = 0;
        //Movimiento.movimiento.velocidadRotacion = 0;
        //Movimiento.movimiento.audio.Stop();
		AudioSource[] audio = objetivo.GetComponents<AudioSource>();
		audio[0].enabled = false;
		audio[1].enabled = false;
		ataco = true;
        ControlUI.controlUI.Morir();
		


    }

	IEnumerator Anim()
	{
		float f = (transform.position - posAnterior).sqrMagnitude * 3;
		miAnimator.SetFloat("velocidad", f);
		posAnterior = transform.position;
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(Anim());
	}
	IEnumerator DefinirObjetivo()
	{
		if (moviendo)
		{
			miAgente.SetDestination(objetivo.position);
		}
		else
		{
			miAgente.SetDestination(transform.position);
			transform.LookAt(objetivo, Vector3.up);
		}
		
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(DefinirObjetivo());
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rangoAtaque);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
	}

    public void PlaySound()
    {
        int rnd = Random.Range(0, girlSounds.Length - 1);
        audio.clip = girlSounds[rnd];
        audio.Play();
    }
}
