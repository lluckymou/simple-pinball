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
    GameObject Spring;

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
            RightFlipper.motor = RotateFlipper(2000);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            RightFlipper.motor = RotateFlipper(-2000);
        
        // Left flipper
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftFlipper.motor = RotateFlipper(-2000);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            LeftFlipper.motor = RotateFlipper(2000);

        // Launching mechanism
        if (Input.GetKey(KeyCode.Space))
            RetractPlunger();

        if (Input.GetKeyUp(KeyCode.Space))
            ActivatePlunger();
    }

    JointMotor RotateFlipper(float velocity, float force = 200)
    {
        JointMotor jointMotor = new JointMotor();
        jointMotor.force = force;
        jointMotor.targetVelocity = velocity;
        return jointMotor;
    }

    void RetractPlunger()
    {
        Spring.GetComponent<Animator>().Play("Stress");
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

    void ActivatePlunger()
    {
        Spring.GetComponent<Animator>().Play("Release");
        force = MinForce;
        activated = false;
    }
}
