using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Data {

    private IFormatter bf;
    private FileStream file;
    string 
        filename = "/gestiona_tu_empresa_data", 
        extencion = ".gd"
        ;
    /*Según cada plataforma generamos un path*/
    private string Save_path
    {
        get
        {
            //Application.persistentDataPath es string, contiene la direccion 
            //donde podemos crear archivos segun cada plataforma
            return Application.persistentDataPath + filename + extencion;
        }

    }

    //private void Game_Load()
    //{
    //    Deserializar();
    //}

    public void Serializar(Game game)
    {
        /*Inicializo un objeto Game*/
        //Game actualgame = new Game();
        ///*Se guardan los datos*/
        //actualgame.Lot_prod = Game.current.Lot_prod;
        //actualgame.Lot_mat = Game.current.Lot_mat;
        //actualgame.Money = Game.current.Money;
        //actualgame.Day = Game.current.Day;

        bf = new BinaryFormatter();
        //Sobreescribimos el archivo si existe
        file = File.Create(Save_path);
        

        bf.Serialize(file, game);//Cambiar por game si se quiere guardar todo
        file.Close();
    }

    public void Deserializar()
    {
        Game actualgame = new Game();
        bf = new BinaryFormatter();

        if (File.Exists(Save_path))
        {
            file = File.OpenRead(Save_path);
            actualgame = (Game)bf.Deserialize(file);

            /*Se recuperan los datos*/
            //Game.current.Purchase_prices = actualgame.Purchase_prices;
            //Game.current.Total_jam = actualgame.Total_jam;
            //Game.current.Lot_prod = actualgame.Lot_prod;
            //Game.current.Lot_mat = actualgame.Lot_mat;
            //Game.current.Money = actualgame.Money;
            //Game.current.Day = actualgame.Day;
            Game.current = actualgame;
            file.Close();
        }
        else { return; }
    }

    public void DeleteFile()
    {
        if (File.Exists(Save_path))
        {
            //Si existe el archivo lo eliminamos para resetear los valores de inicio
            File.Delete(Save_path);
        }
    }
}

