using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propiedades_ficha : MonoBehaviour {

    /*x_pos e y_pos son las posiciones originales de las fichas
     (posiciones en la primer columna)*/
    private float x_pos;
    private float y_pos;

    /*x_dest e y_dest son las posiciones objetivo de las fichas*/
    private float x_dest;
    private float y_dest;

    /*las posiciones actuales de las fichas se almacenaran en x_actual e y_actual,
     * si estas var estan vacias (0), es porque no se guardo su posicion todavia*/
    private float x_actual = 0;
    private float y_actual = 0;


    public float X_pos
    {
        get
        {
            return x_pos;
        }

        set
        {
            x_pos = value;
        }
    }

    public float Y_pos
    {
        get
        {
            return y_pos;
        }

        set
        {
            y_pos = value;
        }
    }

    public float X_dest
    {
        get
        {
            return x_dest;
        }

        set
        {
            x_dest = value;
        }
    }

    public float Y_dest
    {
        get
        {
            return y_dest;
        }

        set
        {
            y_dest = value;
        }
    }

    public float X_actual
    {
        get
        {
            return x_actual;
        }

        set
        {
            x_actual = value;
        }
    }

    public float Y_actual
    {
        get
        {
            return y_actual;
        }

        set
        {
            y_actual = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {

    }
}
