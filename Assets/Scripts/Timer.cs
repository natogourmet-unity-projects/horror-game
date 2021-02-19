using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int segundos = 0;
    int timer = 0;
    void FixedUpdate()
    {
        time();
    }

    //tiempo del juego
 	void time()
    {
        timer++;
        timer %= 50;
        if (timer == 49)
        {
            segundos++;
        }
    }

	public int GetSegundos()
	{
		return segundos;
	}

}
