using System;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Gun currentlyEquippedGun;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform aimBase;
    [SerializeField] private Transform lookAtBase;
    private PlayerState _playerState;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Fire1Key))
        {
            currentlyEquippedGun.TriggerDown(aimBase, lookTarget, lookAtBase, _playerState);
        }

        if (Input.GetButtonUp(Constants.Fire1Key))
        {
            currentlyEquippedGun.TriggerUp();
        }
    }
}