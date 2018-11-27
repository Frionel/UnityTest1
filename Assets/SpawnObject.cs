using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject _objToSpawn;
    public bool _removePrevious = false;
    private int _spawnedId;

    void Start()
    {
        _spawnedId = Random.Range(int.MinValue, int.MaxValue);
    }

    void FixedUpdate ()
    {
        string spawnedName = "Spawned_" + _spawnedId;
        GameObject previousSpawned = GameObject.Find(spawnedName);

        if(previousSpawned != null)
        {
            previousSpawned.transform.position = Vector3.zero;
            Object.Destroy(previousSpawned);
        }

        GameObject instance = Instantiate(_objToSpawn);
        instance.transform.position = transform.position;
        instance.name = spawnedName;

        enabled = false;
	}
}
