using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
                         IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    //Se ejecuta repetidamente mientras se esté arrastrando
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Está siendo arrastrado");
        transform.position = eventData.position;
        //Objectos 2D 
        //transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    //Se ejecuta cuando ha empezado a arrastrarse, antes del OnDrag
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Va a ser arrastrado");
    }

    //Se ejecuta cuando se ha soltado, antes del OnDrop
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Va a ser soltado");
    }


    //Se ejecuta al finalizar la pulsación completa (levantar el dedo o ratón)
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ha sido pulsado");
    }

    //Se ejecuta cuando el punto del ratón pasa por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("El ratón está encima");
    }

    //Se ejecuta cuando el puntero, después de haber pasado por encima, sale de su collider
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("El ratón ya NO está encima");
    }

}
