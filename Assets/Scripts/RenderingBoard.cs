using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class RenderingBoard : MonoBehaviour
{
    public ThemeController themeController;
    public int color = 1;

    protected Tilemap tilemap;

    protected static ColorTheme currentColor;
    public ColorTheme lightColor;
    public ColorTheme darkColor;

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

    public void ChangeTheme()
    {
        if (currentColor == lightColor)
        {
            color = 2;
            currentColor = darkColor;
        } else
        {
            color = 1;
            currentColor = lightColor;
        }
        
    }

    public void Awake()
    {
        color = PlayerPrefs.GetInt("color", 0);
        if (color == 2)
        {
            themeController.ChangeColorTheme();
        }
    }

    public void SaveTheme()
    {
        PlayerPrefs.SetInt("color", color);
        PlayerPrefs.Save();
    }

    public int GetColorName()
    {
        return currentColor.name;
    }

    public abstract  void Draw(Cell[,] state);
    public abstract  void Erase(Cell[,] state);
}
