using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour

{
    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Title");
    }

    public void ToTavern()
    {
        SceneManager.LoadScene("TavernScene");
    }

    public void ResetProgress()
    {
        _GameManager.Reset();
    }
}
