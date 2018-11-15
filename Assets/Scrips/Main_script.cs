using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_script : MonoBehaviour {

    public Animator options;

    public GameObject soundON;
    public GameObject soundOFF;
    public GameObject FXON;
    public GameObject FXOFF;

    int count_options = 0;

    const string
    animOptions = "options"
    ;

    // Use this for initialization
    void Start () {

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

    // Update is called once per frame
    void Update () {
        if (Convert.ToBoolean(PlayerPrefs.GetFloat("sound", 1)))
        {
            this.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            this.GetComponent<AudioSource>().mute = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void empezarJuego()
    {
        SceneManager.LoadScene("InGame");
    }


    public void OptionsMainMenu()
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
}
