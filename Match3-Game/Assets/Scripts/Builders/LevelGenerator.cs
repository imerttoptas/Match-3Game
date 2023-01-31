using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<MatchObject> matchObjectPrefabList;
    
    
    public void GenerateRandomLevel(List<Cell> cellList)
    {
        foreach (Cell cell in cellList)
        {
            CreateMatchObject(cell.transform);
        }
    }
    
    private void CreateMatchObject(Transform targetTransform)
    {
        MatchObject matchObjectPrefab = GetRandomMatchObject();
        MatchObject createdMatchObject = Instantiate(matchObjectPrefab);
        createdMatchObject.Init(targetTransform);
    }
    
    private MatchObject GetRandomMatchObject()
    {
        int randomIndex = Random.Range(0, matchObjectPrefabList.Count - 1);
        return matchObjectPrefabList[randomIndex];
    }
}
