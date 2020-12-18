using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDrag2 : MonoBehaviour
{
    Vector2 MinPos;
    Vector2 MaxPos;

    Vector2 targetPos;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 Size = GetComponent<SpriteRenderer>().bounds.extents;
        MinPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Size;
        MaxPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) - Size;

        targetPos = new Vector2(0, 0);
        startPos = new Vector2(0, 0);
    }

    Vector2 OnMouseDrag(Vector2 originDrag, Vector2 startPos)
    {
        Vector2 mousePos = (Input.mousePosition);
        Vector2 targetPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);

        targetPos.x = Mathf.Clamp(targetPos.x, MinPos.x, MaxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, MinPos.y, MaxPos.y);

        Debug.Log(startPos.ToString() + (targetPos.x - originDrag.x).ToString() + targetPos.x.ToString() + originDrag.x.ToString());

        return new Vector2(startPos.x + (targetPos.x - originDrag.x), startPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = (Input.mousePosition);
            targetPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);

            targetPos.x = Mathf.Clamp(targetPos.x, MinPos.x, MaxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, MinPos.y, MaxPos.y);

            startPos = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            transform.position = OnMouseDrag(targetPos, startPos);
        }
    }
}
