using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int[,] Cells;

    public Block(int[,] cells) {
        Cells = cells;       
    }

    public static Block CreateI() {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateO() {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 1, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateS()
    {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 1, 1, 0},
            {1, 1, 0, 0},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateZ()
    {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 1, 1, 0},
            {0, 0, 1, 1},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateL()
    {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 0, 1, 0},
            {1, 1, 1, 0},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateJ()
    {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 1, 1},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public static Block CreateT()
    {
        int[,] cells = new int[,] {
            {0, 0, 0, 0},
            {0, 1, 0, 0},
            {1, 1, 1, 0},
            {0, 0, 0, 0}
        };

        return new Block(cells);
    }

    public void RotateClockwise()
    {
        int rows = Cells.GetLength(0);
        int cols = Cells.GetLength(1);
        int[,] rotated = new int[cols, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                rotated[col, rows - 1 - row] = Cells[row, col];
            }
        }

        Cells = rotated;
    }

    public void RotateAntiClockwise()
    {
        int rows = Cells.GetLength(0);
        int cols = Cells.GetLength(1);
        int[,] rotated = new int[cols, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                rotated[cols - 1 - col, row] = Cells[row, col];
            }
        }

        Cells = rotated;
    }
}
