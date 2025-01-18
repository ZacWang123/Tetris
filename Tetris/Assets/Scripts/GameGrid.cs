using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
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
            for (int rows = 0; rows < Rows - 2; rows++)
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
            for (int rows = 0; rows < Rows - 2; rows++)
            {
                Renderer Cell = VisualGrid[cols, rows];

                switch (Grid[cols, rows])
                {
                    case 0:
                        Cell.material.color = new Color32(127, 127, 127, 255);
                        break;

                    case 1:
                        Cell.material.color = new Color32(0, 255, 255, 255);
                        break;

                    case 2:
                        Cell.material.color = new Color32(255, 255, 0, 255);
                        break;

                    case 3:
                        Cell.material.color = new Color32(0, 255, 0, 255);
                        break;

                    case 4:
                        Cell.material.color = new Color32(255, 0, 0, 255);
                        break;

                    case 5:
                        Cell.material.color = new Color32(255, 127, 0, 255);
                        break;

                    case 6:
                        Cell.material.color = new Color32(0, 0, 255, 255);
                        break;

                    case 7:
                        Cell.material.color = new Color32(128, 0, 128, 255);
                        break;

                    case -1:
                        Cell.material.color = new Color32(0, 255, 255, 64);
                        break;

                    case -2:
                        Cell.material.color = new Color32(255, 255, 0, 64);
                        break;

                    case -3:
                        Cell.material.color = new Color32(0, 255, 0, 64);
                        break;

                    case -4:
                        Cell.material.color = new Color32(255, 0, 0, 64);
                        break;

                    case -5:
                        Cell.material.color = new Color32(255, 127, 0, 64);
                        break;

                    case -6:
                        Cell.material.color = new Color32(0, 0, 255, 64);
                        break;

                    case -7:
                        Cell.material.color = new Color32(128, 0, 128, 64);
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

    public int CheckRowClear() {
        int numRows = 0;
        for (int row = 0; row < Rows; row++) {
            if (RowFull(row) == 1) {
                numRows += 1;
                ClearRow(row);
                ShiftRowsDown(row);
            }
        }
        UpdateGridColour();
        return numRows;
    }

    public void ShiftRowsDown(int row) {
        for (int r = row; r < Rows - 1; r++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Grid[col, r] = Grid[col, r + 1];
            }
        }

        for (int col = 0; col < Columns; col++)
        {
            Grid[col, Rows - 1] = 0;
        }
    }

    public int RowFull(int row) {
        for (int col = 0; col < Columns; col++)
        {
            if (Grid[col, row] == 0) {
                return 0;
            }
        }
        return 1;
    }

    public void ClearRow(int row) {
        for (int col = 0; col < Columns; col++) {
            Grid[col, row] = 0;
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