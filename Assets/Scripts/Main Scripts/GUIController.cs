using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    // Same-scene "singleton" pattern 
    private static GUIController _instance;
    public static GUIController instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<GUIController>();
            return _instance;
        }
    }

    [Header("Debug")]
    [SerializeField]
    TMP_Text FPS;

    [Header("Player labels")]
    public TMP_Text Score;

    public TMP_Text Multiplier;

    public TMP_Text Lives;

    float deltaTime = 0, refreshRate = 0;

    void Update()
    {
        refreshRate += Time.deltaTime;
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if(refreshRate > 0.5f)
        {
            FPS.text = $"FPS: {((int) (1f / deltaTime))}";
            refreshRate = 0;
        }
    }
}
