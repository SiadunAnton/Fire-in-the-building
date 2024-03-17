using UnityEngine;

public class BreakableStuff : MonoBehaviour
{
    [SerializeField] private CharacterMovement _character;
    [SerializeField] private float _hp = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EquipedItem") && _character.Slashing)
        {
            Debug.Log("Box get hit");
            _hp -= 3;
            if (_hp <= 0f)
                Destroy(gameObject);
        }
    }
}
