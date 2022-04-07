using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : MonoBehaviour
{
    public int difficulty;
    public Color backgroundColor;
    public UIMainMenu mainMenu;

    void Start()
    {
        DataHolder.dataHolder.SetColor();
        backgroundColor = DataHolder.dataHolder.backgroundColor;
        difficulty = DataHolder.dataHolder.difficulty;
        GameObject.Find("DifficultySlider").GetComponent<Slider>().value = DataHolder.dataHolder.difficulty;
    }

    public void BackToMenu()
    {
        DataHolder.dataHolder.OpenMainMenu();
        DataHolder.dataHolder.difficulty = difficulty;
        DataHolder.dataHolder.backgroundColor = backgroundColor;
        DataHolder.dataHolder.SaveData();
    }

    public void ColorChange(Button myButton)
    {
        backgroundColor = myButton.GetComponent<Image>().color;
    }

    public void DifficultyChange(Slider mySlider)
    {
        difficulty = (int)mySlider.value;
    }
}
