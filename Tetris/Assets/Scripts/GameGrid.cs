using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class GameGrid : MonoBehaviour
{

    public GameObject GridCell;

    public int Rows;
    public int Columns;
    public int[,] grid;
    public GameObject[,] VisualGrid;

    public bool IsInside(int row, int column)
    {
        if (row >= 0 && row < Rows && column >= 0 && column <= Columns)
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
        for (int c = 0; c <= Columns; c++)
        {
            if (grid[row, c] == 0)
                return false;
        }

        return true;
    }

    public bool IsRowEmpty(int row)
    {
        for (int c = 0; c <= Columns; c++)
        {
            if (grid[row, c] != 0)
                return false;
        }

        return true;
    }

    private void Start()
    {
        grid = new int[Rows, Columns];

        VisualGrid = new GameObject[Rows, Columns];

        for (int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Columns; cols++)
            {
                VisualGrid[rows, cols] = (GameObject)Instantiate(GridCell, new Vector2(rows, cols), Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        for(int rows = 0; rows < Rows; rows++)
        {
            for (int cols = 0; cols < Columns; cols++)
            {
                GameObject Cell = VisualGrid[rows, cols];

                switch (grid[rows, cols])
                {
                    case 0:
                        Cell.GetComponent<Renderer>().material.color = new Color(127, 127, 127);
                        break;

                    case 1:
                        Cell.GetComponent<Renderer>().material.color = new Color(0, 255, 255);
                        break;

                    case 2:
                        Cell.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
                        break;

                    case 3:
                        Cell.GetComponent<Renderer>().material.color = new Color(128, 0, 128);
                        break;

                    case 4:
                        Cell.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                        break;

                    case 5:
                        Cell.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                        break;

                    case 6:
                        Cell.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
                        break;

                    case 7:
                        Cell.GetComponent<Renderer>().material.color = new Color(255, 127, 0);
                        break;
                }
            }
        }
    }
}
