using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BrickUI : MonoBehaviour
{
    [SerializeField] TextMeshPro powerText;
    public void UpdatePowerText(int currentPower,bool specaillyPowered)
    {
        powerText.text = currentPower.ToString();
        if (specaillyPowered) powerText.text = powerText.text + " X";
    }
}
