using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject dot;
    private PlayerUI playerUI;
    private GenerateDots generateDots;
    private GenerateBrick generateBrick;
    private SetPositionsAndScale setPositionsAndScale;
    private BallsGeneration ballsGeneration;
    private bool ballsGenerated = false; 
    private bool gameEnd =false;
    private int points = 0;
    private int totalPonits=0;
    private int bricksDestroyed = 0;
    private int totalhits = 0;
    private Vector2 endPos;
    private Vector2 startPos;
    void Start()
    {
        setPositionsAndScale = GetComponent<SetPositionsAndScale>();
        generateBrick = GetComponent<GenerateBrick>();
        generateDots = GetComponent<GenerateDots>();
        playerUI = GetComponent<PlayerUI>();
        ballsGeneration = GetComponent<BallsGeneration>();
        playerUI.ChangeActiveStateOfResetButton(false);
        SetStartPos(setPositionsAndScale.startPos);
        SetEndPos(startPos);
    }
    private void Update()
    {
        if (generateBrick.BricksHasEnded() && !gameEnd && ballsGeneration.ballsAtEndPos == ballsGeneration.ballsAtATime)
        {
            playerUI.GameEnd(points, totalhits, totalPonits, bricksDestroyed);
            gameEnd = true;
        }
    }
    public Vector2 GetEndPos()
    {
        return endPos;
    }
    public Vector2 GetStartPos()
    {
        return startPos;
    }
    public void SetEndPos(Vector2 endPos)
    {
        ballsGeneration.endPos = endPos;
        this.endPos = endPos;
    }
    public void SetStartPos(Vector2 startPos)
    {
        ballsGeneration.startPos = startPos;
        transform.position = startPos;
        this.startPos = startPos;
    }
    public void IncreaseScore(int rawPower)
    {
        points += rawPower;
        playerUI.SetScore(points);
    }
    public void increaseSpeciallypowered()
    {
        ballsGeneration.speciallyPowered++;
    }
    public void increaseDestroyedBricks()
    {
        bricksDestroyed++;
    }
    public void increaseHits()
    {
        totalhits++;
    }
    public void SetTotalPoints(int points)
    {
        totalPonits += points;
        playerUI.SetTotalPoints(totalPonits);
    }
    public void LeftDragging()
    {
        playerUI.changeActiveStateOfSlider(false,ballsGeneration.speciallyPowered!=0,ballsGeneration.speciallyPowered);
       generateDots.unDraw();
        Vector2 directionOfBall = playerUI.SliderValue();
        ballsGeneration.ballsAtEndPos = 0;
        StartCoroutine(ballsGeneration.throwBall(directionOfBall.x, directionOfBall.y,playerUI));
        if (!ballsGenerated)
        {
            ballsGenerated = true;
        }
    }
    public void OnValueChangedWhileDragging()
    {
        Vector2 direction = playerUI.SliderValue() ;
        generateDots.Draw(direction,startPos);
    }
}
