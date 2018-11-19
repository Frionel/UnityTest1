using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInvoker : MonoBehaviour {

    private Vector3 _initialPos;
    private bool _pressed = false;

	// Use this for initialization
	void Start () {
        _initialPos = transform.position;
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ENTER");
        if (_pressed)
        {
            return;
        }
        
        _pressed = true;
        moveSwitch();
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("EXIT");

        if (_pressed)
        {
            _pressed = false;
            moveSwitch();
            collision.gameObject.transform.parent = null;
        }
    }

    private void moveSwitch()
    {
        Debug.Log("MOVING");
        float newZ = _pressed ? _initialPos.z + 0.3f : _initialPos.z;

        iTween.Stop(gameObject);

        iTween.MoveTo(gameObject,
            iTween.Hash("z", newZ,
                        "time", 0.1f,
                        "easeType", iTween.EaseType.linear));
    }
}
