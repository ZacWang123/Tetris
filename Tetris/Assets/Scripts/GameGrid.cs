using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    public int Rows;
    public int Columns;
    public int[,] grid;
    public Renderer[,] VisualGrid;
    public GameObject GridCell;

    public GameGrid(int rows, int columns, GameObject cell)
    {
        Rows = rows;
        Columns = columns;
        grid = new int[Rows, Columns];
        GridCell = cell;
    }

    public void DrawGrid()
    {
        VisualGrid = new Renderer[Rows, Columns];

        for (int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Columns; cols++)
            {
                GameObject Cell = Object.Instantiate(GridCell, new Vector2(rows, cols), Quaternion.identity);
                VisualGrid[rows, cols] = Cell.GetComponent<Renderer>();
            }
        }
    }

    public void UpdateGrid(int row, int col, int Id)
    {
        grid[row, col] = Id;
    }

    public void UpdateGridColour()
    {
        for (int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Columns; cols++)
            {
                Renderer Cell = VisualGrid[rows, cols];

                switch (grid[rows, cols])
                {
                    case 0:
                        Cell.material.color = new Color(127, 127, 127);
                        break;

                    case 1:
                        Cell.material.color = new Color(0, 255, 255);
                        break;

                    case 2:
                        Cell.material.color = new Color(255, 255, 0);
                        break;

                    case 3:
                        Cell.material.color = new Color(128, 0, 128);
                        break;

                    case 4:
                        Cell.material.color = new Color(0, 255, 0);
                        break;

                    case 5:
                        Cell.material.color = new Color(255, 0, 0);
                        break;

                    case 6:
                        Cell.material.color = new Color(0, 0, 255);
                        break;

                    case 7:
                        Cell.material.color = new Color(255, 127, 0);
                        break;
                }
            }
        }
    }

    public bool IsInside(int row, int column)
    {
        if (row >= 0 && row < Rows && column >= 0 && column < Columns)
        {
            return true;
        }
        return false;
    }

    public bool IsEmpty(int row, int column)
    {
        if (IsInside(row, column) && grid[row, column] == 0)
        {
            return true;

        }
        return false;
    }

    public bool IsRowFull(int row)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (grid[row, c] == 0)
                return false;
        }
        return true;
    }

    public bool IsRowEmpty(int row)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (grid[row, c] != 0)
                return false;
        }
        return true;
    }

    public void ClearRow(int row)
    {
        for (int c = 0; c < Columns; c++)
        {
            grid[row, c] = 0;
        }
    }

    public void MoveRowDown(int row, int numRows)
    {
        for (int c = 0; c < Columns; c++)
        {
            grid[row - numRows, c] = grid[row, c];
            grid[row, c] = 0;
        }
    }

    public int ClearFullRow()
    {
        int clearedRows = 0;
        for (int r = 0; r < Rows; r++)
        {
            if (IsRowFull(r))
            {
                ClearRow(r);
                clearedRows++;
            }
            else if (clearedRows > 0)
            {
                MoveRowDown(r, clearedRows);
            }
        }
        return clearedRows;
    }
}
