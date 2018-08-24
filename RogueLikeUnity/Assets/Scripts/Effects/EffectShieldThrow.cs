using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class EffectShieldThrow : EffectBase
{
    private CharacterDirection Direction;
    private Vector3 TargetPoint;
    private Vector3 ReturnPoint;
    private EffectDamage eDamage;
    //private string Damage;
    //private AttackState State;
    private bool isDirect;
    private sbyte wait;
    
    private void Update()
    {
        if(wait > 0)
        {
            wait--;
            return;
        }
        //移動単位の取得
        Vector3 velocity = (TargetPoint - this.transform.localPosition).normalized;

        //移動方向と移動単位が一致するか調べる
        if (CommonFunction.CheckDirectionVector(Direction, velocity) == true)
        {
            //一致していれば通常の移動操作

            //キャラクターを目標に移動
            //this.transform.localPosition += (velocity * MoveSpeed);
            this.transform.localPosition += CommonFunction.GetVelocity(velocity, CommonConst.SystemValue.MoveSpeedItemThrow);
        }
        else
        {
            //一致していなければ目標地点を追い越したとみなす
            //行きなら
            if(isDirect == true)
            {
                SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

                //ダメージエフェクト
                eDamage.Play();


                //方向を逆転
                isDirect = false;
                Direction = CommonFunction.ReverseDirection[Direction];

                TargetPoint = ReturnPoint;
            }
            else
            {
                //帰りならそのまま終了
                End();
            }
            //終了


            return;
        }
    }

    public static EffectShieldThrow CreateObject(BaseCharacter target,BaseCharacter attacker,string damage,AttackState at)
    {

        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Stone").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectStoneThrow d = obj.AddComponent<EffectStoneThrow>();
        //d.Parent = obj;
        EffectShieldThrow d = GetGameObject<EffectShieldThrow>(false, "EffectShield", ResourceInformation.Effect.transform);
        
        d.Direction = attacker.Direction;
        d.isDirect = true;
        d.wait = 5;

        d.eDamage = EffectDamage.CreateObject(target);
        d.eDamage.SetText(damage, at);

        //目標位置の取得
        d.TargetPoint = new Vector3(target.CurrentPoint.X * target.PositionUnit,
            attacker.PosY,
            target.CurrentPoint.Y * target.PositionUnit);

        //現在位置の取得
        Vector3 v = new Vector3(attacker.CurrentPoint.X * attacker.PositionUnit,
            attacker.PosY,
            attacker.CurrentPoint.Y * attacker.PositionUnit);

        //戻り位置の取得
        d.ReturnPoint = new Vector3(attacker.CurrentPoint.X * attacker.PositionUnit,
            attacker.PosY,
            attacker.CurrentPoint.Y * attacker.PositionUnit);


        d.Parent.transform.localPosition = v;

        return d;
    }
    
}
