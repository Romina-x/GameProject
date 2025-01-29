using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subject interface for animal rescues
public interface IRescueSubject
{
    void RegisterRescueObserver(IRescueObserver observer);
    void UnregisterRescueObserver(IRescueObserver observer);
    void NotifyRescueObservers();
}
