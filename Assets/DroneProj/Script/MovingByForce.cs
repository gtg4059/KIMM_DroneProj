using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingByForce : MonoBehaviour
{
    //public GameObject objectToMove;
    SHOIntegrator theIntegrator;
    public DroneRotor f1;
    public DroneRotor f2;
    public DroneRotor f3;
    public DroneRotor f4;
    //public Text UI;
    float t = 0.0f;
    float h = 0.01f;
    List<float[]> data;
    Vector3 velocity;
    Vector3 previous;
    Vector3 pos, rot;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        //string txt = UI.GetComponent<Text>().text;
        theIntegrator = new SHOIntegrator();
        //GetComponent<UnityEngine.UI.Text>
        /*rb = gameObject.GetComponent<Rigidbody>();
        theIntegrator.m = rb.mass;
        theIntegrator.Ax = rb.drag;
        theIntegrator.Ay = rb.drag;
        theIntegrator.Az = rb.drag;
        theIntegrator.Ixx = rb.inertiaTensor.x;
        theIntegrator.Iyy = rb.inertiaTensor.z;
        theIntegrator.Izz = rb.inertiaTensor.y;*/
        //x,y,z,x`,y`,z`,roll,pitch,yaw,roll`,pitch`,yaw`
        float[] x0 = new float[12] { gameObject.transform.position.x, gameObject.transform.position.z, gameObject.transform.position.y
            , 0, 0, 0, -1*gameObject.transform.rotation.eulerAngles.x* Mathf.PI / 180, -1*gameObject.transform.rotation.eulerAngles.z* Mathf.PI / 180,
            -1*gameObject.transform.rotation.eulerAngles.y* Mathf.PI / 180, 0, 0, 0 };
        //theIntegrator.m = gameObject.GetComponent<Rigidbody>().mass;
        //theIntegrator.Ax = gameObject.GetComponent<Rigidbody>().drag;
        //theIntegrator.Ay = gameObject.GetComponent<Rigidbody>().drag;
        //theIntegrator.Az = gameObject.GetComponent<Rigidbody>().drag;
        //data = new List<float[]>();
        //time = new List<float>();
        theIntegrator.SetIC(x0);
    }

    void FixedUpdate()
    {
        t = theIntegrator.RK4Step(theIntegrator.X, t, h);
        pos = gameObject.transform.position;
        //velocity = (pos - previous) / Time.deltaTime;
        //previous = pos;
        rot = gameObject.transform.rotation.eulerAngles;
        pos.x = theIntegrator.X[0];
        pos.z = theIntegrator.X[1];
        pos.y = theIntegrator.X[2];
        rot.x = -theIntegrator.X[6] * 180 / Mathf.PI;
        rot.z = -theIntegrator.X[7] * 180 / Mathf.PI;
        rot.y = -theIntegrator.X[8] * 180 / Mathf.PI;
        /*float[] dat = new float[10];
        dat[0] = pos.x;
        dat[1] = pos.z;
        dat[2] = pos.y;
        dat[3] = velocity.x;
        dat[4] = velocity.z;
        dat[5] = velocity.y;
        dat[6] = -1 * rot.x * Mathf.PI / 180;
        dat[7] = -1 * rot.z * Mathf.PI / 180;
        dat[8] = -1 * rot.y * Mathf.PI / 180;
        dat[9] = t;
        data.Add(dat);*/
        theIntegrator.z_d += Input.GetAxis("Throttle");
        theIntegrator.phi_d = Input.GetAxis("Roll") ;//+ Input.GetAxis("Yaw")
        theIntegrator.theta_d = Input.GetAxis("Pitch") ;// +Input.GetAxis("Yaw")
        theIntegrator.psi_d += Input.GetAxis("Yaw");
        f1.setPower(theIntegrator.w1);
        f2.setPower(theIntegrator.w2);
        f3.setPower(theIntegrator.w3);
        f4.setPower(theIntegrator.w4);
        Quaternion rotq = Quaternion.Euler(rot.x, rot.y, rot.z);
        //gameObject.transform.position = pos;
        //gameObject.transform.rotation = rotq;
    }
    void OnDestroy()
    {
        /*using (StreamWriter sw = new StreamWriter(new FileStream(Application.streamingAssetsPath + ".txt", FileMode.CreateNew, FileAccess.Write), System.Text.Encoding.UTF8))
        {
            foreach (float[] dt in data)
            {
                for (int i = 0; i < 9; i++) sw.Write(dt[i] + ",");
                sw.WriteLine(dt[9]);
            }
            sw.Flush();
            sw.Close();
        }*/

    }
    /*private void OnTriggerEnter(Collider other)
    {

        CollisionEnter = true;
        //rb.AddForce(-2*velocity, ForceMode.Impulse);
        FixedVelocity = velocity;
    }
    private void OnTriggerStay(Collider other)
    {
        //m*v
        rb.AddForce(-1 * FixedVelocity, ForceMode.VelocityChange);
    }*/
    
    private void OnCollisionEnter(Collision collision)
    {
        theIntegrator.f[0] = collision.impulse.x * 10;
        theIntegrator.f[1] = collision.impulse.z * 10;
        theIntegrator.f[2] = collision.impulse.y * 10;
        /*if (collision.relativeVelocity.magnitude > 1)
        {
            theIntegrator.X = Xsave;
            theIntegrator.SetIC(Xsave);
            pos.x = theIntegrator.X[0];
            pos.z = theIntegrator.X[1];
            pos.y = theIntegrator.X[2];
            rot.x = -theIntegrator.X[6] * 180 / Mathf.PI;
            rot.z = -theIntegrator.X[7] * 180 / Mathf.PI;
            rot.y = -theIntegrator.X[8] * 180 / Mathf.PI;
            Quaternion rotq = Quaternion.Euler(rot.x, rot.y, rot.z);
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rotq;
        }*/
    }
    // Update is called once per frame
    void Update()
    {



        //Vector4 movement = new Vector4(Input.GetAxis("Throttle"), Input.GetAxis("Yaw"), Input.GetAxis("Roll"), Input.GetAxis("Pitch"));
        //z_d, phi_d, theta_d, psi_d
        //theIntegrator.z_d += Input.GetAxis("Throttle");
        //theIntegrator.phi_d = Input.GetAxis("Roll")+ Input.GetAxis("Yaw");
        //theIntegrator.theta_d = Input.GetAxis("Pitch")+ Input.GetAxis("Yaw");
        //theIntegrator.psi_d += Input.GetAxis("Yaw");
        /*theIntegrator.z_d = 10;
        theIntegrator.phi_d = 20 * Mathf.PI / 180;
        theIntegrator.theta_d = 0;
        theIntegrator.psi_d = 0;*/
        
    }
}
