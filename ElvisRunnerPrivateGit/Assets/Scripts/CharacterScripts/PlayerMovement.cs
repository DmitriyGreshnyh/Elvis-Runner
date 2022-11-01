using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _lineOffset;
    private int _lineCurrent = 1;
    private Vector3 _targetPosition;
    private float[] _linePositions;
    [SerializeField] private float _lineChangingSpeed;

    [SerializeField] float _jumpForce = 5;
    [SerializeField] float _gravityForce = -9.8f;
    

    private bool _isChangingLine = false;
    private bool _isJump = false;
    [SerializeField] AnimationCurve _animationCurve;  

    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _linePositions = new float[] { transform.position.x - _lineOffset, transform.position.x, transform.position.x +_lineOffset };
        _targetPosition = SetPosition(_linePositions[_lineCurrent], "x");
    }

    private void Update()
    {
        ChangeLineNumber();
       // Jump();
       // print(_targetPosition);
    }

    private void FixedUpdate()
    {
        ChangePlayerPosition();
        //if (_characterController.isGrounded)
        //{
        //    _isJump = false;
        //    _targetPosition.y = 0;
        //}
        //if (!_characterController.isGrounded) _targetPosition.y += _gravityForce;
       
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
        _targetPosition = SetPosition(_linePositions[_lineCurrent], "x");
    }

    public void ChangePlayerPosition()
    {
        Vector3 direction = _targetPosition - transform.position;
        

        _characterController.Move(direction*Time.deltaTime * _lineChangingSpeed);
        
        if (Mathf.Abs(direction.x) < 0.5f) _isChangingLine = false;
    }

    private void Jump()
    {
        float dirY = Input.GetAxisRaw("Vertical");

        if(dirY == 0) return;

        if (dirY > 0 && _isJump == false)
        {
            _targetPosition = SetPosition(_jumpForce, "y");
            _isJump = true;
        }
    }


    /// <summary>
    /// Set new position in transform.position
    /// </summary>
    /// <param name="value">new position </param>
    /// <param name="pos">"x","y","z"</param>
    /// <returns></returns>
    Vector3 SetPosition(float value, string pos)
    {
        Vector3 newPos = transform.position;

        switch (pos)
        {
            case "x":
                newPos.x = value;
                break;
            case "y":
                newPos.y = value;
                break;
            case "z":
                newPos.z = value;
                break;
            default:
                print("Wrong string in SetPosition");
                break;
        }

        return newPos;
    }
}
