using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple level builder with 9x9 harcoded
public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private Color[] blockColors;

    [SerializeField] private VoidEventSO LevelWin;
    [SerializeField] private VoidEventSO LevelFailed;
    [SerializeField] private IntEventSO OnBlockDestroyed;


    private Vector2 blockSize;
    private Queue<Block> _pool = new Queue<Block>();

    private readonly string[] Level1 = new string[] {
        "000000000",
        "000000000",
        "000010000",
        "000121000",
        "001222100",
        "011111110",
        "000000000",
        "000000000",
        "000000000"
    };
    private readonly string[] Level2 = new string[] {
        "000000000",
        "011111110",
        "012222210",
        "001232100",
        "000131000",
        "001232100",
        "012222210",
        "011111110",
        "000000000"
    };
    private readonly string[] Level3 = new string[] {
        "000000000",
        "000111000",
        "001222100",
        "012333210",
        "012333210",
        "012333210",
        "001222100",
        "000111000",
        "000000000"
    };
    private int levelIndex = 0;
    private string[][] levels;
    private List<Block> spawnedBlocks = new List<Block>();

   
    private void Awake()
    {
        var render =  blockPrefab.GetComponent<SpriteRenderer>();
        blockSize = new Vector2(render.bounds.size.x, render.bounds.size.y);
    }
    private void Start()
    {
        SetupPool();
        levels = new string[][] { Level1, Level2, Level3 };
        BuildLevel(levels[levelIndex]);
    }
    private void OnEnable()
    {
        LevelWin.OnEventRaised += BuildNextLevel;
        LevelFailed.OnEventRaised += ResetLevelBlocks;
    }
    private void OnDisable()
    {
        LevelWin.OnEventRaised += BuildNextLevel;
        LevelFailed.OnEventRaised -= ResetLevelBlocks;
    }
    private void ResetLevelBlocks()
    {
        if (spawnedBlocks.Count > 0)
        {
            foreach (Block block in spawnedBlocks)
            {
                block.gameObject.SetActive(false);
            }
            spawnedBlocks.Clear();
        }
        BuildLevel(levels[levelIndex]);
    }
    private void BuildNextLevel()
    {
        levelIndex++;
        if (levelIndex > levels.Length - 1)
        {
            levelIndex = 0;
        }
        ResetLevelBlocks();
    }


 
    private void SetupPool()
    {
        if (blockPrefab != null)
        {
            int size = 9 * 9;
            for (int i = 0; i < size; ++i)
            {
                Block block = Instantiate(blockPrefab);
                block.gameObject.SetActive(false);
                _pool.Enqueue(block);
            }
        }
    }

    private Block SetBlock(int type, Vector2 position)
    {
        if (type == 0)
        {
            return null;
        }

        var pooledBlock = _pool.Dequeue();
        pooledBlock.lifeAmount = type;
        pooledBlock.GetComponent<SpriteRenderer>().color = blockColors[type - 1];
        pooledBlock.transform.position = position;
        pooledBlock.gameObject.SetActive(true);
        pooledBlock.BlockRemoved += OnBlockRemoved;
        _pool.Enqueue(pooledBlock);
        spawnedBlocks.Add(pooledBlock);
        return pooledBlock;
    }

    private void OnBlockRemoved(Block block)
    {
        //block.BlockRemoved -= OnBlockRemoved;
        if (spawnedBlocks.Contains(block))
        {
            spawnedBlocks.Remove(block);
            OnBlockDestroyed.RaiseEvent(spawnedBlocks.Count);
        }
      
    }

    private void BuildLevel(string[] level)
    {
        var topLeftPosition = new Vector2(-4 * blockSize.x, 4 * blockSize.y);
        var newBlockPosition = topLeftPosition;
        //lines
        for (int i = 0; i < level.Length; i++)
        {
            newBlockPosition.y = topLeftPosition.y - blockSize.y * i;
            //rows
            for (int j = 0; j < level[i].Length; j++)
            {
                newBlockPosition.x = topLeftPosition.x + blockSize.x * j;
                               
                int val = (int)Char.GetNumericValue(level[i][j]);
                
                SetBlock(val, newBlockPosition);
            }
        }
       
    }
}
