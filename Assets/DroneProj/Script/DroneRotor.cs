using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRotor : MonoBehaviour {
    Rigidbody rBody;
    public float power;
    SHOIntegrator theIntegrator;
    public bool counterclockwise;
    

    // Use this for initialization
    void Start () {
        theIntegrator = new SHOIntegrator();
        Transform t = this.transform;
        while (t.parent != null && t.tag != "Player") t = t.parent;
        rBody = t.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() { transform.Rotate(0, 0, power * (counterclockwise ? -1 : 1)); }
    public void setPower(float intensity) { power = intensity; }
}
