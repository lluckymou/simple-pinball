using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Flippers")]
    [SerializeField]
    HingeJoint LeftFlipper;
    [SerializeField]
    HingeJoint RightFlipper;

    [Header("Ball launching mechanism")]
    [SerializeField]
    GameObject Spring;

    [SerializeField, Range(0, 255)]
    byte MaxForce;
    
    [SerializeField, Range(0, 255)]
    byte MinForce;

    void Update()
    {
        // Right flipper
        if (Input.GetKey(KeyCode.RightArrow))
            RightFlipper.motor = Rotate(2000);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            RightFlipper.motor = Rotate(-2000);
        
        // Left flipper
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftFlipper.motor = Rotate(-2000);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            LeftFlipper.motor = Rotate(2000);
    }

    JointMotor Rotate(float velocity, float force = 200)
    {
        JointMotor jointMotor = new JointMotor();
        jointMotor.force = force;
        jointMotor.targetVelocity = velocity;
        return jointMotor;
    }
}
