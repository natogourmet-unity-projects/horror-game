using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Botones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  
    public UnityEvent subirPitch;
    public UnityEvent bajarPitch;

    public void OnPointerEnter(PointerEventData eventData)
    {
        bajarPitch.Invoke();
        print("bajar");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        subirPitch.Invoke();
        print("Subir");
    }
}
