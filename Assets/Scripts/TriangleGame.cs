using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGame : Game
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


        GenerateCells();

        grid = gameObject.GetComponent<Grid>();
        grid.cellLayout = GridLayout.CellLayout.Rectangle;
        grid.cellGap = new Vector2(-0.5f, 0);
        grid.cellSize = new Vector2(1, 1);
        float padding = 2f;
        
        float w = width * grid.cellSize.x * grid.transform.localScale.x * grid.transform.lossyScale.x;
        float h = height * grid.cellSize.y * grid.transform.localScale.y * grid.transform.lossyScale.y;

       
        w += w + padding;
        h += h + padding;

        
        Camera.main.orthographicSize = Mathf.Max(w , h ) / 4f;
        Camera.main.transform.position = new Vector3(width / 4f, height / 2f, -10f);


        board.Draw(state);

    }


    protected override int CountMines(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }

        if (!isInverted(x, y))
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                    {
                        count++;
                    }
                }
            }

        }
        else
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 0; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                    {
                        count++;
                    }
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
                if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                {
                    mine = state[x + i, y + j];
                    return mine;
                }
            }
        }

        if (!isInverted(x, y))
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                    {
                        mine = state[x + i, y + j];
                        break;
                    }
                }
            }

        }
        else
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 0; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine)
                    {
                        mine = state[x + i, y + j];
                        break;
                    }
                }
            }
        }

        return mine;
    }

    private bool isInverted(int x, int y)
    {
        return (x + y) % 2 == 0;
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
                if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height)
                {
                    RevealAll(state[x + i, y + j]);
                }
            }
        }

        if (!isInverted(x, y))
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height)
                    {
                        RevealAll(state[x + i, y + j]);
                    }
                }
            }

        }
        else
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 0; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height)
                    {
                        RevealAll(state[x + i, y + j]);
                    }
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
                if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && x + i == xCeil && y + j == yCeil)
                {
                    return true;
                }
            }
        }

        if (!isInverted(x, y))
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && x + i == xCeil && y + j == yCeil)
                    {
                        return true;
                    }
                }
            }

        }
        else
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 0; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && x + i == xCeil && y + j == yCeil)
                    {
                        return true;
                    }
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
                if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine && !state[x + i, y + j].Flagged)
                {
                    return true;
                }
            }
        }


        if (!isInverted(x, y))
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine && !state[x + i, y + j].Flagged)
                    {
                        return true;
                    }
                }
            }

        }
        else
        {
            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 0; ++j)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i < width && y + j < height && state[x + i, y + j].type == Cell.Type.Mine && !state[x + i, y + j].Flagged)
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
        Vector3 cellSize = grid.cellSize;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int clickedCell = grid.WorldToCell(mousePosition);

        Vector3 cellCenter = grid.CellToWorld(clickedCell);

        Vector3 bottomLeft = clickedCell - new Vector3(0.5f * cellSize.x, 0.5f * cellSize.y, 0f);
        Vector3 clickedPoint = mousePosition - cellCenter;

        clickedPoint = mousePosition - cellCenter + bottomLeft;
        Debug.Log("clickedPoint " + clickedPoint.ToString());

        Vector3Int clickedCellInt = grid.WorldToCell(mousePosition);
        Cell cell = GetCellByPosition(clickedCellInt.x, clickedCellInt.y);

        Vector3 A;
        Vector3 B;
        Vector3 C;
        Vector3 D = clickedPoint;

        if (!isInverted(cell.Position.x, cell.Position.y))
        {
            Debug.Log("ПЕРЕВЕРНУТЫЙ " + clickedCell.ToString());
            A = clickedCell + new Vector3(-0.5f * cellSize.x, 0.5f * cellSize.y, 0f);
            C = clickedCell + new Vector3(0.5f * cellSize.x, 0.5f * cellSize.y, 0f);
            B = clickedCell + new Vector3(0f, -0.5f * cellSize.y, 0f);
        }
        else
        {
            Debug.Log("НЕ ПЕРЕВЕРНУТЫЙ " + clickedCell.ToString());
            A = clickedCell - new Vector3(0.5f * cellSize.x, 0.5f * cellSize.y, 0f);
            C = clickedCell + new Vector3(0.5f * cellSize.x, -0.5f * cellSize.y, 0f);
            B = clickedCell + new Vector3(0f, 0.5f * cellSize.y, 0f);
        }

        Debug.Log("Точка A " + A.ToString());
        Debug.Log("Точка C " + C.ToString());
        Debug.Log("Точка B " + B.ToString());
        Debug.Log("Точка D " + D.ToString());

        float sideAB = (B.x - A.x) * (D.y - A.y) - (D.x - A.x) * (B.y - A.y);
        float sideBC = (C.x - B.x) * (D.y - B.y) - (D.x - B.x) * (C.y - B.y);
        float sideCA = (A.x - C.x) * (D.y - C.y) - (D.x - C.x) * (A.y - C.y);

        if ((sideAB >= 0 && sideBC >= 0 && sideCA >= 0) || (sideAB <= 0 && sideBC <= 0 && sideCA <= 0))
        {
        }
        else
        {

            if (sideAB >= 0 && sideCA >= 0)
            {
                cell = GetCellByPosition(clickedCellInt.x + 1, clickedCellInt.y);
            }
            else if (sideBC >= 0 && sideAB >= 0)
            {
                Debug.Log("Точка D находится слева от стороны AC");
            }
            else if (sideCA >= 0 && sideBC >= 0)
            {
                cell = GetCellByPosition(clickedCellInt.x - 1, clickedCellInt.y);
            }
            else if (sideAB <= 0 && sideCA <= 0)
            {
                cell = GetCellByPosition(clickedCellInt.x + 1, clickedCellInt.y);
            }
            else if (sideBC <= 0 && sideAB <= 0)
            {

            }
            else if (sideCA <= 0 && sideBC <= 0)
            {
                cell = GetCellByPosition(clickedCellInt.x - 1, clickedCellInt.y);
            }
        }

        if (cell.Position.x < 0 || cell.Position.x >= width)
        {
            cell.type = Cell.Type.Invalid;
        }
        
        return  cell;
    }
}
