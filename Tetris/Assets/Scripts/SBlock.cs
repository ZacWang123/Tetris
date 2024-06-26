using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBlock : Tetromino
{
    private CellPosition[][] cells = new CellPosition[][]
     {
        new CellPosition[]
        {
            new(0,1), new(0,2), new(1,0), new(1,1)
        },
        new CellPosition[]
        {
            new(0,1), new(1,1), new(1,2), new(2,2)
        },
        new CellPosition[]
        {
            new(1,1), new(1,2), new(2,0), new(2,1)
        },
        new CellPosition[]
        {
            new(0,0), new(1,0), new(1,1), new(2,1)
        }
     };

    public override int Id => 5;
    protected override CellPosition StartOffset => new CellPosition(0, 17);
    protected override CellPosition[][] Cells => cells;
}
