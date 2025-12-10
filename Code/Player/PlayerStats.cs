using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int bulletDamage = 2;

    public int GetDamage()
    {
        return bulletDamage;
    }

    public void AddDamage(int amount)
    {
        bulletDamage += amount;
    }
}
