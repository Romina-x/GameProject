using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Observer interface for entities observing animal rescues
public interface IRescueObserver
{
    void OnAnimalRescued();
}
