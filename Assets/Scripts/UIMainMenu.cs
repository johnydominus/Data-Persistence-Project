using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UIPlayerName;

    void Start()
    {
        DataHolder.dataHolder.SetColor();
        SetUIPlayerName();
    }

    public void StartGame()
    {
        if(DataHolder.dataHolder.playersNameAndScore != null
           && DataHolder.dataHolder.playersNameAndScore.Item1.Length != 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            DataHolder.dataHolder.ShowErrorMessage();
        }
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenScores()
    {
        SceneManager.LoadScene(3);
    }

    public void BackToMenu()
    {
        DataHolder.dataHolder.OpenMainMenu();
    }
    
    public void Exit()
    {
        DataHolder.dataHolder.SaveData();
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }

    public void ChangePlayer(InputField playerName)
    {
        if(playerName.text.Length != 0)
        {
            DataHolder.dataHolder.playersNameAndScore = new Tuple<string, int>(playerName.text, 0);
            SetUIPlayerName();
        }
        else
        {
            DataHolder.dataHolder.ShowErrorMessage();
        }
    }

    void SetUIPlayerName()
    {
        if(DataHolder.dataHolder.playersNameAndScore != null && DataHolder.dataHolder.playersNameAndScore.Item1.Length > 0)
        {
            UIPlayerName.color = new Color32(0, 254, 13, 255);
            UIPlayerName.text = "Player: " + DataHolder.dataHolder.playersNameAndScore.Item1;
        }
        else
        {
            UIPlayerName.color = new Color32(255, 69, 69, 255);
            UIPlayerName.text = "Enter player's name to play";;
        }
    }
}
