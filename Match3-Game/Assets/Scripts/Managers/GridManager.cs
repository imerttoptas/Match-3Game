using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private int gridSizeX, gridSizeY;
    [SerializeField] private float offset;
    
    void Start()
    {
        gridBuilder.GenerateGrid(gridSizeX, gridSizeY, offset);
    }
    
}
