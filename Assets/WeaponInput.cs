using System;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Gun currentlyEquippedGun;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform lookAtBase;
    
    private void Update()
    {
        if (Input.GetButtonDown(Constants.Fire1Key))
        {
            currentlyEquippedGun.TriggerDown(aimTarget, lookTarget, lookAtBase);
        }

        if (Input.GetButtonUp(Constants.Fire1Key))
        {
            currentlyEquippedGun.TriggerUp();
        }
    }
}