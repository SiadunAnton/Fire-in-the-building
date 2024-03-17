using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasZone : Zone
{
    [SerializeField] private float _damageOxygen = 5;


    protected override void EntranceAction(Player player)
    {
        Debug.Log("Enter gas zone");
        if (!player.IsIntoxicated)
        {
            player.IsIntoxicated = true;
            StopAllCoroutines();
            StartCoroutine(Aftereffect(player));
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player;
            bool isComponentExist = other.gameObject.TryGetComponent(out player);
            if (isComponentExist)
            {
                player.IsIntoxicated = true;
            }
        }
    }

    protected override void ExitAction(Player player)
    {
        player.IsIntoxicated = false;
        StopAllCoroutines();
        StartCoroutine(Aftereffect(player));
        Debug.Log("Exit gas zone");
    }

    IEnumerator Aftereffect(Player player)
    {
        
        for(int i = 0; i < 3; i++)
        {
            player.Oxygen -= _damageOxygen;
            yield return new WaitForSeconds(1f);
        }
        if(player.IsIntoxicated == true)
        {
            yield return Aftereffect(player);
        }
    }
}
