using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //�� ������� � ������, ������ ��� ��� �����, ��� �������, ���� ����� ����� ��������
    //������������� ���������� ������� �� ������� ��������� ���� ��� ��������� 
    //����� �������� ���������� � ����� I + �������������� (Movable "��������")

    //[SerializeField] private Joystick _moveJoystick; ����� ������������ ����� ���������� ��������
    [SerializeField] private Joystick _joystick;
    //[SerializeField] private Rigidbody2D _rb; � �������� �� ������������� ������������ ���������� ��� �������� ����������
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheckPosition;
    //������ ���� � �����, ������ ��� ��������� ���������� �� ����� ��������� ScriptableObject � ����������� � ��, ��� ������� � ������� + �������� ������ � ������ �����
    //[SerializeField] private float _jump; ������� �������� �� �������� ���� ����������
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDistanceTrigger;
    [SerializeField] private float _speed;

    //��������� ���������� �� ScriptableObject`�, ������ ��� �� �����
    //public float Jump => _jumpPower;
    //public LayerMask GroundLayer => _groundLayer;
    //public float GroundDistanceTrigger => _groundDistanceTrigger;

    private void FixedUpdate()
    {
        Move();

        if ((_joystick.Direction.y > 0.5f || Input.GetKeyDown(KeyCode.Space)) && IsGround())
            Jump();
    }

    //������� ����������� ������� ������ Move,
    //*������� ����� ��-��� �� ������ ����� ����������� ��������� ����������� ������������� ������, ��� �������� ������� ����������
    private void Move()
    {
        Vector2 movement = new Vector2(_joystick.Direction.x * _speed, _rigidbody.velocity.y);

        if (_rigidbody.velocity.x < _speed)
            _rigidbody.AddForce(movement);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircleAll(_groundCheckPosition.position,
            _groundDistanceTrigger, _groundLayer).Length > 0;
    }
}