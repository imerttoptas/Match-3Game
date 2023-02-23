using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GridManager gridManager;
    private Cell _firstCell;
    private bool _hasSwapped;
    
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
                    gridManager.SwapBlocks(_firstCell,hitCell);
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
    
    
}
