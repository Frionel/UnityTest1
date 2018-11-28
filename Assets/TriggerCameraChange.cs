using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraChange : MonoBehaviour {

    public enum CameraOrientation
    {
        Default,
        Lateral
    }

    public CameraOrientation _cameraOrientation;

    private CameraMovement _cameraMovementScript;

	// Use this for initialization
	void Start ()
    {
        _cameraMovementScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        setCameraOrientation(_cameraOrientation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        setCameraOrientation(getOppositeCameraOrientation(_cameraOrientation));
    }

    private CameraOrientation getOppositeCameraOrientation(CameraOrientation co)
    {
        switch (co)
        {
            case CameraOrientation.Default: return CameraOrientation.Lateral;
            case CameraOrientation.Lateral: return CameraOrientation.Default;
        }

        Debug.Assert(false, "Incorrect camera orientation");
        return CameraOrientation.Default;
    }

    private void setCameraOrientation(CameraOrientation co)
    {
        switch (co)
        {
            case CameraOrientation.Default:
                {
                    _cameraMovementScript.setDefaultCamera();
                    break;
                }
            case CameraOrientation.Lateral:
                {
                    _cameraMovementScript.setLateralCamera();
                    break;
                }
        }
    }
}
