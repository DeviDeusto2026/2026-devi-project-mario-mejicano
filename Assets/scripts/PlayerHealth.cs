using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 20;
    bool immunity = false;
    public void SetImmunity(bool value)
    {
        immunity = value;
    }
    public int getHealth()
    {
        return health;
    }
    public void TakeDamagee(int amount)
    {
        if (!immunity)
        {
            health -=amount;
            if(health <= 0)
            {
                Time.timeScale = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
