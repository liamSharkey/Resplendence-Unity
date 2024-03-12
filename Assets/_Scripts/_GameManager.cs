using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static bool bossOneDefeated;
    public static int numberOfDefeatedBosses;
    public static int totalNumberOfBosses;

    public static bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        bossOneDefeated = PlayerPrefs.GetString("Boss1") == "true";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void savePref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    [ContextMenu("Reset Scoreboard")]
    public static void Reset()
    {
        PlayerPrefs.SetString("Boss1", "false");
    }

}
