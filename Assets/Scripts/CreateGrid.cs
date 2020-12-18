using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGrid : MonoBehaviour
{
    public GameObject background;
    public GameObject emptyBackground;

    public GameObject Grid;

    Vector2 boardSize = new Vector2(10, 20);


    List<GameObject> backgroundItems;

    Grid gameGrid;

    void renderAtPos(GameObject obj, int x, int y)
    {
        Rect rekt = Grid.GetComponent<RectTransform>().rect;
        //Debug.Log(rekt);
        rekt.x /= boardSize.x;
        rekt.y /= boardSize.y;

        Rect size = obj.GetComponent<RectTransform>().rect;
        //obj.transform.localPosition = new Vector2(
        //        (rekt.x * 2 * x - size.x / 2) - rekt.x * (boardSize.x - 2),
        //        (rekt.y * 2 * y - size.y / 2) - rekt.y * (boardSize.y - 2)
        //    );
        obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        //Debug.Log(size);
        //obj.transform.localScale = new Vector3(
        //    (rekt.width / boardSize.x) / size.width,
        //    (rekt.height / boardSize.y) / size.height,
        //    0.0f);
    }


    void Start()
    {
        //for (int i = 0; i < boardSize.x; i += 1)
        //{
        //    for (int j = 0; j < boardSize.y; j += 1)
        //    {
        //        GameObject inst = Instantiate(background, Grid.transform);
        //        renderAtPos(inst, i, j);
        //        inst.SetActive(true);
        //    }
        //}

        //renderAtPos(background, 0, 0);
    }

    void Update()
    {
        /*
        get user input
        update dropping tetrimino
        if (dropped) {
            blend to the grid
            update grid()
            get new tetrimino
        } 
         
         */
    }
}
