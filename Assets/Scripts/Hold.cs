using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hold : MonoBehaviour
{
    public GameObject sample;

    public Canvas next;

    public List<Tetrimino> tetriminos;

    List<Cube> holdCubes;

    List<GameObject> dropping = new List<GameObject>();

    Tetrimino hold;

    void placeObject(GameObject obj, int x, int y, Transform tr, Vector2Int shape)
    {
        Rect test = tr.GetComponent<RectTransform>().rect;
        Rect me = obj.GetComponent<RectTransform>().rect;

        obj.transform.localScale = new Vector3(
            (test.width / shape.x) / me.width,
            (test.height / shape.y) / me.height, 1.0f);

        obj.transform.localPosition = new Vector3(
                test.x + (test.width / shape.x) * x + (test.width / shape.x) / 2,
                test.y + (test.height / shape.y) * y + (test.height / shape.y) / 2, 0
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
            GameObject tmp = Instantiate(sample, transform);
            tmp.GetComponent<Image>().sprite = holdCubes[i].sprite;
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
