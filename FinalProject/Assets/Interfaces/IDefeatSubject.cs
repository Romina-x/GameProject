using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefeatSubject
{

    public void RegisterObserver(IDefeatObserver observer);
    public void UnregisterObserver(IDefeatObserver observer);
    public void Notify();
}
