using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int level=1;
    public int food=10;
    //public Text textLevel;
    public Image dayImage;
    public Text dayText;
    public Text textFood;
    public Player player;
    private MapManager mapManager;
    private Loader firstStart;

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    // Use this for initialization
    void Awake() {
        _instance = this;
        if (level == 0)
        {
            level += 1;
            food += 10;
        }
        else
        {
            level = PlayerPrefs.GetInt("level");
            food = PlayerPrefs.GetInt("food");
        }
        mapManager = this.GetComponent<MapManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        textFood = GameObject.FindGameObjectWithTag("FoodText").GetComponent<Text>();
        dayImage = GameObject.Find("dayImage").GetComponent<Image>();
        dayText = GameObject.Find("dayText").GetComponent<Text>();
        //textLevel = GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>();
        //此处需要改。游戏是否继承
        dayText.text = "DAY " + level;
        Invoke("HideBlack",1);
        ShowText();
    }

    void ShowText() {
        textFood.text = "Food : "+ food;
    }

    public void Update()
    {
        GameOver();
    }

    public void exitOut() {
        level += 1;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("food", food);
        SceneManager.LoadScene("Main");
    }

    public void AddFood(int count){
        food += count;
        textFood.text = "+" + count + " Food: " + food;
    }

    public void ReduceFood(int count) {
        food -= count;
        textFood.text = "-" + count + " Food: " + food;
    }

    private void GameOver() {
        if (food <= 0) {
            SceneManager.LoadScene("GameOver");
        }
    }

    void HideBlack() {
        dayImage.gameObject.SetActive(false);
    }
}
