using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        GetComponent<AudioSource>().Play();
    }
}
