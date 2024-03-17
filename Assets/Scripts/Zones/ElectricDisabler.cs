using UnityEngine;

public class ElectricDisabler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private ElectricZone _zoneComponent;

    private void OnDestroy()
    {
        _ps.Stop();
        _zoneComponent.gameObject.SetActive(false);
    }
}
