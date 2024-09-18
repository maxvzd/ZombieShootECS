using System;
using ItemProperties;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private FireGun _fireGunScript;
    private Transform _muzzleTransform;

    [SerializeField] private AnimationCurve recoilAnimCurve;
    [SerializeField] private GunProperties properties;

    public GunProperties Properties => properties;

    private void Start()
    {
        _fireGunScript = GetComponent<FireGun>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == "Muzzle")
            {
                _muzzleTransform = child;
                break;
            }
        }

        if (ReferenceEquals(_muzzleTransform, null))
        {
            throw new Exception("Add muzzle to gun");
        }
    }

    public void TriggerDown(Transform aimBase, Transform lookTarget, Transform lookAtBase, PlayerState playerState)
    {
        FireGunTransforms gunTransforms = new FireGunTransforms(lookAtBase, aimBase, lookTarget, _muzzleTransform.position);
        _fireGunScript.Fire(gunTransforms, Properties, playerState, recoilAnimCurve);
    }

    public void TriggerUp()
    {
    }
}