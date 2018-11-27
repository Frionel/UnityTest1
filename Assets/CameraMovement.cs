using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private GameObject _playerObj;
    private Vector3 _offset;
    bool _autoCenter = true;

	void Start () {
        _playerObj = GameObject.Find("Player");
        _offset = transform.position - _playerObj.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_autoCenter)
        {
            transform.position = _offset + _playerObj.transform.position;
        }
	}

    public IEnumerator centerToObject(GameObject obj)
    {
        _autoCenter = false;

        float duration = 2.0f;
        var dest = _offset + obj.transform.position;
        iTween.MoveTo(gameObject, dest, duration);

        yield return new WaitForSeconds(duration);
    }

    public IEnumerator centerToPlayer()
    {
        float duration = 1.0f;
        var dest = _offset + _playerObj.transform.position;
        iTween.MoveTo(gameObject, dest, duration);

        yield return new WaitForSeconds(duration);

        _autoCenter = true;
    }
}
