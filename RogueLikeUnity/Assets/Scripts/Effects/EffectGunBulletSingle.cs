using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectGunBulletSingle : EffectBase
{
    
    private CharacterDirection Direction;
    private Vector3 TargetPoint;
    private BaseObject Obj;

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
            if (CommonFunction.IsNull(Obj) == false
                && CommonFunction.IsNull(Obj.ThisTransform) == false)
            {
                EffetHitShockWave.CreateObject(Obj).Play();
                Obj = null;
            }

            End();
            return;
        }
    }

    public static EffectGunBulletSingle CreateObject(Vector3 current, Vector3 target,BaseObject targetobj, CharacterDirection dir)
    {
        
        EffectGunBulletSingle d = GetGameObject<EffectGunBulletSingle>(false, "GunFireSingle", ResourceInformation.Effect.transform);
        d.Parent.transform.localEulerAngles = CommonFunction.EulerAngles[dir];

        d.Obj = targetobj;

        d.Direction = dir;

        //目標位置の取得
        d.TargetPoint = target;

        //現在位置の取得
        d.Parent.transform.localPosition = current;

        //発射位置を多少ずらすS
        float loc = (float)UnityEngine.Random.Range(-10, 10) / 100;
        d.Parent.transform.localPosition += new Vector3(loc, 0, 0);

        return d;
    }
}