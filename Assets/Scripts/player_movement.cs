using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;

    [Header("Movement variables")]
    private Vector3 _axisMovement;
    [SerializeField] private float _speed = 5f;

    
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _axisMovement.x = Input.GetAxisRaw("Horizontal");
        _axisMovement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
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
}