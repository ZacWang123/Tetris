using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    public int Rows;
    public int Columns;
    public int[,] Grid;
    public GameObject GridCell;
    public Renderer[,] VisualGrid;

    public GameGrid(int rows, int cols, GameObject cell)
    {
        Rows = rows;
        Columns = cols;
        Grid = new int[Columns, Rows];
        ZeroGrid();
        GridCell = cell;
    }

    public void ZeroGrid() {
        for (int cols = 0; cols < Columns; cols++)
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                Grid[cols, rows] = 0;
            }
        }
    }

    public void DrawGrid()
    {
        VisualGrid = new Renderer[Columns, Rows];

        for (int cols = 0; cols < Columns; cols++)
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                GameObject Cell = Object.Instantiate(GridCell, new Vector2(cols, rows), Quaternion.identity);
                Renderer CellObject = Cell.GetComponent<Renderer>();
                CellObject.sortingOrder = 1;
                VisualGrid[cols, rows] = CellObject;
            }
        }
    }

    public void UpdateGridColour()
    {
        for (int cols = 0; cols < Columns; cols++)
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                Renderer Cell = VisualGrid[cols, rows];

                switch (Grid[cols, rows])
                {
                    case 0:
                        Cell.material.color = new Color32(91, 168, 58, 255);
                        break;

                    case 1:
                        Cell.material.color = new Color32(51, 72, 227, 255);
                        break;
                }
            }
        }
    }
    
    public int GetGridCell(int col, int row)
    {
        return Grid[col, row];
    }

    public void UpdateGrid(int col, int row, int value)
    {
        Grid[col, row] = value;
    }

    public bool WithinGrid(int col, int row)
    {
        if (col >= 0 && col < Columns && row >= 0 && row < Rows)
        {
            return true;
        }
        return false;
    }

    public void ClearRow(int row) {
        for (int i = 0; i < Columns; i++) {
            Grid[i, row] = 0;
        }
    }

    public void ResetGrid()
    {
        for (int cols = 0; cols < Columns; cols++)
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                Grid[cols, rows] = 0;
            }
        }
    }
}