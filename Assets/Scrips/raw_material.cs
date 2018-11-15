using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raw_material : MonoBehaviour {
    
    private Text lbl_cant01;
    private Text lbl_cant02;
    private Text lbl_cant03;
    private Text lbl_cant04;
    private Text lbl_cant05;

    private Text lbl_prec01;
    private Text lbl_prec02;
    private Text lbl_prec03;
    private Text lbl_prec04;
    private Text lbl_prec05;

    private Text lblTot;

    private Dropdown drp_forma_pago;

    private Data file;

    // Use this for initialization
    void Start () {
        lbl_cant01 = GameObject.Find("lbl_cant01").GetComponent<Text>();
        lbl_cant02 = GameObject.Find("lbl_cant02").GetComponent<Text>();
        lbl_cant03 = GameObject.Find("lbl_cant03").GetComponent<Text>();
        lbl_cant04 = GameObject.Find("lbl_cant04").GetComponent<Text>();
        lbl_cant05 = GameObject.Find("lbl_cant05").GetComponent<Text>();

        lbl_prec01 = GameObject.Find("lbl_prec01").GetComponent<Text>();
        lbl_prec02 = GameObject.Find("lbl_prec02").GetComponent<Text>();
        lbl_prec03 = GameObject.Find("lbl_prec03").GetComponent<Text>();
        lbl_prec04 = GameObject.Find("lbl_prec04").GetComponent<Text>();
        lbl_prec05 = GameObject.Find("lbl_prec05").GetComponent<Text>();

        lblTot = GameObject.Find("lbl_total").GetComponent<Text>();

        drp_forma_pago = GameObject.Find("drp_forma_pago").GetComponent<Dropdown>();
        drp_forma_pago.onValueChanged.AddListener(delegate {
            DropdownValueChanged(drp_forma_pago);
        });

        file = new Data();
        file.Deserializar();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickSumRawMat()
    {
        //if (Game.current.Money > Convert.ToInt32(lblTot.text))
        //{
            lbl_cant01.text = Convert.ToString(Convert.ToInt32(lbl_cant01.text) + 50);
            lbl_cant02.text = Convert.ToString(Convert.ToInt32(lbl_cant02.text) + 50);
            lbl_cant03.text = Convert.ToString(Convert.ToInt32(lbl_cant03.text) + 50);
            lbl_cant04.text = Convert.ToString(Convert.ToInt32(lbl_cant04.text) + 50);
            lbl_cant05.text = Convert.ToString(Convert.ToInt32(lbl_cant05.text) + 50);

            updatePrices();
        //}
    }

    public void clickRestRawMat()
    {
        if(Convert.ToInt32(lbl_cant05.text) > 0)
        {
            //se puede seguir restando cantidad
            lbl_cant01.text = Convert.ToString(Convert.ToInt32(lbl_cant01.text) - 50);
            lbl_cant02.text = Convert.ToString(Convert.ToInt32(lbl_cant02.text) - 50);
            lbl_cant03.text = Convert.ToString(Convert.ToInt32(lbl_cant03.text) - 50);
            lbl_cant04.text = Convert.ToString(Convert.ToInt32(lbl_cant04.text) - 50);
            lbl_cant05.text = Convert.ToString(Convert.ToInt32(lbl_cant05.text) - 50);

            updatePrices();
        }
    }

    public void updatePrices()
    {
        int price_aux = Game.current.Purchase_prices[drp_forma_pago.value];
        
        int val1 = Convert.ToInt32(lbl_cant01.text) * price_aux;
        int val2 = Convert.ToInt32(lbl_cant02.text) * price_aux;
        int val3 = Convert.ToInt32(lbl_cant03.text) * price_aux;
        int val4 = Convert.ToInt32(lbl_cant04.text) * price_aux;
        int val5 = Convert.ToInt32(lbl_cant05.text) * price_aux;

        lbl_prec01.text = price_aux.ToString();
        lbl_prec02.text = price_aux.ToString();
        lbl_prec03.text = price_aux.ToString();
        lbl_prec04.text = price_aux.ToString();
        lbl_prec05.text = price_aux.ToString();

        lblTot.text = Convert.ToString(val1 + val2 + val3 + val4 + val5);
    }

    public void checkout()
    {
        /*Se calcula si se debe descontar al contado o en tantos días*/
        if ((61 - (Game.current.Day + Game.current.Form_pay[drp_forma_pago.value])) >= 0)
        {

            if (Game.current.Form_pay[drp_forma_pago.value] == 0 
                    && Game.current.Money >= Convert.ToInt32(lblTot.text)
                    || Game.current.Form_pay[drp_forma_pago.value] > 0)
            {
                int effect_day = Game.current.Day + Game.current.Form_pay[drp_forma_pago.value];
                /*se agregan el movimiento dinero, el de stock se agregará cuando ingrese la mercaderia*/
                int[] aux_aad01 = new int[] { (Game.current.Day + 3), Convert.ToInt32(lbl_cant05.text), 1, 2, 0};
                Game.current.Mat_stock.Add(aux_aad01);
                //Se agrega monto, 1 porque está listo para descontar, 0 ingresa a la planilla de costos ahora
                //Se agrega el último 1 porque es Costo Variable
                //Se agrega 2 que es el ID de la descripción de la cuenta Materia Prima
                int[] aux_aad02 = new int[] { effect_day, Convert.ToInt32(lblTot.text) * -1, 1, 0, 1, 2 };
                Game.current.Money_amount.Add(aux_aad02);

                UIScript.actualUI.HelpText("La materia prima ingresa al tercer día, efectuada la compra.", Game.current.t_go);

                blankFields();
            }
            else
            {
                UIScript.actualUI.HelpText("No tiene dinero suficiente para la compra.", Game.current.t_go);
            }
        }
        else
        {
            UIScript.actualUI.HelpText("No se puede comprar a un plazo mayor que el cierre.", Game.current.t_load);
            UIScript.actualUI.HelpText("El juego termina a los 60 días.", Game.current.t_load);
        }
        UIScript.actualUI.Play();
        UIScript.actualUI.UpdateUIExt();
    }

    public void back()
    {
        blankFields();

        UIScript.actualUI.Play();
    }

    private void blankFields()
    {
        lbl_cant01.text = "0";
        lbl_cant02.text = "0";
        lbl_cant03.text = "0";
        lbl_cant04.text = "0";
        lbl_cant05.text = "0";

        lbl_prec01.text = "0";
        lbl_prec02.text = "0";
        lbl_prec03.text = "0";
        lbl_prec04.text = "0";
        lbl_prec05.text = "0";

        lblTot.text = "0";
    }

    void DropdownValueChanged(Dropdown change)
    {
        file.Deserializar();
        updatePrices();
    }
}
