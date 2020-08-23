using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetMotorSpeed : MonoBehaviour
{
    public DroneRotor f1;
    public DroneRotor f2;
    public DroneRotor f3;
    public DroneRotor f4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = $" Motor 1 : {f1.power.ToString("F2")}\n Motor 2 : {f2.power.ToString("F2")}\n Motor 3 : {f3.power.ToString("F2")}\n Motor 4 : {f4.power.ToString("F2")}";
    }
}
