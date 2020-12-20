using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    public GameObject sample;

    public List<Tetrimino> tetriminos;

    List<Cube> nextCubes;

    List<GameObject> dropping = new List<GameObject>();

    Tetrimino next;

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

    // Start is called before the first frame update
    void Start()
    {
        nextCubes = new List<Cube>();

        next = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);

        nextCubes = next.GetCubes();
    }

    void UpdateGrid()
    {
        next.Offset(next.GetShape());
        nextCubes = next.GetCubes();
        for (int i = 0; i < nextCubes.Count; i += 1)
        {
            GameObject tmp = Instantiate(sample, transform);
            tmp.GetComponent<Image>().sprite = nextCubes[i].sprite;
            placeObject(tmp, nextCubes[i].pos.x, nextCubes[i].pos.y, transform, next.GetShape());
            tmp.SetActive(true);
            dropping.Add(tmp);
        }
    }

    public Tetrimino GetNextTetrimino()
    {
        Tetrimino ret = next;
        next = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);
        foreach (GameObject g in dropping)
        {
            Destroy(g);
        }
        dropping.Clear();
        UpdateGrid();
        return ret;
    }

    void Update()
    {
        if (nextCubes.Count == 0 && next.GetCubes().Count != nextCubes.Count)
            UpdateGrid();
    }
}
