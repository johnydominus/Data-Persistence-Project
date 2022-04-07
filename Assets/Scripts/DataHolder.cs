using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    public static DataHolder dataHolder;
    public int difficulty;
    public Color backgroundColor;
    public List<Tuple<string, int>> scores;
    public Tuple<string, int> playersNameAndScore;
    public GameObject errorMessage;
    public UIMainMenu mainMenu;

    void Awake()
    {
        if(dataHolder == null)
        {
            dataHolder = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class Holder
    {
        public int difficulty;
        public Color backgroundColor;
        public List<Tuple<string, int>> scores;
        public Tuple<string, int> playersNameAndScore;    
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveData()
    {
        Holder data = new Holder();
        data.difficulty = difficulty;
        data.backgroundColor = backgroundColor;
        data.scores = scores;
        data.playersNameAndScore = playersNameAndScore;

        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Data saved: difficulty: " + difficulty);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Holder data = JsonUtility.FromJson<Holder>(json);
            if( data.difficulty != 0)
            {
                difficulty = data.difficulty;
            }
            else
            {
                difficulty = 1;
            }
            backgroundColor = data.backgroundColor;
            scores = data.scores;
            playersNameAndScore = data.playersNameAndScore;

            Debug.Log("Data loaded: difficulty: " + difficulty);
        }        
    }

    public void ShowErrorMessage()
    {
        GameObject mainMenu = GameObject.Find("Menu");

        mainMenu.SetActive(false);
        errorMessage.SetActive(true);
    }

    public void SetColor()
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = DataHolder.dataHolder.backgroundColor;
    }
}
