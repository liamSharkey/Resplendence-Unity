using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour

{
    public GameObject levelChangerObject;
    private LevelChanger levelchanger;

    public void Start()
    {
        levelchanger = levelChangerObject.GetComponent<LevelChanger>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        changeSceneTo("Title");
        //SceneManager.LoadScene("Title");
    }
    public void MenuFromPause()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void ToTavern()
    {
        changeSceneTo("TavernScene");
        //SceneManager.LoadScene("TavernScene");
    }

    public void ResetProgress()
    {
        _GameManager.Reset();
    }

    private void changeSceneTo(string SceneName)
    {
        levelchanger.fadeToLevelWithSelectNoise(SceneName);
    }
}
