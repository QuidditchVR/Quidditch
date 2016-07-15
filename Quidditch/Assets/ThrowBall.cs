using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float throwSpeed = 20.0f;
    public GameObject cam;
    public GameObject ballPrefab;
    private GameObject ball;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        //device = SteamVR_Controller.Input((int)SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestLeft));
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (ball == null && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            ball = Instantiate(ballPrefab);
            ball.transform.parent = transform;
            ball.transform.localPosition = Vector3.zero;
        }

        if (ball != null && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.transform.SetParent(null);
            TossObject(ball.GetComponent<Rigidbody>());
            ball = null;
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
