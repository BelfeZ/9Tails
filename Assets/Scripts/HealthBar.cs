using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider Healthbar_Silder;
    public int maxHealth = 100;
    public int currentHealth;
    public bool life;
    
    public void SetMaxHealth(int health)
    {
        Healthbar_Silder.maxValue = health;
        Healthbar_Silder.value = health;
    }

    public void SetHealth(int health)
    {
        Healthbar_Silder.value = health;
    }

    public void Start()
    {
        life = true;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

    }

    public void Perfect_Healthbar()
    {
        currentHealth += 40;
        SetHealth(currentHealth);
    }

    public void Great_Healthbar()
    {
        currentHealth += 20;
        SetHealth(currentHealth);
    }

    public void Missed_Healthbar()
    {
        currentHealth -= 20;
        SetHealth(currentHealth);
    }
    public void Update()
    {
        if (currentHealth > 0)
        {
            life = true;
        }
        else
        {
            life = false;
        }

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        else if (life == false)
        {
            currentHealth = 0;
        }

    }

}
