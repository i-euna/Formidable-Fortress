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
        UpdateAmmoText();
    }

    public void DecreaseAmmo() {
        AmmoCount.Value--;
        UpdateAmmoText();
    }

    public void IncreaseAmmo() {
        AmmoCount.Value++;
        UpdateAmmoText();
    }

    void UpdateAmmoText() {
        TextComponent.text = "Ammo: " + AmmoCount.Value;
    }
}
