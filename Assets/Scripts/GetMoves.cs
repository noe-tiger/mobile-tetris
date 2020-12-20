using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoves : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 firstPressPosVertical;

    Vector2 secondPressPos;

    bool dropped = false;
    bool holded = false;

    public int Side(float delta)
    {
        if (Input.GetMouseButtonDown(0))
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButton(0) == false)
            return 0;

        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 swipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

        if (Math.Abs(swipe.x) < Math.Abs(swipe.y))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            return 0;
        }
        if (Math.Abs((int)(swipe.x / delta)) >= 1)
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if ((int)(swipe.x / delta) != 0)
            return (int)(swipe.x / delta) > 0 ? 1 : -1;
        return (int)(swipe.x / delta);
    }
    
    public bool Drop()
    {
        if (Input.GetMouseButtonDown(0))
            firstPressPosVertical = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 swipe = new Vector2(secondPressPos.x - firstPressPosVertical.x, secondPressPos.y - firstPressPosVertical.y);

            if (Math.Abs(swipe.x) < Math.Abs(swipe.y))
            {
                if (swipe.y < 0 && !dropped)
                {
                    dropped = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool Hold()
    {
        if (Input.GetMouseButtonDown(0))
            firstPressPosVertical = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 swipe = new Vector2(secondPressPos.x - firstPressPosVertical.x, secondPressPos.y - firstPressPosVertical.y);

            if (Math.Abs(swipe.x) < Math.Abs(swipe.y))
            {
                if (swipe.y > 0 && !holded)
                {
                    holded = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool Rotate()
    {
        if (Input.GetMouseButtonDown(0))
            firstPressPosVertical = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 swipe = new Vector2(secondPressPos.x - firstPressPosVertical.x, secondPressPos.y - firstPressPosVertical.y);

            if (Math.Abs(swipe.x) < 50 && 
                Math.Abs(swipe.y) < 50)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == false) {
            dropped = false;
            holded = false;
        }
    }
}
