using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subject interface for enemy defeats
public interface IDefeatSubject
{

    public void RegisterDefeatObserver(IDefeatObserver observer);
    public void UnregisterDefeatObserver(IDefeatObserver observer);
    public void NotifyDefeatObservers();
}
