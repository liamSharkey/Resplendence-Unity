using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour

{
    public GameObject levelChangerObject;
    private LevelChanger levelchanger;
    public AudioSource source;

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
        source.Play();
        changeSceneTo("Title");
        //SceneManager.LoadScene("Title");
    }
    public void MenuFromPause()
    {
        Time.timeScale = 1f;
        source.Play();
        SceneManager.LoadScene("Title");
    }

    public void ToTavern()
    {
        source.Play();
        changeSceneTo("TavernScene");
        //SceneManager.LoadScene("TavernScene");
    }

    public void ResetProgress()
    {
        source.Play();
        _GameManager.Reset();
    }

    private void changeSceneTo(string SceneName)
    {
        levelchanger.FadeToLevel(SceneName);
    }
}
