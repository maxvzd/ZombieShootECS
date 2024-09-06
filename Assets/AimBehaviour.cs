using System.Collections;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{

    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform adsTransform;

    private Vector3 _originalPosition;
    private IEnumerator _weaponPositionLerper;
    
    private void Start()
    {
        _originalPosition = weaponTransform.localPosition;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            StartWeaponLerpCoroutine(adsTransform.localPosition, 0.2f);
        }

        if (Input.GetMouseButtonUp(1))
        {
            StartWeaponLerpCoroutine(_originalPosition, 0.2f);
        }
    }

    private void StartWeaponLerpCoroutine(Vector3 positionToLerpTo, float lerpTime)
    {
        if (!ReferenceEquals(_weaponPositionLerper, null))
        {
            StopCoroutine(_weaponPositionLerper);   
        }

        _weaponPositionLerper = LerpWeaponCoRoutine(positionToLerpTo, lerpTime);
        StartCoroutine(_weaponPositionLerper);
    }

    private IEnumerator LerpWeaponCoRoutine(Vector3 positionToLerpTo, float lerpTime)
    {
        Vector3 fromPosition = weaponTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            float t = elapsedTime / lerpTime;
            elapsedTime += Time.deltaTime;

            weaponTransform.localPosition = Vector3.Lerp(fromPosition, positionToLerpTo, t);

            yield return new WaitForEndOfFrame();
        }

        weaponTransform.localPosition = positionToLerpTo;
    }
}
