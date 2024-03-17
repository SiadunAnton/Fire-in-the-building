using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool IsWeaponInHand => _weaponInHand != null;

    [SerializeField] private Transform _hand;
    [SerializeField] private Animator _animator;

    [Header("UI")]
    [SerializeField] private Button _accept;

    [SerializeField] private GameObject _pickingObject;
    private GameObject _weaponInHand;

    private void Start()
    {
        _accept.onClick.AddListener(() =>
        {
            PickUpAndHoldItem(_pickingObject);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Find Item");
            _pickingObject = other.gameObject;
            _accept.gameObject.SetActive(true);
        }
    }

    private void PickUpAndHoldItem(GameObject item)
    {
        item.tag = "EquipedItem";

        item.gameObject.SetActive(false);

        item.GetComponent<RemoveVFXNotification>()?.Remove();

        item.transform.SetParent(_hand);

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;

        item.gameObject.SetActive(true);

        _weaponInHand = item;
        _pickingObject = null;
        _accept.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Lost Item");
            _pickingObject = null;
            _accept.gameObject.SetActive(false);
        }
    }

    public void HideWeapon()
    {
        if (_weaponInHand != null)
            _weaponInHand.SetActive(false);
    }

    public void ShowWeapon()
    {
        if (_weaponInHand != null)
            _weaponInHand.SetActive(true);
    }
}
