using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget;

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(lookAtTarget);
    }
}
