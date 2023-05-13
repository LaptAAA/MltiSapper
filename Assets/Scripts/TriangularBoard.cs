using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriangularBoard : RenderingBoard
{
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.tileAnchor = new Vector3(0.5f, 0.5f, 0.5f);
        currentColor = lightColor;
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
                case Cell.Type.Empty:
                    if (isInverted(cell))
                    {
                        return currentColor.tileEmpty_1_triangle;
                    }
                    else
                    {
                        return currentColor.tileEmpty_2_triangle;
                    }
                case Cell.Type.Mine:
                    if (cell.Exploded)
                    {
                        if (isInverted(cell))
                        {
                            return currentColor.tileExploded_1_triangle;
                        }
                        else
                        {
                            return currentColor.tileExploded_2_triangle;
                        }
                    }
                    else
                    {
                        if (isInverted(cell))
                        {
                            return currentColor.tileMine_1_triangle;
                        }
                        else
                        {
                            return currentColor.tileMine_2_triangle;
                        }
                    }
                case Cell.Type.Number:
                    switch (cell.Number)
                    {
                        case 1:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum1_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum1_2_triangle;
                            }
                        case 2:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum2_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum2_2_triangle;
                            }
                        case 3:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum3_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum3_2_triangle;
                            }
                        case 4:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum4_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum4_2_triangle;
                            }
                        case 5:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum5_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum5_2_triangle;
                            }
                        case 6:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum6_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum6_2_triangle;
                            }
                        case 7:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum7_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum7_2_triangle;
                            }
                        case 8:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum8_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum8_2_triangle;
                            }
                        case 9:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum9_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum9_2_triangle;
                            }
                        case 10:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum10_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum10_2_triangle;
                            }
                        case 11:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum11_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum11_2_triangle;
                            }
                        case 12:
                            if (isInverted(cell))
                            {
                                return currentColor.tileNum12_1_triangle;
                            }
                            else
                            {
                                return currentColor.tileNum12_2_triangle;
                            }
                        default: return null;

                    }
                default: return null;
            }

        }
        else if (cell.Flagged)
        {
            if (isInverted(cell))
            {
                return currentColor.tileFlag_1_triangle;
            }
            else
            {
                return currentColor.tileFlag_2_triangle;
            }
        }
        else
        {
            if (isInverted(cell))
            {
                return currentColor.tileUnknown_1_triangle;
            }
            else
            {
                return currentColor.tileUnknown_2_triangle;
            }

        }
    }

    

    private bool isInverted(Cell cell)
    {
        int x = cell.Position.x;
        int y = cell.Position.y;

        return (x + y) % 2 == 0;

    }
}
