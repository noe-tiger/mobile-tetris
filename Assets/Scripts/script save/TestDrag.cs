using UnityEngine;
using UnityEngine.UI;

public class TestDrag : MonoBehaviour
{
    public GameObject text;

    //Vector2 MinPos;
    //Vector2 MaxPos;
    //Vector2 mousePos;


    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                text.GetComponent<Text>().text = "up swipe";
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
                text.GetComponent<Text>().text = "dowm swipe";
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
                text.GetComponent<Text>().text = "left swipe" + currentSwipe.ToString();
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
                text.GetComponent<Text>().text = "right swipe" + currentSwipe.ToString();
            }
        }
    }



    void Start()
    {
        //Vector2 Size = GetComponent<SpriteRenderer>().bounds.extents;
        //MinPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Size;
        //MaxPos = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) /*– Size*/;
    }

    void Update()
    {
        Swipe();
        //Debug.Log(Input.GetMouseButton(0));

        //mousePos = (Input.mousePosition);
        //Vector2 targetPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);

        //targetPos.x = Mathf.Clamp(targetPos.x, MinPos.x, MaxPos.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, MinPos.y, MaxPos.y);
        //transform.position = targetPos;

        //text.GetComponent<Text>().text = "click: " + targetPos.ToString();
        ////}
    }
}