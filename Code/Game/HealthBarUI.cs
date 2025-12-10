using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public List<Image> Bars; 

    public void UpdateHealthUI()
    {
        if (Bars == null || Bars.Count == 0)
        {
            Debug.LogError("Bars list is EMPTY. Assign the bar images in the inspector!");
            return;
        }

        int hp = playerHealth.GetCurrentHealth();
        int maxHP = playerHealth.maxHealth;
        float hpPerBar = (float)maxHP / Bars.Count;  

        for (int i = 0; i < Bars.Count; i++)
        {
            float barStartHP = i * hpPerBar;
            float barEndHP = barStartHP + hpPerBar;

            if (hp >= barEndHP)
            {
                Bars[i].fillAmount = 1f; 
            }
            else if (hp <= barStartHP)
            {
                Bars[i].fillAmount = 0f; 
            }
            else
            {
                float portion = (hp - barStartHP) / hpPerBar;
                Bars[i].fillAmount = portion; 
            }
        }
    }
}
