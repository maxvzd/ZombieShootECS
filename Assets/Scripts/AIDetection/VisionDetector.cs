using System.Collections.Generic;
using UnityEngine;

namespace AIDetection
{
    public class VisionDetector : MonoBehaviour
    {
        [SerializeField] private float sightRadius;
        [SerializeField] private float fov;

        [SerializeField] private Transform eyes;

        private float _sightRadiusSquared;
        private float halfFov;

        private void Start()
        {
            _sightRadiusSquared = sightRadius * sightRadius;
            halfFov = fov / 2;
        }

        private void Update()
        {
            IList<Detectee> detectees = SightManager.Detectees;

            for (int i = 0; i < detectees.Count; i++)
            {
                //TODO: do some benchmarking to test if it's better to do this with a collider trigger;
                //Calculate if detectee is close enough
                Detectee currentDetectee = detectees[i];
                Vector3 relativePos = currentDetectee.transform.position + Vector3.up * 0.3f - eyes.position;
                if (relativePos.sqrMagnitude > _sightRadiusSquared) continue;

                // detect if detectee is in fov
                Vector3 forward = transform.forward;
                Vector3 direction = relativePos.normalized;
                float dotProduct = Vector3.Dot(forward, direction);
                float fovRange = 1 - halfFov * (1f / 90f);

                if (dotProduct < fovRange) continue;

                Ray floorRay = new Ray(eyes.position, direction * sightRadius);
                if (Physics.Raycast(floorRay, out RaycastHit floorHit, sightRadius))
                {
                    if (floorHit.transform.gameObject.CompareTag("Player"))
                    {
                        Debug.DrawRay(floorRay.origin, floorRay.direction * relativePos.magnitude, Color.green);
                        return;
                    }
                }
                
                Vector3 headDirection = GetModifiedPositionDirection(
                    currentDetectee.transform.position,
                    currentDetectee.Height, 
                    0.3f,
                    0f,
                    0f);
                
                if (FireRayWithInDirection(headDirection))
                {
                    Debug.DrawRay(eyes.position, headDirection * relativePos.magnitude, Color.yellow);
                    return;
                }

                Vector3 rightDirection = GetModifiedPositionDirection(
                    currentDetectee.transform.position,
                    currentDetectee.Height / 2f,
                    0f,
                    currentDetectee.Width / 2f,
                    0.1f);

                if (FireRayWithInDirection(rightDirection))
                {
                    Debug.DrawRay(eyes.position, rightDirection * relativePos.magnitude, Color.red);
                    return;
                }

                Vector3 leftDirection = GetModifiedPositionDirection(
                    currentDetectee.transform.position,
                    currentDetectee.Height / 2f,
                    0f,
                    -(currentDetectee.Width / 2f),
                    0.1f);

                if (FireRayWithInDirection(leftDirection))
                {
                    Debug.DrawRay(eyes.position, leftDirection * relativePos.magnitude, Color.cyan);
                    return;
                }
            }
        }

        private Vector3 GetModifiedPositionDirection(Vector3 currentDetecteePos, float upMagnitude, float upTolerance, float rightMagnitude, float rightTolerance)
        {
            Vector3 eyesPosition = eyes.position;
            Transform currentTransform = transform;

            if (rightMagnitude < 0)
            {
                rightTolerance *= -1f;
            }

            Vector3 positionWithModifier = currentDetecteePos + currentTransform.up * (upMagnitude - upTolerance) + currentTransform.right * (rightMagnitude - rightTolerance);
            Vector3 relativePos = positionWithModifier - eyesPosition;
            return relativePos.normalized;
        }

        private bool FireRayWithInDirection(Vector3 direction)
        {
            Ray heightRay = new Ray(eyes.position, direction * sightRadius);
            return Physics.Raycast(heightRay, out RaycastHit heightHit, sightRadius) && heightHit.transform.gameObject.CompareTag("Player");
        }

        private void OnDrawGizmos()
        {
            // float halfFOV = fov / 2.0f;
            // Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            // Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            //
            // Vector3 forwardTransform = transform.forward;
            // Vector3 leftRayDirection = leftRayRotation * forwardTransform;
            // Vector3 rightRayDirection = rightRayRotation * forwardTransform;
            //
            // Vector3 currentPos = transform.position;
            // Gizmos.DrawRay(currentPos, leftRayDirection * sightRadius);
            // Gizmos.DrawRay(currentPos, rightRayDirection * sightRadius);
        }
    }
}