using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparent : MonoBehaviour {

    private GameObject _playerObj;
    private Renderer _renderer;
    public float _opacityPercent = 0.75f;

	// Use this for initialization
	void Start () {
        _playerObj = GameObject.Find("Player");
        _renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        bool showAlpha = _playerObj.transform.position.y > transform.position.y;
        float alpha = showAlpha ? _opacityPercent : 1.0f;

        Color c = _renderer.material.color;
        c.a = alpha;

        _renderer.material.SetColor("_Color", c);
	}
}
