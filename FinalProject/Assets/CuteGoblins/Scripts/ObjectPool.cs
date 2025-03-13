using System.Collections.Generic;
using UnityEngine;
//AUTHOR Chris Kurhan

/// <summary>
/// Manages a pool of reusable poolable objects. Used for the archer enemy's arrows.
/// ObjectPool allows arrows to be reused instead of creating and destroying many.
/// </summary>
public class ObjectPool
{
    private PoolableObject _prefab; // Arrow
    private int _size; // How many arrows to use
    private List<PoolableObject> _availableObjectsPool;

    /// <summary>
    /// Creates a new instance of ObjectPool with the specified prefab and size.
    /// </summary>
    /// <param name="prefab">The prefab to be pooled.</param>
    /// <param name="size">The number of objects to create in the pool.</param>
    private ObjectPool(PoolableObject prefab, int size)
    {
        this._prefab = prefab;
        this._size = size;
        _availableObjectsPool = new List<PoolableObject>(size);
    }

    /// <summary>
    /// Uses the constructor to create a new object pool, creates the objects and returns the new pool.
    /// </summary>
    /// <param name="prefab">The prefab to be pooled.</param>
    /// <param name="size">The number of objects to create in the pool.</param>
    /// <returns>The new object pool.</returns>
    public static ObjectPool CreateInstance(PoolableObject prefab, int size)
    {
        ObjectPool pool = new ObjectPool(prefab, size);

        GameObject poolGameObject = new GameObject(prefab + " Pool");
        pool.CreateObjects(poolGameObject);

        return pool;
    }

    /// <summary>
    /// Creates the objects in the pool and adds them to the available objects list.
    /// </summary>
    /// <param name="parent">The parent GameObject under which the pooled objects will be placed.</param>
    private void CreateObjects(GameObject parent)
    {
        for (int i = 0; i < _size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false); // PoolableObject handles re-adding the object to the AvailableObjects
        }
    }

    /// <summary>
    /// Gets an available object from the pool.
    /// </summary>
    /// <returns>Pooled object to use.</returns>
    public PoolableObject GetObject()
    {
        PoolableObject instance = _availableObjectsPool[0];

        _availableObjectsPool.RemoveAt(0);

        instance.gameObject.SetActive(true);

        return instance;
    }

    /// <summary>
    /// Returns an object to the pool after its been disabled to make it available for reuse.
    /// </summary>
    /// <param name="Object">The object to return to the pool.</param>
    public void ReturnObjectToPool(PoolableObject Object)
    {
        _availableObjectsPool.Add(Object);
    }
}
