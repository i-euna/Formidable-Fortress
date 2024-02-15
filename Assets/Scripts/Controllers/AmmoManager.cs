using System.Collections;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable AmmoCount;

    [SerializeField]
    private TextMeshProUGUI TextComponent;

    void Start()
    {
        AmmoCount.Value = 3;
    }

    public void DecreaseAmmo() {
        AmmoCount.Value--;
        TextComponent.text = "Ammo: " + AmmoCount.Value;
    }

    public void IncreaseAmmo() {
        AmmoCount.Value++;
        TextComponent.text = "Ammo: " + AmmoCount.Value;
    }
}
