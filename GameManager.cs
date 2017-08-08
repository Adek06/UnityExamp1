using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int level = 1;
    public int food = 100;
    public List<Enemy> enemyList = new List<Enemy>();

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    // Use this for initialization
    void Awake () {
        _instance = this;
	}

    public void AddFood(int count){
        food += count;
    }

    public void ReduceFood(int count) {
        food -= count;
    }

}
