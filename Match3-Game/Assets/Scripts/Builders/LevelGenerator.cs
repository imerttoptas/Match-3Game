using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Block> blockPrefabs;
    [SerializeField] private MatchChecker matchChecker;
    
    public void GenerateRandomLevel(List<Cell> cellList)
    {
        foreach (Cell cell in cellList)
        {
            CreateBlock(cell);
        }
    }
    
    private void CreateBlock(Cell cell)
    {
        Transform targetTransform = cell.transform;
        Block blockPrefab = GetRandomBlock();
        
        while (!IsValidBlock(cell,blockPrefab.BlockType))
        {
            blockPrefab = GetRandomBlock();
        }
        Block createdBlock = Instantiate(blockPrefab); 
        createdBlock.Init(targetTransform);
    }
    
    private Block GetRandomBlock()
    {
        int randomIndex = Random.Range(0, blockPrefabs.Count - 1);
        return blockPrefabs[randomIndex];
    }

    private bool IsValidBlock(Cell cell,BlockType blockType)
    {
        return !matchChecker.IsThereAMatch(cell,blockType);
    }

}
