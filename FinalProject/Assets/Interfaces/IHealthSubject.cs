using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IHealthSubject
{
    public void RegisterObserver(IHealthObserver observer);
    public void UnregisterObserver(IHealthObserver observer);
    public void Notify();
}

