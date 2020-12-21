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

    public AudioSource Music;
    public AudioSource SoundEffects;

    public AudioClip HardDrop;
    public AudioClip SoftDrop;
    public AudioClip LevelUp;
    public AudioClip HoldClip;
    public AudioClip GameOver;
    public GameObject GameOverlay;
    public List<AudioClip> LineSound;

    int level = 1;
    int score = 0;
    int cleared = 0;

    int downed = 0;

    bool holded = false;
    bool moved = false;

    IEnumerator Start()
    {
        yield return StartCoroutine(UpdateGridDown());
    }

    void playSound(AudioClip clip)
    {
        SoundEffects.clip = clip;
        SoundEffects.Play();
    }

    void IncreaseScore(int lines)
    {
        if (downed <= 1)
        {
            downed = -1;
            return;
        }
        int[] increase = { 0, 40, 100, 300, 1200};
        score += increase[lines] * level;
        if (10 * level < cleared)
        {
            level += 1;
            playSound(LevelUp);
        }
        else
            playSound(LineSound[lines]);
        cleared += lines;
        downed = 0;
    }

    IEnumerator UpdateGridDown()
    {
        float time = 1.0f;
        for (int i = 0; i < level - 1; i += 1)
            time *= 0.66f;
        yield return new WaitForSeconds(time);
        bool ret = grid.GetComponent<Grid>().Down();
        if (!ret)
        {
            playSound(SoftDrop);
            IncreaseScore(grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino()));
            holded = false;
        } else
            downed += 1;
        if (downed >= 0)
            StartCoroutine(UpdateGridDown());
    }

    void Update()
    {
        if (downed == -1)
        {
            downed = -2;
            playSound(GameOver);
            Music.Stop();
            GameOverlay.SetActive(true);
            Debug.Log("GAME OVER");
            return ;
        }
        if (grid.GetComponent<Grid>().Move(GetComponent<GetMoves>().Side(grid.GetComponent<Grid>().GetMoveDela())) == false)
            moved = true;
        grid.GetComponent<Grid>().Ghost();

        //Debug.Log(Input.GetMouseButton(0).ToString() + " " + moved.ToString());
        if (Input.GetMouseButton(0) == false)
            moved = false;
        if (moved == true)
            return;

        if (GetComponent<GetMoves>().Rotate())
            grid.GetComponent<Grid>().Rotate();
        else if (GetComponent<GetMoves>().Hold() && !holded)
        {
            playSound(HoldClip);
            holded = true;
            Tetrimino tmp = hold.GetComponent<Hold>().HoldExchange(grid.GetComponent<Grid>().GetTetrimino());
            grid.GetComponent<Grid>().SetTetrimino(tmp);
        }
        else if (GetComponent<GetMoves>().Drop())
        {
            downed += 2;
            playSound(HardDrop);
            while (grid.GetComponent<Grid>().Down()) ;
            IncreaseScore(grid.GetComponent<Grid>().Blend(next.GetComponent<Next>().GetNextTetrimino()));
            holded = false;
        }
        Level.GetComponent<Text>().text = "Level : " + level.ToString();
        Score.GetComponent<Text>().text = "Score : " + score.ToString();
    }
}
