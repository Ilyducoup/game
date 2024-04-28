using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    private float _speed;

    [Header("Movement variables")]
    private Vector3 _axisMovement;
    private float _moveSpeed = 7f;

    [Header("Dashing")]
    private float _dashSpeed = 40f;
    private bool _isDashing = false;
    private bool _canDash = true;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _axisMovement.x = Input.GetAxisRaw("Horizontal");
        _axisMovement.y = Input.GetAxisRaw("Vertical");
        var dashInput = Input.GetKeyDown(KeyCode.Space);

        if (dashInput && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            StartCoroutine(StopDashing());
        }
    }

    private void FixedUpdate()
    {
        _speed = _moveSpeed;
        if (_isDashing)
            _speed = _dashSpeed;
        Move();
    }

    private void Move()
    {
        _body.velocity = _axisMovement.normalized * _speed;
        CheckForFlipping();
    }

    private void CheckForFlipping()
    {
        if(_axisMovement.x < 0)
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        else if(_axisMovement.x > 0)
            transform.localScale = new Vector3(1f, transform.localScale.y);
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(.15f);
        _canDash = true;
        _isDashing = false;
    }
}
