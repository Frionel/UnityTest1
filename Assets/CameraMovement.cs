using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    struct CameraConfig
    {
        public Vector3 offset;
        public Quaternion rotation;

        public CameraConfig(Vector3 o, Quaternion q)
        {
            offset = o;
            rotation = q;
        }
    }

    private GameObject _playerObj;
    private CameraConfig _defaultConfig;
    private CameraConfig _lateralConfig;
    private CameraConfig _activeCameraConfig;

    bool _autoCenter = true;

	void Start () {
        _playerObj = GameObject.Find("Player");

        _defaultConfig = new CameraConfig(transform.position - _playerObj.transform.position, transform.rotation);

        Vector3 lateralPosition = new Vector3(30.0f, 0.0f, -5.40f);
        Quaternion lateralRotation = Quaternion.Euler(0.0f, -90.0f, 90.0f);
        _lateralConfig = new CameraConfig(lateralPosition - _playerObj.transform.position, lateralRotation);

        _activeCameraConfig = _defaultConfig;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(_autoCenter)
        {
            transform.position = _activeCameraConfig.offset + _playerObj.transform.position;
        }
	}

    public IEnumerator centerToObject(GameObject obj)
    {
        _autoCenter = false;

        float duration = 2.0f;
        var dest = _activeCameraConfig.offset + obj.transform.position;
        iTween.MoveTo(gameObject, dest, duration);

        yield return new WaitForSeconds(duration);
    }

    public IEnumerator centerToPlayer()
    {
        float duration = 1.0f;
        var dest = _activeCameraConfig.offset + _playerObj.transform.position;
        iTween.MoveTo(gameObject, dest, duration);

        yield return new WaitForSeconds(duration);

        _autoCenter = true;
    }

    public void setLateralCamera()
    {
        _activeCameraConfig = _lateralConfig;
        transform.rotation = _lateralConfig.rotation;
    }

    public void setDefaultCamera()
    {
        _activeCameraConfig = _defaultConfig;
        transform.rotation = _defaultConfig.rotation;
    }
}
