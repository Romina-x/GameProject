using NUnit.Framework;
using UnityEngine;

public class ObjectPoolTests
{
    private ObjectPool _objectPool;
    private MockPoolableObject _mockPrefab; // Using mock prefab
    private GameObject _parentObject;

    [SetUp]
    public void SetUp()
    {
        // Create a mock prefab 
        _parentObject = new GameObject("Parent");
        _mockPrefab = new GameObject("MockPoolableObject").AddComponent<MockPoolableObject>();

        // Create the object pool with the mock prefab and 5 objects
        _objectPool = ObjectPool.CreateInstance(_mockPrefab, 5);

    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        GameObject.DestroyImmediate(_parentObject);
    }

    [Test]
    public void CreateInstance_Creates_Pool_With_Correct_Size()
    {
        // Verify that the pool contains the correct number of objects
        var availableObjects = _objectPool.AvailableObjectsPool;
        Assert.AreEqual(5, availableObjects.Count);  // The pool size should be 5
    }

    [Test]
    public void GetObject_Returns_Valid_Object()
    {
        // Get an object from the pool
        var pooledObject = _objectPool.GetObject();
        
        // Assert that the object is not null and is active
        Assert.IsNotNull(pooledObject);
        Assert.IsTrue(pooledObject.gameObject.activeSelf);
    }

    [Test]
    public void ReturnObjectToPool_Returns_Object()
    {
        // Get an object, then return it to the pool
        var pooledObject = _objectPool.GetObject();
        _objectPool.ReturnObjectToPool(pooledObject);
        
        // Assert that the object is added back to the pool
        var availableObjects = _objectPool.AvailableObjectsPool;
        Assert.Contains(pooledObject, availableObjects);
    }
}
