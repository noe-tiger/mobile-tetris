using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetrimino : MonoBehaviour
{
    public List<string> shape;
    public GameObject sprite;

    int drop = 0;
    int mid = 0;

    int rotation = 0;

    List<Cube> cubes = new List<Cube>();

    void Init()
    {
        int x = mid;
        int y = drop;

        cubes = new List<Cube>();

        for (int i = 0; i < shape[0].Length; i += 1)
        {
            if (shape[0][i] == '#')
            {
                Cube tmp = gameObject.AddComponent<Cube>();
                tmp.pos = new Vector2Int(x, y);
                tmp.sprite = sprite;
                cubes.Add(tmp);
            }

            if (shape[0][i] == '\\')
            {
                y += 1;
                x = mid;
            }
            else
                x += 1;
        }
    }

    void Start()
    {
        Init();
    }

    public string GetStrShape()
    {
        return shape[rotation];
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
                size.x = cubes[i].pos.x;
            if (cubes[i].pos.y > size.y)
                size.y = cubes[i].pos.y;
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
                max = cubes[i].pos.x;
            if (max < cubes[i].pos.y)
                max = cubes[i].pos.y;
        }
        return new Vector2Int(max + 1, max + 1);
    }

    public Vector2Int ResetShape()
    {
        for (int i = 0; i < cubes.Count; i += 1)
            Destroy(cubes[i]);
        cubes.Clear();
        cubes = new List<Cube>();
        Init();
        return GetShape();
    }

    public void ApplyRotation(List<Vector2Int> newPos)
    {
        rotation += 1;
        if (rotation == shape.Count)
            rotation = 0;
        for (int i = 0; i < newPos.Count; i += 1)
            cubes[i].pos = newPos[i];
    }

    public List<Vector2Int> TryRotate(Vector2Int gridSize) // TODO Wall Kick
    {
        List<Vector2Int> pos = new List<Vector2Int>();

        int level = gridSize.y;
        int offset = gridSize.x;

        for (int i = 0; i < cubes.Count; i += 1)
        {
            if (level > cubes[i].pos.y)
                level = cubes[i].pos.y;
            if (offset > cubes[i].pos.x)
                offset = cubes[i].pos.x;
        }

        int r = rotation + 1;
        if (r == shape.Count)
            r = 0;

        int x = 0;
        int y = 0;
        for (int i = 0; i < shape[r].Length; i += 1)
        {
            if (shape[r][i] == '#')
                pos.Add(new Vector2Int(x + offset, y + level));

            if (shape[r][i] == '\\')
            {
                y += 1;
                x = 0;
            }
            else
                x += 1;
        }
        return pos;
    }
}
