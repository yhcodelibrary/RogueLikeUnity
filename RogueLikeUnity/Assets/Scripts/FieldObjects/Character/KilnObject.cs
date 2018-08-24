using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KilnObject : BaseCharacter
{
    public override void Initialize()
    {
        base.Initialize();

        Direction = CharacterDirection.Bottom;

        DisplayName = "窯";

        PosY = 2.52f;
        CurrentHp = 0;
        MaxHpDefault = 0;
        ThisDisplayObject = ResourceInformation.Kiln;
        IsVisible = true;
        IsActive = true;
        RestActionTurn = 0;
        ActionTurn = 0;
        DefaultActionTurn = 0;
        Type = ObjectType.Kiln;
    }

    public override bool Move(ManageDungeon dun, bool isNextAttack, out bool isSpecial, MapPoint plcurrent)
    {
        isSpecial = false;
        return false;
    }
    public override string GetMessageDeath(int exp = 0)
    {
        return string.Format(CommonConst.Message.DeathKiln);
    }

    protected override void SetAnimator()
    {
        if (CommonFunction.IsNull(ThisAnimator.Anim) == false)
        {
            return;
        }
        ThisAnimator.Anim = ThisDisplayObject.transform.Find("KilnBody").GetComponent<Animator>();
        
    }
}
