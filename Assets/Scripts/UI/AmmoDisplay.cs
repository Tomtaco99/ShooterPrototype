using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerGunSelector _gunSelector;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.SetText($"{_gunSelector.currentGun.reloadConfiguration.currentMagAmmo} / " + $"{_gunSelector.currentGun.reloadConfiguration.currentAmmo}");
    }
}
