using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Player player;
    public int damage = 1;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private BallsGeneration ballsGeneration;
    void Start()
    {
        ballsGeneration = player.gameObject.GetComponent<BallsGeneration>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            float speed = Vector2.Distance(new Vector2(0, 0), rigidBody.velocity);
            rigidBody.velocity = new Vector2(0, 0);

            if (ballsGeneration.checkIfFirstBallToReachFloor())
            {
                player.SetEndPos(transform.position);
                ballsGeneration.UpdateBallsAtEndPos();
                return;
            }

            StartCoroutine(Lerping(player.GetEndPos(),speed));
        }
        else if (collision.gameObject.tag == "Brick" || collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.GetComponent<IHitAble>().Hit(damage);
        }
    }
    private IEnumerator Lerping(Vector2 endPos,float speed)
    {
        Vector2 direction = (endPos - new Vector2(transform.position.x, transform.position.y));
        direction.Normalize();
        
        while ((Vector2.Distance(endPos, transform.position) > speed * Time.deltaTime))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        
        ballsGeneration.UpdateBallsAtEndPos();
        transform.position = endPos;
        
        if (ballsGeneration.checkIfLastBallToReachFloor())
        {
            player.SetStartPos(endPos);
        }
    }
    public void SetColor()
    {
        if (damage == 1)
        {
            spriteRenderer.color = Color.white;
        }
        else if (damage == 2)
        {
            spriteRenderer.color = Color.red;
        }
    }
}
