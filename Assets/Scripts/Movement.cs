using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Flippers")]
    [SerializeField]
    Rigidbody LeftFlipper;
    [SerializeField]
    Rigidbody RightFlipper;
    [SerializeField]
    float RangeAngle;

    [Header("Ball launching mechanism")]
    [SerializeField]
    GameObject Spring;

    [SerializeField, Range(0, 255)]
    byte MaxForce;
    
    [SerializeField, Range(0, 255)]
    byte MinForce;

    // Private variables

    void Awake()
    {
        RightFlipper.centerOfMass += new Vector3(0, -0.86f, 0);
        // LeftFlipper.centerOfMass += new Vector3(0, -0.86f, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            RightFlipper.AddRelativeTorque(new Vector3(0, 0, -1) * 200);
        
        // if (Input.GetKey(KeyCode.LeftArrow))
            // LeftFlipper.AddRelativeTorque(new Vector3(0, 0, 1) * 200);
    }
}
