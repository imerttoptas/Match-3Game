using UnityEngine;

public class MatchObject : MonoBehaviour
{
    [SerializeField] private float offset;
    public MatchObjectType matchObjectType;
    
    
    public void Init(Transform _transform)
    {
        transform.position = _transform.position;
        transform.SetParent(_transform);
        transform.localScale = _transform.localScale;
    }
    
    
}
