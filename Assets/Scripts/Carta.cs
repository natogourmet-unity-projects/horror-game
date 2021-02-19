using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
	private void Start()
	{
		Control.control.AgregarCartaPosible();
	}
	public void SumarCarta()
    {
        Control.control.SumarCarta();
        Destroy(gameObject);
    }
}
