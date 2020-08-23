using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using System.IO;

public class SHOModel : MonoBehaviour
{

    //public GameObject objectToMove;
    
    SHOIntegrator theIntegrator;
    public DroneRotor f1;
    public DroneRotor f2;
    public DroneRotor f3;
    public DroneRotor f4;
    float t = 0.0f;
    float h = 0.01f;
    List<float[]> data;
    Vector3 velocity;
    Vector3 previous;
    Vector3 pos, rot;
    Rigidbody rb;
    float[] Xsave;
    bool CollisionEnter = false, StartTrigger = false, ResetTrigger = false;
    Vector3 FixedVelocity;
    Quaternion rotq;
    public Transform ColliderTransform;
    float innertimer=0;
    void Start()
    {
        Collider collider = ColliderTransform.GetComponent<Collider>();
        theIntegrator = new SHOIntegrator();
        rb = gameObject.GetComponent<Rigidbody>();
        theIntegrator.m = UIButton.mass;
        theIntegrator.l = UIButton.length;
        theIntegrator.Ax = UIButton.ax;
        theIntegrator.Ay = UIButton.ay;
        theIntegrator.Az = UIButton.az;
        theIntegrator.Ixx = UIButton.ixx;
        theIntegrator.Iyy = UIButton.iyy;
        theIntegrator.Izz = UIButton.izz;
        theIntegrator.k = UIButton.k;
        theIntegrator.b = UIButton.b;
        gameObject.transform.localScale = new Vector3(theIntegrator.l/0.225f, theIntegrator.l / 0.225f, theIntegrator.l/0.225f);
        gameObject.transform.Translate(Vector3.up * theIntegrator.l / 0.45f);
        float[] x0 = new float[12] { UIButton.posX, UIButton.posY, UIButton.posZ+0.5f
            , 0, 0, 0, -1*gameObject.transform.rotation.eulerAngles.x* Mathf.PI / 180, -1*gameObject.transform.rotation.eulerAngles.z* Mathf.PI / 180,
            -1*UIButton.rotZ* Mathf.PI / 180, 0, 0, 0 };
        gameObject.transform.position = new Vector3(UIButton.posX, UIButton.posZ+0.5f, UIButton.posY);
        gameObject.transform.rotation = Quaternion.Euler(0, UIButton.rotZ, 0);
        Xsave = x0;
        theIntegrator.SetIC(x0);
    }

    void FixedUpdate()
    {
        if (StartTrigger)
        {
            //play runge-kutta method
            t = theIntegrator.RK4Step(theIntegrator.X, t, h);
            pos = gameObject.transform.position;
            velocity = (pos - previous) / Time.deltaTime; //get velocity at Scene ifself
            previous = pos;
            //verify position, rotation
            pos.x = theIntegrator.X[0];
            pos.z = theIntegrator.X[1];
            pos.y = theIntegrator.X[2];
            rot = gameObject.transform.rotation.eulerAngles;
            rot.x = -theIntegrator.X[6] * 180 / Mathf.PI;
            rot.z = -theIntegrator.X[7] * 180 / Mathf.PI;
            rot.y = -theIntegrator.X[8] * 180 / Mathf.PI;
            rotq = Quaternion.Euler(rot.x, rot.y, rot.z);
            //verify Input
            theIntegrator.z_d += Input.GetAxis("Throttle");
            theIntegrator.phi_d = Input.GetAxis("Roll");
            theIntegrator.theta_d = Input.GetAxis("Pitch");
            theIntegrator.psi_d += Input.GetAxis("Yaw");
            //verify Rotor Angular Velocity(Power)
            f1.setPower(theIntegrator.w1);
            f2.setPower(theIntegrator.w2);
            f3.setPower(theIntegrator.w3);
            f4.setPower(theIntegrator.w4);
        }
        if (Input.GetAxis("Throttle") > 0.3 && !StartTrigger)
        {
            StartTrigger = true;
            CollisionEnter = false;

        }

        if (StartTrigger && !CollisionEnter)
        {
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rotq;
            ResetTrigger = true;
            innertimer += Time.deltaTime;
        }
        //reset trigger
        if ((velocity.magnitude <= 0.01f && CollisionEnter && ResetTrigger) || gameObject.transform.position.y < 18)
        {
            if (innertimer < 0.5f) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 40, this.transform.position.z);

            else
            {
                ResetTrigger = false;
                UIButton.posX = gameObject.transform.position.x;
                UIButton.posY = gameObject.transform.position.z;
                UIButton.posZ = gameObject.transform.position.y;
                UIButton.rotZ = gameObject.transform.rotation.eulerAngles.y;
                SceneManager.LoadScene("DroneProj");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter = true;
    }

}
