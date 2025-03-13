using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Observer interface for entities observing health changes.
/// </summary>
public interface IHealthObserver
{
    public void OnNotify(float maxHealth, float currentHealth);
}
