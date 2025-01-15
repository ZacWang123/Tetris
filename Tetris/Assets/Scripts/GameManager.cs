using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;
    public int height = 20;
    public int width = 10;
    public Block currentBlock;
    public int blockX;
    public int blockY;
    private float updateInterval = 0.7f;
    private float time;
    private bool gameActive = true;

    void Start()
    {
        grid = new GameGrid(height, width, Cell);
        grid.DrawGrid();
        NewBlock();
        PlaceBlock();
        grid.UpdateGridColour();
    }

    public void NewBlock() {
        int randomBlock = Random.Range(0, 7);
        switch (randomBlock) {
            case 0:
                currentBlock = Block.CreateI();
                break;
            case 1:
                currentBlock = Block.CreateO();
                break;
            case 2:
                currentBlock = Block.CreateS();
                break;
            case 3:
                currentBlock = Block.CreateZ();
                break;
            case 4:
                currentBlock = Block.CreateL();
                break;
            case 5:
                currentBlock = Block.CreateJ();
                break;
            case 6:
                currentBlock = Block.CreateT();
                break;
        }

        blockX = width / 2 - currentBlock.Cells.GetLength(1) / 2;
        blockY = height - 1;
        PlaceBlock();
    }

    public void PlaceBlock() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++) {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++) {
                if (currentBlock.Cells[cols, rows] == 1) {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    grid.UpdateGrid(gridX, gridY, 1);
                }
            }
        }
    }

    public void CheckMovement() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();

        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            RotateAnticlockwise();

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            RotateClockwise();

        }
    }

    public void MoveLeft() {
        ClearBlock();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows - 1;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0) {
                        PlaceBlock();
                        return;
                    }
                }
            }
        }

        blockX--;
        PlaceBlock();
    }

    public void MoveRight() {
        ClearBlock();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows + 1;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0)
                    {
                        PlaceBlock();
                        return;
                    }
                }
            }
        }

        blockX++;
        PlaceBlock();
    }

    public void MoveDown()
    {
        ClearBlock();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols - 1;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0)
                    {
                        PlaceBlock();
                        grid.CheckRowClear();
                        NewBlock();
                        return;
                    }
                }
            }
        }

        blockY--;
        PlaceBlock();
    }
    public void RotateAnticlockwise() {
        ClearBlock();
        currentBlock.RotateAntiClockwise();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0)
                    {
                        if (WallKick(1) != 1)
                        {
                            if (WallKick(-1) != 1) {
                                currentBlock.RotateClockwise();
                            }
                        }
                    }
                }
            }
        }

        PlaceBlock();
    }

    public void RotateClockwise()
    {
        ClearBlock();
        currentBlock.RotateClockwise();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0)
                    {
                        if (WallKick(1) != 1)
                        {
                            if (WallKick(-1) != 1)
                            {
                                currentBlock.RotateAntiClockwise();
                            }
                        }
                    }
                }
            }
        }

        PlaceBlock();
    }

    public int WallKick(int x) {
        blockX += x;

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) != 0)
                    {
                        blockX -= x;
                        return 0;
                    }
                }
            }
        }

        return 1;
    }

    public void ClearBlock() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (grid.WithinGrid(gridX, gridY))
                    {
                        grid.UpdateGrid(gridX, gridY, 0);
                    }
                }
            }
        }
    }

    public void GameOver() {
        Debug.Log("Game Over");
    }

    void Update()
    {
        if (gameActive) {
            CheckMovement();
            grid.UpdateGridColour();
            time += Time.deltaTime;
            if (time > updateInterval) {
                MoveDown();
                time = 0f;
            }
        }
    }
}
