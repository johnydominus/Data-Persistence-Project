using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField] List<GameObject> names;
    [SerializeField] List<GameObject> scores;
    [SerializeField] Transform canvasTransform;
    [SerializeField] Font textFont;

    int step = 15;
    int yPlace = 64;
    int fontSize = 14;
    string placeholder = "---";
    Color textColor;

    void Start()
    {
        DataHolder.dataHolder.SetColor();
        canvasTransform = GameObject.Find("Canvas").GetComponent<Transform>();
        textColor = new Color(255, 255, 255, 255);
        FillScores();
    }

    public void BackToMenu()
    {
        DataHolder.dataHolder.OpenMainMenu();
    }

    void FillScores()
    {
        int scoresNumber = DataHolder.dataHolder.scores.Count;
        string nameToPrint;
        string scoreToPrint;
        Debug.Log(scoresNumber + " scores to be processed");

        DataHolder.dataHolder.SortScores();
        for(int i = 0; i < 10; i++)
        {
            if(i < scoresNumber)
            {
                nameToPrint = DataHolder.dataHolder.scores[i].Item1;
                scoreToPrint = DataHolder.dataHolder.scores[i].Item2.ToString();
            }
            else
            {
                nameToPrint = placeholder;
                scoreToPrint = placeholder;
            }
            names.Add(CreateText(canvasTransform, 6f, yPlace, nameToPrint, fontSize, textColor));
            scores.Add(CreateText(canvasTransform, 156f, yPlace, scoreToPrint, fontSize, textColor));

            yPlace -= step;
            Debug.Log(i + " scores filled");
        }
    }

    GameObject CreateText(Transform canvas_transform, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);
        trans.sizeDelta = new Vector2(160, 30);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;
        text.fontSize = font_size;
        text.color = text_color;
        text.font = textFont;
        text.alignment = TextAnchor.MiddleCenter;

        return UItextGO;
    }
}
