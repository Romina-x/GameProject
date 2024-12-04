using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefeatSubject
{

    public void RegisterDefeatObserver(IDefeatObserver observer);
    public void UnregisterDefeatObserver(IDefeatObserver observer);
    public void NotifyDefeatObservers();
}
