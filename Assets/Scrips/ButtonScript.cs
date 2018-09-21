﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject attention;
    public GameObject soundON;
    public GameObject soundOFF;
    public GameObject FXON;
    public GameObject FXOFF;
    public GameObject popup_machine;

    public Text lot_produce;
    public Text lot_top;
    public Text lot_tag;
    public Text lot_jar;
    public Text lot_strawberry;
    public Text lot_sugar;

    public Animator stock;
    public Animator costs;
    public Animator flows;
    public Animator purchases;
    public Animator sales;
    public Animator taxes;
    public Animator options;

    public static int count_pc = 0;
    public static int count_options = 0; 
    public static int count_machine = 0; 
    int count_sound = 0;
    int count_fx = 0;
    int lot_prod = 0;
    int lot_mat = 500;

    const string
    animStock = "stock",
    animCost = "costos",
    animFlow = "flujos",
    animPurchase = "compras",
    animSale = "ventas",
    animTax = "impuestos",
    animOptions = "options"
    ;

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        lot_produce.text = lot_prod.ToString();
        lot_top.text = lot_mat.ToString();
        lot_tag.text = lot_mat.ToString();
        lot_jar.text = lot_mat.ToString();
        lot_strawberry.text = lot_mat.ToString();
        lot_sugar.text = lot_mat.ToString();
    }
    public void Exit()
    {
        Exit();
    }
    public void ExitOfScreenGame()
    {
        stock.SetBool(animStock, false);
        costs.SetBool(animCost, false);
        flows.SetBool(animFlow, false);
        purchases.SetBool(animPurchase, false);
        sales.SetBool(animSale, false);
        taxes.SetBool(animTax, false);
        options.SetBool(animOptions, false);
    }
    public void Play()
    {
        SceneManager.LoadScene("InGame");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void HelpMain()
    {
    }

    public void Attention()
    {
        //Quitar globo atención
        attention.gameObject.SetActive(false);
        if (count_pc % 2 == 0)
        {
            //Mostrar botones
            stock.SetBool(animStock, true);
            costs.SetBool(animCost, true);
            flows.SetBool(animFlow, true);
            purchases.SetBool(animPurchase, true);
            sales.SetBool(animSale, true);
            taxes.SetBool(animTax, true);
        }
        else
        {
            //Esconder botones
            stock.SetBool(animStock, false);
            costs.SetBool(animCost, false);
            flows.SetBool(animFlow, false);
            purchases.SetBool(animPurchase, false);
            sales.SetBool(animSale, false);
            taxes.SetBool(animTax, false);
        }
        count_pc++;
    }
    public void LotProducePlus()
    {
        if (lot_mat - 100 >= 0)
        {
            lot_mat -= 100;
            lot_prod += 100;
            UpdateUI();
        }
        else
        {
            return;
        }
    }
    public void LotProduceMinus()
    {
        if (lot_mat + 100 <= 500)
        {
            lot_mat += 100;
            lot_prod -= 100;
            UpdateUI();
        }
        else
        {
            return;
        }

    }
    public void OptionsInGame()
    {
        if (count_options % 2 == 0)
        {
            //Mostrar opciones
            options.SetBool(animOptions, true);
        }
        else
        {
            //Esconder opciones
            options.SetBool(animOptions, false);
        }
        count_options++;
    }
    public void MachinePopUp()
    {
        if (count_machine % 2 == 0)
        {
            //Mostrar opciones
            popup_machine.SetActive(true);
        }
        else
        {
            //Esconder opciones
            popup_machine.SetActive(false);
        }
        count_machine++;
    }
    public void Stock()
    {
        SceneManager.LoadScene("Stock");
    }
    public void Cost()
    {
        SceneManager.LoadScene("Cost");
    }
    public void Flow()
    {
        SceneManager.LoadScene("Flow");
    }
    public void Purchase()
    {
        SceneManager.LoadScene("Purchase");
    }
    public void Sale()
    {
        SceneManager.LoadScene("Sale");
    }
    public void Tax()
    {
        SceneManager.LoadScene("Tax");
    } 
    public void Sound()
    {
        if (count_sound % 2 == 0)
        {
            //Mostrar OFF
            soundON.gameObject.SetActive(false);
            soundOFF.gameObject.SetActive(true);
        }
        else
        {
            //Mostrar ON
            soundOFF.gameObject.SetActive(false);
            soundON.gameObject.SetActive(true);
        }
        count_sound++;
    }
    public void FX()
    {
        if (count_fx % 2 == 0)
        {
            //Mostrar OFF
            FXON.gameObject.SetActive(false);
            FXOFF.gameObject.SetActive(true);
        }
        else
        {
            //Mostrar ON
            FXOFF.gameObject.SetActive(false);
            FXON.gameObject.SetActive(true);
        }
        count_fx++;
    }

}       
