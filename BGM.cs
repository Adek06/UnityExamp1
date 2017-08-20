using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    private static BGM _instance;

    public static BGM Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<BGM>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
	// Use this for initialization
	void Awake () {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);
        } else if (this != _instance) {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
