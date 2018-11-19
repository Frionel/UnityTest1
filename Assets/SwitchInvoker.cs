using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInvoker : MonoBehaviour {

    public GameObject _disabledObject;

    private Vector3 _initialPos;
    private bool _pressed = false;
    private GameObject _switchObject;

	// Use this for initialization
	void Start () {
        _initialPos = transform.position;
        _switchObject = transform.parent.gameObject;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        if (_pressed)
        {
            return;
        }

        _pressed = true;
        moveSwitch();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_pressed)
        {
            _pressed = false;
            moveSwitch();
        }
    }
    
    private void moveSwitch()
    {
        float newZ = _pressed ? _initialPos.z + 0.4f : _initialPos.z;

        iTween.Stop(_switchObject);

        iTween.MoveTo(_switchObject, iTween.Hash("z", newZ,
                                                  "time", 0.1f,
                                                  "easeType", iTween.EaseType.linear));

        if (_pressed)
        {
            onPressed();
        }
    }

    private void onPressed()
    {
        _disabledObject.SetActive(true);
    }
}
