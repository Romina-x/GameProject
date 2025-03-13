using UnityEngine;
//AUTHOR Chris Kurhan

/// <summary>
/// Represents an object to be used in an object pool. 
/// When the object is disabled, it is returned to the parent <see cref="ObjectPool"/> for reuse.
/// </summary>
public class PoolableObject : MonoBehaviour
{
    public ObjectPool Parent;

    public virtual void OnDisable()
    {
        Parent.ReturnObjectToPool(this);
    }
}
