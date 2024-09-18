using System;
using System.Collections;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    [SerializeField] private Transform rearSightTransform;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Camera mainCamera;

    private Vector3 _originalPosition;
    private IEnumerator _weaponPositionLerper;
    private float _originalFOV;

    public event EventHandler<EventArgs> PlayerAiming;
    public event EventHandler<EventArgs> PlayerNotAiming;
    
    private void Start()
    {
        _originalPosition = rearSightTransform.localPosition;
        _originalFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        Vector3 rearSightPositionInWorld = rearSightTransform.position;

        if (Input.GetButtonDown(Constants.Fire2Key))
        {
            Ray middleOfScreen = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            Debug.DrawRay(middleOfScreen.origin, middleOfScreen.direction, Color.red, 3f);

            rearSightPositionInWorld = middleOfScreen.GetPoint(0.1f);

            Vector3 localPoint = rearSightTransform.parent.InverseTransformPoint(rearSightPositionInWorld);
            StartWeaponLerpCoroutine(localPoint, 40, 0.2f);

            PlayerAiming?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetButtonUp(Constants.Fire2Key))
        {
            StartWeaponLerpCoroutine(_originalPosition, _originalFOV, 0.2f);
            PlayerNotAiming?.Invoke(this, EventArgs.Empty);
        }

        Quaternion lookAtRotation = Quaternion.LookRotation(rearSightPositionInWorld - aimTarget.position);
        rearSightTransform.rotation = lookAtRotation;
    }

    private void StartWeaponLerpCoroutine(Vector3 positionToLerpTo, float fovToLerpTo, float lerpTime)
    {
        if (!ReferenceEquals(_weaponPositionLerper, null))
        {
            StopCoroutine(_weaponPositionLerper);
        }

        _weaponPositionLerper = LerpWeaponCoRoutine(positionToLerpTo, fovToLerpTo, lerpTime);
        StartCoroutine(_weaponPositionLerper);
    }

    private IEnumerator LerpWeaponCoRoutine(Vector3 positionToLerpTo, float fovToLerpTo, float lerpTime)
    {
        Vector3 fromPosition = rearSightTransform.localPosition;
        float fromFov = mainCamera.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            float t = elapsedTime / lerpTime;
            elapsedTime += Time.deltaTime;

            rearSightTransform.localPosition = Vector3.Lerp(fromPosition, positionToLerpTo, t);
            mainCamera.fieldOfView = Mathf.Lerp(fromFov, fovToLerpTo, t);

            yield return new WaitForEndOfFrame();
        }

        rearSightTransform.localPosition = positionToLerpTo;
        mainCamera.fieldOfView = fovToLerpTo;
    }
}