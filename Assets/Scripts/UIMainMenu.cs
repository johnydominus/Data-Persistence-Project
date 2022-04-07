using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainMenu : MonoBehaviour
{
    public DataHolder dataHolder;

    void Start()
    {
        DataHolder.dataHolder.SetColor();
        if(DataHolder.dataHolder.playersNameAndScore != null 
           && DataHolder.dataHolder.playersNameAndScore.Item1 != null 
           /*&& GameObject.Find("InputFieldText") != null*/)
        {
            GameObject.Find("InputFieldText").GetComponent<Text>().text = DataHolder.dataHolder.playersNameAndScore.Item1;
        }
    }

    public void StartGame()
    {
        if(dataHolder.playersNameAndScore != null && dataHolder.playersNameAndScore.Item1.Length != 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            dataHolder.ShowErrorMessage();
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
            dataHolder.playersNameAndScore = new Tuple<string, int>(playerName.text, 0);
        }
        else
        {
            dataHolder.ShowErrorMessage();
        }
    }
}
