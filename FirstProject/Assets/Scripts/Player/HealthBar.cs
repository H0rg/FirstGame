using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Button btnHeal;
    [SerializeField] private Button btnDamage;
    public Slider slider;

    
    public int maxHp = 100;
    public int hp;

    private void Awake()
    {
        btnHeal.onClick.AddListener(Heal);
        btnDamage.onClick.AddListener(Damage);
        slider.maxValue = maxHp;
        hp = maxHp;
        slider.value = hp;
    }


    

    private void Heal()
    {
        hp += 10;
        if (hp > maxHp) hp = maxHp;
        slider.value = hp;
    }
     
    private void Damage()
    {
        hp -= 10;
        if (hp < 0 ) hp = 0;
        slider.value = hp;
    }   
}
