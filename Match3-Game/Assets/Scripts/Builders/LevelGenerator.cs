using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> matchObjectPrefabsList;
    
    
    public void GenerateRandomLevel(List<Cell> cellList)
    {
        foreach (Cell cell in cellList)
        {
            CreateMatchObject(cell);
        }
    }
    
    private void CreateMatchObject(Cell cell)
    {
        Transform matchObjectTransform = cell.transform;
        GameObject matchObjectPrefab = GetRandomMatchObject();
        GameObject createdMatchObject = Instantiate(matchObjectPrefab, matchObjectTransform, true);
        cell.cellType = (CellType)matchObjectPrefabsList.IndexOf(matchObjectPrefab);
        createdMatchObject.transform.position = matchObjectTransform.position;
        createdMatchObject.transform.localScale = matchObjectTransform.localScale;
        createdMatchObject.transform.SetParent(matchObjectTransform);
    }
    
    private GameObject GetRandomMatchObject()
    {
        int randomIndex = Random.Range(0, matchObjectPrefabsList.Count - 1);
        return matchObjectPrefabsList[randomIndex];
    }
}
