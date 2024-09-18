using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private DeadZoneLook _playerLookBehaviour;
    private AimBehaviour _playerAimBehaviour;

    private bool _isAiming;
    public bool IsAiming => _isAiming;

    private void Start()
    {
        _playerLookBehaviour = GetComponentInChildren<DeadZoneLook>();
        _playerAimBehaviour = GetComponent<AimBehaviour>();
        
        _playerAimBehaviour.PlayerAiming += (sender, args) =>
        {
            _playerLookBehaviour.UseDeadZone = false;
            _playerLookBehaviour.LerpAimToLook();
            _isAiming = true;
        };
        
        _playerAimBehaviour.PlayerNotAiming += (sender, args) =>
        {
            _playerLookBehaviour.UseDeadZone = true;
            _isAiming = false;
        };
    }
}