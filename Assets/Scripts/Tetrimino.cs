using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetrimino : MonoBehaviour
{
    public string shape;
    public Sprite sprite;

    int drop = 0;
    int mid = 0;

    List<Cube> cubes = new List<Cube>();

    // down
    // right
    // left
    // getCubies

    void Start()
    {
        int x = mid;
        int y = drop;

        cubes = new List<Cube>();

        for (int i = 0; i < shape.Length; i += 1)
        {
            if (shape[i] == '#')
            {
                Cube tmp = gameObject.AddComponent<Cube>();
                tmp.pos = new Vector2Int(x, y);
                tmp.sprite = sprite;
                cubes.Add(tmp);
            }

            if (shape[i] == '\\')
            {
                y += 1;
                x = mid;
            } else
            {
                x += 1;
            }
        }
    }

    public List<Cube> GetCubes()
    {
        return cubes;
    }

    public Vector2Int GetShape()
    {
        int max = 0;

        for (int i = 0; i < cubes.Count; i += 1)
        {
            if (max < cubes[i].pos.x)
            {
                max = cubes[i].pos.x;
            }
            if (max < cubes[i].pos.y)
            {
                max = cubes[i].pos.y;
            }
        }
        return new Vector2Int(max + 1, max + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
