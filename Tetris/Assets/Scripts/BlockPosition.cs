using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPosition
{
    public int RowOffset;
    public int ColumnOffset;

    public CellPosition(int rowOffset, int columnOffset)
    {
        RowOffset = rowOffset;
        ColumnOffset = columnOffset;
    }
}
