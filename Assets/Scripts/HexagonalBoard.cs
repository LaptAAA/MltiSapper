using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexagonalBoard : RenderingBoard
{
    
    private void Awake()
    {
        base.Awake();
        tilemap = GetComponent<Tilemap>();
        tilemap.tileAnchor = new Vector3(0f, 0f, 0f);
        if (color == 2)
        {
            currentColor = darkColor;
        } else
        {
            color = 1;
            currentColor = lightColor;
        }
    }

    public override void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);
        tilemap.tileAnchor = new Vector3(0f, 0f, 0f);

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

        tilemap.tileAnchor = new Vector3(0f, 0f, 0f);

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
                case Cell.Type.Empty: return currentColor.tileEmpty_hex;
                case Cell.Type.Mine:
                    if (cell.Exploded)
                    {
                        return currentColor.tileExploded_hex;
                    }
                    else if (cell.Flagged)
                    {
                        return currentColor.tileGoodMine_hex;
                    }
                    else
                    {
                        return currentColor.tileMine_hex;
                    }
                case Cell.Type.Number:
                    switch (cell.Number)
                    {
                        case 1: return currentColor.tileNum1_hex;
                        case 2: return currentColor.tileNum2_hex;
                        case 3: return currentColor.tileNum3_hex;
                        case 4: return currentColor.tileNum4_hex;
                        case 5: return currentColor.tileNum5_hex;
                        case 6: return currentColor.tileNum6_hex;
                        default: return null;

                    }
                default: return null;
            }

        }
        else if (cell.Flagged)
        {
            return currentColor.tileFlag_hex;
        }
        else
        {
            return currentColor.tileUnknown_hex;
        }
    }

}
