using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public SpriteRenderer popUp;
    private void Start()
    {
        popUp = GetComponent<SpriteRenderer>();
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
        popUp.sortingOrder = 5;
    }
}
