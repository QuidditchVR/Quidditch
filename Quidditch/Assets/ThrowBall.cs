using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    public float throwSpeed = 5.0f;
    public GameObject ball;
    public GameObject cam;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        //device = SteamVR_Controller.Input((int)SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft));
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    // Update is called once per frame
    void Update()
    {
        //device = SteamVR_Controller.Input((int)trackedObject.index);
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ball.GetComponent<Rigidbody>().isKinematic = false;
            //ball.GetComponent<Rigidbody>().useGravity = true;
            ball.transform.SetParent(null);
            TossObject(ball.GetComponent<Rigidbody>());
        }
    }

    public void TossObject(Rigidbody rigidBody)
    {

        //add my speed 
        //transform.attachedRigidbody
        rigidBody.velocity = cam.GetComponent<Rigidbody>().velocity + device.velocity * throwSpeed;
        //rigidBody.AddForce(device.angularVelocity * throwSpeed);
        //rigidBody.angularVelocity = cam.GetComponent<Rigidbody>().angularVelocity + device.angularVelocity;
    }

}
