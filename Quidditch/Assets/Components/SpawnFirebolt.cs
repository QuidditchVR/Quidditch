using UnityEngine;
using System.Collections;

public class SpawnFirebolt : MonoBehaviour
{
    public GameObject fireboltPrefab;
    public Transform spawnFireboltPoint;
    private float startFireboltSize = 0.3f;
    private float maxFireboltSize = 3.0f;
    private float fireboltScaleupSpeed = 1.0f;
    private float fireboltSpeed = 200.0f;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private ulong spawnButtton = SteamVR_Controller.ButtonMask.Touchpad;
    private GameObject firebolt;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)this.trackedObj.index);
    }

    // Update is called once per frame
    void Update() {

        //if (firebolt == null && Input.GetKeyDown(KeyCode.Space)) {
        if (firebolt == null && device != null && device.GetPressDown(spawnButtton)) {
            Spawn();
        }

        //if (firebolt != null && Input.GetKey(KeyCode.Space)) {
        if (firebolt != null && device != null && device.GetPress(spawnButtton)) {
            Scaleup();
        }

        //if (firebolt != null && Input.GetKeyUp(KeyCode.Space)) {
        if (firebolt != null && device != null && device.GetPressUp(spawnButtton)) {
            Shoot();
        }
    }

    void Spawn() {
        firebolt = Instantiate(fireboltPrefab) as GameObject;
        firebolt.transform.parent = spawnFireboltPoint;
        firebolt.transform.localPosition = Vector3.zero;
        firebolt.transform.localScale = Vector3.one * startFireboltSize;
    }

    void Scaleup() {
        firebolt.transform.localScale += Vector3.one * fireboltScaleupSpeed * Time.deltaTime;
        if (firebolt.transform.localScale.x > maxFireboltSize) {
            firebolt.transform.localScale = Vector3.one * maxFireboltSize;
        }
    }

    void Shoot() {
        var scale = firebolt.transform.localScale;
        firebolt.transform.parent = null;
        firebolt.transform.localScale = scale;
        firebolt.GetComponent<Rigidbody>().isKinematic = false;
        firebolt.GetComponent<Rigidbody>().AddForce(transform.forward * fireboltSpeed * scale.x);
        firebolt = null;
    }
}