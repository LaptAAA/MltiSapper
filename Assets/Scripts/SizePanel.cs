using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizePanel : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    private void Start()
    {
        // Устанавливаем начальное значение текстового поля
        text1.text = slider1.value.ToString();
        text2.text = slider2.value.ToString();
        text3.text = slider3.value.ToString();
    }

    public void ChangedWight()
    {
        text1.text = slider1.value.ToString();

        int w = Mathf.RoundToInt(slider1.value);
        int h = Mathf.RoundToInt(slider2.value);

        slider3.minValue = w * h * 10 / 100;
        slider3.maxValue = w * h * 30 / 100;
    }

    public void ChangedHight()
    {
        text2.text = slider2.value.ToString();

        int w = Mathf.RoundToInt(slider1.value);
        int h = Mathf.RoundToInt(slider2.value);

        slider3.minValue = w * h * 10 / 100;
        slider3.maxValue = w * h * 30 / 100;
    }

    public void ChangedCountMines()
    {
        text3.text = slider3.value.ToString();
    }
}
