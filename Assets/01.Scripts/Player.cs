﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CharacterModel;

    void Start()
    {
        InitState();
        ChangeState(eState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseLock();
        UpdateInput();
        UpdateRotate();
        UpdateState();
     
    }

    // State 
    public enum eState
    {
        IDLE,
        MOVE,
        ATTACK,
    }

    Dictionary<eState, State> _stateDic = new Dictionary<eState, State>();
   
    State _currentState;

    void InitState()
    {
        State idleState = new IdleState();
        State moveState = new MoveState();
        State attackState = new AttackState();

        idleState.Init(this);
        moveState.Init(this);
        attackState.Init(this);

        _stateDic.Add(eState.IDLE, idleState);
        _stateDic.Add(eState.MOVE, moveState);
        _stateDic.Add(eState.ATTACK, attackState);
    }

    void UpdateState()
    {
        _currentState.Update();
    }
    
    public void StartIdleState()
    {
        CharacterModel.GetComponent<Animator>().SetTrigger("idle");

    }
    public void ChangeState(eState nextState)
    {
        _currentState =_stateDic[nextState];
        _currentState.Start();
  
    }


    //Input
    bool _mouseLock = true;

    void CheckMouseLock()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _mouseLock = !_mouseLock;
        }

        if(_mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public enum eInputDirection
    {
        NONE, FRONT, BACK, LEFT, RIGHT
    }
    eInputDirection _inputVerticalDirection = eInputDirection.NONE;
    eInputDirection _inputHorizontalDirection = eInputDirection.NONE;
    eInputDirection _inputAniDirection = eInputDirection.NONE;

    void UpdateInput()
    {
        _inputVerticalDirection = eInputDirection.NONE;
        _inputHorizontalDirection = eInputDirection.NONE;
        _inputAniDirection = eInputDirection.NONE;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _inputVerticalDirection = eInputDirection.NONE;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _inputVerticalDirection = eInputDirection.FRONT;
            _inputAniDirection = eInputDirection.FRONT;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            _inputVerticalDirection = eInputDirection.BACK;
            _inputAniDirection = eInputDirection.BACK;
        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _inputHorizontalDirection = eInputDirection.NONE;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _inputHorizontalDirection = eInputDirection.LEFT;
            _inputAniDirection = eInputDirection.LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _inputHorizontalDirection = eInputDirection.RIGHT;
            _inputAniDirection = eInputDirection.RIGHT;
        }       
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(eState.ATTACK);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeState(eState.IDLE);
        }

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    public string bulletName = "Bullet";
    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);
        bullet.transform.position = transform.position + transform.up;
        bullet.SetActive(true);
    }

    public eInputDirection GetInputVerticalDirection()
    {
        return _inputVerticalDirection;
    }

    public eInputDirection GetInputHorizontalDirection()
    {
        return _inputHorizontalDirection;
    }

    public eInputDirection GetAniDirection()
    {
        return _inputAniDirection;
    }
    // Rotate
    float _rotationY = 0.0f;
    void UpdateRotate()
    {
        if (false == _mouseLock)
        {
            return;
        }
        // 마우스를 이용해 수평 회전을 시칸다.
        float rotateSpeed = 360.0f;
        float addRotationY = Input.GetAxis("Mouse X") * rotateSpeed;
        _rotationY += (addRotationY * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0.0f, _rotationY, 0.0f);
    }
    // Move

    public void UpdateMove()
    {
        Vector3 addPosition = Vector3.zero;
        
        switch (_inputVerticalDirection)
        {
            case eInputDirection.FRONT:
                addPosition.z = MoveOffset(5.0f);
                break;
            case eInputDirection.BACK:
                addPosition.z = MoveOffset(-3.0f);
                break;
        }
        switch (_inputHorizontalDirection)
        {
            case eInputDirection.LEFT:
                addPosition.x = MoveOffset(-3.0f);
                break;
            case eInputDirection.RIGHT:
                addPosition.x = MoveOffset(3.0f);
                break;
        }

        transform.position += (transform.rotation *addPosition);

    }

    float MoveOffset(float moveSpeed)
    {
        return (moveSpeed * Time.deltaTime);

    }

    // Attack
    float _shotSpeed = 0.5f;
    public float GetShotSpeed()
    {
        return _shotSpeed;
    }

    // Animation
    public AnimationModule GetAnimationModule()
    {
        return CharacterModel.GetComponent<AnimationModule>();
    }
}
