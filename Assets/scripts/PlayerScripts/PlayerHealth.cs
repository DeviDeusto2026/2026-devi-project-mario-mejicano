using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 20;
    public bool immunity = false;
    public int contadorTacos = 0;
    [SerializeField] private healthBar healthUI;
    public void SetImmunity(bool value)
    {
        contadorTacos++;
        immunity = value;
        if(contadorTacos >= 10)
        {
            StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Countdown(10));
        }
        healthUI.ChangeUI(health, contadorTacos);
    }
    public bool getImmunity()
    {
        return immunity;
    }
    IEnumerator Countdown(int segudos)
    {
        yield return new WaitForSeconds(segudos);
        immunity = false;
    }
    public int getHealth()
    {
        return health;
    }
    public void TakeDamage(int amount)
    {
        if (!immunity)
        {
            health +=amount;
            if(health <= 0)
            {
                Time.timeScale = 0;
                gameObject.SetActive(false);
            }
        }
        healthUI.ChangeUI(health, contadorTacos);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            contadorTacos = 10;
            SetImmunity(true);
        }
    }
}
