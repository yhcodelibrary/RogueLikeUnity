using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectGunFIre : EffectBase
{
    int time;
    int wait;
    int interval;
    string Damage;
    AttackState atst;
    BaseCharacter Target;
    Vector3 TargetPoint;
    Vector3 current;
    private CharacterDirection Direction;

    public override void Play()
    {
        base.Play();
    }

    private void Update()
    {
        if(time > wait)
        {
            //終了
            EffectDamage d = EffectDamage.CreateObject(Target);
            Target = null;
            d.SetText(Damage, atst);
            d.Play();
            End();
        }
        else if(time % interval == 0)
        {
            EffectGunFIreSub.CreateObject(current, TargetPoint, this.Direction).Play();
        }
        time++;
    }

    public static EffectGunFIre CreateObject(BaseCharacter target, BaseCharacter attacker, string damage, AttackState at)
    {

        //var obj = new GameObject();
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectGunFIre d = obj.AddComponent<EffectGunFIre>();
        //d.Parent = obj;
        EffectGunFIre d = GetGameObject<EffectGunFIre>(false, "GunFire", ResourceInformation.Effect.transform);

        d.Direction = attacker.Direction;
        d.Target = target;
        d.Damage = damage;
        d.atst = at;
        d.time = 0;
        d.wait = 15;
        d.interval = 1;

        //目標位置の取得
        d.TargetPoint = new Vector3(target.CurrentPoint.X * target.PositionUnit,
            target.PosY + 0.5f,
            target.CurrentPoint.Y * target.PositionUnit);

        //現在位置の取得
        d.current = new Vector3(attacker.CurrentPoint.X * attacker.PositionUnit,
            attacker.PosY + 0.5f,
            attacker.CurrentPoint.Y * attacker.PositionUnit);

        return d;
    }
    private class EffectGunFIreSub : EffectBase
    {
        private CharacterDirection Direction;
        private Vector3 TargetPoint;

        private void Update()
        {
            //移動単位の取得
            Vector3 velocity = (TargetPoint - this.transform.localPosition).normalized;

            //移動方向と移動単位が一致するか調べる
            if (CommonFunction.CheckDirectionVector(Direction, velocity) == true)
            {
                //一致していれば通常の移動操作

                //キャラクターを目標に移動
                //this.transform.localPosition += (velocity * MoveSpeed);
                this.transform.localPosition += CommonFunction.GetVelocity(velocity, CommonConst.SystemValue.MoveSpeedGunBullet);
            }
            else
            {
                //一致していなければ目標地点を追い越したとみなす
                End();
                return;
            }
        }
        public static EffectGunFIreSub CreateObject(Vector3 current, Vector3 target,CharacterDirection dir)
        {

            //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("GunFire").gameObject);
            //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
            //EffectGunFIreSub d = obj.AddComponent<EffectGunFIreSub>();
            //obj.transform.localEulerAngles = CommonFunction.EulerAngles[dir];
            EffectGunFIreSub d = GetGameObject<EffectGunFIreSub>(false, "GunFireSub", ResourceInformation.Effect.transform);
            d.Parent.transform.localEulerAngles = CommonFunction.EulerAngles[dir];

            d.Direction = dir;

            //目標位置の取得
            d.TargetPoint = target;

            //現在位置の取得
            d.Parent.transform.localPosition = current;

            //発射位置を多少ずらすS
            float loc = (float)UnityEngine.Random.Range(-30, 30) / 100;
            d.Parent.transform.localPosition += new Vector3(loc, 0, 0);

            return d;
        }
        
    }
}
