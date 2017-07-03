using UnityEngine;
using System.Collections;

public class Savor_RotateThis : MonoBehaviour {

    public float rotationSpeedX = 0.0f;
    public float rotationSpeedY = 0.0f;
    public float rotationSpeedZ = 0.0f;

    public bool local = true;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (local == true)
            transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);

        if (local == false)
            transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime, Space.World);


    }
}
