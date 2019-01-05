using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    string TargetScene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            // + transisi animation
            TimeManager.instance.addTransitionMinutes();
            JumpToOtherScene.quickGoToScene(TargetScene);
        }
    }
}