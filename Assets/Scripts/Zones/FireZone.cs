using System.Collections;
using UnityEngine;

public class FireZone : Zone
{
    [SerializeField] private float _damageMultipier = 1.2f;
    [SerializeField] private int _startDamage = 5;

    [Range(0,100)]
    [SerializeField] private int _percentage = 10;

    private float _currentDamage;
    private float _averageDamage;

    protected override void EntranceAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Enter fire zone");
        _currentDamage = _startDamage;
        _averageDamage = 0;
        StartCoroutine(DealDamageProcess(player, 1f));
    }

    IEnumerator DealDamageProcess(Player player,float rate)
    {
        for(; ; )
        {
            player.Health -= _currentDamage;
            _averageDamage += _currentDamage;
            _currentDamage *= _damageMultipier;
            yield return new WaitForSeconds(rate);
        }
    }

    protected override void ExitAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Exit fire zone");
        StartCoroutine(DealDamageAfterExit(player));
    }

    IEnumerator DealDamageAfterExit(Player player)
    {
        for(int i = 0; i < 3; i++)
        {
            player.Health -= _averageDamage * _percentage / 100;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
