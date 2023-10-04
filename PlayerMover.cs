using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //Ќе копаюсь в логике, потому что это долго, это сделаем, если будет плохо работать
    //»спользование интерфейса излишне по причине описанной ниже под звЄздочкой 
    //Ћучше называть интерфейсы в стиле I + прилагательное (Movable "движимый")

    //[SerializeField] private Joystick _moveJoystick; ћожно использовать более локаничное название
    [SerializeField] private Joystick _joystick;
    //[SerializeField] private Rigidbody2D _rb; ¬ компании не рекомендуетс€ использовать сокращени€ дл€ названий переменных
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheckPosition;
    //ѕеренЄс пол€ в класс, теперь дл€ настройки компонента не нужно создавать ScriptableObject и настраивать в нЄм, это быстрее и удобнее + получить доступ к данным проще
    //[SerializeField] private float _jump; ѕрошлое название не отражает суть переменной
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDistanceTrigger;
    [SerializeField] private float _speed;

    //Ѕлагодар€ избавлению от ScriptableObject`а, теперь это не нужно
    //public float Jump => _jumpPower;
    //public LayerMask GroundLayer => _groundLayer;
    //public float GroundDistanceTrigger => _groundDistanceTrigger;

    private void FixedUpdate()
    {
        Move();

        if ((_joystick.Direction.y > 0.5f || Input.GetKeyDown(KeyCode.Space)) && IsGround())
            Jump();
    }

    //»зменил модификатор доступа метода Move,
    //*никакой класс из-вне не должен иметь возможность управл€ть внутренними возможност€ми класса, это нарушает принцип сокрытости
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