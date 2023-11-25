using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    void Awake()
    {
        StartDisabled[] toDisable = FindObjectsByType<StartDisabled>(
            findObjectsInactive: FindObjectsInactive.Include,
            FindObjectsSortMode.InstanceID);

        foreach(StartDisabled obj in toDisable)
        {
            obj.gameObject.SetActive(false);
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
