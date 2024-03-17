using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider _slider;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (_slider != null)
            UpdateScore();
    }
    public abstract void Setup();

    public abstract void UpdateScore();
}
