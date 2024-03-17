using UnityEngine;

public class SwimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Inventory _inventory;

    public void EnterWater()
    {
        _animator.SetTrigger("EnterWater");
        _inventory.HideWeapon();
    }

    public void ExitWater()
    {
        _animator.SetTrigger("ExitWater");
        _inventory.ShowWeapon();
    }
}
