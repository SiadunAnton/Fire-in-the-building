using System.Collections;
using UnityEngine;

public class ElectricZone : Zone
{
    [SerializeField] private float _damageHealth = 7;

    protected override void EntranceAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Enter electric zone");
        StartCoroutine(DealDamage(player, 1f));
    }

    IEnumerator DealDamage(Player player, float rate)
    {
        for (; ; )
        {
            player.Health -= _damageHealth;
            yield return new WaitForSeconds(rate);
        }
    }

    protected override void ExitAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Exit electric zone");
    }
}
