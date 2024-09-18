using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireGun : MonoBehaviour
{
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void Fire(Vector3 from, Transform lookTarget, Transform aimBase, Transform lookAtBase, float recoilAmount, PlayerState playerState, AnimationCurve recoilAnimCurve)
    {
        float distanceBetweenLookAtBaseAndTarget = Vector3.Distance(lookAtBase.position, lookTarget.position);
        float theta = Mathf.Atan(recoilAmount / distanceBetweenLookAtBaseAndTarget);
        float thetaInDegrees = theta * (180 / Mathf.PI);
        lookAtBase.Rotate(new Vector3(1, 0, 0), -thetaInDegrees);
        aimBase.Rotate(new Vector3(1, 0, 0), -thetaInDegrees);

        float yVariance = Random.Range(-recoilAmount * 10, recoilAmount * 10);
        lookAtBase.Rotate(new Vector3(0, 1, 0), yVariance);
        aimBase.Rotate(new Vector3(0, 1, 0), yVariance);

        StartCoroutine(AddVisualRecoil(playerState, recoilAnimCurve));
    }

    private IEnumerator AddVisualRecoil(PlayerState playerState, AnimationCurve recoilAnimCurve)
    {
        float timeElapsed = 0f;
        float lerpTime = 0.15f;

        Transform currentTransform = transform;

        float recoilModifier = playerState.IsAiming ? 0.1f : 1f;
        
        Vector3 newPos = _originalPosition + currentTransform.up * (0.02f * recoilModifier) - currentTransform.forward * (0.05f * recoilModifier);

        while (timeElapsed < lerpTime)
        {
            float t = timeElapsed / lerpTime;

            transform.localPosition = Vector3.Lerp(_originalPosition, newPos, recoilAnimCurve.Evaluate(t));

            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }
    }
}