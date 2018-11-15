using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    /*Creo instancia static*/
    public static UIScript actualUI;

    /*Variables que se guardarán*/
    private Data file;
    private AnimationScript anim_script;

    public static int total_jam;
    public static int lot_prod;
    public static int lot_mat;
    public static int money;
    public static int day;

    public GameObject client;
    
    public GameObject soundON;
    public GameObject soundOFF;
    public GameObject FXON;
    public GameObject FXOFF;
    public GameObject intro;
    public GameObject popup_machine;
    public GameObject popup_stock;
    public GameObject popup_exit;
    public GameObject popup_message;
    public GameObject popup_credits;

    public GameObject main_scene;
    public GameObject obfuscated;
    public GameObject stock_scene;
    public GameObject purchase_scene;
    public GameObject tax_scene;
    public GameObject flow_scene;
    public GameObject cost_scene;

    public AudioClip ok;
    public AudioClip fail;

    public Text lot_produce;
    public Text lot_top;
    public Text lot_tag;
    public Text lot_jar;
    public Text lot_strawberry;
    public Text lot_sugar;
    public Text day_text;
    public Text temp_text;
    public Text money_text;
    public Text message_text;
    public Text total_jam_text;
    public Text total_jam_text_gui;
    public Text total_top;
    public Text total_tag;
    public Text total_jar;
    public Text total_jar_gui;
    public Text total_strawberry;
    public Text total_sugar;
    public Text help_text;

    public RectTransform bar_money;

    public Animator options;

    int count_options = 0; 
    int count_machine = 0;
    int count_stock = 0;
    int count_intro = 0;
    int number_screen = 50000;
    int time_wait_mge = 0;
    float temp = 0f;

    private IEnumerator coroutine;

    const string
    animOptions = "options"
    ;
    //Se puede producir?
    bool check_produce = true;
    //Hay algun mensaje en help?
    bool check_help = false;
    //Se puede pasar de día?, el primero si
    bool avaiable_next = true;
    //Se completaron las planillas? se deben completar + se tiene que poder pasar de día
    bool spreadsheet = false;
    //Se puede comprar? se debe esperar primero que salgan las cartas del día, el primer día está liberado
    bool purchase = true;
    //se puede poner frascos en la cinta? cada día se eliminan de la cinta
    private bool jam = true;

    private void Start()
    {

        intro.SetActive(true);

        actualUI = this;

        //game = new Game();
        //game = Game.current;

        anim_script = new AnimationScript();
        anim_script = AnimationScript.instance;

        file = new Data();
        file.Deserializar();

        total_jam = Game.current.Total_jam;
        lot_prod = Game.current.Lot_prod;
        lot_mat = Game.current.Lot_mat;
        money = Game.current.Money;
        day = Game.current.Day;

        temp = Game.current.t_day;

        UpdateUIExt();

        /*++++++++++++++se setean los iconos de sonidos segun preferencias++++++++++*/
        if (PlayerPrefs.GetFloat("sound", 0) == 0)
        {
            soundON.gameObject.SetActive(false);
            soundOFF.gameObject.SetActive(true);
        }
        else
        {
            soundON.gameObject.SetActive(true);
            soundOFF.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetFloat("fx", 0) == 0)
        {
            FXON.gameObject.SetActive(false);
            FXOFF.gameObject.SetActive(true);
        }
        else
        {
            FXOFF.gameObject.SetActive(false);
            FXON.gameObject.SetActive(true);
        }
        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
    }
    
    private void Update()
    {
        UpdateUITemp();
        if (Convert.ToBoolean(PlayerPrefs.GetFloat("sound", 1)))
        {
            this.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            this.GetComponent<AudioSource>().mute = true;
        }
    }
    
    //Update desde afuera
    public void UpdateUIExt()
    {
        /*Actualiza calendario día*/
        string fill = day < 10? "0" : "";
        day_text.text = "Día " + fill + day.ToString();

        /*Controlo si debe sumarse dinero, producto o materia*/
        Check_Amounts();

        /*Cuando se necesita actualizar pantalla desde otro script*/
        total_jam = Game.current.Total_jam;
        lot_mat = Game.current.Lot_mat;
        money = Game.current.Money;

        //Actualizo Stock
        UpdateUIStock();

        /*Guardo cualquier cambio*/
        Save();

        UpdateUI();
    }
    //Update Stock
    public void UpdateUIStock()
    {
        /*Actualiza pop-up de estanterías stock*/
        total_top.text = lot_mat.ToString();
        total_tag.text = lot_mat.ToString();
        total_jar.text = lot_mat.ToString();
        total_jar_gui.text = lot_mat.ToString();
        total_sugar.text = lot_mat.ToString();
        total_strawberry.text = lot_mat.ToString();

        total_jam_text.text = Game.current.Total_jam.ToString();
        total_jam_text_gui.text = Game.current.Total_jam.ToString();
    }
    //Actualizar UI
    public void UpdateUI()
    {
        /*Actualiza pop-up de máquina*/
        lot_produce.text = lot_prod.ToString();
        lot_top.text = lot_mat.ToString();
        lot_tag.text = lot_mat.ToString();
        lot_jar.text = lot_mat.ToString();
        lot_sugar.text = lot_mat.ToString();
        lot_strawberry.text = lot_mat.ToString();

        /*Actualiza Dinero en pantalla*/
        if(money.ToString() != money_text.text)
            StartCoroutine(UpdateUIMoney());
    }
    //Devuelve el dinero a mostrar con el formato correcto
    string UIMoney(int mny)
    {
        int pad;
        string amount_text;
        string final_amount;

        //Cuando es menor de 0 debe llevar el "-" y luego llenarse con 0
        if (mny < 0)
        {
            pad = 4 - (Math.Abs(mny).ToString().Length);
            amount_text = new String('0', pad);
            final_amount = "-" + amount_text + Math.Abs(mny).ToString();
        }
        else
        {
            pad = 5 - (mny.ToString().Length);
            amount_text = new String('0', pad);
            final_amount = amount_text + mny.ToString();
        }

        return final_amount;
    }
    //Update Money
    public IEnumerator UpdateUIMoney()
    {
        int diff = 111;

        //Esperamos un segundo para que el usuario pueda aprovechar a ver la animación
        yield return new WaitForSeconds(1);

        //Realiza la animación de suma o resta a lo antiguo
        while (number_screen != money)
        {
            //diff cambia la diferencia de 111 a 1 para hacer más rápida y precisa la animación
            if (Math.Abs(money - number_screen) < 111)
            {
                diff = 1;
            }
            yield return new WaitForSeconds(0.0001f);
            if (money > number_screen)
            {
                number_screen+=diff;
            }
            else
            {
                number_screen -=diff;
            }

            money_text.text = UIMoney(number_screen);
            UpdateUIBarMoney(number_screen);
        }
        //Valor final
        money_text.text = UIMoney(money);
        UpdateUIBarMoney(money);
    }
    //Update Bar
    public void UpdateUIBarMoney(int sum)
    {
        //Dividimos money por 1000 para tener un número entra -10 y 50
        //Restamos -40 xq x va de -50 a 10
        //Tener en cuenta que el valor máximo es 100 y el mínimo -10
        //Calculamos valores entre 0 a 110
        int qty_mny = (sum / 1000) + 10;
        //Calculamos el porcentaje que representa la cantidad de dinero
        int perc_mny = (qty_mny * 100) / 110;
        //Calculamos las unidades que le corresponde ese dinero
        int unit_bar = (perc_mny * 60) / 100;
        //Calculamos esa cantidad en el rango específico de -50 a 10
        int rest = unit_bar - 50;
        
        //x= -50 a 10
        bar_money.anchoredPosition = new Vector2(rest, bar_money.anchoredPosition.y);
    }
    //UpdateTiempo
    public void UpdateUITemp()
    {
        //Cuando es vacaciones el tiempo es 0
        if (temp >= 0 && !AnimationScript.Is_holiday)
        {
            temp -= Time.deltaTime;
            temp_text.text = temp.ToString("f0");
        }
        else
        {
            temp_text.text = "0";
        }
    }
    //Actualiza los valores a mostrar si corresponde el día
    public void Check_Amounts()
    {
        //Por cada elemento en las listas se revisa si el día corresponde y si no se registró ya el movimiento
        foreach (int[] mat in Game.current.Mat_stock)
        {
            if (mat[0] == day && mat[2] == 1)
            {
                Game.current.Lot_mat += mat[1];
                mat[2] = 0;
                mat[3] = 0;
                mat[4] = Game.current.Lot_mat;
                HelpText("Nuevo movimiento de materia prima:  " + mat[1].ToString(), Game.current.t_go);
            }
        }
        foreach (int[] prod in Game.current.Prod_stock)
        {
            if (prod[0] == day && prod[2] == 1)
            {
                prod[2] = 0;
                Game.current.Total_jam += prod[1];
                HelpText("Nuevo movimiento de mermeladas:  " + prod[1].ToString(), Game.current.t_go);
            }
        }
        foreach (int[] money in Game.current.Money_amount)
        {
            if (money[0] == day && money[2] == 1)
            {
                money[2] = 0;
                Game.current.Money += money[1];
                HelpText("Nuevo movimiento de dinero en caja:  " + money[1].ToString(), Game.current.t_go);
            }
        }
    }
    //Serializa los datos en un archivo
    public void Save()
    {
        //Confirmado se actualizan los datos en el archivo
        file.Serializar(Game.current);
    }
    
    //Escenas
    public void MainMenu()
    {
        //Se elimina el progreso
        file.DeleteFile();
        SceneManager.LoadScene("MainMenu");
    }
    //public void InGame()
    //{
    //    SceneManager.LoadScene("InGame");
    //}
    
    //Activar diferentes interfaces
    public void Play()
    {
        DisablePopUps();
        DisableSpriteRdrs();
        obfuscated.SetActive(false);
        popup_exit.SetActive(false);
        popup_message.SetActive(false);
        intro.SetActive(false);
        main_scene.SetActive(true);
    }
    //Cuando Acepta las planillas se puede pasar de día.
    public void SheetOk()
    {
        Play();
        spreadsheet = true;
    }
    public void sheetStockOk()
    {
        bool spreadsheet_ok = true;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Canvas_stk_ficha"))
        {
            /*se borran todas las fichas anteriores antes de crearlas*/
            //Destroy(go);

            propiedades_ficha ficha = go.GetComponent<propiedades_ficha>();
            if (ficha.X_dest != ficha.X_actual || ficha.Y_dest != ficha.Y_actual)
                spreadsheet_ok = false;
        }

        if(spreadsheet_ok)
        {
            SoundPlayOK();
            SheetOk();
        }
        else
        {
            SoundPlayFail();
            spreadsheet = false;
        }
    }
    public void Stock()
    {
        DisablePopUps();
        //Si no está habilitado no puede pasar de día
        if (avaiable_next)
        {
            DisableSpriteRdrs();
            stock_scene.SetActive(true);
        }
        else
        {
            GameObject.Find("btn_sheet").GetComponent<Button>().interactable = false;
            HelpText("Al final del día se completan las planillas.", Game.current.t_go);
        }
    }
    public void Cost()
    {
        DisableSpriteRdrs();
        cost_scene.SetActive(true);
    }
    public void Flow()
    {
        DisableSpriteRdrs();
        flow_scene.SetActive(true);
    }
    public void Flow02()
    {
        DisableSpriteRdrs();
    }
    public void Purchase()
    {
        DisablePopUps();
        if (purchase)
        {
            DisableSpriteRdrs();
            purchase_scene.SetActive(true);
        }
        else
        {
            GameObject.Find("btn_store").GetComponent<Button>().interactable = false;
            HelpText("El proveedor debe enviar los precios del día.", Game.current.t_go);
        }
    }
    public void Tax()
    {
        DisablePopUps();
        DisableSpriteRdrs();
        tax_scene.SetActive(true);
    }
    public void HelpMain()
    {
    }
    private void DisableSpriteRdrs()
    {
        main_scene.SetActive(false);
        obfuscated.SetActive(true);
        purchase_scene.SetActive(false);
        stock_scene.SetActive(false);
        tax_scene.SetActive(false);
        flow_scene.SetActive(false);
        cost_scene.SetActive(false);
    }
    
    //Pup-ups interactuables
    public void NextDay()
    {

        DisablePopUps();

        string message;

        //Si estamos de vacaciones se habilita para poder pasar de día
        if (AnimationScript.Is_holiday && spreadsheet)
        {
            avaiable_next = true;
            AnimationScript.Is_holiday = false;
        }

        //Cuando está habilitado pasar de día y se completó las planillas
        if (avaiable_next && spreadsheet)
        {
            //Habilita el botón producir
            temp = Game.current.t_day;
            time_wait_mge = 0;
            avaiable_next = false;
            spreadsheet = false;
            purchase = false;
            check_help = false;
            day++;

            //Mensaje del día
            HelpText("Bienvenido al día: " + day.ToString() + ", no te olvides de completar...", Game.current.t_load);
            HelpText(" las planillas antes de pasar al siguiente día.", Game.current.t_load);

            //Actualizo día en Game
            Game.current.Day = day;

            //Se habilita para producir cuando se pasa de día
            Avaiable_Produce();

            //Actualiza toda la UI (desencadena los demás update y guarda los datos correspondientes)
            UpdateUIExt();
            
            //Un día mínimo dura tantos segundos
            StartCoroutine(Denegated(temp, "day"));


            //El método Sale dice la carta aleatoria que te toca
            message = Game.current.Sale();
            StartCoroutine(Events(message,Game.current.t_load,true, message_text));

            //Si pasan 18 segundos sin hacer nada se desactiva
            StartCoroutine(Events("", Game.current.t_close, false, message_text));

            //Se chequea si hay un suceso en el día
            message = Game.current.CheckEventDay(day);
            StartCoroutine(Events(message, Game.current.t_event, true, message_text));

            //Si no hay evento especial (precios en compras)
            float temp_purch = message == "" ? 18 : 30;

            //Para comprar debemos esperar el evento del proveedor si hay
            StartCoroutine(Denegated(temp_purch, "purchase"));
        }
        else if (!spreadsheet)
        {
            HelpText("Debe completar las planillas primero.", Game.current.t_load);
        }
        else if (!avaiable_next)
        {
            HelpText("Debe esperar para pasar al siguiente día.", Game.current.t_load);
        }
    }
    public void Avaiable_Produce()
    {
        //Para estar habilitado a producir deben actualizarse los siguientes valores
        check_produce = true;
        jam = true;
        lot_prod = 0;
        Game.current.Lot_prod = lot_prod;
        anim_script.ClearJam();
    }
    public void Produce()
    {

        DisablePopUps();
        if (lot_prod > 0 && !AnimationScript.Is_holiday && check_produce)
        {
            //Una vez que confirma producir se actualiza stock y desactiva poner más a producir
            check_produce = false;

            /*se agrega el movimiento de stock de producto terminado y materia prima*/
            //Se agrega a la lista el stock a descontar
            int[] array2 = new int[] { day, (lot_prod * (-1)), 1, 0, Game.current.Lot_mat - (lot_prod) };
            Game.current.Mat_stock.Add(array2);
            //Se agrega los productos terminados
            int[] array1 = new int[] { day, lot_prod, 1, 0 };
            Game.current.Prod_stock.Add(array1);

            /*Actualiza variables en Game para serializar luego*/
            Game.current.Lot_prod = lot_prod;

            //Actualizo Game con las listas
            Check_Amounts();

            //Actualizo Stock
            UpdateUIStock();

            Save();

            //Esta corutina hace que empiecen a salir mermeladas de la maquina
            anim_script.StartAnimationJar(3, jam, 3);

            jam = false;
        }
        else if (AnimationScript.Is_holiday)
        {
            GameObject.Find("produce").GetComponent<Button>().interactable = false;
            HelpText("No puede producir cuando no es un día laboral.", Game.current.t_go);
        }
        else if (!check_produce)
        {
            GameObject.Find("produce").GetComponent<Button>().interactable = false;
            HelpText("Debe esperar que termine de producir la tanda actual.", Game.current.t_go);
        }
    }
    public void LotProduceMinus()
    {
        if (lot_mat + 10 <= Game.current.Lot_mat && check_produce == true)
        {
            lot_mat += 10;
            lot_prod -= 10;
            UpdateUI();
        }
        else
        {
            return;
        }

    }
    public void LotProducePlus()
    {
        if (lot_mat - 10 >= 0 && check_produce == true && lot_prod <=40)
        {
            lot_mat -= 10;
            lot_prod += 10;
            UpdateUI();
        }
        else if (!check_produce)
        {
            return;
        }
        else if (lot_mat - 10 <= 0)
        {
            HelpText("No tiene la cantidad suficiente de materia prima.", Game.current.t_go);
            return;
        }
    }

    //Mensaje al usuario
    public void HelpText(string mge, int t_wait)
    {
        //Text aux_txt = Instantiate(help_text);
        //aux_txt.text = mge;
        //aux_txt.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        //Destroy(aux_txt.gameObject, game.t_close);
        time_wait_mge += t_wait;
        if (!check_help)
        {
            check_help = true;
            help_text.text = mge;
        }
        else
        {
            //StartCoroutine(Events("", time_wait_mge, false, help_text));
            StartCoroutine(Events(mge, time_wait_mge, true, help_text));
        }

    }
    private void Message(string mg, bool act, Text textUI)
    {
        //Si el objeto Canvas Text no es el de ayuda es el de las cartas
        if (textUI != help_text)
        {
            if (mg == "")
            {
                act = false;
                Game.current.Choice(act);
            }
            obfuscated.SetActive(act);
            popup_message.SetActive(act);
        }

        //Cuando es el de ayuda se muestra el mensaje en el de ayuda
        textUI.text = mg;
        time_wait_mge -= Game.current.t_go;
    }
    public void Acept()
    {
        Game.current.Choice(true);
    }
    public void Denegate()
    {
        Game.current.Choice(false);
    }
    
    //Funciones de botones
    public void DisablePopUps ()
    {
        popup_stock.SetActive(false);
        count_stock = 0;
        popup_machine.SetActive(false);
        count_machine = 0;

    }
    public void MachinePopUp()
    {
        if (count_machine % 2 == 0)
        {
            //Mostrar opciones
            DisablePopUps();
            popup_machine.SetActive(true);
            if (!check_produce)
            {
                GameObject.Find("produce").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("produce").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            //Esconder opciones
            popup_machine.SetActive(false);
        }
        count_machine++;
    }
    public void StockPopUp()
    {
        if (count_stock % 2 == 0)
        {
            //Mostrar opciones
            DisablePopUps();
            popup_stock.SetActive(true);
        }
        else
        {
            //Esconder opciones
            popup_stock.SetActive(false);
        }
        count_stock++;
    }
    public void OptionsInGame()
    {
        if (count_options % 2 == 0)
        {
            //Mostrar opciones
            DisablePopUps();
            options.SetBool(animOptions, true);
        }
        else
        {
            //Esconder opciones
            options.SetBool(animOptions, false);
        }
        count_options++;
    }
    public void Sound()
    {
        if (PlayerPrefs.GetFloat("sound", 0) == 0)
        {
            soundON.gameObject.SetActive(true);
            soundOFF.gameObject.SetActive(false);
            PlayerPrefs.SetFloat("sound", 1);
        }
        else
        {
            soundON.gameObject.SetActive(false);
            soundOFF.gameObject.SetActive(true);
            PlayerPrefs.SetFloat("sound", 0);
        }
    }
    public void FX()
    {
        if (PlayerPrefs.GetFloat("fx", 0) == 0)
        {
            FXON.gameObject.SetActive(true);
            FXOFF.gameObject.SetActive(false);
            PlayerPrefs.SetFloat("fx", 1);
        }
        else
        {
            FXOFF.gameObject.SetActive(true);
            FXON.gameObject.SetActive(false);
            PlayerPrefs.SetFloat("fx", 0);
        }
    }
    public void SoundPlayOK()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetFloat("fx", 1)))
            AudioSource.PlayClipAtPoint(ok, Camera.main.transform.position);
    }
    public void SoundPlayFail()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetFloat("fx", 1)))
            AudioSource.PlayClipAtPoint(fail, Camera.main.transform.position);
    }
    public void Exit()
    {
        DisablePopUps();
        obfuscated.SetActive(true);
        popup_exit.SetActive(true);
    }
    public void Holiday()
    {
        //para que no pueda seleccionar para producir
        check_produce = false;
        //es feriado, se avisa en el texto de ayuda
        HelpText("Los días feriados no se puede producir", Game.current.t_go);
    }
    public void EnableIntro(string name)
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (go.name == name)
            {
                go.SetActive(true);
                break;
            }
        }
    }

    public void NextIntro()
    {
        switch (count_intro)
        {
            case 0:
                GameObject.Find("intro_money").SetActive(false);
                EnableIntro("intro_day");
                count_intro++;
                break;
            case 1:
                GameObject.Find("intro_day").SetActive(false);
                EnableIntro("intro_purchase");
                count_intro++;
                break;
            case 2:
                GameObject.Find("intro_purchase").SetActive(false);
                EnableIntro("intro_sheet");
                count_intro++;
                break;
            case 3:
                GameObject.Find("intro_sheet").SetActive(false);
                EnableIntro("intro_help_text");
                count_intro++;
                break;
            case 4:
                GameObject.Find("intro_help_text").SetActive(false);
                EnableIntro("intro_jar");
                count_intro++;
                break;
            case 5:
                GameObject.Find("intro_jar").SetActive(false);
                EnableIntro("intro_jam");
                count_intro++;
                break;
            case 6:
                GameObject.Find("intro_jam").SetActive(false);
                EnableIntro("intro_machine");
                count_intro++;
                break;
            case 7:
                GameObject.Find("intro_machine").SetActive(false);
                EnableIntro("intro_next_day");
                count_intro++;
                break;
            case 8:
                GameObject.Find("intro_next_day").SetActive(false);
                EnableIntro("intro_stock");
                count_intro++;
                break;
            case 9:
                GameObject.Find("intro_stock").SetActive(false);
                EnableIntro("intro_options");
                count_intro++;
                break;
            case 10:
                GameObject.Find("intro_options").SetActive(false);
                EnableIntro("intro_exit");
                count_intro++;
                break;
            default:
                Play();
                count_intro = 0;
                break;
        }
    }
    
    //Créditos final
    public void FinalAnimation()
    {
        DisableSpriteRdrs();
        DisablePopUps();
        main_scene.SetActive(true);
        popup_exit.SetActive(false);
        popup_message.SetActive(false);
        help_text.enabled = false;
        popup_credits.SetActive(true);
    }
    
    //Agrega tiempos entre eventos y animaciones
    public IEnumerator Denegated(float time, string var)
    {
        yield return new WaitForSeconds(time);
        if (var == "day")
        {
            avaiable_next = true;
            GameObject.Find("btn_sheet").GetComponent<Button>().interactable = true;
            HelpText("Debe completar las planillas del día. Ya están habilitadas.", Game.current.t_go);
        }
        else if(var == "purchase")
        {
            purchase = true;
            GameObject.Find("btn_store").GetComponent<Button>().interactable = true;
            HelpText("Se habilitaron las compras del día.", Game.current.t_go);
        }
        else if (var == "end")
        {
            FinalAnimation();
        }
        //else if(var == "welcome")
        //{
        //    GameObject.Find(var).SetActive(false);
        //}
    }
    public IEnumerator Events(string mg, float time, bool act, Text textUI)
    {        
        //Primero tiempo en segundos para que aparezca el mensaje de las cartas
        yield return new WaitForSeconds(time);
        //Luego se muestra el mensaje
        Message(mg, act, textUI);
    }
    public IEnumerator LoadEvent(Game.OrganizeEvents e, float time, int x)
    {
        //Primero tiempo en segundos para que aparezca el mensaje de las cartas
        yield return new WaitForSeconds(time);

        //Invoco la función solicitada luego de cierto tiempo
        e.Invoke(x);
    }
}       
