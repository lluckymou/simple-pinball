using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        BoostObject bo = c.gameObject.GetComponent<BoostObject>();
        if (bo != null)
        {
            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * bo.BoostForce);
        }

        GetComponent<AudioSource>().Play();
    }
}
