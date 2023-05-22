using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RectangularBoard : RenderingBoard
{
    private void Awake()
    {
        base.Awake();
        tilemap = GetComponent<Tilemap>();
        tilemap.tileAnchor = new Vector3(0.5f, 0.5f, 0.5f);
        if (color == 2)
        {
            currentColor = darkColor;
        }
        else
        {
            color = 1;
            currentColor = lightColor;
        }
    }

    public override void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        tilemap.tileAnchor = new Vector3(0.5f, 0.5f, 0.5f);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                Cell cell = state[i, j];
                tilemap.SetTile(cell.Position, GetTile(cell));
                
            }
        }
    }



    public override void Erase(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        tilemap.tileAnchor = new Vector3(0.5f, 0.5f, 0.5f);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                Cell cell = state[i, j];
                tilemap.SetTile(cell.Position, currentColor.None);
            }
        }

    }


    private Tile GetTile(Cell cell)
    {
        if (cell.Revealed)
        {
            switch (cell.type)
            {
                case Cell.Type.Empty: return currentColor.tileEmpty_rectangle;
                case Cell.Type.Mine:
                    if (cell.Exploded)
                    {
                        return currentColor.tileExploded_rectangle;
                    }
                    else if (cell.Flagged)
                    {
                        return currentColor.tileGoodMine_rectangle;
                    } else
                    {
                        return currentColor.tileMine_rectangle;
                    }
                case Cell.Type.Number:
                    switch (cell.Number)
                    {
                        case 1: return currentColor.tileNum1_rectangle;
                        case 2: return currentColor.tileNum2_rectangle;
                        case 3: return currentColor.tileNum3_rectangle;
                        case 4: return currentColor.tileNum4_rectangle;
                        case 5: return currentColor.tileNum5_rectangle;
                        case 6: return currentColor.tileNum6_rectangle;
                        case 7: return currentColor.tileNum7_rectangle;
                        case 8: return currentColor.tileNum8_rectangle;
                        default: return null;

                    }
                default: return null;
            }

        }
        else if (cell.Flagged)
        {
            return currentColor.tileFlag_rectangle;
        }
        else
        {
            return currentColor.tileUnknown_rectangle;
        }
    }
}
