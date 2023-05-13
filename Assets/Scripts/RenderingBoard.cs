using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class RenderingBoard : MonoBehaviour
{
    protected Tilemap tilemap;

    protected static ColorTheme currentColor;
    public ColorTheme lightColor;
    public ColorTheme darkColor;

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

    public void ChangeTheme()
    {
        if (currentColor == lightColor)
        {
            currentColor = darkColor;
        } else
        {
            currentColor = lightColor;
        }
        
    }


    public abstract  void Draw(Cell[,] state);
    public abstract  void Erase(Cell[,] state);
}
