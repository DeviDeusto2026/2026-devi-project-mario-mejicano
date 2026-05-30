using TMPro;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text tacoText;
    private void Start()
    {
        healthText.text = health.getHealth() + "/20";
        tacoText.text = "0/10";
    }
    public void ChangeUI(int health, int tacos)
    {
        healthText.text = health + "/20";
        tacoText.text = tacos + "/10";
    }
}
