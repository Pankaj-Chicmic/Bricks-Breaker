using UnityEngine;
using TMPro;
public class BombUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro powerText;
    public void SetPower(int power)
    {
        powerText.text = power.ToString();
    }
    public void DeletePowerText()
    {
        Destroy(powerText.gameObject);
    }
}
