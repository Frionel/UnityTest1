using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : OnActivate
{
    public Quaternion _initialRotation;

    public void Start()
    {
        _initialRotation = transform.rotation;
    }

    public override IEnumerator onActivate()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        var cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        yield return StartCoroutine(cameraScript.centerToObject(gameObject));
        yield return StartCoroutine(openDoor());
        yield return StartCoroutine(cameraScript.centerToPlayer());

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator openDoor()
    {
        Vector3 destRotation = _initialRotation.eulerAngles + new Vector3(0.0f, 0.0f, 90.0f);
        iTween.RotateTo(gameObject, destRotation, 1.0f);
        yield return new WaitForSeconds(1.0f);
    }

    public override void onDeactivate()
    {
        Vector3 destRotation = _initialRotation.eulerAngles + new Vector3(0.0f, 0.0f, 0.0f);
        iTween.RotateTo(gameObject, destRotation, 1.0f);
    }
}
