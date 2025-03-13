using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for game objects that can take damage: player and enemies.
/// Defines methods for handling damage and accessing the object's transform.
/// </summary>
public interface IDamageable
{
    public void TakeDamage(int damage);
    public Transform GetTransform();
    public string GetName();
}
