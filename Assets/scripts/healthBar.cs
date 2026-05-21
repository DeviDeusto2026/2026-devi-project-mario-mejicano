using TMPro;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private TMP_Text text;
    private void Start()
    {
        text.text = health.getHealth() + "/20";
    }
    public void ChangeUI(int health)
    {
        text.text = health + "/20";
    }
}
