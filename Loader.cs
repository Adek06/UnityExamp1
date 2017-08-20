using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public bool firstStart;

    // Use this for initialization
  /*  private static Loader _instance;
    public static Loader Instance
    {
        get
        {
            return _instance;
        }
    }
    */
    void Awake () {
        //_instance = this;
        if (GameManager.Instance == null)
        {
            //PlayerPrefs.DeleteAll();
           // firstStart = true;
            GameObject.Instantiate(gameManager);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
