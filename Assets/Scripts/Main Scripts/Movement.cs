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

    [Header("Plunger")]
    [SerializeField]
    Plunger Spring;

    [SerializeField, Range(0, 50)]
    byte MaxForce;
    
    [SerializeField, Range(0, 50)]
    byte MinForce;

    [SerializeField]
    float IncreasingFactor;

    float force;
    bool activated;

    void Update()
    {
        if(Player.instance.Lives < 0) return;

        // Right flipper
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            RightFlipper.GetComponent<AudioSource>().Play();

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            RightFlipper.motor = RotateFlipper(1500);

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            RightFlipper.motor = RotateFlipper(-1500);
        
        // Left flipper
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            LeftFlipper.GetComponent<AudioSource>().Play();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            LeftFlipper.motor = RotateFlipper(-1500);

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
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
            force += IncreasingFactor;

            if(force >= MaxForce)
            {
                Spring.Fail();
                activated = true;
                force *= Random.Range(0.7f, 0.5f);
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