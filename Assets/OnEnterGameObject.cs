using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterGameObject : MonoBehaviour {

    public GameObject _triggerObject;
    public GameObject _activateObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsTriggerObject(other.gameObject))
        {
            StartCoroutine(GetActivateScript().onActivate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsTriggerObject(other.gameObject))
        {
            GetActivateScript().onDeactivate();
        }
    }

    private OnActivate GetActivateScript()
    {
        return _activateObject.GetComponent<OnActivate>();
    }

    private bool IsTriggerObject(GameObject obj)
    {
        return obj.tag == _triggerObject.tag;
    }
}
