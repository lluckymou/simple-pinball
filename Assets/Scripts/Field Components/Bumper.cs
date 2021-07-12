using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        GetComponent<Animator>().Play("Activation");
        GetComponent<AudioSource>().Play();

        // Scoring
        Player.instance.IncrementScore(50);
    }
}
