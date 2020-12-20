using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Canvas grid;
    public Canvas next;
    public Canvas hold;

    int level = 1;
    bool holded = false;

    IEnumerator Start()
    {
        yield return StartCoroutine(UpdateGridDown());
    }

    IEnumerator UpdateGridDown()
    {
        yield return new WaitForSeconds(0.5f); // make a function with the level // 1.0f / (float)level
        bool ret = grid.GetComponent<Grid>().Down();
        if (!ret)
        {
            grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino());
            holded = false;
        }
        StartCoroutine(UpdateGridDown());
    }

    void Update()
    {
        grid.GetComponent<Grid>().Move(GetComponent<GetMoves>().Side(grid.GetComponent<Grid>().GetMoveDela()));

        // TODO touch -> rotate piece
        if (GetComponent<GetMoves>().Rotate())
        {
            grid.GetComponent<Grid>().Rotate();
        }
        if (GetComponent<GetMoves>().Hold() && !holded)
        {
            holded = true;
            Tetrimino tmp = hold.GetComponent<Hold>().HoldExchange(grid.GetComponent<Grid>().GetTetrimino());
            grid.GetComponent<Grid>().SetTetrimino(tmp);
        }
        if (GetComponent<GetMoves>().Drop())
        {
            while (grid.GetComponent<Grid>().Down()) ;
            grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino());
            holded = false;
        }

    }
}
