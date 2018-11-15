using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tax_gm : MonoBehaviour {


    private Text lbl_prec01;
    private Text lbl_prec02;
    private Text lbl_prec03;
    private Text lbl_prec04;

    private Text lbl_total;

    private int total_electricidad_y_gas = 3500;
    private int total_agua_y_cargas_sociales = 1500;

    private Data file;

    // Use this for initialization
    void Start () {
        lbl_prec01 = GameObject.Find("lbl_electricidad").GetComponent<Text>();
        lbl_prec02 = GameObject.Find("lbl_agua").GetComponent<Text>();
        lbl_prec03 = GameObject.Find("lbl_gas").GetComponent<Text>();
        lbl_prec04 = GameObject.Find("lbl_sueldo").GetComponent<Text>();

        lbl_total = GameObject.Find("lbl_total").GetComponent<Text>();

        //float part = UnityEngine.Random.value;
        //if(part > .5f)
        //{
        //    lbl_prec04.text = Convert.ToString(Convert.ToInt32(total_electricidad_y_gas * part));
        //    lbl_prec01.text = Convert.ToString(Convert.ToInt32(total_electricidad_y_gas * (1 - part)));
        //}
        //else
        //{
        //    lbl_prec01.text = Convert.ToString(Convert.ToInt32(total_electricidad_y_gas * part));
        //    lbl_prec04.text = Convert.ToString(Convert.ToInt32(total_electricidad_y_gas * (1 - part)));
        //}

        //lbl_prec02.text = Convert.ToString(Convert.ToInt32(total_agua_y_cargas_sociales * part));
        //lbl_prec03.text = Convert.ToString(Convert.ToInt32(total_agua_y_cargas_sociales * (1 - part)));

        /*Se ponen valores fijos para completar la planilla de costos con las cuentas correspondientes*/
        lbl_prec01.text = 3000.ToString();
        lbl_prec02.text = 300.ToString();
        lbl_prec03.text = 500.ToString();
        lbl_prec04.text = 1200.ToString();

        lbl_total.text = Convert.ToString(total_electricidad_y_gas + total_agua_y_cargas_sociales);

        file = new Data();
        file.Deserializar();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void back()
    {
        //UIScript.actualUI.Play();
        payTaxes();
    }

    public void payTaxes()
    {
        file.Deserializar();
        //Actualizo el valor de is_tax
        Game.current.Is_tax = false;
        //if(Game.current.Money >= (total_electricidad_y_gas + total_agua_y_cargas_sociales))
        //{
        Game.current.Money = Game.current.Money - (total_electricidad_y_gas + total_agua_y_cargas_sociales);

            UIScript.actualUI.Play();
            UIScript.actualUI.UpdateUIExt();
        //}
        //else
        //{
        //    //no se tiene dinero para pagar los impuestos
        //}
    }
}
