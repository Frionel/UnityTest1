using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private GameObject _playerObj;
    private Vector3 _offset;

	void Start () {
        _playerObj = GameObject.Find("Player");
        _offset = transform.position - _playerObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = _offset + _playerObj.transform.position;
	}
}
