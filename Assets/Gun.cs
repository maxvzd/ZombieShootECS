using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private FireGun _fireGunScript;
    private Transform _muzzleTransform;

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

    public void TriggerDown(Transform aimTarget, Transform lookTarget, Transform lookAtBase)
    {
        _fireGunScript.Fire(_muzzleTransform.position, lookTarget, aimTarget, lookAtBase,0.1f);
    }

    public void TriggerUp()
    {
        
    }
}
