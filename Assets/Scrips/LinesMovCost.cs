using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LinesMovCost : MonoBehaviour
{

    private Data file;
    private Image row;

    float y_row_pos = -9;
    int i = 0;
    float pos_x = 0;

    //Se guardan las filas de cost
    private static List<Image> row_cost = new List<Image>();
    public static List<Image> Row_cost
    {
        get
        {
            return row_cost;
        }

        set
        {
            row_cost = value;
        }
    }

    public void Up()
    {
        if (Row_cost[0].GetComponent<RectTransform>().anchoredPosition.y > -9)
        {
            Vector3 destination;
            RectTransform rt;

            foreach (Image row in Row_cost)
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
        if(Row_cost[Row_cost.Count - 1].GetComponent<RectTransform>().anchoredPosition.y < -210)
        {
            Vector3 destination;
            RectTransform rt;

            foreach (Image row in Row_cost)
            {
                rt = row.GetComponent<RectTransform>();
                float act_pos_y = rt.anchoredPosition.y;
                destination = new Vector3(0, act_pos_y + 20f, 0);
                row.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, destination.y);
            }
        }
    }
    private void AddRow(int[] mov)
    {
        if (mov[3] == 0)
        {
            mov[3] = 2;

            if (mov[4] == 5)
            {
                AddRow(new int[] { mov[0], -3000, 0, 0, 1, 3 });
                AddRow(new int[] { mov[0], -500, 0, 0, 1, 4 });
                AddRow(new int[] { mov[0], -1200, 0, 0, 2, 5 });
                AddRow(new int[] { mov[0], -300, 0, 2, 0, 6 });
            }
            else
            {
                /*Se calcula el saldo*/
                string cuenta = Game.current.desc_account[mov[5]];

                /*se instancia la fila*/
                if (i % 2 == 0)
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/cost_row01"));
                }
                else
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/cost_row02"));
                }

                row.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, y_row_pos);
                row.transform.SetParent(GameObject.Find("visible_zone").transform, false);

                Button btn = Resources.Load<Button>("Prefabs/ficha_cost");
                btn.transform.Find("Text").GetComponent<Text>().text = mov[1].ToString();

                Button lbl = Instantiate(btn, row.transform) as Button;

                /*se setean las posiciones de la ficha*/
                lbl.GetComponent<propiedades_ficha>().X_pos = lbl.GetComponent<RectTransform>().anchoredPosition.x;
                lbl.GetComponent<propiedades_ficha>().Y_pos = lbl.GetComponent<RectTransform>().anchoredPosition.y;

                switch (mov[4])
                {
                    case 0:
                        pos_x = -80;
                        break;
                    case 1:
                        pos_x = 20;
                        break;
                    case 2:
                        pos_x = 120;
                        break;
                    case 3:
                        pos_x = 220;
                        break;
                    case 4:
                        pos_x = 320;
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }

                lbl.GetComponent<propiedades_ficha>().X_dest = pos_x;
                lbl.GetComponent<propiedades_ficha>().Y_dest = lbl.GetComponent<propiedades_ficha>().Y_pos;

                /*se instancia el nro del dia*/
                Text txt_dia = Instantiate(Resources.Load<Text>("Prefabs/nro_dia_cost"), row.transform);
                txt_dia.text = mov[0].ToString();
                /*---------------------------*/

                /*se instancia el texto de cuentas*/
                Text txt_cuentas = Instantiate(Resources.Load<Text>("Prefabs/cuenta_cost"), row.transform);
                txt_cuentas.text = cuenta;

                Row_cost.Add(row);

                i++;
                y_row_pos -= 20f;
            }
        }
    }

    private void OnEnable()
    {
        foreach (int[] mov in Game.current.Money_amount)
        {
            AddRow(mov);
            
        }
        UIScript.actualUI.Save();
    }
}
