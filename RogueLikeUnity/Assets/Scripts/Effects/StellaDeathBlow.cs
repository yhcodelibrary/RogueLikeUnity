using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StellaDeathBlow : MonoBehaviour
{
    public GameObject GunLeft;
    public GameObject GunRight;
    public GameObject GunLeftPos;
    public GameObject GunRightPos;
    public GameObject EquipLeft;
    public GameObject EquipRight;
    public GameObject StartUp;
    public BaseCharacter Character;
    private const float PosY = 3f;

    public void OnCallStart()
    {
        if(CommonFunction.IsNull(EquipLeft) == false)
        {
            EquipLeft.SetActive(false);
        }
        if (CommonFunction.IsNull(EquipRight) == false)
        {
            EquipRight.SetActive(false);
        }
        //Character = StartUp.GetComponent<ManageGame>()._player;
    }

    public void OnCallGunEquip()
    {
        GunLeft.SetActive(true);
        GunRight.SetActive(true);

    }

    private enum GunFireActionNumber
    {
        Top = 1,
        TopLeft = 2,
        TopRight = 4,
        Left = 8,
        Right = 16,
        BottomLeft = 32,
        BottomRight = 64,
        Bottom = 128,
        LeftGun = 256,
        RightGun = 512,
        BothGun = 1024
    }

    private static Array _GunFireActionNumbers;
    public static Array GunFireActionNumbers
    {
        get
        {
            if (CommonFunction.IsNull(_GunFireActionNumbers) == false)
            {
                return _GunFireActionNumbers;
            }
            _GunFireActionNumbers = Enum.GetValues(typeof(GunFireActionNumber));
            return _GunFireActionNumbers;
        }
    }

    public void OnCallGunFire(int idir)
    {

        //銃弾エフェクト

        //現在位置の取得
        Vector3 CurrentPoint = new Vector3(Character.CurrentPoint.X * Character.PositionUnit,
            PosY,
            Character.CurrentPoint.Y * Character.PositionUnit);

        foreach (GunFireActionNumber val in GunFireActionNumbers)
        {
            if(val == GunFireActionNumber.BothGun || val == GunFireActionNumber.LeftGun || val == GunFireActionNumber.RightGun)
            {
                continue;
            }
            if(((int)val & idir ) != 0)
            {
                CharacterDirection dir = CharacterDirection.Top;
                //方向の取得
                switch (val)
                {
                    case GunFireActionNumber.Top:
                        dir = Character.Direction;
                        break;
                    case GunFireActionNumber.TopLeft:
                        dir = CommonFunction.RelativeFrontLeft[Character.Direction];
                        break;
                    case GunFireActionNumber.TopRight:
                        dir = CommonFunction.RelativeFrontRight[Character.Direction];
                        break;
                    case GunFireActionNumber.Left:
                        dir = CommonFunction.RelativeLeft[Character.Direction];
                        break;
                    case GunFireActionNumber.Right:
                        dir = CommonFunction.RelativeRight[Character.Direction];
                        break;
                    case GunFireActionNumber.BottomLeft:
                        dir = CommonFunction.ReverseLeftDirectionVector[Character.Direction];
                        break;
                    case GunFireActionNumber.BottomRight:
                        dir = CommonFunction.ReverseRightDirectionVector[Character.Direction];
                        break;
                    case GunFireActionNumber.Bottom:
                        dir = CommonFunction.ReverseDirection[Character.Direction];
                        break;
                }
                //MapPoint mp = Character.DeathBlowTargetPoint[dir].Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseDirection[dir]]);
                MapPoint mp = Character.DeathBlowTargetPoint[dir];
                //目標位置の取得
                Vector3 TargetPoint = new Vector3(mp.X * Character.PositionUnit + ((float)CommonFunction.CharacterDirectionVector[CommonFunction.ReverseDirection[dir]].X * 0.5f),
                    PosY,
                    mp.Y * Character.PositionUnit + ((float)CommonFunction.CharacterDirectionVector[CommonFunction.ReverseDirection[dir]].Y * 0.5f));

                //銃弾エフェクト
                EffectGunBulletSingle.CreateObject(CurrentPoint, TargetPoint,Character.DeathBlowTargetCharacter[dir], dir).Play();

            }
        }
        
        //マズルエフェクト
        if (((int)GunFireActionNumber.LeftGun & idir) != 0)
        {
            EffectMuzzleFlash.CreateObject(GunLeftPos).Play();
        }
        else if (((int)GunFireActionNumber.RightGun & idir) != 0)
        {
            EffectMuzzleFlash.CreateObject(GunRightPos).Play();
        }
        else if (((int)GunFireActionNumber.BothGun & idir) != 0)
        {
            EffectMuzzleFlash.CreateObject(GunRightPos).Play();
            EffectMuzzleFlash.CreateObject(GunLeftPos).Play();
        }

        //音
        SoundInformation.Sound.Play(SoundInformation.SoundType.Gun);
    }

    public void OnCallEnd()
    {
        Character.DeathBlowInformation.AttackUpdate(Character, null);
        Character.DeathBlowInformation.Clear();
        //Character.DeathBlowInformation = null;
        Character.DeathBlowTargetCharacter.Clear();
        //Character.DeathBlowTargetCharacter = null;
        Character.DeathBlowTargetPoint.Clear();
        //Character.DeathBlowTargetPoint = null;

        Character = null;
        GunLeft.SetActive(false);
        GunRight.SetActive(false);
        EquipLeft.SetActive(true);
        EquipRight.SetActive(true);
    }
}
