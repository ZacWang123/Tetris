using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockQueue
{
    private Tetromino[] Blocks = new Tetromino[]
    {
        new IBlock(),
        new JBlock(),
        new LBlock(),
        new OBlock(),
        new SBlock(),
        new TBlock(),
        new ZBlock()
    };

    public Tetromino NextBlock 
    { 
        get; 
        private set; 
    }

    public BlockQueue()
    {
        NextBlock = RandomBlock();
    }
    private Tetromino RandomBlock()
    {
        return Blocks[Random.Range(0, Blocks.Length)];
    }

    public Tetromino PickBlock()
    {
        Tetromino Block = NextBlock;

        while (Block.Id == NextBlock.Id)
        {
            NextBlock = RandomBlock();
        }

        return Block;
    }
}
