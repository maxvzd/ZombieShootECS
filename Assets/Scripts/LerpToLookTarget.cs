using UnityEngine;

public class LerpToLookTarget : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private AnimationCurve animCurve;

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.LerpUnclamped(transform.position, lookTarget.position, animCurve.Evaluate(Time.deltaTime * lerpSpeed));
    }

    // private Vector3 LerpWithoutClamp(Vector3 current, Vector3 target, float t)
    // {
    //     if (t > 1)
    //     {
    //         Debug.Log("Hello");
    //     }
    //     return current + (target - current) * t;
    // }
}