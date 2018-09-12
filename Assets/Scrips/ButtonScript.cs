﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject attention;
    public Animator stock;
    public Animator costs;
    public Animator flows;
    public Animator purchases;
    public Animator sales;
    public Animator taxes;

    const string
    animStock = "stock",
    animCost = "costos",
    animFlow = "flujos",
    animPurchase = "compras",
    animSale = "ventas",
    animTax = "impuestos"
    ;

    int count = 0;

    private void Start()
    {
    }
    public void Exit()
    {
        Exit();
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
        if (count % 2 == 0)
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
        count++;


    }   
}       
