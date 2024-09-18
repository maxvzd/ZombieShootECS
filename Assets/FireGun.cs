using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireGun : MonoBehaviour
{
    private Vector3 _originalPosition;
    private AudioSource _audioSource;

    private void Start()
    {
        _originalPosition = transform.localPosition;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Fire(FireGunTransforms gunTransforms, float recoilAmount, PlayerState playerState, AnimationCurve recoilAnimCurve, AudioClip fireSound)
    {
        _audioSource.PlayOneShot(fireSound);
        
        // float distanceBetweenLookAtBaseAndTarget = Vector3.Distance(gunTransforms.LookAtBase.position, gunTransforms.LookTarget.position);
        // float theta = Mathf.Atan(recoilAmount / distanceBetweenLookAtBaseAndTarget);
        // float thetaInDegrees = theta * (180 / Mathf.PI);
        // gunTransforms.LookAtBase.Rotate(new Vector3(1, 0, 0), -thetaInDegrees);
        // gunTransforms.AimBase.Rotate(new Vector3(1, 0, 0), -thetaInDegrees);
        //
        // float yVariance = Random.Range(-recoilAmount * 10, recoilAmount * 10);
        // gunTransforms.LookAtBase.Rotate(new Vector3(0, 1, 0), yVariance);
        // gunTransforms.AimBase.Rotate(new Vector3(0, 1, 0), yVariance);

        StartCoroutine(AddVisualRecoil(playerState, recoilAnimCurve));
    }

    private IEnumerator AddVisualRecoil(PlayerState playerState, AnimationCurve recoilAnimCurve)
    {
        float timeElapsed = 0f;
        float lerpTime = 0.15f;

        float recoilModifier = playerState.IsAiming ? 0.1f : 1f;

        Vector3 newPos = _originalPosition + Vector3.up * (0.02f * recoilModifier) + Vector3.forward * (0.05f * recoilModifier);

        while (timeElapsed < lerpTime)
        {
            float t = timeElapsed / lerpTime;

            transform.localPosition = Vector3.Lerp(_originalPosition, newPos, recoilAnimCurve.Evaluate(t));

            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }

        transform.localPosition = _originalPosition;
    }
}

public struct FireGunTransforms
{
    public FireGunTransforms(Transform lookAtBase, Transform aimBase, Transform lookTarget, Vector3 from)
    {
        LookAtBase = lookAtBase;
        AimBase = aimBase;
        LookTarget = lookTarget;
        From = from;
    }

    public Vector3 From { get; }
    public Transform LookTarget { get; }
    public Transform AimBase { get; }
    public Transform LookAtBase { get; }
}