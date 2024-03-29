using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Tetromino
{
    protected abstract CellPosition[][] Cells
    {
        get;
    }
    protected abstract CellPosition StartOffset
    {
        get;
    }
    public CellPosition Offset;
    public abstract int Id
    { 
        get;
    }
    public int rotationState;

    public Tetromino()
    {
        Offset = new CellPosition(StartOffset.RowOffset, StartOffset.ColumnOffset);
    }

    public IEnumerable<CellPosition> CellPositions()
    {
        foreach (CellPosition cell in Cells[rotationState])
        {
            yield return new CellPosition(cell.RowOffset + Offset.RowOffset, cell.ColumnOffset + Offset.ColumnOffset);
        }
    }

    public void RotateClockWise()
    {
        rotationState = (rotationState + 1) % Cells.Length;
    }

    public void RotateAntiClockWise()
    {
        if (rotationState == 0)
        {
            rotationState = Cells.Length - 1;
        }
        else
        {
            rotationState -= 1;
        }
    }

    public void MoveBlock(int row, int col)
    {
        Offset.RowOffset += row;
        Offset.ColumnOffset += col;
    }

    public void Reset()
    {
        rotationState = 0;
        Offset.RowOffset = StartOffset.RowOffset;
        Offset.ColumnOffset = StartOffset.ColumnOffset;
    }
}

