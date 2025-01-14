using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;
    public int height = 20;
    public int width = 10;
    public Block currentBlock;
    public int blockX;
    public int blockY;
    private float updateInterval = 0.05f;
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
        blockY = height - currentBlock.Cells.GetLength(0);
    }

    public void PlaceBlock() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++) {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++) {
                if (currentBlock.Cells[cols, rows] == 1) {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols + 1;

                    if (grid.WithinGrid(gridX, gridY)) {
                        grid.UpdateGrid(gridX, gridY, 1);
                    }
                    else {
                        GameOver();
                    }
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
    }

    public void MoveLeft() {
        return;
    }

    public void MoveRight() {
        return;
    }

    public void MoveDown() {
        return;
    }

    public void ClearBlock() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols + 1;

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
            time += Time.deltaTime;
            if (time > updateInterval) {
                CheckMovement();
                grid.UpdateGridColour();
            }
        }
    }
}
