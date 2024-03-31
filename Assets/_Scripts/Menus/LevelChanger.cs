using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;

    public void FadeToLevel(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = sceneName;
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
