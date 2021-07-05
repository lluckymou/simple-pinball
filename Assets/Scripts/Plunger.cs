using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    [SerializeField]
    Animator Spring;

    public List<Rigidbody> ObjectsInSpring = new List<Rigidbody>();

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ball")
            ObjectsInSpring.Add(c.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Ball")
            ObjectsInSpring.Remove(c.GetComponent<Rigidbody>());
    }

    public void Retract() => Spring.Play("Stress");

    public void Release() => Spring.Play("Release");

}
