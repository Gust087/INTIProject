using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentFlow : MonoBehaviour {

private Data file;
    private Image row;

    float y_row_pos = -70;
    int i = 0;
    float pos_x = 0;

    //Se guardan las filas de cost
    private static List<Image> row_flow = new List<Image>();
    public static List<Image> Row_flow
    {
        get
        {
            return row_flow;
        }

        set
        {
            row_flow = value;
        }
    }

    public void Up()
    {
        if (Row_flow[0].GetComponent<RectTransform>().anchoredPosition.y > -9)
        {
            //Muevo row ingresos
            GameObject.Find("ingresos").gameObject.GetComponent<RectTransform>().anchoredPosition
                = RowMov(GameObject.Find("ingresos").gameObject.GetComponent<RectTransform>(), -20f);

            //Muevo row otros_ing
            GameObject.Find("otros_ing").gameObject.GetComponent<RectTransform>().anchoredPosition
                = RowMov(GameObject.Find("otros_ing").gameObject.GetComponent<RectTransform>(), -20f);

            //Muevo row total_ing
            GameObject.Find("total_ing").gameObject.GetComponent<RectTransform>().anchoredPosition
                = RowMov(GameObject.Find("total_ing").gameObject.GetComponent<RectTransform>(), -20f);

            foreach (Image row in Row_flow)
            {
                row.gameObject.GetComponent<RectTransform>().anchoredPosition
                    = RowMov(row.gameObject.GetComponent<RectTransform>(), -20f);
            }
        }
    }

    public void Down()
    {
        if(Row_flow[Row_flow.Count - 1].GetComponent<RectTransform>().anchoredPosition.y < -210)
        {
            //Muevo row ingresos
            GameObject.Find("ingresos").gameObject.GetComponent<RectTransform>().anchoredPosition 
                = RowMov(GameObject.Find("ingresos").gameObject.GetComponent<RectTransform>(), 20f);

            //Muevo row otros_ing
            GameObject.Find("otros_ing").gameObject.GetComponent<RectTransform>().anchoredPosition
                = RowMov(GameObject.Find("otros_ing").gameObject.GetComponent<RectTransform>(), 20f);

            //Muevo row total_ing
            GameObject.Find("total_ing").gameObject.GetComponent<RectTransform>().anchoredPosition
                = RowMov(GameObject.Find("total_ing").gameObject.GetComponent<RectTransform>(), 20f);

            foreach (Image row in Row_flow)
            {
                row.gameObject.GetComponent<RectTransform>().anchoredPosition
                    = RowMov(row.gameObject.GetComponent<RectTransform>(), 20f);
            }
        }
    }

    private Vector2 RowMov(RectTransform rt, float mov)
    {
        Vector3 destination;

        float act_pos_y = rt.anchoredPosition.y;
        destination = new Vector3(0, act_pos_y + mov, 0);
        return new Vector2(rt.anchoredPosition.x, destination.y);
    }

    private void OnEnable()
    {
        foreach (int[] mov in Game.current.Money_amount)
        {
            if (mov[1] > 0)
            {
                
                EditRow(mov);
            }
            else
            {
                AddRow(mov);
            }
            
        }
        UIScript.actualUI.Save();
    }

    private void EditRow(int[] mov)
    {

    }

    private void AddRow(int[] mov)
    {

        if (mov[3] == 2)
        {
            mov[3] = 1;
            if (mov[4] == 5)
            {
                AddRow(new int[] {mov[0], -3000, 2, 2, 1, 3 });
                AddRow(new int[] {mov[0], -500, 2, 2, 1, 4 });
                AddRow(new int[] {mov[0], -1200, 2, 2, 2, 5 });
                AddRow(new int[] {mov[0], -300, 2, 2, 2, 6 });
            }
            else
            {
                /*Se calcula el saldo*/
                string cuenta = Game.current.desc_account[mov[5]];

                /*se instancia la fila*/
                if (i % 2 == 0)
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/flow_row01"));
                }
                else
                {
                    row = Instantiate(Resources.Load<Image>("Prefabs/flow_row02"));
                }

                row.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, y_row_pos);
                row.transform.SetParent(GameObject.Find("visible_zone").transform, false);

                Button btn = Resources.Load<Button>("Prefabs/ficha_flujo");
                btn.transform.Find("Text").GetComponent<Text>().text = mov[1].ToString();

                Button lbl = Instantiate(btn, row.transform) as Button;

                /*se setean las posiciones de la ficha*/
                lbl.GetComponent<propiedades_ficha>().X_pos = lbl.GetComponent<RectTransform>().anchoredPosition.x;
                lbl.GetComponent<propiedades_ficha>().Y_pos = lbl.GetComponent<RectTransform>().anchoredPosition.y;

                pos_x = 125;

                lbl.GetComponent<propiedades_ficha>().X_dest = pos_x;
                lbl.GetComponent<propiedades_ficha>().Y_dest = lbl.GetComponent<propiedades_ficha>().Y_pos;

                /*se instancia el nro del dia*/
                Text txt_dia = Instantiate(Resources.Load<Text>("Prefabs/type"), row.transform);
                txt_dia.text = Game.current.type_account[mov[4]];
                /*---------------------------*/

                /*se instancia el texto de cuentas*/
                Text txt_cuentas = Instantiate(Resources.Load<Text>("Prefabs/account"), row.transform);
                txt_cuentas.text = cuenta;

                Row_flow.Add(row);

                i++;
                y_row_pos -= 20f;
            }
        }
    }
}
