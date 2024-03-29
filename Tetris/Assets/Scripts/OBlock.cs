using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBlock : Tetromino
{
    private CellPosition[][] cells = new CellPosition[][]
{
        new CellPosition[]
        {
            new(0,0), new(0,1), new(1,0), new(1,1)
        }
};

    public override int Id => 4;
    protected override CellPosition StartOffset => new CellPosition(0, 4);
    protected override CellPosition[][] Cells => cells;
}
