using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    public static float SinnerChance;
    public static float MinSinChance;
    public static float MaxSinChance;
    public float sinnerChance;
    public float minSinChance;
    public float maxSinChance;

    public float spawnTime;
    public float timer;
    public GameObject soulPrefab;

    public int maxSoulsCount;
    public static int currentSoulsCount;

    public int addPointsForSuccess;
    public int subPointsForUnsucces;
    public int startPoints;
    public static int AddPointsForSuccess;
    public static int SubPointsForUnsuccess;
    public static int currentPoints;
    public static int currentVasesCount;

    public GameObject scoreText;
    public GameObject message1;
    public GameObject message2;
    public bool gameOver;

    void Start()
    {
        SinnerChance = sinnerChance;
        MinSinChance = minSinChance;
        MaxSinChance = maxSinChance;
        AddPointsForSuccess = addPointsForSuccess;
        SubPointsForUnsuccess = subPointsForUnsucces;
        currentPoints = startPoints;
        currentVasesCount = 5;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime && currentSoulsCount < maxSoulsCount)
        {
            timer = 0;
            Instantiate(soulPrefab);
            currentSoulsCount++;
        }

        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + currentPoints;

        if (currentPoints <= 0)
        {
            Time.timeScale = 0;
            message1.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            gameOver = true;
        }
        if (currentVasesCount == 0)
        {
            Time.timeScale = 0;
            message2.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            gameOver = true;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }
}
