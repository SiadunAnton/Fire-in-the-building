using System.Collections;
using UnityEngine;

public class WaterZone : Zone
{
    [SerializeField] private float _damageOxygen;

    private float _lostOxygen = 0f;

    protected override void EntranceAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Enter water zone");
        StartCoroutine(SpendOxygenProcess(player, 1f));

        player.GetComponent<SwimController>().EnterWater();
        player.Speed = 30;
    }

    IEnumerator SpendOxygenProcess(Player player, float rate)
    {
        for (; ; )
        {
            player.Oxygen -= _damageOxygen;
            _lostOxygen += _damageOxygen;
            yield return new WaitForSeconds(rate);
        }
    }

    protected override void ExitAction(Player player)
    {
        StopAllCoroutines();
        Debug.Log("Exit water zone");
        StartCoroutine(RestoreOxygenAfterExit(player));

        player.GetComponent<SwimController>().ExitWater();
        player.Speed = 100;
    }

    IEnumerator RestoreOxygenAfterExit(Player player)
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1f);
            player.Oxygen += _lostOxygen / 2;
        }
        _lostOxygen = 0f;
    }
}
