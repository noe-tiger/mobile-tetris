using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public GameObject sample;

    public Canvas holdCanva;
    public Canvas nextCanva;

    public List<Tetrimino> tetriminos;

    private Vector2Int gridSize = new Vector2Int(10, 20);

    List<Cube> gridContent;
    List<Cube> nextCubes;
    List<Cube> dropCubes;

    List<GameObject> dropping = new List<GameObject>();

    Tetrimino hold;
    Tetrimino next;
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

        hold = null;
        next = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);
        drop = Instantiate<Tetrimino>(tetriminos[Random.Range(0, tetriminos.Count)], transform);

        nextCubes = next.GetCubes();
        dropCubes = drop.GetCubes();

        //for (int i = 0; i < cubes.Count; i += 1)
        //{
        //    Debug.Log(i.ToString() + cubes[i].pos.ToString());
        //    //GameObject tmp = Instantiate(sample, transform);
        //    //placeObject(tmp, cubes[i].pos.x, cubes[i].pos.y);
        //}


        //for (int i = 0; i < gridSize.x; i += 1)
        //{
        //    for (int j = 0; j < gridSize.y; j += 1)
        //    {
        //        GameObject tmp = Instantiate(sample, transform);
        //        placeObject(tmp, i, j);
        //        tmp.SetActive(true);
        //    }
        //}
    }

    void Update()
    {
        if (dropCubes.Count == 0 && next.GetCubes().Count != dropCubes.Count)
        {
            dropCubes = next.GetCubes();
            for (int i = 0; i < dropCubes.Count; i += 1)
            {                
                GameObject tmp = Instantiate(sample, holdCanva.transform);
                tmp.GetComponent<Image>().sprite = dropCubes[i].sprite;
                placeObject(tmp, dropCubes[i].pos.x, dropCubes[i].pos.y, holdCanva.transform, next.GetShape());
                tmp.SetActive(true);
                Debug.Log(next.GetShape());
                dropping.Add(tmp);
            }
        }
        // foreach cube in gridContent placeObject
        // tetrimino -> godown
        // render hold
        // render next
    }
}
