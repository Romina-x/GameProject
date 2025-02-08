using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Observer interface for entities observing enemy defeats
public interface IDefeatObserver
{
    public void OnNotify(int score);
}
