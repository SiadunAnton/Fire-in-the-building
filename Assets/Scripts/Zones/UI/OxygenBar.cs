using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBar : Bar
{
    [SerializeField] private Player _player;

    public override void Setup()
    {
        _slider.maxValue = _player.Oxygen;
    }

    public override void UpdateScore()
    {
        _slider.value = _player.Oxygen;
    }
}
