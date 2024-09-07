using System.Collections;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform adsTransform;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Camera mainCamera;

    private Vector3 _originalPosition;
    private IEnumerator _weaponPositionLerper;
    private float _originalFOV;

    private bool _isAiming;

    private void Start()
    {
        _originalPosition = adsTransform.localPosition;
        _originalFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray middleOfScreen = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Debug.DrawRay(middleOfScreen.origin, middleOfScreen.direction, Color.red, 3f);

            Vector3 adsPositionWorld = middleOfScreen.GetPoint(0.05f);
            Vector3 localPoint = adsTransform.parent.InverseTransformPoint(adsPositionWorld); //+ transform.up * 0.005f;

            //IMPROVE THIS
            adsTransform.forward = -middleOfScreen.direction;

            StartWeaponLerpCoroutine(localPoint, 40, 0.2f);
            _isAiming = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            StartWeaponLerpCoroutine(_originalPosition, _originalFOV, 0.2f);
            _isAiming = false;
        }

        if (!_isAiming)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(weaponTransform.position - aimTarget.position);
            adsTransform.rotation = lookAtRotation;
        }
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
        Vector3 fromPosition = adsTransform.localPosition;
        float fromFov = mainCamera.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            float t = elapsedTime / lerpTime;
            elapsedTime += Time.deltaTime;

            adsTransform.localPosition = Vector3.Lerp(fromPosition, positionToLerpTo, t);
            mainCamera.fieldOfView = Mathf.Lerp(fromFov, fovToLerpTo, t);

            yield return new WaitForEndOfFrame();
        }

        adsTransform.localPosition = positionToLerpTo;
        mainCamera.fieldOfView = fovToLerpTo;
    }
}