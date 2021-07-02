using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperProperties : MonoBehaviour
{
    [Header("General properties")]

    [SerializeField]
    bool RandomForce = false;

    [SerializeField, Range(0, 100)]
    float MinForce = 5f;

    [Header("Random Force Properties")]
    [SerializeField, Range(0, 100), Tooltip("Will be ignored if RandomForce is false")]
    float MaxForce = 5f;


    public float BoostForce
    {
        get
        {
            if(RandomForce)
                return Random.Range(MinForce, MaxForce);
            else
                return MinForce;
        }
    }
}
