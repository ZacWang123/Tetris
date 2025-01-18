using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NextBlockDisplay
{
    public int Rows;
    public int Columns;
    public int[,] Grid;
    public GameObject GridCell;
    public Renderer[,] VisualGrid;
    public int offsetX = 11;
    public int offsetY = 14;

    public NextBlockDisplay(int rows, int cols, GameObject cell)
    {
        Rows = rows;
        Columns = cols;
        Grid = new int[Columns, Rows];
        GridCell = cell;
    }

    public void DrawGrid()
    {
        VisualGrid = new Renderer[Columns, Rows];

        for (int cols = 0; cols < Columns; cols++)
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                GameObject Cell = Object.Instantiate(GridCell, new Vector2(cols + offsetX, rows + offsetY), Quaternion.identity);
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
                }
            }
        }
    }

    public void UpdateGrid(int col, int row, int value)
    {
        Grid[col, row] = value;
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