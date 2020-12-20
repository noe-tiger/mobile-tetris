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

    int rotation = 0;

    List<Cube> cubes = new List<Cube>();

    void Init()
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
            }
            else
            {
                x += 1;
            }
        }
    }

    void Start()
    {
        Init();
    }

    public string GetStrShape()
    {
        return shape;
    }

    public List<Cube> GetCubes()
    {
        return cubes;
    }

    public void Offset(Vector2Int offset)
    {
        Vector2Int size = new Vector2Int(0, 0);

        for (int i = 0; i < cubes.Count; i += 1)
        {
            if (cubes[i].pos.x > size.x)
            {
                size.x = cubes[i].pos.x;
            }
            if (cubes[i].pos.y > size.y)
            {
                size.y = cubes[i].pos.y;
            }
        }
        for (int i = 0; i < cubes.Count; i += 1)
        {
            cubes[i].pos.x += offset.x / 2 - size.x + 1;
            cubes[i].pos.y += offset.y - size.y - 1;
        }
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

    public Vector2Int ResetShape()
    {
        for (int i = 0; i < cubes.Count; i += 1)
        {
            Destroy(cubes[i]);
        }
        cubes.Clear();
        cubes = new List<Cube>();
        Init();
        return GetShape();
    }

    public List<Vector2Int> TryRotate()
    {
        Debug.Log(shape);

        List<Vector2Int> pos = new List<Vector2Int>();

        int level = 20;
        int offset = 10;

        for (int i = 0; i < cubes.Count; i += 1)
        {
            if (level > cubes[i].pos.y)
                level = cubes[i].pos.y;
            if (offset > cubes[i].pos.x)
                offset = cubes[i].pos.x;
        }

        rotation += 1; // TODO a remove quand on fera vraiment le try <3
        if (rotation == 4)
            rotation = 0;

        List<List<char>> grid = new List<List<char>>();
        grid.Add(new List<char>());
        int p = 0;

        for (int i = 0; i < shape.Length; i += 1)
        {
            if (shape[i] == '\\')
            {
                grid.Add(new List<char>());
                p += 1;
            }
            else
                grid[p].Add(shape[i]);
        }

        List<List<char>> newGrid = new List<List<char>>();
        p = 0;
        for (int i = 0; i < grid[0].Count; i += 1)
        {
            newGrid.Add(new List<char>());
            for (int j = 0; j < grid.Count; j += 1)
            {
                newGrid[p].Add(grid[j][i]);
            }
            p += 1;
        }

        int k = 0;
        for (int i = 0; i < newGrid.Count; i += 1)
        {
            for (int j = 0; j < grid[i].Count; j += 1)
            {
                cubes[k].pos = new Vector2Int(i + offset, j + level);
                k += 1;
            }
        }

        // assign new pos

        return pos;
    }
}
