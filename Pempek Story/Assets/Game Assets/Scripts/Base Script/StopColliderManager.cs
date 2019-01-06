using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopColliderManager : MonoBehaviour
{
    public static StopColliderManager instance;

    public bool stopLeftColActive;
    public bool stopRightColActive;
    public bool stopUpColActive;
    public bool stopDownColActive;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stopLeftColActive = false;
        stopRightColActive = false;
        stopUpColActive = false;
        stopDownColActive = false;
    }
}