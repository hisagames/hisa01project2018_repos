using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCollider : MonoBehaviour
{
    enum StopColliderType
    {
        left,
        right,
        up,
        down
    };

    [SerializeField]
    StopColliderType stopColliderType;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (stopColliderType)
            {
                case StopColliderType.left:
                    StopColliderManager.instance.stopLeftColActive = true;
                    break;
                case StopColliderType.right:
                    StopColliderManager.instance.stopRightColActive = true;
                    break;
                case StopColliderType.up:
                    StopColliderManager.instance.stopUpColActive = true;
                    break;
                case StopColliderType.down:
                    StopColliderManager.instance.stopDownColActive = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (stopColliderType)
            {
                case StopColliderType.left:
                    StopColliderManager.instance.stopLeftColActive = false;
                    break;
                case StopColliderType.right:
                    StopColliderManager.instance.stopRightColActive = false;
                    break;
                case StopColliderType.up:
                    StopColliderManager.instance.stopUpColActive = false;
                    break;
                case StopColliderType.down:
                    StopColliderManager.instance.stopDownColActive = false;
                    break;
            }
        }
    }
}