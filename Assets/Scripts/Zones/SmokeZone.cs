using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeZone : Zone
{
    [SerializeField] private float _damageOxygen = 5;
    [SerializeField] private float _damageHealth = 2;

    protected override void EntranceAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Enter smoke zone");
        StartCoroutine(SpendOxygenProcess(player, 0.5f));
        player.Speed = 50;
    }

    IEnumerator SpendOxygenProcess(Player player, float rate)
    {
        for (; ; )
        {
            player.Oxygen -= _damageOxygen;
            player.Health -= _damageHealth;
            yield return new WaitForSeconds(rate);
        }
    }

    protected override void ExitAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Exit smoke zone");
        StartCoroutine(DealAdditionalDamage(player));

        player.Speed = 100;
    }

    IEnumerator DealAdditionalDamage(Player player)
    {
        yield return new WaitForSeconds(1f);
        player.Oxygen -= 10f;
    }
}
