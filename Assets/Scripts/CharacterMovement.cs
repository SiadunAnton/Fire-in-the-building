using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private CharacterController _character;
    [SerializeField] private float _step = 0.05f;

    [SerializeField] private Animator _animator;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _movable;

    public bool Slashing = false;
    public bool LockXAxis = false;
    public bool LockYAxis = false;
    public bool LockRotation = false;
    public Vector2 PushDirection;
    public bool IsIntoxicated = false;

    private float _distance;
    public bool IsMovingObject = false;

    private void Update()
    {
        if (_character == null)
            return;

        if (Input.GetKey(KeyCode.Space) && _inventory.IsWeaponInHand && !Slashing)
        {
            StartCoroutine(SlashProcess());
        }

        if (Slashing)
            return;

        var lengthOfMovement = _step * _player.Speed / 100;

        if(_movable != null && _distance != 0 && Mathf.Abs(_distance - Magnitude()) >= 0.01f)
        {
            Debug.LogWarning("Abort moving: distance betweeb movable object and player has changed.");
            StopMoving();
        }


        if (Input.GetKey(KeyCode.LeftArrow) && !LockXAxis)
        {
            //transform.position += new Vector3(-lengthOfMovement,0f,0f);
            //_rb.velocity = Vector3.left * _player.Speed;
            //_rb.AddForce(Vector3.left * _player.Speed);
            _character.Move(new Vector3(-lengthOfMovement, 0f,0f));
            if (!LockRotation)
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _animator.SetBool("IsMoving", true);

            if (PushDirection == Vector2.left)
                _animator.SetBool("IsPushing",true);
            else if(PushDirection == Vector2.right)
                _animator.SetBool("IsPushing", false);

        }
        else if (Input.GetKey(KeyCode.RightArrow) && !LockXAxis)
        {
            //_rb.velocity = Vector3.right * _player.Speed;
            //_rb.AddForce(Vector3.right*_player.Speed);
            _character.Move(new Vector3(lengthOfMovement, 0f,0f));
            if (!LockRotation)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _animator.SetBool("IsMoving", true);

            if (PushDirection == Vector2.right)
                _animator.SetBool("IsPushing", true);
            else if (PushDirection == Vector2.left)
                _animator.SetBool("IsPushing", false);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !LockYAxis)
        {
            //_rb.AddForce(Vector3.forward * _player.Speed);
            _character.Move(new Vector3(0f,0f, lengthOfMovement));
            if (!LockRotation)
                transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            _animator.SetBool("IsMoving", true);

            if (PushDirection == Vector2.up)
                _animator.SetBool("IsPushing", true);
            else if (PushDirection == Vector2.down)
                _animator.SetBool("IsPushing", false);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !LockYAxis)
        {
            //_rb.AddForce(Vector3.back * _player.Speed);
            _character.Move(new Vector3(0f, 0f, -lengthOfMovement));
            if (!LockRotation)
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            _animator.SetBool("IsMoving", true);

            if (PushDirection == Vector2.down)
                _animator.SetBool("IsPushing", true);
            else if (PushDirection == Vector2.up)
                _animator.SetBool("IsPushing", false);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }    
    }

    IEnumerator SlashProcess()
    {
        Slashing = true;

        _animator.SetTrigger("Slash");
        yield return new WaitForSeconds(1.8f);

        Slashing = false;
    }

    public void StartMovingObject(Transform movable, Axis axis, Vector2 pushDirection)
    {

        transform.rotation = Quaternion.LookRotation(new Vector3(-pushDirection.y, 0f, pushDirection.x), Vector3.up); 

        IsMovingObject = true;

        _inventory.HideWeapon();

        _movable = movable;
        _animator.SetBool("MoveObject", true);
        movable.SetParent(transform);

        PushDirection = pushDirection;

        _player.Speed = 10;
        LockRotation = true;
        if (axis == Axis.Y)
            LockXAxis = true;
        else
            LockYAxis = true;
        Debug.Log("StartMoving");

        _distance = Magnitude();

    }

    public void StopMoving()
    {
        IsMovingObject = false;

        _inventory.ShowWeapon();

        _movable.SetParent(null);
        _movable = null;
        _animator.SetBool("MoveObject", false);
        _animator.SetBool("IsPushing", true);

        _player.Speed = 100;
        LockRotation = LockXAxis = LockYAxis = false;

        Debug.Log("StopMoving");

        _distance = 0f;
    }

    private float Magnitude() => Vector3.Magnitude(transform.position - _movable.position);
}
