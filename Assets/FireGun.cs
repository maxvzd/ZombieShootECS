using UnityEngine;

public class FireGun : MonoBehaviour
{
    public void Fire(Vector3 from, Transform lookTarget, Transform aimTarget, Transform lookAtBase, float recoilAmount)
    {
        aimTarget.position += aimTarget.up * recoilAmount;

         float distanceBetweenLookAtBaseAndTarget = Vector3.Distance(lookAtBase.position, lookTarget.position);
         float theta = Mathf.Atan(recoilAmount / distanceBetweenLookAtBaseAndTarget);
         float thetaInDegrees = theta * (180 / Mathf.PI);
        //
        lookAtBase.Rotate(new Vector3(1, 0, 0), -thetaInDegrees);
    }
}
