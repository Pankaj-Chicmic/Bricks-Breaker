using UnityEngine;

public class Bomb : MonoBehaviour,IHitAble
{
    public GenerateBrick generateBrick;
    public int indexI;
    public int indexJ;
    [SerializeField] private int minPower;
    [SerializeField] private int maxPower;
    [SerializeField] private int minBombDamage;
    [SerializeField] private int maxBombDamage;
    [SerializeField] private int minBlastRadius;
    [SerializeField] private int maxBlastRadius;
    private AudioManager audioManager;
    private BombUI bombUI;
    private int power;
    private int bombDamage;
    private int blastRadius;
    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        bombUI = GetComponent<BombUI>();
        bombDamage = Random.Range(minBombDamage, maxBombDamage+1);
        power = Random.Range(minPower, maxPower + 1);
        blastRadius = Random.Range(minBlastRadius, maxBlastRadius+1);
        bombUI.SetPower(power);
    }
    public void Hit(int damage)
    {
        audioManager.PlaybombHit();
        power = Mathf.Max(0, power-damage);
        bombUI.SetPower(power);
        if (power <= 0)
        {
            audioManager.PlayBombed();
            GetComponent<Collider2D>().enabled = false;
            bombUI.DeletePowerText();
            GetComponent<Animator>().SetBool("Play", true);
            generateBrick.bombEffect(indexI,indexJ,bombDamage,blastRadius);
            Invoke("DestroyGameObject",0.5f);
        }
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
