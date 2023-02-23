using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private BlockType blockType;
    public BlockType BlockType => blockType;
        
    public void Init(Transform _transform)
    {
        transform.position = _transform.position;
        transform.SetParent(_transform);
        transform.localScale = _transform.localScale;
    }
    
}
