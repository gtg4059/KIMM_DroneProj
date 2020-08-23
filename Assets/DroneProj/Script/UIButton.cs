using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public InputField Mass, Length, Ax, Ay, Az, Ixx, Iyy, Izz, K, B;
    public Text warning;
    static public float mass { get; set; } // drone mass
    static public float length { get; set; } // drone length
    static public float ax { get; set; } // x axis drag force coefficient
    static public float ay { get; set; } // y axis drag force coefficient
    static public float az { get; set; } // z axis drag force coefficient
    static public float ixx { get; set; } // x axis rotational inertia
    static public float iyy { get; set; } // y axis rotational inertia
    static public float izz { get; set; } // z axis rotational inertia
    static public float k { get; set; } // thrust coefficient over angular velocity
    static public float b { get; set; } // damping coefficient
    static public float posX { get; set; }
    static public float posY { get; set; }
    static public float posZ { get; set; }
    static public float rotZ { get; set; }
    public void start()
    {
        try
        {
            posX = 10; posZ = 20; posY = 0; rotZ = 0;
            mass = float.Parse(Mass.text, System.Globalization.CultureInfo.InvariantCulture);
            length = float.Parse(Length.text, System.Globalization.CultureInfo.InvariantCulture) / 100;
            ax = float.Parse(Ax.text, System.Globalization.CultureInfo.InvariantCulture);
            ay = float.Parse(Ay.text, System.Globalization.CultureInfo.InvariantCulture);
            az = float.Parse(Az.text, System.Globalization.CultureInfo.InvariantCulture);
            ixx = float.Parse(Ixx.text, System.Globalization.CultureInfo.InvariantCulture);
            iyy = float.Parse(Iyy.text, System.Globalization.CultureInfo.InvariantCulture);
            izz = float.Parse(Izz.text, System.Globalization.CultureInfo.InvariantCulture);
            k = float.Parse(K.text, System.Globalization.CultureInfo.InvariantCulture);
            b = float.Parse(B.text, System.Globalization.CultureInfo.InvariantCulture);
            SceneManager.LoadScene("DroneProj");
        }
        catch
        {
            warning.text = "Input is not fulfiled!";
        }

    }
    public void defaults()
    {
        Mass.text = "0.468";
        Length.text = "22.5";
        Ax.text = "0.25";
        Ay.text = "0.25";
        Az.text = "0.25";
        Ixx.text = "0.004856";
        Iyy.text = "0.004856";
        Izz.text = "0.008801";
        K.text = "0.00000298";
        B.text = "0.000000114";
    }
    public void Exit()
    {
        Application.Quit();
    }



}
