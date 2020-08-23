using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public GameObject Drone;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Drone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Drone.transform.position.x, Drone.transform.position.y +0.6f, Drone.transform.position.z);
        this.transform.rotation = Quaternion.Euler(15, Drone.transform.rotation.eulerAngles.y, 0);
    }
}
