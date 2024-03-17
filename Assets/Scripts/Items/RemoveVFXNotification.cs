using UnityEngine;

public class RemoveVFXNotification : MonoBehaviour
{
    [SerializeField] private GameObject _vfx;

    public void Remove()
    {
        Destroy(_vfx);
    }
}
