using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjects : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static SceneObjects _instance;
    public static SceneObjects instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<SceneObjects>();
            return _instance;
        }
    }

    public Light MainLight;

    public Camera MainCamera;
}
