using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleGame : Game
{

    public override void NewGame(GameObject gameObject, Grid grid)
    {
        timer.go = false;
        timer.SetTime(0);

        countMinesText.text = mineCount.ToString();

        state = new Cell[width, height];
        gameOver = false;
        notGenerated = true;

        Vector2 cellSize = grid.cellSize;
        Vector2 spacing = grid.cellGap;
        int width_screen = Mathf.RoundToInt(cellSize.x * width + spacing.x * (width - 1)) * 3;
        int height_screen = Mathf.RoundToInt(cellSize.y * height + spacing.y * (height - 1)) * 3;

        //Screen.SetResolution(1920, 1080, false);

        //Resolution resolution = Screen.currentResolution;

        // Выводим информацию о разрешении в консоль
        //Debug.Log("Ширина экрана: " + resolution.width);
        //Debug.Log("Высота экрана: " + resolution.height);

        //Screen.SetResolution(resolution.width, Mathf.RoundToInt(resolution.width * 1080f / 1920f), false);
        GenerateCells();


        grid = gameObject.GetComponent<Grid>();
        grid.cellLayout = GridLayout.CellLayout.Rectangle;
        grid.cellGap = new Vector2(0, 0);
        grid.cellSize = new Vector2(1, 1);
        float padding = 2f;
        
        float w = width * grid.cellSize.x * grid.transform.localScale.x * grid.transform.lossyScale.x;
        float h = height * grid.cellSize.y * grid.transform.localScale.y * grid.transform.lossyScale.y;

        
        w += w + padding;
        h += h + padding;

        
        Camera.main.orthographicSize = Mathf.Max(w, h) / 4f;
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);

        board.Draw(state);

    }


    protected override int CountMines(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }
        return count;
    }

    protected override Cell GetNearestMine(int x, int y)
    {
        Cell mine = new Cell();
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                {
                    mine = state[x + i, y + j];
                }
            }
        }
        return mine;
    }

    protected override void RevealAll(Cell cell)
    {
        if (cell.type == Cell.Type.Mine || cell.Revealed || cell.type == Cell.Type.Invalid)
        {
            return;
        }
        cell.Revealed = true;

        int x = cell.Position.x;
        int y = cell.Position.y;

        state[x, y] = cell;

        if (cell.type != Cell.Type.Empty)
        {
            return;
        }

        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
                {
                    RevealAll(state[x + i, y + j]);
                }
            }
        }
    }
    protected override bool isNeighbor(Cell cellStart, int xCeil, int yCeil)
    {
        int x = cellStart.Position.x;
        int y = cellStart.Position.y;

        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && x + i == xCeil && y + j == yCeil)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected override bool CheckNeihborToUnflaggedMine(int x, int y)
    {
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                {
                    if (!state[x + i, y + j].Flagged)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    protected override Cell getClickedCell()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.Tilemap.WorldToCell(mousePosition);
        return GetCellByPosition(cellPosition.x, cellPosition.y);
    }
}
