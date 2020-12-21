using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hold : MonoBehaviour
{
    public Canvas next;

    public List<Tetrimino> tetriminos;

    List<Cube> holdCubes;

    List<GameObject> dropping = new List<GameObject>();

    Tetrimino hold;

    void placeObject(GameObject obj, int x, int y, Transform tr, Vector2Int shape)
    {
        Vector2 me = new Vector2(obj.transform.localScale.x * 2, obj.transform.localScale.y * 2);

        Rect test = tr.GetComponent<RectTransform>().rect;

        Vector3 newScale = new Vector3(
            (test.width / shape.x) / me.x,
            (test.height / shape.y) / me.y,
            (test.width / shape.x) / me.x);
        if (newScale.x != 1.0f)
            obj.transform.localScale = newScale;

        obj.transform.localPosition = new Vector3(
                test.x + (test.width / shape.x) * x + (test.width / shape.x) / 2,
                test.y + (test.height / shape.y) * y + (test.height / shape.y) / 2, 1
            );
    }

    void Start()
    {
        holdCubes = new List<Cube>();

        hold = null;
    }

    void UpdateGrid()
    {
        hold.Offset(hold.ResetShape());
        holdCubes = hold.GetCubes();
        for (int i = 0; i < holdCubes.Count; i += 1)
        {
            GameObject tmp = Instantiate(holdCubes[i].sprite, transform);
            //tmp.GetComponent<Image>().sprite = holdCubes[i].sprite;
            placeObject(tmp, holdCubes[i].pos.x, holdCubes[i].pos.y, transform, hold.GetShape());
            tmp.SetActive(true);
            dropping.Add(tmp);
        }

    }

    public Tetrimino HoldExchange(Tetrimino tet)
    {
        Tetrimino ret = hold;
        hold = tet;
        foreach (GameObject g in dropping)
            Destroy(g);
        dropping.Clear();
        UpdateGrid();
        if (ret == null)
            ret = next.GetComponent<Next>().GetNextTetrimino();
        return ret;
    }

    void Update()
    {
        if (hold == null)
            return;
        if (holdCubes.Count == 0 && hold.GetCubes().Count != holdCubes.Count)
            UpdateGrid();
    }
}
