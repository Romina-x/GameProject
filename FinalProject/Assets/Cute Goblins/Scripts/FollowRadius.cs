using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRadius : MonoBehaviour
{
    public delegate void PlayerEnterEvent();
    public PlayerEnterEvent PlayerEnter;
    public delegate void PlayerExitEvent();
    public PlayerExitEvent PlayerExit;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Ontriggerenter followradius");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null){
            PlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Ontriggerexit followradius");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null){
            PlayerExit?.Invoke();
        }
    }
}
