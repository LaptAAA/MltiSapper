using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using System;

public partial class GameController : MonoBehaviour
{
    public enum Tile
    {
        Rectangle,
        Triangle,
        Hexagon
    }

    public enum Complexity
    {
        Easy,
        Standart,
        Hard
    }

    private Tile currentTile;
    private Complexity complexity;
    public Game game;


    public RectangleGame gameRectangle;
    public TriangleGame gameTriangle;
    public HexagonGame gameHexagonal;

    public Slider sliderWidth;
    public Slider sliderHeight;
    public Slider sliderCountMines;

    private Grid grid;


    public void ClickExit()
    {
        PlayerPrefs.SetInt("countMine", Game.MineCount);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("width", Game.Width);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("height", Game.Height);
        PlayerPrefs.Save();
        game.SaveTheme();
        PlayerPrefs.Save();

        Application.Quit();
    }




    private void Awake()
    {
        int countMine = PlayerPrefs.GetInt("countMine", 0);
        if (countMine == 0)
        {
            countMine = 32;
        }
        Game.MineCount = countMine;
        int w = PlayerPrefs.GetInt("width", 0);
        if (w == 0)
        {
            w = 16;
        }
        Game.Width = w;
        int h = PlayerPrefs.GetInt("height", 0);
        if (h == 0)
        {
            h = 16;
        }
        Game.Height = h;

        grid = gameObject.GetComponent<Grid>();
    }

    private void Start()
    {
        game.NewGame(gameObject, grid);
    }


    public void SetTriangle()
    {
        if (currentTile == Tile.Triangle)
        {
            if (game.GameOver)
            {
                game.NewGame(gameObject, grid);
            }
            return;
        }
        currentTile = Tile.Triangle;
        game = gameTriangle;
        game.NewGame(gameObject, grid);
    }

    public void SetHexagonale()
    {
        if (currentTile == Tile.Hexagon)
        {
            if (game.GameOver)
            {
                game.NewGame(gameObject, grid);
            }
            return;
        }
        currentTile = Tile.Hexagon;
        game = gameHexagonal;
        game.NewGame(gameObject, grid);

    }

    public void SetRectangle()
    {
        if (currentTile == Tile.Rectangle)
        {
            if (game.GameOver)
            {
                game.NewGame(gameObject, grid);
            }
            return;
        }
        currentTile = Tile.Rectangle;
        game = gameRectangle;
        game.NewGame(gameObject, grid);

    }

    public void SetEasyCountMine()
    {
        complexity = Complexity.Easy;
        int count = Game.Width * Game.Height;
        if (currentTile == Tile.Rectangle)
        {
            Game.MineCount = (count - 9) * 10 / 100;
        }
        else if (currentTile == Tile.Triangle)
        {
            Game.MineCount = (count - 13) * 10 / 100;
        }
        else
        {
            Game.MineCount = (count - 7) * 10 / 100;
        }
        game.NewGame(gameObject, grid);
    }

    public void SetStandartCountMine()
    {
        complexity = Complexity.Standart;
        int count = Game.Width * Game.Height;
        if (currentTile == Tile.Rectangle)
        {
            Game.MineCount = (count - 9) * 20 / 100;
        }
        else if (currentTile == Tile.Triangle)
        {
            Game.MineCount = (count - 13) * 20 / 100;
        }
        else
        {
            Game.MineCount = (count - 7) * 20 / 100;
        }
        game.NewGame(gameObject, grid);
    }

    public void SetHardCountMine()
    {
        complexity = Complexity.Hard;
        int count = Game.Width * Game.Height;
        if (currentTile == Tile.Rectangle)
        {
            Game.MineCount = (count - 9) * 30 / 100;
        }
        else if (currentTile == Tile.Triangle)
        {
            Game.MineCount = (count - 13) * 30 / 100;
        }
        else
        {
            Game.MineCount = (count - 7) * 30 / 100;
        }
        game.NewGame(gameObject, grid);
    }

    public void ChangeTheme()
    {
        game.ChangeTheme();
    }


    public void ChangeSizes()
    {
        game.Erase();
        Game.Width = Mathf.RoundToInt(sliderWidth.value);
        Game.Height = Mathf.RoundToInt(sliderHeight.value);
        Game.MineCount = Mathf.RoundToInt(sliderCountMines.value);
        game.NewGame(gameObject, grid);
    }

    public void FlaggOneMine()
    {
        if (!game.NotGenerated)
        {
            bool f = game.FlaggOneMine();
        }
        
    }


    private void Update()
    {

        if (!game.GameOver)
        {
            if (Input.GetMouseButtonDown(1))
            {
                game.PutFlag();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (game.NotGenerated)
                {
                    game.GenerateAll();

                }
                bool win = game.RevealCell();
                if (win)
                {
                    if (complexity == Complexity.Easy)
                    {
                        game.CompareEasyBest();
                    }
                    else if (complexity == Complexity.Standart)
                    {
                        game.CompareStandartBest();
                    }
                    else
                    {
                        game.CompareHardBest();
                    }
                }
            }
        }



    }
}
