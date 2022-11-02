using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _lineOffset;
    private int _lineCurrent = 1;
    private Vector3 _targetPosition;
    private float[] _linePositions;
    [SerializeField] private float _lineChangingSpeed;


    [SerializeField] float _gravityForce = -9.8f;


    private bool _isChangingLine = false;
    private bool _isJump = false;

    private float _jumpCurrentTime;
    [SerializeField] private float _jumpMaxTime;
    [SerializeField] private float _jumpHeight;
    private Vector3 _jumpStartPossition;
    [SerializeField] private AnimationCurve _jumpCurve;

    private Coroutine _jumpCoroutine;

    private Animator _animator;

    private bool _isSliding = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _linePositions = new float[] { transform.position.x - _lineOffset, transform.position.x, transform.position.x + _lineOffset };
        _targetPosition = SetHandles.SetVector3x(transform.position, _linePositions[_lineCurrent]);

        CharacterObstacleReaction.OnObstacleHit += OnObstacleHit;

    }
    
    private void OnObstacleHit(string sideName)
    {
        switch (sideName) {
            case "front":
                print(1);
                break;
            case "right":
                _lineCurrent--;
                print(2);
                break;
            case "left":
                _lineCurrent++;
                print(3);
                break;
            default:
                print(4);
                break;

                }

        _targetPosition = SetHandles.SetVector3x(transform.position, _linePositions[_lineCurrent]);
        _isChangingLine = true;
    }

    private void Update()
    {
        ChangeLineNumber();
        JumpSlide();
    }

    private void FixedUpdate()
    {
         ChangePlayerPosition();
     
        if (!_characterController.isGrounded && !_isJump) _targetPosition.y += _gravityForce*Time.deltaTime;

    }

    public int LineCurrent {
        
        get { return _lineCurrent; }
        set {
            _lineCurrent = value;
            _targetPosition = SetHandles.SetVector3x(transform.position, _linePositions[_lineCurrent]);

        }
    }

    private void ChangeLineNumber()
    {
        float dir = Input.GetAxisRaw("Horizontal");


        if (dir == 0f || _isChangingLine == true) return;
        if (dir > 0 && _lineCurrent != 2)
        {
            _lineCurrent++;

        }
        if (dir < 0 && _lineCurrent != 0)
        {
            _lineCurrent--;
        }

       
        _isChangingLine = true;

        _targetPosition = SetHandles.SetVector3x(transform.position, _linePositions[_lineCurrent]);
    }

    private void ChangePlayerPosition()
    {
        Vector3 direction = _targetPosition - transform.position;


        _characterController.Move(direction * Time.deltaTime * _lineChangingSpeed);

        if (Mathf.Abs(direction.x) < 0.1f) _isChangingLine = false;
    }

    IEnumerator ChangePlayerPositionCoroutine()
    {
        Vector3 direction = _targetPosition - transform.position;
        while (Mathf.Abs(direction.x) < 0.5f)
        {
            direction = _targetPosition - transform.position;
            _characterController.Move(direction * Time.deltaTime * _lineChangingSpeed);
        }
        yield return null;
    }


    private void JumpSlide()
    {
        float dirY = Input.GetAxisRaw("Vertical");

        if (dirY == 0) return;

        if (dirY > 0 && _isJump == false)
        {
            _isJump = true;
            _jumpCoroutine =StartCoroutine(Jump());
        }
        if (dirY < 0 && _isJump == true)
        {
            print("down");
            _isJump = false;
        
            StopCoroutine(_jumpCoroutine);
        }
        if (dirY < 0 && _isSliding == false)
        {
            StartCoroutine(Sliding());
            _isSliding = true;
        }



    }

    IEnumerator Sliding()
    {
        _animator.SetTrigger("Right");
        yield return new WaitForSeconds(1);
        _isSliding = false;

    }
    IEnumerator Jump()
    {
        _jumpCurrentTime = 0;
        Vector3 direction = Vector3.zero;
        _jumpStartPossition = transform.position;
        while (_jumpCurrentTime <= _jumpMaxTime)
        {

            float normalizedTime = _jumpCurrentTime / _jumpMaxTime;

            direction.y = _jumpStartPossition.y + _jumpCurve.Evaluate(normalizedTime) * _jumpHeight - transform.position.y;


            _characterController.Move(direction);


            _jumpCurrentTime += Time.deltaTime;
            print("jump");
            yield return new WaitForFixedUpdate();

        }

        _isJump = false;
        yield return null;

    }




    
}
