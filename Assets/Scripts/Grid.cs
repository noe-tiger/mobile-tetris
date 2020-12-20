using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public GameObject sample;

    public List<Tetrimino> tetriminos;

    private Vector2Int gridSize = new Vector2Int(10, 20);

    List<GameObject> dropped = new List<GameObject>();
    List<Cube> gridContent;

    List<GameObject> dropping = new List<GameObject>();
    List<Cube> dropCubes;

    Tetrimino drop;

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
        gridContent = new List<Cube>();

        drop = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);

        dropCubes = drop.GetCubes();
    }

    void UpdateGrid()
    {
        drop.Offset(gridSize);
        dropCubes = drop.GetCubes();
        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            GameObject tmp = Instantiate(sample, transform);
            tmp.GetComponent<Image>().sprite = dropCubes[i].sprite;
            placeObject(tmp, dropCubes[i].pos.x, dropCubes[i].pos.y, transform, gridSize);
            tmp.SetActive(true);
            dropping.Add(tmp);
        }
    }

    public int Blend(Tetrimino nextTetrimino)
    {
        int score = 0;
        int[] lines = new int[gridSize.y];

        for (int i = 0; i < gridSize.y; i += 1)
            lines[i] = 0;

        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            dropped.Add(dropping[i]);
            gridContent.Add(dropCubes[i]);
        }

        for (int i = 0; i < gridContent.Count; i += 1)
            lines[gridContent[i].pos.y] += 1;

        Destroy(drop);
        drop = nextTetrimino;
        dropCubes.Clear();
        dropping.Clear();

        for (int i = 0; i < gridSize.y; i += 1)
        {
            if (lines[i] == gridSize.x)
            {
                List<int> id = new List<int>();
                for (int j = 0; j < gridContent.Count; j += 1)
                    if (gridContent[j].pos.y == i - score)
                        id.Add(j);
                for (int j = id.Count - 1; j >= 0; j -= 1)
                {
                    Cube tmp = gridContent[id[j]];
                    gridContent.RemoveAt(id[j]);
                    Destroy(dropped[id[j]]);
                    dropped.RemoveAt(id[j]);
                    Destroy(tmp);
                }
                for (int j = 0; j < gridContent.Count; j += 1)
                    if (gridContent[j].pos.y >= i)
                        gridContent[j].pos.y -= 1;
                score += 1;
            }
        }

        for (int i = 0; i < gridContent.Count; i += 1)
            placeObject(dropped[i], gridContent[i].pos.x, gridContent[i].pos.y, transform, gridSize);

        UpdateGrid();
        return score;
    }

    public bool Down()
    {
        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            if (dropCubes[i].pos.y - 1 < 0)
                return false;
            for (int j = 0; j < gridContent.Count; j += 1)
            {
                if (gridContent[j].pos.x == dropCubes[i].pos.x &&
                    gridContent[j].pos.y == dropCubes[i].pos.y - 1)
                    return false;
            }
        }
        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            dropCubes[i].pos.y -= 1;
            placeObject(dropping[i], dropCubes[i].pos.x, dropCubes[i].pos.y, transform, gridSize);
        }
        return true;
    }

    public bool Move(int move)
    {
        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            if (dropCubes[i].pos.x + move < 0 || dropCubes[i].pos.x + move > gridSize.x - 1)
                return false;
            for (int j = 0; j < gridContent.Count; j += 1)
            {
                if (gridContent[j].pos.x == dropCubes[i].pos.x + move &&
                    gridContent[j].pos.y == dropCubes[i].pos.y)
                    return false;
            }
        }
        for (int i = 0; i < dropCubes.Count; i += 1)
        {
            dropCubes[i].pos.x += move;
            placeObject(dropping[i], dropCubes[i].pos.x, dropCubes[i].pos.y, transform, gridSize);
        }
        return true;
    }

    public Tetrimino GetTetrimino()
    {
        Tetrimino ret = drop;
        drop = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);
        foreach (GameObject g in dropping)
        {
            Destroy(g);
        }
        dropping.Clear();
        UpdateGrid();
        return ret;
    }

    public void SetTetrimino(Tetrimino tet)
    {
        drop = tet;
        UpdateGrid();
    }

    public float GetMoveDela()
    {
        return GetComponent<RectTransform>().rect.width / gridSize.x;
    }

    public bool Rotate()
    {
        List<Vector2Int> pos = drop.TryRotate(gridSize);

        for (int i = 0; i < pos.Count; i += 1)
        {
            if (pos[i].x < 0 || pos[i].x > gridSize.x - 1 ||
                pos[i].y < 0 || pos[i].y > gridSize.y - 1)
                return false;
            for (int j = 0; j < gridContent.Count;  j += 1)
            {
                if (pos[i].x == gridContent[j].pos.x && pos[i].y == gridContent[j].pos.y)
                    return false;
            }
        }
        drop.ApplyRotation(pos);
        return true;
    }

    void Update()
    {
        if (dropCubes.Count == 0 && drop.GetCubes().Count != dropCubes.Count)
            UpdateGrid();
    }
}
