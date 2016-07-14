using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public GameObject cam;
    public Rigidbody camRigid;
    private float maxUpSpeed = 30.0f;
    private float minUpSpeed = 5.0f;
    public float forwardSpeed = 20.0f;
    public float boostMultiply = 2.0f;
    public ParticleSystem windEffect;
    public Transform head;

    public bool hasStarted {
        get { return _hasStarted; }
    }
    private bool _hasStarted;
    private ulong fowardButtton = SteamVR_Controller.ButtonMask.Axis1;
    private ulong boostButton = SteamVR_Controller.ButtonMask.Grip;

    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //get device index

        device = SteamVR_Controller.Input((int)trackedObj.index);
        //(int)SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)
    }

    void Update() {
        if (_hasStarted == false && device != null && device.GetPress(fowardButtton)) {
            _hasStarted = true;
        }
        if (_hasStarted == false) {
            return;
        }

        Vector3 upVelocity = Vector3.zero;
        if (Mathf.Abs(transform.forward.y) > 0.1) {
            upVelocity = Vector3.up * (transform.forward.y * (maxUpSpeed - minUpSpeed) + minUpSpeed);
        }

        Vector3 forwardVelocity = Vector3.zero;
        if (device != null && device.GetPress(fowardButtton)) {
            forwardVelocity = transform.forward;
            forwardVelocity.y = 0;
            forwardVelocity.Normalize();
            forwardVelocity *= forwardSpeed;
        }
        if (device != null && device.GetPress(boostButton)) {
            forwardVelocity *= boostMultiply;
            windEffect.Play();
        }
        if (forwardVelocity == Vector3.zero) {
            windEffect.Stop();
        }

        camRigid.velocity = forwardVelocity + upVelocity;
        windEffect.transform.position = cam.transform.position + camRigid.velocity.normalized * 3 + Vector3.up * head.localPosition.y;
        windEffect.transform.forward = -camRigid.velocity.normalized;

    }

}

