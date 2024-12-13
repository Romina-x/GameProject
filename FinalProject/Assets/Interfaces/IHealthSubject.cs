using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subject interface for health changes
public interface IHealthSubject
{
    public void RegisterHealthObserver(IHealthObserver observer);
    public void UnregisterHealthObserver(IHealthObserver observer);
    public void NotifyHealthObservers();
}

