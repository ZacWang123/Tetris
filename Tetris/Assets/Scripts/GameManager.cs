using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject Cell;
    public GameGrid grid;
    public int height = 22;
    public int width = 10;
    public Block currentBlock;
    public GameObject gameOver;
    public int blockX;
    public int blockY;
    public int ghostX;
    public int ghostY;
    private int ID;
    private float updateInterval = 0.7f;
    private float time;
    private bool gameActive = true;

    void Start()
    {
        gameOver.SetActive(false);
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
        blockY = height;
        CheckGameOver();
        ID = currentBlock.ID;
        PlaceBlock();
    }

    public void CheckGameOver() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (grid.GetGridCell(gridX, gridY) != 0) {
                        GameOver();
                    }
                }
            }
        }
    }

    public void PlaceBlock() {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++) {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++) {
                if (currentBlock.Cells[cols, rows] == 1) {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    grid.UpdateGrid(gridX, gridY, ID);
                }
            }
        }
    }

    public void PlaceGhost()
    {
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = ghostX + rows;
                    int gridY = ghostY - cols;

                    grid.UpdateGrid(gridX, gridY, ID * -1);
                }
            }
        }
    }

    public void ClearGhost() 
    {
        for (int cols = 0; cols<currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows<currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = ghostX + rows;
                    int gridY = ghostY - cols;

                    if (grid.WithinGrid(gridX, gridY))
                    {
                        grid.UpdateGrid(gridX, gridY, 0);
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
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            RotateAnticlockwise();

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            RotateClockwise();

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBlock();

        }
    }

    public void MoveLeft() {
        ClearBlock();
        ClearGhost();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows - 1;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0) {
                        PlaceBlock();
                        return;
                    }
                }
            }
        }

        blockX--;
        GhostBlock();
        PlaceBlock();
    }

    public void MoveRight() {
        ClearBlock();
        ClearGhost();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows + 1;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
                    {
                        PlaceBlock();
                        return;
                    }
                }
            }
        }

        blockX++;
        GhostBlock();
        PlaceBlock();
    }

    public void DropBlock() {
        while (ValidMoveDown() == 1) {
            blockY--;
        }
        ClearGhost();
        PlaceBlock();
        NewBlock();
        GhostBlock();
    }

    public int ValidMoveDown()
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

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
                    {
                        ClearBlock();
                        return 0;
                    }
                }
            }
        }
        ClearBlock();
        return 1;
    }

    public void GhostBlock()
    {
        ghostX = blockX;
        ghostY = blockY;

        while (GhostValidMoveDown() == 1)
        {
            ghostY--;
        };
        PlaceGhost();
    }

    public int GhostValidMoveDown()
    {
        ClearBlock();
        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = ghostX + rows;
                    int gridY = ghostY - cols - 1;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
                    {
                        PlaceBlock();
                        return 0;
                    }
                }
            }
        }
        PlaceBlock();
        return 1;
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

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
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
        GhostBlock();
        PlaceBlock();
    }

    public void RotateAnticlockwise() {
        ClearBlock();
        ClearGhost();
        currentBlock.RotateAntiClockwise();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
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

        GhostBlock();
        PlaceBlock();
    }

    public void RotateClockwise()
    {
        ClearBlock();
        ClearGhost();
        currentBlock.RotateClockwise();

        for (int cols = 0; cols < currentBlock.Cells.GetLength(0); cols++)
        {
            for (int rows = 0; rows < currentBlock.Cells.GetLength(1); rows++)
            {
                if (currentBlock.Cells[cols, rows] == 1)
                {
                    int gridX = blockX + rows;
                    int gridY = blockY - cols;

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
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

        GhostBlock();
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

                    if (!grid.WithinGrid(gridX, gridY) || grid.GetGridCell(gridX, gridY) > 0)
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
        gameActive = false;
        gameOver.SetActive(true);
    }

    public void RestartGame() {
        gameOver.SetActive(false);
        grid.ResetGrid();
        gameActive = true;
        NewBlock();
    }

    public void ExitGame() {
        Application.Quit();

        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
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
