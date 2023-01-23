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
            CreateMatchObject(cell.transform);
        }
    }
    
    private void CreateMatchObject(Transform transform)
    {
        GameObject createdMatchObject = Instantiate(GetRandomMatchObject(), transform, true);
        createdMatchObject.transform.position = transform.position;
        createdMatchObject.transform.localScale = transform.localScale;
        createdMatchObject.transform.SetParent(transform);
    }

    private GameObject GetRandomMatchObject()
    {
        int randomIndex = Random.Range(0, matchObjectPrefabsList.Count - 1);
        return matchObjectPrefabsList[randomIndex];
    }
}
