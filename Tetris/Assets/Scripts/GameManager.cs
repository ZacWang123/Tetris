using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;
    public int height = 20;
    public int width = 10;

    void Start()
    {
        grid = new GameGrid(height, width, Cell);
        grid.DrawGrid();
    }

    void Update()
    {
        grid.UpdateGrid(0, 10, 1);
        grid.UpdateGridColour();
    }
}
