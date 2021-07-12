using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField]
    Material LitMaterial;

    Material original;

    void Awake() => original = GetComponent<MeshRenderer>().material;

    void OnCollisionEnter(Collision c)
    {
        GetComponent<MeshRenderer>().material = LitMaterial;
        GetComponent<AudioSource>().Play();

        // Scoring
        Player.instance.IncrementScore(50);
    }

    void OnCollisionExit(Collision c) =>
        GetComponent<MeshRenderer>().material = original;
}
