using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private FireGun _fireGunScript;
    private Transform _muzzleTransform;
    
    [SerializeField] private AnimationCurve recoilAnimCurve;

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
        _fireGunScript.Fire(_muzzleTransform.position, lookTarget, aimBase, lookAtBase,0.1f, playerState, recoilAnimCurve);
    }

    public void TriggerUp()
    {
        
    }
}
