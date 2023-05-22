using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Game : MonoBehaviour
{
    public RenderingBoard board;
    public Timer timer;
    public Grid grid;
    public TextMeshProUGUI countMinesText;

    protected Cell[,] state;
    protected static int width = 16;
    protected static int height = 16;
    protected static int mineCount = 32;
    protected bool gameOver = false;
    protected bool notGenerated = true;

    public static int MineCount { get => mineCount; set => mineCount = value; }
    public static int Height { get => height; set => height = value; }
    public static int Width { get => width; set => width = value; }
    public Cell[,] State { get => state; set => state = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool NotGenerated { get => notGenerated; set => notGenerated = value; }

    public void ChangeTheme()
    {
        board.ChangeTheme();
        board.Draw(State);
    }


    public void CompareEasyBest()
    {
        timer.CompareEasyBest();
    }

    public void CompareStandartBest()
    {
        timer.CompareStandartBest();
    }

    public void CompareHardBest()
    {
        timer.CompareHardBest();
    }

    public void GenerateAll()
    {
        Cell cellStart = getClickedCell();

        if (cellStart.type == Cell.Type.Invalid)
        {
            return;
        }
        
        NotGenerated = false;
        GenerateMines(cellStart);
        GenerateNumbers();
        
        timer.go = true;

    }

    protected void GenerateCells()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                Cell cell = new Cell();
                cell.Position = new Vector3Int(i, j, 0);
                cell.type = Cell.Type.Empty;
                State[i, j] = cell;

            }
        }
    }

    private void GenerateMines(Cell cellStart)
    {
        for (int i = 0; i < MineCount; ++i)
        {
            int x = Random.Range(0, Width);
            int y = Random.Range(0, Height);
            while (State[x, y].type == Cell.Type.Mine || isNeighbor(cellStart, x, y))
            {
                x = Random.Range(0, Width);
                y = Random.Range(0, Height);

            }
            State[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if (State[i, j].type == Cell.Type.Mine)
                {
                    continue;
                }
                State[i, j].Number = CountMines(i, j);
                if (State[i, j].Number > 0)
                {
                    State[i, j].type = Cell.Type.Number;
                }
            }
        }
    }

    protected Cell GetCellByPosition(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return State[x, y];
        }
        else
        {
            Cell cell = new Cell();
            cell.Position = new Vector3Int(x, y);
            cell.type = Cell.Type.Invalid;
            return cell;
        }
    }

    
    private bool CheckUnflaggedMine()
    {
        bool flag = false;
        for (int i=0; i<width; ++i)
        {
            for (int j=0; j<height; ++j)
            {
                if (State[i,j].Revealed && State[i, j].type == Cell.Type.Number)
                {
                    Debug.Log("CheckUnflaggedMine " + i + " " + j);
                    flag = CheckNeihborToUnflaggedMine(i,j);
                    if (flag)
                    {
                        return true;
                    }
                }
            }
        }
        return flag;
    }




    public bool FlaggOneMine()
    {
        Debug.Log("FlaggOneMine()");

        bool flag = CheckUnflaggedMine();
        if (!flag)
        {
            return flag;
        }

        Debug.Log("FlaggOneMine()    notOk");
        bool notOk = true;

        Cell mine = new Cell();
        while (notOk)
        {
            int x = Random.Range(0, Width);
            int y = Random.Range(0, Height);

            while (!State[x, y].Revealed || State[x, y].type != Cell.Type.Number)
            {
                Debug.Log("Random coord " + x + " " + y);
                x = Random.Range(0, Width);
                y = Random.Range(0, Height);

            }

            Debug.Log("Number coord " + x + " " + y);
            mine = GetNearestMine(x, y);
            notOk = mine.Flagged;
        }
        
        PutFlag(mine);
        return flag;
    }

    public bool RevealCell()
    {
        bool win = false;
        Cell cell = getClickedCell();

        if (cell.type != Cell.Type.Invalid && !cell.Revealed && !cell.Flagged)
        {
            switch (cell.type)
            {
                case Cell.Type.Mine:
                    Explode(cell);
                    break;
                case Cell.Type.Empty:
                    RevealAll(cell);
                    win = CheckWin();
                    break;
                default:
                    cell.Revealed = true;
                    State[cell.Position.x, cell.Position.y] = cell;
                    win = CheckWin();
                    break;
            }
            board.Draw(State);
        }
        return win;
    } 

    private void PutFlag(Cell mine)
    {
        Debug.Log("Mine coord " + mine.Position.ToString());

        if (mine.type != Cell.Type.Invalid && !mine.Revealed)
        {
            int number;
            int.TryParse(countMinesText.text, out number);
            number += -1;
            countMinesText.text = number.ToString();


            mine.Flagged = true;
            State[mine.Position.x, mine.Position.y] = mine;
            board.Draw(State);
        }
    }


    public void PutFlag()
    {
        int x = 0;
        Cell cell = getClickedCell();

        if (cell.type != Cell.Type.Invalid && !cell.Revealed)
        {
            
            if (cell.Flagged)
            {
                x = 1;
            } else
            {
                x = -1;
            }

            int number;
            int.TryParse(countMinesText.text, out number);
            number += x;
            countMinesText.text = number.ToString();


            cell.Flagged = !cell.Flagged;
            State[cell.Position.x, cell.Position.y] = cell;
            board.Draw(State);
        }
    }

    private bool CheckWin()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if (State[i, j].type != Cell.Type.Mine && !State[i, j].Revealed)
                {
                    return false;
                }
            }
        }
        GameOver = true;
        timer.go = false;
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if (State[i, j].type == Cell.Type.Mine)
                {
                    State[i, j].Flagged = true;
                }
            }
        }
        board.Draw(State);
        return true;
    }

    private void Explode(Cell cell)
    {
        GameOver = true;
        timer.go = false;

        cell.Revealed = true;
        cell.Exploded = true;

        State[cell.Position.x, cell.Position.y] = cell;

        for (int i = 0; i < Width; ++i)
        {

            for (int j = 0; j < Height; ++j)
            {
                if (State[i, j].type == Cell.Type.Mine)
                {
                    State[i, j].Revealed = true;
                }
            }
        }
    }

    public void SaveTheme()
    {
        board.SaveTheme();
    }


    public void Erase()
    {
        board.Erase(State);
    }

    public abstract void NewGame(GameObject gameObject, Grid grid);
    protected abstract int CountMines(int x, int y);
    protected abstract Cell GetNearestMine(int x, int y);
    protected abstract bool CheckNeihborToUnflaggedMine(int x, int y);
    protected abstract void RevealAll(Cell cell);
    protected abstract bool isNeighbor(Cell cellStart, int xCeil, int yCeil);
    protected abstract Cell getClickedCell();
}
