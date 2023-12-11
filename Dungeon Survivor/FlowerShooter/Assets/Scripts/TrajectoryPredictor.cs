using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on https://github.com/ForlornU/Trajectory-Predictor/blob/main/Assets/Scripts/TrajectoryPredictor.cs

//[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{


    //LineRenderer trajectoryLine;
    [SerializeField, Tooltip("The marker will show where the projectile will hit")]
    Transform hitMarker;
    [SerializeField, Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")]
    int maxPoints = 50;
    [SerializeField, Range(0.01f, 0.5f), Tooltip("The time increment used to calculate the trajectory")]
    float increment = 0.025f;
    [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
    float rayOverlap = 1.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        //Calculate Velocity & Position
        Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
        Vector3 position = projectile.initialPosition;

        Vector3 nextPosition;
        float overlap;

        for(int i = 0; i < maxPoints; i++)
        {
            //Calculate estimated velocity & next position
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            //If u overlap the rays you will never miss a surface.
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            //Check if we hit something
            if(Physics.Raycast(position,velocity.normalized,out RaycastHit hit, overlap))
            {

                MoveHitMarker(hit);
                break;
            }

            //If nothing is hit, continue
            hitMarker.gameObject.SetActive(false);
            position = nextPosition;


        }
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        hitMarker.gameObject.SetActive(true);
        hitMarker.position = hit.point;

    }

    Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }
}


public struct ProjectileProperties 
{
    public Vector3 direction;
    public Vector3 initialPosition;
    public float initialSpeed;
    public float mass;
    public float drag;

}
