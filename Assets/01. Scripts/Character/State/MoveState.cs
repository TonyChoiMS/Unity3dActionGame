using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    Character.eInputDirection _prevAniDirection;

    override public void Start()
    {
        _prevAniDirection = Character.eInputDirection.NONE;
        UpdateAnimation();
    }

    override public void Update()
    {
        if(Character.eInputDirection.NONE == _character.GetInputVerticalDirection() &&
            Character.eInputDirection.NONE == _character.GetInputHorizontalDirection())
        {
            _character.ChangeState(Character.eState.IDLE);
            return;
        }
        UpdateAnimation();
        _character.UpdateMove();
    }

    void UpdateAnimation()
    {
        if (_prevAniDirection == _character.GetAniDirection())
            return;

        _prevAniDirection = _character.GetAniDirection();
        switch (_character.GetAniDirection())
        {
            case Character.eInputDirection.FRONT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("movefront");
                break;
            case Character.eInputDirection.BACK:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveback");
                break;
            case Character.eInputDirection.RIGHT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveright");
                break;
            case Character.eInputDirection.LEFT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveleft");
                break;
        }
    }
}
