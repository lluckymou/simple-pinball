using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
     void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Bumper")
        {
            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * c.gameObject.GetComponent<BumperProperties>().BoostForce);
        }
    }
}
