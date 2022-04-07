using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScores : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;
    [SerializeField] List<TextMeshProUGUI> numbers;

    void Start()
    {
        DataHolder.dataHolder.SetColor();
    }

    public void BackToMenu()
    {
        DataHolder.dataHolder.OpenMainMenu();
    }
}
