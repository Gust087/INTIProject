using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //importante!! sino no salva nada!!!
public class Game
{
    //Delegado
    public delegate void OrganizeEvents(int c);
    public OrganizeEvents callback;

    //Instancias
    public static Game current;
    //private UIScript UI = UIScript.actualUI; // Problemas para serializar debería dejarse fuera

    //Todas las variables a grabar deben crearse Get y Set
    private int lot_prod = 0;
    private int lot_mat = 500;
    private int total_jam = 500;
    private int money = 50000;
    private int day = 1;

    //Variables a utilizar para las ventas
    int qj = 0;
    int qd = 0;
    int price;
    int cash_price;
    string message;
    bool total_sale;
    bool is_tax = false;
    bool is_sale = false;
    bool is_event = false;
    bool is_game_over = false;

    //Variables para animaciones (tiempos)
    public int t_load = 2;
    public int t_go = 5;
    public int t_close = 18;
    public int t_event = 22;
    public int t_day = 40;

    //Todas estas convinaciones se sacan del excel Logica del Juego
    private int[] fixed_day = new int[] { 3, 5, 7, 11, 15, 17, 20 };
    private string[] fixed_events = new string[] {
        "Hoy se pagan los impuestos y servicios (luz, agua, gas y tasa).",
        "Hoy se paga el sueldo al operario.",
        "Hoy se pagan los honorarios del contador.",
        "Hoy se paga el alquiler del local.",
        "Hoy se pagan las cargas sociales.",
        "Hoy se pagan los gastos de publicidad.",
        "Hoy se debe completar la planilla Estado de Rdo. y analizar si el dueño puede retirar o no beneficios. " +
        "El dueño debe retirar $30.000 (puede retrasarse 1 mes, pero luego retirará 60 mil)."
    };
    /*Las cuentas se corresponden con el array type_account, en el caso de impuestos va 5 xq luego se divide en 4 y en el caso que no tiene movimiento de diner va 6*/
    private int[] fixed_type_account = new int[] { 5, 2, 3, 2, 2, 4, 3 };
    private int[] events_type_account = new int[] { 2, 6, 2, 2, 2, 2, 2, 2, 6, 2, 0 };

    private int[] amount_fixed_events = new int[] { -5000, -8000, -8000, -7500, -1200, -1500, -30000 };
    private string[] events = new string[] {
        "Reparar máquina $5000",
        "Cliente consulta y no compra",
        "Empleado pide adelanto $6000",
        "Accidente laboral $15000",
        "Pintar el local $18000",
        "Cambiar cubiertas de transporte $5000",
        "Multa de seguridad e higiene $2000",
        "Paga seguro contra incendios $1200",
        "Reparar instalaciones $7500",
        "Feriado no se trabaja",
        "Reformas en el local $8000",
        "Vende maquinaria en desuso gana $7500"//Tener en cuenta que es positiva esta carta
    };
    private int[] amount_events = new int[] { -5000, 0, -6000, -15000, -18000, -5000, -2000, -7500, 0, -8000, 7500 };
    /*Cuentas de los eventos*/
    public int[] events_accounts = new int[] { 10, 21, 7, 13, 11, 12, 14, 15, 16, 21, 17, 0 };
    public int[] fixed_accounts = new int[] { 3, 8, 18, 9, 5, 20, 19 };

    private string[] random_events = new string[] {
        "Aumentos del precio de materia prima por el día (aumenta todo el 100%)",
        "Descuentos del precio de materia prima por el día (todo al 80%)",
        "Oferta materia prima 2x1 (todo al 50%)"
    };
    private string[] sales_text = new string[] {
        "Un cliente quiere comprar ",
        " frascos de mermelada ",
        "paga ",
        "al contado",
        "en ",
        " días",
        " y "
    };
    private int[] quantity_sales = new int[] { 0, 5, 10, 15, 30, 50, 70, 80, 90, 100, 120, 130, 150, 180, 200, 225, 250, 300, 350, 500, 600, };

    /*Precios de venta: contado, 10 días, 15 o 30 días*/
    private int[] sale_prices = new int[] { 45, 55, 58, 65 };

    /*Precios de compra: contado, 10 días, 15 o 30 días*/
    private int[] base_purchase_prices = new int[] { 4, 6, 8, 10 };
    
    /*Evento todo al 100%*/
    private int[] max_purchase_prices = new int[] { 8, 12, 16, 20 };

    /*Evento todo 80%*/
    private int[] disc_purchase_prices = new int[] { 3, 5, 7, 8 };

    /*Evento todo 50%*/
    private int[] min_purchase_prices = new int[] { 2, 3, 4, 5};

    /*Precio actual*/
    private int[] purchase_prices = new int[] { 4, 6, 8, 10 };

    /*Formas de pago contado, 10, 15 o 30 días*/
    private int[] form_pay = new int[] { 0, 10, 15, 30 };

    /*Lista tiopos de cuentas*/
    public string[] type_account = new string[] { "Ingreso", "Costos variables", "Costo fijo producción", "Costo fijo administrativo", "Costo fijo comercial" };

    /*Lista descripción de cuentas*/
    public string[] desc_account = new string[]
    {   "Otros ingresos",
        "Venta mercadería",
        "Materia prima",
        "Energía Eléctrica",
        "Gas",
        "Cargas Sociales",
        "Agua",
        "Adelanto Sueldo",
        "Sueldo Operario",
        "Alquileres",
        "Mantenimiento maquinaria",
        "Mantenimiento local",
        "Mantenimiento transporte",
        "Accidente laboral",
        "Multa seg e hig",
        "Seguros",
        "Mantenimiento instalaciones",
        "Mejoras local",
        "Honorarios Contador",
        "Sueldo Gerenciamiento",
        "Publicidad",
        "Otros"
    };

    //Modelo de array para la lista de materiales, productos y saldos
    //Pos 0 = day, 1 = monto, 
    //2 = (es 1 true si está listo para sumarse a las planillas -> debe coincidir el día también y 0 false ya se sumó)
    //3 = bool para Ale (1 true 0 false)
    //4 = saldo actual en ese instante de tiempo

    //Se guardan registros de compras y saldos
    private List<int[]> mat_stock = new List<int[]>();
    private List<int[]> prod_stock = new List<int[]>();
    private List<int[]> money_amount = new List<int[]>();

    /*Get and Set*/
    public int Total_jam
    {
        get
        {
            return total_jam;
        }

        set
        {
            total_jam = value;
        }
    }
    public int Lot_prod
    {
        get
        {
            return lot_prod;
        }

        set
        {
            lot_prod = value;
        }
    }
    public int Lot_mat
    {
        get
        {
            return lot_mat;
        }

        set
        {
            lot_mat = value;
        }
    }
    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            if(value <= (-10000)) GameOver();
            money = value > 99999 ? 99999 : value;
        }
    }
    public int Day
    {
        get
        {
            return day;
        }

        set
        {
            if (value > 61) GameOver();
            day = value;

        }
    }
    public int[] Purchase_prices
    {
        get
        {
            return purchase_prices;
        }

        set
        {
            purchase_prices = value;
        }
    }
    public int[] Form_pay
    {
        get
        {
            return form_pay;
        }

        set
        {
            form_pay = value;
        }
    }
    public bool Is_game_over
    {
        get
        {
            return is_game_over;
        }

        set
        {
            is_game_over = value;
        }
    }
    public bool Is_tax
    {
        get
        {
            return is_tax;
        }

        set
        {
            is_tax = value;
        }
    }
    public List<int[]> Mat_stock
    {
        get
        {
            return mat_stock;
        }

        set
        {
            mat_stock = value;
        }
    }
    public List<int[]> Prod_stock
    {
        get
        {
            return prod_stock;
        }

        set
        {
            prod_stock = value;
        }
    }
    public List<int[]> Money_amount
    {
        get
        {
            return money_amount;
        }

        set
        {
            money_amount = value;
        }
    }


    /*Fin del juego*/
    public void GameOver()
    {
        string mg;
        if (day > 61)
        {
            mg = "Fin del Juego. " + Environment.NewLine +
                "Felicitaciones por haber superado el juego.";
        }
        else
        {
            mg = "Fin del Juego. " + Environment.NewLine +
                "Te fundiste.";
        }
        UIScript.actualUI.StartCoroutine(UIScript.actualUI.Events("", t_load, false, UIScript.actualUI.message_text));
        UIScript.actualUI.StartCoroutine(UIScript.actualUI.Events(mg, t_go, true, UIScript.actualUI.message_text));
        Is_game_over = true;
    }

    /*Eventos que se llaman desde Coroutine de UI*/
    public void ExtraEvents(int d)
    {
        //Se agrega el ID de tipo de cuenta
        int type_account = events_type_account[d];
        //Se agrega el ID de cuenta
        int account = events_accounts[d];

        int[] array = new int[] { Day, amount_events[d], 1, 0, type_account, account };
        Money_amount.Add(array);
        
        UIScript.actualUI.UpdateUIExt();
    }
    public void FixedEvents(int c)
    {
        Is_tax = c == 0 ? true : false;
        is_sale = false;
        is_event = true;

        int m = c == 0 ? 0 : amount_fixed_events[c];
        //Se agrega el ID de tipo de cuenta
        int type_account = fixed_type_account[c];
        //Se agrega el ID de cuenta
        int account = fixed_accounts[c];

        int[] array = new int[] { Day, m, 1, 0, type_account, account };
        Money_amount.Add(array);

        UIScript.actualUI.UpdateUIExt();
    }
    public void UpdatePrices(int opt)
    {
        is_sale = false;
        is_event = true;

        switch (opt)
        {
            case 2:
                Purchase_prices = min_purchase_prices;
                /*Cada vez que se necesita guardar algún dato este es el método encargado*/
                UIScript.actualUI.Save();
                break;
            case 1:
                Purchase_prices = disc_purchase_prices;
                UIScript.actualUI.Save();
                break;
            case 0:
                Purchase_prices = max_purchase_prices;
                UIScript.actualUI.Save();
                break;
        }

    }

    /*Decide si vendes o tenés algun evento extraordinario (tarjetas negativas) 
    * y devuelve el mensaje a mostrar de lo que sucede*/
    public string Sale()
        {
        /*Eventos extraordinarios*/
        if (UnityEngine.Random.Range(0, 5) == 3)
        {
            int ed = UnityEngine.Random.Range(0, (events.Length - 1));
            is_sale = false;
            is_event = true;
            if (ed == 9)
            {
                AnimationScript.Is_holiday = true;
                UIScript.actualUI.Holiday();
            }
            else
            {
                UIScript.actualUI.StartCoroutine(UIScript.actualUI.LoadEvent(ExtraEvents, t_load, ed));
            }
            return events[ed];
        }
        /*Ventas*/
        else
        {
            /*Cantidad de jarros aleatorios, cantidad de días a pagar aleatorios, precio contado, 
             * venta en 2 formas e pago diferentes, mensaje a mostrar en juego*/
            is_sale = true;
            total_sale = true;
            is_event = false;
            qj = UnityEngine.Random.Range(0, (quantity_sales.Length - 1));
            qd = UnityEngine.Random.Range(0, (Form_pay.Length - 1));
            cash_price = sale_prices[0];
            message = sales_text[0] + quantity_sales[qj].ToString() + sales_text[1] + ", " + sales_text[2];

            //se habilita la animacion del cliente
            //UIScript.actualUI.client.GetComponent<Animator>().SetBool("is_purchase", true);
            UIScript.actualUI.client.SetActive(true);


            /*Solo si la cantidad de jarros es mayor a 100 y la forma de pago no es al contado*/
            if (quantity_sales[qj] > 100 && qd != 0)
            {
                if (UnityEngine.Random.Range(0, 5) == 3)
                {
                    total_sale = false;
                }
            }

            /*Según la cantidad de días es un precio diferente*/
            switch (Form_pay[qd])
            {
                case 10:
                    price = sale_prices[1];
                    break;
                case 15:
                    price = sale_prices[2];
                    break;
                case 30:
                    price = sale_prices[3];
                    break;
                default:
                    price = cash_price;
                    break;
            }

            /*Si es un pago dividido en 2*/
            if (!total_sale)
            {
                return message += (quantity_sales[qj] / 2).ToString() + " " + sales_text[3] + sales_text[6]
                    + (quantity_sales[qj] / 2).ToString() + " " + sales_text[4] + Form_pay[qd] + sales_text[5];
            }
            /*Si es una compra de 0 producto es una consulta*/
            else if (quantity_sales[qj] == 0)
            {
                return "Un cliente consulta y se va sin comprar.";
            }
            /*Si es una compra al contado es un mensaje diferente*/
            else if (Form_pay[qd] == 0)
            {
                return message += sales_text[3];
            }
            /*Si es una compra a N días el mensaje*/
            else
            {
                return message += sales_text[4] + Form_pay[qd].ToString() + sales_text[5];
            }
            /* Convinaciones posibles
            0,QJ,1,2,3
            0,QJ,1,2,4,QD,5
            0,QJ,1,2,QJ/2,3,6,QJ/2,4,QD,5
            */

        }
    }
    
    /*Efectivizo la compra del cliente*/
    public void Choice(bool yn)
    {
        /*Para los eventos, cuando es impuesto abre su propia interfaz*/
        if (!is_sale)
        {
            if (Is_tax)
            {
                Is_tax = false;
                UIScript.actualUI.Tax();
            }
            else if (Is_game_over)
            {
                UIScript.actualUI.StartCoroutine(UIScript.actualUI.Denegated(t_go,"end"));
            }
            else
            {
                UIScript.actualUI.Play();
            }
        }
        else
        {
            //se deshabilita la animacion del cliente
            //UIScript.actualUI.client.GetComponent<Animator>().SetBool("is_purchase", false);
            UIScript.actualUI.client.SetActive(false);

            is_sale = false;
            if (Total_jam - quantity_sales[qj] < 0 || yn == false)
            {
                UIScript.actualUI.Play();
            }
            else
            {
                /*Si es una compra con el pago dividido en 2*/
                if (!total_sale)
                {
                    //Se agrega el monto y 1 porque se tiene que descontar
                    //Se agrega 0 para que entre en la planilla costos
                    //Se agrega el último 0 que es el ID del tipo de cuenta Ingresos
                    //Se agrega 1 que es el ID de la descripción de la cuenta Ventas
                    int[] array = new int[] { Day , ((quantity_sales[qj] / 2) * price), 1, 0, 0, 1 };
                    Money_amount.Add(array);

                    //Se debe controlar los movimientos a futuro, se pone el 2 xq todavía no va en Costos
                    //Se agrega el último 0 que es el ID del tipo de cuenta Ingresos
                    //Se agrega 1 que es el ID de la descripción de la cuenta Ventas
                    int[] array1 = new int[] { (Day + Form_pay[qd]), ((quantity_sales[qj] / 2) * price), 1, 2, 0, 1 };
                    Money_amount.Add(array1);

                    //Se agrega a la lista el stock a descontar
                    int[] array2 = new int[] { Day, (quantity_sales[qj] * (-1)), 1, 0 };
                    Prod_stock.Add(array2);
                }
                /*Si es una compra de 0 producto es una consulta*/
                else if (quantity_sales[qj] == 0)
                {
                    UIScript.actualUI.Play();
                }
                /*Si es una compra al contado*/
                else if (Form_pay[qd] == 0)
                {
                    //Se pone monto, 1 xq hay que registrarlo, 0 porque entra en la planilla de costos
                    //Se agrega el último 0 que es el ID del tipo de cuenta Ingresos
                    //Se agrega 1 que es el ID de la descripción de la cuenta Ventas
                    int[] array1 = new int[] { Day, ((quantity_sales[qj]) * price), 1, 0, 0, 1 };
                    Money_amount.Add(array1);

                    //Se agrega a la lista el stock a descontar
                    int[] array2 = new int[] { Day, (quantity_sales[qj] * (-1)), 1, 0 };
                    Prod_stock.Add(array2);
                }
                /*Si es una compra a N días*/
                else
                {
                    //Se agrega monto, 1 porque está listo para descontar, 2 xq es un movimiento a futuro
                    //Se agrega el último 0 que es el ID del tipo de cuenta Ingresos
                    //Se agrega 1 que es el ID de la descripción de la cuenta Ventas
                    int[] array1 = new int[] { (Day + Form_pay[qd]), ((quantity_sales[qj]) * price), 1, 2, 0, 1 };
                    Money_amount.Add(array1);

                    //Se agrega a la lista el stock a descontar
                    int[] array2 = new int[] { Day, (quantity_sales[qj] * (-1)), 1, 0 };
                    Prod_stock.Add(array2);
                }
            }
            /*Actualizo UI y quito el cartel*/
            UIScript.actualUI.UpdateUIExt();
            UIScript.actualUI.Play();
        }
    }

    /*Se controla el día para los eventos fijos*/
    public string CheckEventDay(int d)
    {
            /*Cuando estamos en el segundo mes debemos calcular los días fijos*/
            d = d > 30 ? (d - 30) : d;

            //file = new Data();
            int count = 0;
            int a = UnityEngine.Random.Range(0, 4);
            /*Actualizo el precio de compra al base*/
            Purchase_prices = base_purchase_prices;

            /*Primero revisamos los eventos fijos*/
            foreach (int day in fixed_day)
            {
                if (day == d)
                {
                    UIScript.actualUI.StartCoroutine(UIScript.actualUI.LoadEvent(FixedEvents, t_event, count));
                    return fixed_events[count];
                }
                count++;
            }

            /*Si no hay eventos fijos, hay 20% de probabilidad que salga una extraordinario*/
            if (a == 3)
            {
                a = UnityEngine.Random.Range(0, 5);
                /*Actualizamos el precio de compra del día y  mandamos el mensaje,
                 según el evento hay más o menos probabilidad que salga*/
                if (a == 0 && !AnimationScript.Is_holiday)
                {

                    UIScript.actualUI.StartCoroutine(UIScript.actualUI.LoadEvent(UpdatePrices, t_event, 2));
                    return random_events[2];
                }
                else if (a == 1 || a == 2)
                {

                    UIScript.actualUI.StartCoroutine(UIScript.actualUI.LoadEvent(UpdatePrices, t_event, 1));
                    return random_events[1];

                }
                else if (a == 3 || a == 4 || a == 5)
                {

                    UIScript.actualUI.StartCoroutine(UIScript.actualUI.LoadEvent(UpdatePrices, t_event, 0));
                    return random_events[0];

                }
            }
            /*Puede ser que no haya eventos*/
            return "";
    }

    public Game() //constructor
    {
        current = this;
        //eventos = eventos ?? new List<Eventos>(); //si eventos es null, entonces crea una nueva lista;
    }
}
