using TMPro;
using UnityEngine;

public class TowerHealthManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable Health;

    [SerializeField]
    private TextMeshProUGUI TextComponent;

    private void Start()
    {
        Health.Value = 100;
        UpdateHealth();
    }

    public void DecreaseHealth() {
        Health.Value -= 10;
        UpdateHealth();
    }

    void UpdateHealth() {
        TextComponent.text = "Health: " + Health.Value;
    }
}
