using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analogy : MonoBehaviour {

    public RectTransform handle;
    public RectTransform rect;
    public Transform player;
    private bool dragging = false;

    private static Analogy _instance;
    public static Analogy Instance {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        Drage();
    }

    public void StartDrag() {
        dragging = true;
    }

    public Vector2 Drage() {
        if (!dragging) { return new Vector2(0,0); }
        Vector2 mPos = Input.mousePosition;
        Vector2 newPos = mPos - rect.anchoredPosition;
        Vector2 pos = Vector2.ClampMagnitude(newPos,70);
        handle.anchoredPosition = pos;

        Vector2 dir = pos.normalized* 60 * Time.deltaTime;
        print(dir);
        return dir;
    }

   public void StopDrage() {
        dragging = false;
        handle.anchoredPosition = Vector2.zero;
    }
}
