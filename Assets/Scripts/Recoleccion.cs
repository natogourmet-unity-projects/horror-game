using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoleccion : MonoBehaviour
{
    public float rango;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Recolectar();            
        }      
        
    }

    void Recolectar()
    {
        RaycastHit hit;
            
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, rango))
        {              
            Debug.DrawLine(this.transform.position, this.transform.forward.normalized * rango + this.transform.position, Color.red);
                
            Carta carta = hit.transform.GetComponent<Carta>();
            if (carta != null)
            {
                carta.SumarCarta();
            }            
        }  
    }
}
