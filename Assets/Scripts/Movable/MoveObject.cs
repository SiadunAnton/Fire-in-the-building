using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button _buttonPrefab;

    [SerializeField] private Axis _axis;
    [SerializeField] private Vector2 _pushDirection;

    [SerializeField] private CharacterMovement _movement;

    private Button _button;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //BUG - stinky solution
            if (_button != null)
                Destroy(_button.gameObject);


            _button = Instantiate(_buttonPrefab);
            _button.transform.SetParent(canvas.transform, false);

            _button.onClick.AddListener(() =>
            {
                if (!_movement.IsMovingObject)
                {
                    _movement.StartMovingObject(transform.parent, _axis, _pushDirection);
                }
                else
                {
                    _movement.StopMoving();
                }
            });

            Debug.Log("EnterInteraction");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(_button.gameObject);
            Debug.Log("ExitInteraction");
        }
    }
}

public enum Axis
{
    X,
    Y
}