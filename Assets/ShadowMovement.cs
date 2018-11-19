using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour {

    public GameObject _gameObject = null;
    public Vector3 _offset = Vector3.zero;
    public float _maxScale = 0.9f;

    private float kMinScale = 0.5f;


    void Update ()
    {
        RaycastHit rayHit;
        int ignoreLayerMask = ~(1 << 8); // Player + Shadow

        Vector3 center = GetColliderCenter();
        Collider collider = _gameObject.GetComponent<Collider>();

        if (!Physics.Raycast(_gameObject.transform.position + center, collider.transform.TransformDirection(Vector3.forward), out rayHit, Mathf.Infinity, ignoreLayerMask))
        {
            return;
        }

        float radius = GetColliderRadius();
        transform.position = rayHit.point - (new Vector3(0.0f, 0.0f, radius * 0.5f) + _offset);

        float newScale = Mathf.Max(kMinScale * _maxScale, _maxScale * (1.0f - Mathf.Min(1.0f, rayHit.distance)));
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private Vector3 GetColliderCenter()
    {
        CapsuleCollider capsuleCollider = _gameObject.GetComponent<CapsuleCollider>();

        if(capsuleCollider != null)
        {
            return capsuleCollider.center;
        }

        return Vector3.zero;
    }

    private float GetColliderRadius()
    {
        CapsuleCollider capsuleCollider = _gameObject.GetComponent<CapsuleCollider>();

        if (capsuleCollider != null)
        {
            return capsuleCollider.radius;
        }

        return 0.5f;
    }
}
