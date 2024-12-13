using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Observer interface for entities observing health changes
public interface IHealthObserver
{
    public void OnNotify(float maxHealth, float currentHealth);
}
