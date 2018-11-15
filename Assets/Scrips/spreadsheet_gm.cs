using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spreadsheet_gm : MonoBehaviour {

    private Data file;

    private int counter = 0;

    private bool hay_fichas = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void back()
    {
        UIScript.actualUI.Play();
    }

    public void clickStock()
    {
        UIScript.actualUI.Stock();
    }

    public void clickFlujoDeFondos()
    {
        UIScript.actualUI.Flow();
    }

    public void clickCostos()
    {
        UIScript.actualUI.Cost();
    }

    public void clickArriba()
    {
        UIScript.actualUI.Flow();
    }

    public void clickAbajo()
    {
        UIScript.actualUI.Flow02();
    }

}
