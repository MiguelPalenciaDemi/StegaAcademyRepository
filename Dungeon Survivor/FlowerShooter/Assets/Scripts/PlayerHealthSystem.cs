using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{

    [SerializeField]
    float health = 10;
    float currentHealth = 0;

    [SerializeField]
    Image healthBarImage;

    AudioManager audioManager;
    private void Start()
    {
        currentHealth = health;
        healthBarImage.fillAmount = 1;
        audioManager = GetComponent<AudioManager>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hola");
        if(other.gameObject.tag == "EnemyWeapon") 
        {
            GetHurt(2);//Provisional el daño
        
        }
    }

    private void GetHurt(float damage)
    {
        audioManager.PlayHurt();
        currentHealth -= damage;
        healthBarImage.fillAmount = currentHealth / health;
        if (currentHealth <= 0)
        {
            GameManager.Instance.Lose();
            audioManager.PlayDeath();

        }
        Debug.Log("Has recibido daño. Tu salud actual es: " + currentHealth);
    }

    public void Restart()
    {
        currentHealth = health;
        healthBarImage.fillAmount = 1;

    }
}
