using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameGrid gameArray;
    public GameObject GridCell;

    void Start()
    {
        gameArray = new GameGrid(10, 20, GridCell);
        gameArray.DrawGrid();
    }

    
    void Update()
    {
        gameArray.UpdateGridColour();   
    }
}
