using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LinesMov : MonoBehaviour
{

    private Data file;
    private Image row;

    float y_row_pos = -9;
    int i = 0;
    float pos_x = 0;

    //Se guardan las filas de stock
    private static List<Image> row_stock = new List<Image>();
    public static List<Image> Row_stock
    {
        get
        {
            return row_stock;
        }

        set
        {
            row_stock = value;
        }
    }

    public void Up()
    {
        if (Row_stock[0].GetComponent<RectTransform>().anchoredPosition.y > -9)
        {
            Vector3 destination;
            RectTransform rt;

            foreach (Image row in Row_stock)
            {
                rt = row.GetComponent<RectTransform>();
                float act_pos_y = rt.anchoredPosition.y;
                destination = new Vector3(0, act_pos_y - 20f, 0);
                row.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, destination.y);
            }
        }
    }

    public void Down()
    {
        if(Row_stock[Row_stock.Count - 1].GetComponent<RectTransform>().anchoredPosition.y < -210)
        {
            Vector3 destination;
            RectTransform rt;

            foreach (Image row in Row_stock)
            {
                rt = row.GetComponent<RectTransform>();
                float act_pos_y = rt.anchoredPosition.y;
                destination = new Vector3(0, act_pos_y + 20f, 0);
                row.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, destination.y);
            }
        }
    }

    private void OnEnable()
    {
        foreach (int[] mat in Game.current.Mat_stock)
        {
            if (mat[3] == 0)
            {
                mat[3] = 1;

                /*Se calcula el saldo*/
                int saldo = mat[4]; 

                /*se instancia la fila*/
                if (i % 2 == 0)
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/stock_row01"));
                }
                else
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/stock_row02"));
                }

                row.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, y_row_pos);
                row.transform.SetParent(GameObject.Find("visible_zone").transform, false);

                Button btn = Resources.Load<Button>("Prefabs/ficha_stock");
                btn.transform.Find("Text").GetComponent<Text>().text = mat[1].ToString();

                Button lbl = Instantiate(btn, row.transform) as Button;

                /*se setean las posiciones de la ficha*/
                lbl.GetComponent<propiedades_ficha>().X_pos = lbl.GetComponent<RectTransform>().anchoredPosition.x;
                lbl.GetComponent<propiedades_ficha>().Y_pos = lbl.GetComponent<RectTransform>().anchoredPosition.y;

                if (mat[1] < 0)
                {
                    pos_x = 70;
                }
                else
                {
                    pos_x = -20;
                }
                lbl.GetComponent<propiedades_ficha>().X_dest = pos_x;
                lbl.GetComponent<propiedades_ficha>().Y_dest = lbl.GetComponent<propiedades_ficha>().Y_pos;

                /*se instancia el nro del dia*/
                Text txt_dia = Instantiate(Resources.Load<Text>("Prefabs/nro_dia_planillas"), row.transform);
                txt_dia.text = mat[0].ToString();
                /*---------------------------*/

                /*se instancia el texto de saldo*/
                Text txt_saldo = Instantiate(Resources.Load<Text>("Prefabs/nro_saldo"), row.transform);
                txt_saldo.text = saldo.ToString();

                Row_stock.Add(row);

                i++;
                y_row_pos -= 20f;
            }
            
        }
        UIScript.actualUI.Save();
    }
}
