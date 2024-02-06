using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Timeline;
using static Shoot;

// Source : https://www.youtube.com/watch?v=rgJGkYM2_6E&t=246s 
public class TrajectoryPredictor : MonoBehaviour
{
    LineRenderer trajectoryLine;
    public Transform hitMarker;
    int maxPoints = 50;
    float increment = 0.025f;
    float rayOverlap = 1.1f;

    bool isTPS = true;

    void Start()
    {
        // Subscribe to Camera ViewChange Event
        CameraManager.OnViewChange += HandleViewChange;

        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.enabled = true;
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        if (!isTPS)
        {
            return;
        }
        Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
        Vector3 position = projectile.initialPosition;
        Vector3 nextPosition;
        float overlap;

        UpdateLineRender(maxPoints, (0, position));

        for (int i = 1; i < maxPoints; i++)
        {
            // Estimate velocity and update next predicted position
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            // Overlap our rays by small margin to ensure we never miss a surface
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            //When hitting a surface we want to show the surface marker and stop updating our line
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                UpdateLineRender(i, (i - 1, hit.point));
                MoveHitMarker(hit);
                break;
            }

            //If nothing is hit, continue rendering the arc without a visual marker
            hitMarker.gameObject.SetActive(false);
            position = nextPosition;
            UpdateLineRender(maxPoints, (i, position)); //Unneccesary to set count here, but not harmful
        }
    }
    void HandleViewChange(bool _isTPS)
    {
        if (_isTPS)
        {
            trajectoryLine.enabled = true;
            isTPS = true;
        }
        else
        {
            trajectoryLine.enabled = false;
            hitMarker.gameObject.SetActive(false);
            isTPS = false;
        }
    }
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        trajectoryLine.positionCount = count;
        trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
    }
    private void MoveHitMarker(RaycastHit hit)
    {
        if (!isTPS)
        {
            return;
        }
        hitMarker.gameObject.SetActive(true);

        // Offset marker from surface
        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
    }
    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }
}
