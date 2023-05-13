using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cell
{
    public enum Type
    {
        Invalid,
        Empty,
        Mine,
        Number,
    }

    public Type type;
    private Vector3Int position;
    private int number;
    private bool revealed;
    private bool flagged;
    private bool exploded;

    public Vector3Int Position { get => position; set => position = value; }
    public int Number { get => number; set => number = value; }
    public bool Revealed { get => revealed; set => revealed = value; }
    public bool Flagged { get => flagged; set => flagged = value; }
    public bool Exploded { get => exploded; set => exploded = value; }
}
