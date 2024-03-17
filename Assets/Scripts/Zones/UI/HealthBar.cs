using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;

    public override void Setup()
    {
        _slider.maxValue = _player.Health;
    }

    public override void UpdateScore()
    {
        _slider.value = _player.Health;
    }
}
