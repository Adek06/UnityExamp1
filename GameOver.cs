using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Button btn;
    public Text score;
    // Use this for initialization
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        score.text = "Lived " + GameManager.Instance.level + " Day";
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(gameOverClick);

    }
    void gameOverClick()
    {
        StartButton.Instance.Click();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
