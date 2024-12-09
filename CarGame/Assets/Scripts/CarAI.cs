using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Transform[] wayPoints;
    public Transform[] whell;
    public WheelCollider[] whellCollider;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;

    public float lookHeadDistance = 5f;

    private int currentWayInd = 0;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, 0.5f, 0);
    }

    private void FixedUpdate()
    {
        Drive();
        CheckWayPoint();
        UpdateWhellPos();
    }

    public void Drive()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(wayPoints[currentWayInd].position);
        float newSteer = Mathf.Clamp((relativeVector.x / relativeVector.magnitude) * maxSteeringAngle, -maxSteeringAngle, maxSteeringAngle);

        for(int i = 0; i < whellCollider.Length; i++)
        {
            if (whellCollider[i].transform.localPosition.z > 0)
            {
                whellCollider[i].steerAngle = newSteer;
            }
        }
        foreach(WheelCollider whell in whellCollider)
        {
            whell.motorTorque = maxMotorTorque;
        }
    }

    public void CheckWayPoint()
    {
        float distance = Vector3.Distance(transform.position, wayPoints[currentWayInd].position);
        if(distance < lookHeadDistance)
        {
            currentWayInd = (currentWayInd + 1) % wayPoints.Length;
        }
    }

    public void UpdateWhellPos()
    {
        for(int i = 0;i < whellCollider.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            whellCollider[i].GetWorldPose(out pos, out quat);

            whell[i].position = pos;
            whell[i].rotation = quat;
        }
    }
}
