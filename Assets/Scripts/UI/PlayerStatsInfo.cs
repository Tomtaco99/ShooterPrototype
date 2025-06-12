using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerStatsInfo : MonoBehaviour
{
    private PlayerStats PlayerStatsInstance;
    private PlayerGunSelector _playerGunSelector;
    [SerializeField] 
    private Slider Healthbar;
    [SerializeField]
    private TextMeshProUGUI CoinCounter;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsInstance = PlayerStats.PlayerStatsInstance;
        PlayerStatsInstance.OnTakeDamage += UpdateHealth;
        PlayerStatsInstance.OnHeal += UpdateHealth;
    }

    private void UpdateHealth(float damage)
    {
        Healthbar.value = PlayerStatsInstance.CurrentHealth / PlayerStatsInstance.MaxHealth;
    }

    private void Update()
    {
        CoinCounter.SetText($"{PlayerStatsInstance.CoinCount}");
    }
    // Update is called once per frame

}
