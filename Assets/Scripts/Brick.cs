using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour,IHitAble
{
    public Player player;
    public GenerateBrick generateBrick;
    [SerializeField] private int minPower;
    [SerializeField] private int maxPower;
    [SerializeField] private BrickUI brickUI;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    private AudioManager audioManager;
    private bool specialPower = false;
    private int rawPower, currentPower;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        player = FindAnyObjectByType<Player>();
        brickUI = GetComponent<BrickUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
   
        rawPower = Random.Range(minPower, maxPower + 1);
        currentPower = rawPower;
        
        if (currentPower > 90)
        {
            int random = Random.Range(0, 2);
            if (random == 1) specialPower = true;
        }


        player.SetTotalPoints(currentPower);
        brickUI.UpdatePowerText(currentPower,specialPower);
        spriteRenderer.sprite = sprites[(currentPower-1) / 20];
        spriteRenderer.color = GetColor(currentPower);
    }
    public void Hit(int damage)
    {
        player.increaseHits();
        audioManager.PlayBrickHit();
        currentPower = Mathf.Max(0, currentPower - damage);
        brickUI.UpdatePowerText(currentPower,specialPower);

        spriteRenderer.color = GetColor(currentPower);
        spriteRenderer.sprite = sprites[(currentPower-1) / 20];
        
        if (currentPower <= 0)
        {
            audioManager.PlayBrickBroken();
            if(specialPower) player.increaseSpeciallypowered();
            player.IncreaseScore(rawPower);
            player.increaseDestroyedBricks();
            Destroy(gameObject);
        }
    }
    private Color GetColor(int power)
    {
        Color color = new Color();
        color.r = 1;
        color.b = 1;
        color.g = 1;
        float remainder = currentPower % 20;
        if (remainder == 0)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0.5f + remainder / 40.0f;
        }
        return color;
    }
    private void OnDestroy()
    {
        generateBrick.DecreaseBrick();
    }
}
