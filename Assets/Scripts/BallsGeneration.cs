using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsGeneration : MonoBehaviour
{
    public int ballsAtEndPos;
    public int ballsAtATime;
    public int speciallyPowered;
    public Vector2 startPos;
    public Vector2 endPos;
    [SerializeField] private GameObject ball;
    [SerializeField] private int impulsePower;
    [SerializeField] private float ballPerAvaiablePlaceForBrick = (1f / 6f);
    [SerializeField] private float timeBetweenBalls;
    private PlayerUI playerUI;
    private Player player;
    private GenerateBrick generateBrick;
    private List<GameObject> balls;

    public void Awake()
    {
        player = GetComponent<Player>();
        playerUI = GetComponent<PlayerUI>();
        generateBrick = GetComponent<GenerateBrick>();
        balls = new List<GameObject>();
    }
    public void SetBallsAtATime(int availablePlaceForBricks)
    {
        ballsAtATime =(int) (availablePlaceForBricks * ballPerAvaiablePlaceForBrick);
        playerUI.SetStartPosBalls(startPos, ballsAtATime);
    }
    public IEnumerator throwBall(float x, float y,PlayerUI playerUI)
    {
        if (balls.Count < ballsAtATime)
        {
            for (int i = 0; i < ballsAtATime; i++)
            {
                GameObject tempBall = Instantiate(ball);
                tempBall.GetComponent<Ball>().player = GetComponent<Player>();
                tempBall.transform.position = startPos;
                tempBall.GetComponent<Rigidbody2D>().AddForce(impulsePower * new Vector2(x, y), ForceMode2D.Impulse);
                balls.Add(tempBall);
                playerUI.SetStartPosBalls(startPos, ballsAtATime - i - 1);
                if (i < ballsAtATime)
                    yield return new WaitForSecondsRealtime(timeBetweenBalls);
            }
            playerUI.ChangeActiveStateOfResetButton(true);
        }
        else
        {
            int i = 0;
            bool powered = (speciallyPowered > 0);
            while (i < balls.Count)
            {
                balls[i].GetComponent<Rigidbody2D>().AddForce(impulsePower * new Vector2(x, y), ForceMode2D.Impulse);
                if (powered)
                {
                    balls[i].GetComponent<Ball>().damage = 2;
                }
                else
                {
                    balls[i].GetComponent<Ball>().damage = 1;
                }
                balls[i].GetComponent<Ball>().SetColor();
                playerUI.SetStartPosBalls(startPos, ballsAtATime - i - 1);
                i++;
                if (i < balls.Count)
                    yield return new WaitForSecondsRealtime(timeBetweenBalls);
            }
            if (powered) speciallyPowered--;
            
            playerUI.ChangeActiveStateOfResetButton(true);
        }
    }
    public void ResetAllBalls() 
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            balls[i].transform.position = endPos;
        }
        player.SetStartPos(endPos);
        playerUI.ChangeActiveStateOfResetButton(false);
        generateBrick.MoveBricksBy2Unit();
        playerUI.changeActiveStateOfSlider(true, speciallyPowered != 0, speciallyPowered);
        ballsAtEndPos = ballsAtATime;
        playerUI.SetStartPosBalls(startPos, ballsAtATime);
        playerUI.SetEndPosBalls(endPos, 0);
    }
    public bool checkIfFirstBallToReachFloor()
    {
        return ballsAtEndPos == 0;
    }
    public bool checkIfLastBallToReachFloor()
    {
        bool check = ballsAtEndPos + 1 == ballsAtATime;
        return check;
    }
    public void UpdateBallsAtEndPos()
    {
        ballsAtEndPos++;
        playerUI.SetEndPosBalls(endPos, ballsAtEndPos);
        if (ballsAtEndPos == ballsAtATime)
        {
            generateBrick.MoveBricksBy2Unit();
            playerUI.changeActiveStateOfSlider(true,speciallyPowered != 0, speciallyPowered);
            playerUI.ChangeActiveStateOfResetButton(false);
        }
    }
}

