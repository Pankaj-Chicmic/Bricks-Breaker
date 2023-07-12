using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDots : MonoBehaviour
{
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform upWall;
    [SerializeField] private GameObject dot;
    [SerializeField] float distanceBetweenDots;
    private List<GameObject> dots;
    private void Start()
    {
        dots = new List<GameObject>();
    }
    public void Draw(Vector2 direction, Vector2 startPos)
    {
        Vector2 lastPos = startPos;
        int i = 0;
        int numberOfForwardDots = 0;

        while (true)
        {
            Vector2 nextPos = lastPos + direction * distanceBetweenDots;
            bool isReflected = false;

            if (nextPos.x >= rightWall.position.x - rightWall.localScale.x / 2)
            {
                direction = Reflect(direction, new Vector2(-1, 0));
                isReflected = true;
            }
            else if (nextPos.x <= leftWall.position.x + leftWall.localScale.x / 2)
            {
                direction = Reflect(direction, new Vector2(1, 0));
                isReflected = true;
            }
            else if (nextPos.y >= upWall.position.y - upWall.localScale.y / 2)
            {
                direction = Reflect(direction, new Vector2(0, 1));
                isReflected = true;
            }

            if (isReflected)
            {
                int noOfReflectedDots = numberOfForwardDots / 3;
                for (int j = 0; j < noOfReflectedDots; j++)
                {
                    CreateOrActivateDot(ref i, ref lastPos, ref direction);
                }
                break;
            }
            else
            {
                numberOfForwardDots++;
                CreateOrActivateDot(ref i, ref lastPos, ref direction);
                lastPos = nextPos;
            }
        }

        while (i < dots.Count)
        {
            dots[i].SetActive(false);
            i++;
        }
    }

    private Vector2 Reflect(Vector2 vector, Vector2 normal)
    {
        return vector - 2 * (Vector2.Dot(normal, vector) / normal.sqrMagnitude) * normal;
    }

    private void CreateOrActivateDot(ref int index, ref Vector2 position, ref Vector2 direction)
    {
        Vector2 nextPos = position + direction * distanceBetweenDots;
        if (index >= dots.Count)
        {
            GameObject tempDot = Instantiate(dot);
            tempDot.transform.position = nextPos;
            dots.Add(tempDot);
        }
        else
        {
            dots[index].SetActive(true);
            dots[index].transform.position = nextPos;
        }
        index++;
        position = nextPos;
    }

    public void unDraw()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(false);
        }
    }
}
