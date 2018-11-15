using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
                         IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private AudioClip fail_sound;
    private AudioClip good_sound;

    /*el margen de error que puede tener el usuario cuando arrastra la ficha*/
    private int x_margin = 16;
    private int y_margin = 8;

    private void Start()
    {
        fail_sound = Resources.Load<AudioClip>("sounds/fail_sound");
        good_sound = Resources.Load<AudioClip>("sounds/good_sound");
    }


    //Se ejecuta mientras se esté arrastrando
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Está siendo arrastrado");
        transform.position = eventData.position;
        //Objectos 2D 
        //transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    //Se ejecuta cuando se empieza a arrastrar, antes del OnDrag
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Va a ser arrastrado");
    }

    //Se ejecuta cuando fue soltado, antes del OnDrop
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Va a ser soltado");

        /*aqui se debe corroborar que este o al menos este cerca de la posicion de destino*/
        Vector2 auxp = GetComponent<RectTransform>().anchoredPosition;
        float drop_x_pos = auxp.x;
        float drop_y_pos = auxp.y;

        float x_pos = gameObject.GetComponent<propiedades_ficha>().X_pos;
        float y_pos = gameObject.GetComponent<propiedades_ficha>().Y_pos;
        float x_dest = gameObject.GetComponent<propiedades_ficha>().X_dest;
        float y_dest = gameObject.GetComponent<propiedades_ficha>().Y_dest;

        if (x_dest - x_margin <= drop_x_pos && x_dest + x_margin >= drop_x_pos && y_dest - y_margin <= drop_y_pos && y_dest + y_margin >= drop_y_pos)
        {
            //se encuentra en una posicion valida :D
            if (Convert.ToBoolean(PlayerPrefs.GetFloat("fx", 1)))
                AudioSource.PlayClipAtPoint(good_sound, Camera.main.transform.position);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(x_dest, y_dest);

            /*se guardan las posiciones actuales de la ficha*/
            GetComponent<propiedades_ficha>().X_actual = x_dest;
            GetComponent<propiedades_ficha>().Y_actual = y_dest;

            /*se habilita el saldo*/ //<- el problema aca es que FindGameObjectWithTag no encuentra los GO inactivos
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Canvas_stk_ficha_saldo"))
            {
                if (go.GetComponent<RectTransform>().anchoredPosition.y == GetComponent<RectTransform>().anchoredPosition.y)
                    go.SetActive(true);
            }
        }
        else
        {
            //cualquier otra pos no valida
            if (Convert.ToBoolean(PlayerPrefs.GetFloat("fx", 1)))
                AudioSource.PlayClipAtPoint(fail_sound, Camera.main.transform.position);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(x_pos, y_pos);

            GetComponent<propiedades_ficha>().X_actual = 0;
            GetComponent<propiedades_ficha>().Y_actual = 0;

            /*se deshabilita el saldo*/
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Canvas_stk_ficha_saldo"))
            {
                if (go.GetComponent<RectTransform>().anchoredPosition.y == GetComponent<RectTransform>().anchoredPosition.y)
                    go.SetActive(false);
            }
        }

    }


    //Se ejecuta al finalizar la pulsación completa (levantar el dedo o mouse)
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Ha sido pulsado");
    }

    //Se ejecuta cuando el punto del mouse pasa por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("El mouse está encima");
    }

    //Se ejecuta cuando el puntero, después de haber pasado por encima, sale de su collider
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("El mouse ya NO está encima");
    }

}
