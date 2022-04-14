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
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class Holder
    {
        public int difficulty;
        public Color backgroundColor;
        public string playerName;
        public List<string> scoreNames;
        public List<int> scoreValues;
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
        data.playerName = playersNameAndScore.Item1;
        SaveScores(data);

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
            
            if(data.difficulty != 0)
            {
                difficulty = data.difficulty;
            }
            else
            {
                difficulty = 1;
            }
            backgroundColor = data.backgroundColor;

            scores = LoadScores(data);

            if(data.playerName != null && data.playerName.Length > 0)
            {
                playersNameAndScore = new Tuple<string, int>(data.playerName, 0);
            }
            else
            {
                playersNameAndScore = new Tuple<string, int>("", 0);
            }
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

    public void SortScores()
    {
        if(scores.Count > 0)
        {
            scores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        }
    }

    void TestSorting()
    {
        scores.Add(new Tuple<string, int>("S", 153 ));
        scores.Add(new Tuple<string, int>("asdf", 2));
        scores.Add(new Tuple<string, int>("S", 2405));
        scores.Add(new Tuple<string, int>("ad", 333));
        scores.Add(new Tuple<string, int>("tas", 334));
        scores.Add(new Tuple<string, int>("ad",123));
        scores.Add(new Tuple<string, int>("add", 1));

        Debug.Log("ORIGINAL:");
        OutputTupleList(scores);

        SortScores();

        Debug.Log("SORTED:");
        OutputTupleList(scores);
    }

    void OutputTupleList(List<Tuple<string, int>> mScores)
    {
        foreach (Tuple<string, int> element in mScores)
        {
            Debug.Log("Name: " + element.Item1 + " Score: " + element.Item2);
        }
    }

    void SaveScores(Holder myHolder)
    {
        myHolder.scoreNames = new List<string>();
        myHolder.scoreValues = new List<int>();
        
        for(int i = 0; i < scores.Count; i++)
        {
            if(scores[i] != null)
            {
                myHolder.scoreNames.Add(scores[i].Item1);
                myHolder.scoreValues.Add(scores[i].Item2);
                Debug.Log("Saving result: Name - " + myHolder.scoreNames[i] + " Score - " + myHolder.scoreValues[i]);
            }
        }
    }

    List<Tuple<string, int>> LoadScores(Holder myData)
    {
        List<Tuple<string, int>> newScores = new List<Tuple<string, int>>();
        
        for(int i = 0; i < myData.scoreNames.Count; i++)
        {
            newScores.Add(new Tuple<string, int>(myData.scoreNames[i], myData.scoreValues[i]));
            Debug.Log("Loading results: Name - " + newScores[i].Item1 + " Score - " + newScores[i].Item2);
        }
        
        return newScores;
    }
}
