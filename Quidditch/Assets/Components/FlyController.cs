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
    public AudioSource windAudio;
    public AudioSource crazyWindAudio;

    public Transform head;

    public bool hasStarted {
        get { return _hasStarted; }
    }
    private bool _hasStarted;
    private ulong fowardButtton = SteamVR_Controller.ButtonMask.Axis1;
    private ulong boostButton = SteamVR_Controller.ButtonMask.Grip;
    private bool hasChanged = false;
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //(int)SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight)
    }

    void Update() {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        
        if (_hasStarted == false && device != null && device.GetPress(fowardButtton)) {
            _hasStarted = true;
        }
        if (_hasStarted == false) {
            return;
        }

        Vector3 upVelocity = Vector3.zero;
        //if (Mathf.Abs(transform.forward.y) > 0.1) {
        //    upVelocity = Vector3.up * (transform.forward.y * (maxUpSpeed - minUpSpeed) + minUpSpeed);
        //}

        Vector3 forwardVelocity = Vector3.zero;
		ushort pulsePower = 0;
        bool normalWind = false;
        bool crazyWind = false;
        if (device != null && device.GetPress(fowardButtton)) {
            forwardVelocity = transform.forward;
            forwardVelocity.Normalize();
            forwardVelocity *= forwardSpeed;
            normalWind = true;
            pulsePower = 200;
        }
		if (device != null && device.GetPress (boostButton) && device.GetPress (fowardButtton)) {
			forwardVelocity *= boostMultiply;
            windEffect.Play();
            crazyWind = true;
            normalWind = false;
            pulsePower = 700;
		} else {
			windEffect.Stop();
		}

		if (pulsePower > 0) {
			device.TriggerHapticPulse (pulsePower);
		}
        windSound(normalWind, crazyWind);


            camRigid.velocity = forwardVelocity + upVelocity;
        if (camRigid.velocity != Vector3.zero) {
            windEffect.transform.position = cam.transform.position + camRigid.velocity.normalized * 3 + Vector3.up * head.localPosition.y;
            windEffect.transform.forward = -camRigid.velocity.normalized;
        }

        if(forwardVelocity == Vector3.zero)
        {
            Vector3 up = Vector3.up * 2 * Time.deltaTime;
            transform.position += up; 
        }
    }

    private void windSound(bool normalWind, bool crazyWind) {
        if (normalWind) {
            if (windAudio.isPlaying == false) {
                windAudio.Play();
            }
            if (crazyWindAudio.isPlaying) {
                crazyWindAudio.Stop();
            }
        } else if (crazyWind) {
            if (crazyWindAudio.isPlaying == false) {
                crazyWindAudio.Play();
            }
            if (windAudio.isPlaying) {
                windAudio.Stop();
            }
        } else {
            if (windAudio.isPlaying) {
                windAudio.Stop();
            }
            if (crazyWindAudio.isPlaying) {
                crazyWindAudio.Stop();
            }
        }
    }

}

