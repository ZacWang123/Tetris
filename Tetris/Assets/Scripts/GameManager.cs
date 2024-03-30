using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private GameGrid gameArray;
    public GameObject gridCell;
    public BlockQueue blockQueue;
    public Tetromino currentBlock;


    public bool WithinGrid()
    {
        foreach (CellPosition position in currentBlock.CellPositions())
        {
            if (!gameArray.IsInside(position.RowOffset, position.ColumnOffset))
            {
                return false;
            }
        }
        return true;
    }

    public void RotateClockWise()
    {
        ResetCell();
        currentBlock.RotateClockWise();
        if (!WithinGrid())
        {
            currentBlock.RotateAntiClockWise();
        }
        PlaceBlock();
    }

    public void RotateAntiClockWise()
    {
        ResetCell();
        currentBlock.RotateAntiClockWise();
        if (!WithinGrid())
        {
            currentBlock.RotateClockWise();
        }
        PlaceBlock();
    }

    public void MoveBlockLeft()
    {
        ResetCell();
        currentBlock.MoveBlock(-1, 0);
        if (!WithinGrid())
        {
            currentBlock.MoveBlock(1, 0);
        }
        PlaceBlock();
    }

    public void MoveBlockRight()
    {
        ResetCell();
        currentBlock.MoveBlock(1, 0);
        if (!WithinGrid())
        {
            currentBlock.MoveBlock(-1, 0);
        }
        PlaceBlock();
    }

    public void MoveBlockDown()
    {
        ResetCell();
        currentBlock.MoveBlock(0, -1);
        if (!WithinGrid())
        {
            currentBlock.MoveBlock(0, 1);
        }
        PlaceBlock();
    }

    public void ResetCell()
    {
        foreach (CellPosition position in currentBlock.CellPositions())
        {
            gameArray.UpdateGrid(position.RowOffset, position.ColumnOffset, 0);
        }
    }

    public void PlaceBlock()
    {
        foreach (CellPosition position in currentBlock.CellPositions())
        {
            gameArray.UpdateGrid(position.RowOffset, position.ColumnOffset, currentBlock.Id);
        }
    }

    void Start()
    {
        gameArray = new GameGrid(10, 20, gridCell);
        blockQueue = new BlockQueue();
        currentBlock = blockQueue.PickBlock();
        gameArray.DrawGrid();
        PlaceBlock();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBlockLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBlockRight();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBlockDown();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            RotateAntiClockWise();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            RotateAntiClockWise();
        }

        gameArray.UpdateGridColour();   
    }
}
