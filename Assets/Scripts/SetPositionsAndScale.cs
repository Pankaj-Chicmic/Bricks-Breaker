using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionsAndScale : MonoBehaviour
{
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform upWall;
    [SerializeField] private Transform downWall;
    [SerializeField] private Transform leftUp;
    [SerializeField] private Transform rightUp;
    [SerializeField] private Transform leftDown;
    [SerializeField] private Transform rightDown;
    [SerializeField] GameObject backGround;
    public Vector2 startPos;
    private void OnEnable()
    {
        SetWalls();
    }
    private void SetWalls()
    {
        Camera mainCamera = Camera.main;
        float height = 2.0f * mainCamera.orthographicSize;
        float halfScreenWidth = height * mainCamera.aspect * 0.5f;

        Vector2 positionOfRightWall = new Vector2(halfScreenWidth, 0);
        rightWall.position = positionOfRightWall;
        Vector2 positionOfLeftWall = new Vector2(-halfScreenWidth, 0);
        leftWall.position = positionOfLeftWall;
        Vector2 positionOfUpWall = new Vector2(0, height * 0.5f);
        upWall.position = positionOfUpWall;
        Vector2 positionOfDownWall = new Vector2(0, -height * 0.5f);
        downWall.position = positionOfDownWall;

        float widthOfDownWall = halfScreenWidth * 2;
        downWall.localScale = new Vector2(widthOfDownWall, height / 6);
        upWall.localScale = new Vector2(widthOfDownWall, upWall.localScale.y);
        leftWall.localScale = new Vector2(leftWall.localScale.x, Mathf.Ceil(height));
        rightWall.localScale = new Vector2(rightWall.localScale.x, Mathf.Ceil(height));

        Vector2 leftUpPosition = new Vector2(positionOfLeftWall.x + 2, positionOfUpWall.y - 1.5f);
        leftUp.position = leftUpPosition;
        Vector2 rightUpPosition = new Vector2(positionOfRightWall.x - 2, positionOfUpWall.y - 1.5f);
        rightUp.position = rightUpPosition;
        Vector2 leftDownPosition = new Vector2(positionOfLeftWall.x + 2, positionOfDownWall.y + 6.5f);
        leftDown.position = leftDownPosition;
        Vector2 rightDownPosition = new Vector2(positionOfRightWall.x - 2, positionOfDownWall.y + 6.5f);
        rightDown.position = rightDownPosition;

        float downWallHeight = downWall.localScale.y;
        startPos = new Vector2(0, positionOfDownWall.y + downWallHeight / 2 + 0.25f);
        transform.position = startPos;

        float backGroundWidth = widthOfDownWall;
        backGround.transform.localScale = new Vector2(backGroundWidth, height);
    }

}
