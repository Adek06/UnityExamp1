using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    private Transform mapHolder;
    public GameObject[] floorArray;
    public GameObject[] outWallArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitUI;
    private GameManager gameManager;
    public int rows;
    public int cols;
    public int level;

    private List<Vector2> positionList = new List<Vector2>();

    void Awake() {
        level = PlayerPrefs.GetInt("level");
        gameManager =this.GetComponent<GameManager>();
        InitMap();
	}

    private void InitMap(){
        mapHolder = new GameObject("Map").transform;
        makeFrame(rows,cols);
        makeHinder(rows,cols);
	}

    private void makeFrame(int rows,int cols){
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    creatMap(new Vector2(x,y),outWallArray);
                }
                else
                {
                    creatMap(new Vector2(x,y),floorArray);
                }
            }
        }
    }

    private void makeHinder(int rows,int cols){
        positionList.Clear();
        for (int x = 2; x < rows-2 ; x++) {
            for (int y = 2; y < cols-2 ; y++) {
                positionList.Add(new Vector2(x, y));
            }
        }

        int minCountWall = 0;
        int minFood = 0;
        int minEnemy = 0;
        if (level < 3)
        {
            int positionIndex = Random.Range(0, 4);
            creatMap(positionList[positionIndex], exitUI);
            positionList.RemoveAt(positionIndex);

            positionIndex = Random.Range(0, 5);
            creatMap(positionList[positionIndex], foodArray[1]);
            positionList.RemoveAt(positionIndex);

            positionIndex = Random.Range(0, 5);
            creatMap(positionList[positionIndex], wallArray[1]);
            positionList.RemoveAt(positionIndex);

        }
        else
        {
            decorateExit(exitUI);
            decorateHinder(minCountWall, level * 9, wallArray);
            decorateHinder(minFood, level * 5, foodArray);
            decorateHinder(minEnemy, level * 15, enemyArray);
        }
    }

    private void decorateHinder(int mix,int max,GameObject[] array) {
        int count = Random.Range(mix, max);
        for (int i = 0; i < count; i++) {
            int positionIndex = Random.Range(0, positionList.Count);
            creatMap(positionList[positionIndex], array);
            positionList.RemoveAt(positionIndex);
        }
    }

    private void decorateExit(GameObject exitUI) {
        int positionIndex = Random.Range(0,positionList.Count);
        creatMap(positionList[positionIndex],exitUI);
        positionList.RemoveAt(positionIndex);
    }


    private void creatMap(Vector2 pos, GameObject go)
    {
        GameObject map = Instantiate(go, pos, Quaternion.identity);
        map.transform.SetParent(mapHolder);
    }

    private void creatMap(Vector2 pos, GameObject[] array )
    {
        int arrayIndex = Random.Range(0, array.Length);
        GameObject map = Instantiate(array[arrayIndex], pos, Quaternion.identity);
        map.transform.SetParent(mapHolder);
    }

}
