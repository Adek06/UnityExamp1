using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    // Use this for initialization
    private Button btn;

    private static StartButton _instance;
    public static StartButton Instance{
        get
        {
            return _instance;
        }
    }


    void Awake () {
        _instance = this;
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Click);
	}

   public void Click()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("food",200);
        PlayerPrefs.SetInt("level",1);
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
