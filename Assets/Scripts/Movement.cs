using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Flippers")]
    [SerializeField]
    HingeJoint LeftFlipper;
    [SerializeField]
    HingeJoint RightFlipper;

    [Header("Ball launching mechanism")]
    [SerializeField]
    Plunger Spring;

    [SerializeField, Range(0, 50)]
    byte MaxForce;
    
    [SerializeField, Range(0, 50)]
    byte MinForce;

    float force;
    bool activated;

    void Update()
    {
        // Right flipper
        if (Input.GetKey(KeyCode.RightArrow))
            RightFlipper.motor = RotateFlipper(1500);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            RightFlipper.motor = RotateFlipper(-1500);
        
        // Left flipper
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftFlipper.motor = RotateFlipper(-1500);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            LeftFlipper.motor = RotateFlipper(1500);

        // Launching mechanism
        if (Input.GetKeyDown(KeyCode.Space))
            Spring.Retract();

        if (Input.GetKey(KeyCode.Space))
            AccumulateForce();

        if (Input.GetKeyUp(KeyCode.Space))
            ReleaseForce();
    }

    JointMotor RotateFlipper(float velocity, float force = 150)
    {
        JointMotor jointMotor = new JointMotor();
        jointMotor.force = force;
        jointMotor.targetVelocity = velocity;
        return jointMotor;
    }

    void AccumulateForce()
    {
        if(!activated)
        {
            force += 0.1f;

            if(force >= MaxForce)
            {
                activated = true;
                force *= Random.Range(0.9f, 0.75f);
            }
        }
    }

    void ReleaseForce()
    {
        Spring.Release();

        foreach(Rigidbody rb in Spring.ObjectsInSpring)
            rb.AddForce(force*Vector3.forward);

        force = MinForce;
        activated = false;
    }
}