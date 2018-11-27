using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour {

    public float _gravityPercent = 0.1f;

    private Vector3 _defaultGravity;
    private PlayerMovement _playerMovementScript;


    void Start ()
    {
        _defaultGravity = Physics.gravity;
        _playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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

        Physics.gravity = _defaultGravity * _gravityPercent;
        _playerMovementScript.SetInfiniteJumps(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        Physics.gravity = _defaultGravity;
        _playerMovementScript.SetInfiniteJumps(false);
    }
}
