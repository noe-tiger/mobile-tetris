using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Canvas grid;
    public Canvas next;
    public Canvas hold;

    public GameObject Score;
    public GameObject Level;

    int level = 1;
    int score = 0;
    int cleared = 0;
    bool holded = false;

    IEnumerator Start()
    {
        yield return StartCoroutine(UpdateGridDown());
    }

    void IncreaseScore(int lines)
    {
        // TODO play sound
        int[] increase = { 0, 40, 100, 300, 1200};
        score += increase[lines] * level;
        if (cleared * level > level) // TODO play diferent sound
            level += 1;
        cleared += lines;
    }

    IEnumerator UpdateGridDown()
    {
        yield return new WaitForSeconds(1.0f * ((1 / 3) ^ level));
        bool ret = grid.GetComponent<Grid>().Down();
        if (!ret)
        {
            IncreaseScore(grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino()));
            holded = false;
        }
        StartCoroutine(UpdateGridDown());
    }

    void Update()
    {
        grid.GetComponent<Grid>().Move(GetComponent<GetMoves>().Side(grid.GetComponent<Grid>().GetMoveDela()));

        if (GetComponent<GetMoves>().Rotate())
            grid.GetComponent<Grid>().Rotate();
        if (GetComponent<GetMoves>().Hold() && !holded) // TODO trop sensible
        { // TODO, get le next si null !!
            holded = true;
            Tetrimino tmp = hold.GetComponent<Hold>().HoldExchange(grid.GetComponent<Grid>().GetTetrimino());
            grid.GetComponent<Grid>().SetTetrimino(tmp);
        }
        if (GetComponent<GetMoves>().Drop()) // TODO play sound
        {
            while (grid.GetComponent<Grid>().Down()) ;
            IncreaseScore(grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino()));
            holded = false;
        }
        Level.GetComponent<Text>().text = "Level : " + level.ToString();
        Score.GetComponent<Text>().text = "Score : " + score.ToString();
    }
}
