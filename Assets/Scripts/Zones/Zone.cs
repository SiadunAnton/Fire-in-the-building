using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Zone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player;
            bool isComponentExist = other.gameObject.TryGetComponent(out player);
            if(isComponentExist)
                EntranceAction(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player;
            bool isComponentExist = other.gameObject.TryGetComponent(out player);
            if (isComponentExist)
                ExitAction(player);
        }
    }

    protected virtual void EntranceAction(Player player)
    {
    }

    protected virtual void ExitAction(Player player)
    {
    }
}
