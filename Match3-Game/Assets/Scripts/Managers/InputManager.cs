using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GridManager gridManager;
    private Cell _firstCell;
    private bool _hasSwapped = false;
    // private MoveType _currentMoveType;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cell hitCell = TryToGetHitCell();
            if (hitCell != null)
            {
                if (_firstCell==null)
                {
                    _firstCell = hitCell;
                }
                // swap conditions
                if(_firstCell && hitCell != _firstCell && !_hasSwapped)
                {
                    Debug.Log(_firstCell.name + "    "+ hitCell.name);
                    gridManager.SwapMatchObjects(_firstCell,hitCell);
                    _firstCell = null;
                    _hasSwapped = true;
                }
            }
        }
         
        if (Input.GetMouseButtonUp(0))
        {
            _firstCell = null;
            _hasSwapped = false;
        }
    }
    
    /// <summary>
    /// This function creates a Raycast on the mouse position, to check for a hit on a "Cell" object. If a hit is detected,
    /// it returns the Cell component of the hit object. If no hit is detected, it returns null.
    /// </summary>
    /// <returns>The Cell component of the hit object or null if no hit is detected.</returns>
    private Cell TryToGetHitCell()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            if (hit.collider.CompareTag("Cell"))
            {
                return hit.collider.GetComponent<Cell>();
            }
        }
        return null;
    }

    // private void SetDirection(Cell firstCell, Cell secondCell)
    // {
    //     if (firstCell.RowNumber == secondCell.RowNumber)
    //     {
    //         if (firstCell.ColNumber +1 == secondCell.ColNumber)
    //         {
    //             _currentMoveType = MoveType.Right;
    //         }
    //         else if (firstCell.ColNumber -1 == secondCell.ColNumber)
    //         {
    //             _currentMoveType = MoveType.Left;
    //         }
    //     }
    //     else if (firstCell.ColNumber == secondCell.ColNumber)
    //     {
    //         if (firstCell.RowNumber +1 == secondCell.RowNumber)
    //         {
    //             _currentMoveType = MoveType.Up;
    //         }
    //         else if (firstCell.RowNumber -1 == secondCell.RowNumber)
    //         {
    //             _currentMoveType = MoveType.Down;
    //         }
    //     }
    // }
}
