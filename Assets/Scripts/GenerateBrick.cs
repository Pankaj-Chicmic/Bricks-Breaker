using System.Collections.Generic;
using UnityEngine;

public class GenerateBrick : MonoBehaviour
{
    [SerializeField] private Transform leftUp;
    [SerializeField] private Transform rightUp;
    [SerializeField] private Transform leftDown;
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject bricksParent;
    [SerializeField] private int allowedAttemptOnBottomLine=3;
    private int lastIndexOfBricks = -1;
    private int totalBricks;
    private List<List<GameObject>> bricks;
    private void Start()
    {
        GenerateBricks();
    }
    private void GenerateBricks()
    {
        bricks = new List<List<GameObject>>();
        int availablePlaceFroBricks = 0;
        float upMost = leftUp.position.y;
        float downMost = leftDown.position.y;
        while (upMost >= downMost)
        {
            float leftMost = leftUp.position.x;
            float rightMost = rightUp.position.x;
            List<GameObject> tempBricks = new List<GameObject>();
            bool bombCreated = false;
            while (leftMost <= rightMost)
            {
                availablePlaceFroBricks++;
                int random = Random.Range(0, 3);
                if (random > 0)
                {
                    totalBricks++;
                    GameObject tempBrick = Instantiate(brick);
                    tempBrick.transform.position = new Vector2(leftMost, upMost);
                    tempBrick.GetComponent<Brick>().player = GetComponent<Player>();
                    tempBrick.GetComponent<Brick>().generateBrick = gameObject.GetComponent<GenerateBrick>();
                    tempBrick.transform.SetParent(bricksParent.transform);
                    tempBricks.Add(tempBrick);
                }
                else
                {
                    int randomCheckForBomb = Random.Range(0, 11);
                    if (randomCheckForBomb == 5 && !bombCreated)
                    {
                        GameObject tempBomb = Instantiate(bomb);
                        tempBomb.transform.position = new Vector2(leftMost, upMost);
                        tempBomb.GetComponent<Bomb>().generateBrick = gameObject.GetComponent<GenerateBrick>();
                        tempBomb.GetComponent<Bomb>().indexI = lastIndexOfBricks + 1;
                        tempBomb.GetComponent<Bomb>().indexJ = tempBricks.Count;
                        tempBomb.transform.SetParent(bricksParent.transform);
                        tempBricks.Add(tempBomb);                        
                        bombCreated=true;
                    }
                    else
                    {
                        tempBricks.Add(null);
                    }
                }
                leftMost += 1;
            }
            bricks.Add(tempBricks);
            lastIndexOfBricks++;
            upMost -= 1;
        }
        GetComponent<BallsGeneration>().SetBallsAtATime(availablePlaceFroBricks);
    }
    public void MoveBricksBy2Unit()
    {
        allowedAttemptOnBottomLine--;
        bricksParent.transform.position = new Vector2(0, bricksParent.transform.position.y - 1);
        if (allowedAttemptOnBottomLine <= 0)
        {
            for (int i = 0; i < bricks[lastIndexOfBricks].Count; i++)
            {
                if (bricks[lastIndexOfBricks][i] == null) continue;
                Destroy(bricks[lastIndexOfBricks][i]);
            }
            lastIndexOfBricks--;
        }
    }
    public void DecreaseBrick()
    {
        totalBricks--;
    }
    public bool BricksHasEnded()
    {
        return totalBricks <= 0;
    }
    
    public void bombEffect(int indexI, int indexj, int bombDamage, int blastRadius)
    {
        int numRows = bricks.Count;
        int numCols = bricks[0].Count;
        for (int i = indexI - blastRadius; i <= indexI + blastRadius; i++)
        {
            if (i < 0 || i >= numRows)
            {
                continue;
            }

            for (int j = indexj - blastRadius; j <= indexj + blastRadius; j++)
            {
                if (j < 0 || j >= numCols)
                {
                    continue;
                }

                if (bricks[i][j] != null && bricks[i][j].GetComponent<Brick>() != null)
                {
                    bricks[i][j].GetComponent<Brick>().Hit(bombDamage);
                }
            }
        }
    }
}
