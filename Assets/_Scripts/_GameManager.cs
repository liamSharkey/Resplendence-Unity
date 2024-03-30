using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static int highestBossDefeated;
    public static int numberOfDefeatedBosses;
    public static int totalNumberOfBosses = 1;

    public static bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs : HighestBossDefeated
        highestBossDefeated = PlayerPrefs.GetInt("HighestBossDefeated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void savePrefString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    public static void savePrefInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    [ContextMenu("Reset Bosses")]
    public static void Reset()
    {
        PlayerPrefs.SetInt("Boss1", 0);
    }

    public static bool allBossesDefeated()
    {
        return PlayerPrefs.GetInt("HighestBossDefeated") == totalNumberOfBosses;
    }

}
