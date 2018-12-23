using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    GameObject mainObject;

    [SerializeField]
    Vector2 screenSize;

    [SerializeField]
    float xMin;
    [SerializeField]
    float xMax;
    [SerializeField]
    float yMin;
    [SerializeField]
    float yMax;

    void Start () {
		
	}
	
	void FixedUpdate () {
        if (mainObject.transform.position.x >= xMin + (screenSize.x / 2) && (mainObject.transform.position.x <= xMax - (screenSize.x / 2)))
        {
            transform.position = new Vector3(mainObject.transform.position.x, transform.position.y,
                transform.position.z);
        }

        if (mainObject.transform.position.y >= yMin + (screenSize.y / 2) && (mainObject.transform.position.y <= yMax - (screenSize.y / 2)))
        {
            transform.position = new Vector3(transform.position.x, mainObject.transform.position.y,
                transform.position.z);
        }
    }
}