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

    private List<Vector2> positionList = new List<Vector2>();

    void Awake() {
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
                    creatMap(outWallArray,new Vector2(x,y));
                }
                else
                {
                    creatMap(floorArray,new Vector2(x,y));
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
        decorateHinder(minCountWall,gameManager.level*2,wallArray);
        decorateHinder(minFood,gameManager.level,foodArray);
        decorateHinder(minEnemy,gameManager.level/2,enemyArray);

        GameObject exit = Instantiate(exitUI, new Vector2(cols - 2, rows - 2), Quaternion.identity);
        exit.transform.SetParent(mapHolder);
    }

    private void decorateHinder(int mix,int max,GameObject[] array) {
        int count = Random.Range(mix, max);
        for (int i = 0; i < count; i++) {
            int positionIndex = Random.Range(0, positionList.Count);
            creatMap(array,positionList[positionIndex]);
            positionList.RemoveAt(positionIndex);
        }
    }

    private void creatMap(GameObject[] array, Vector2 pos)
    {
            int arrayIndex = Random.Range(0, array.Length);
            GameObject map = Instantiate(array[arrayIndex],pos, Quaternion.identity);
            map.transform.SetParent(mapHolder);
    }

}
