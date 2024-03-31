using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static int highestBossDefeated;
    public static int numberOfDefeatedBosses;
    public static int totalNumberOfBosses = 3;

    public static bool isPaused = false;


    // Start is called before the first frame update

    private void Awake()
    {
        Time.timeScale = 1.0f;
        // PlayerPrefs : HighestBossDefeated
        highestBossDefeated = PlayerPrefs.GetInt("HighestBossDefeated");
    }
    void Start()
    {
        
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
        PlayerPrefs.SetInt("HighestBossDefeated", 0);
    }

    [ContextMenu("Get Current Highest Boss Beaten")]
    public void getCurrentHighestBoss()
    {
        Debug.Log(PlayerPrefs.GetInt("HighestBossDefeated"));
    }

    public static bool allBossesDefeated()
    {
        return PlayerPrefs.GetInt("HighestBossDefeated") == totalNumberOfBosses;
    }

}
