using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;

    public AudioSource source;

    public void FadeToLevel(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = sceneName;
    }

    public void fadeToLevelWithSelectNoise(string sceneName)
    {
        source.Play();
        animator.SetTrigger("FadeOut");
        levelToLoad = sceneName;
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
