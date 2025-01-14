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
            {1, 1, 1, 1} 
        };

        return new Block(cells);
    }

    public static Block CreateO() {
        int[,] cells = new int[,] {
            {1, 1}, 
            {1, 1}
        };

        return new Block(cells);
    }

    public static Block CreateS()
    {
        int[,] cells = new int[,] {
            {0, 1, 1},
            {1, 1, 0}
        };

        return new Block(cells);
    }

    public static Block CreateZ()
    {
        int[,] cells = new int[,] {
            {1, 1, 0},
            {0, 1, 1}
        };

        return new Block(cells);
    }

    public static Block CreateL()
    {
        int[,] cells = new int[,] {
            {0, 0, 1},
            {1, 1, 1}
        };

        return new Block(cells);
    }

    public static Block CreateJ()
    {
        int[,] cells = new int[,] {
            {1, 0, 0},
            {1, 1, 1}
        };

        return new Block(cells);
    }

    public static Block CreateT()
    {
        int[,] cells = new int[,] {
            {0, 1, 0},
            {1, 1, 1}
        };

        return new Block(cells);
    }
}
