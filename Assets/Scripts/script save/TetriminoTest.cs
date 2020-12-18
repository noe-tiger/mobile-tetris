using UnityEngine;
using UnityEngine.UI;

public class TetriminoTest : MonoBehaviour
{
    public GameObject text;

    Vector2 MinPos;
    Vector2 MaxPos;
    Vector2 mousePos;

    void Start()
    {
        Vector2 Size = GetComponent<SpriteRenderer>().bounds.extents;
        MinPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Size;
        MaxPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) /*– Size*/;
    }

    void Update()
    {

        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log(touch.position);
        //        text.GetComponent<Text>().text = "touch: " + touch.position.ToString();
        //    }
        //}



        //if (Input.GetMouseButtonUp(0))
        //{
        Debug.Log(Input.GetMouseButton(0));

            mousePos = (Input.mousePosition);
            Vector2 targetPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);

            targetPos.x = Mathf.Clamp(targetPos.x, MinPos.x, MaxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, MinPos.y, MaxPos.y);
            transform.position = targetPos;

            text.GetComponent<Text>().text = "click: " + targetPos.ToString();
        //}
    }
}