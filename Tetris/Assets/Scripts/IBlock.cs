using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IBlock : Tetromino
{
    private CellPosition[][] cells = new CellPosition[][] 
    { 
        new CellPosition[] 
        {
            new(1,0), new(1,1), new(1,2), new(1,3)
        },
        new CellPosition[]
        {
            new(0,2), new(1,2), new(2,2), new(3,2)
        },
        new CellPosition[]
        {
            new(2,0), new(2,1), new(2,2), new(2,3)
        },
        new CellPosition[]
        {
            new(0,1), new(1,1), new(2,1), new(3,1)
        }
    };

    public override int Id => 1;
    protected override CellPosition StartOffset => new CellPosition(-1, 16);

    protected override CellPosition[][] Cells => cells;
}
