using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ScoreValue;
    [SerializeField] TextMeshProUGUI HealthValue;
    [Header("References")]
    [SerializeField] PlayerScript playerReference;
    //Score Keeper
    public int Score;

    void Update()
    {
        // Setting the UI text health value to the player health from player script
        HealthValue.text = playerReference.Health.ToString();
        //Setting the UI text score to the score variable with a format of 6 zeros
        ScoreValue.text = Score.ToString("000000");
        //This function is responsible for restarting the game after dying
        GameRestart();

    }

    private void GameRestart()
    {
        //if the player died and the player presses F then the game restarts
        if(playerReference.Health <= 0 && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(0);
        }
    }
}
