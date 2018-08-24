using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShieldBase : BaseItem
{
    public override string DisplayNameNormal
    {
        get
        {
            if (StrengthValue > 0)
            {
                return string.Format(CommonConst.Format.StrengthItemName, base.DisplayNameNormal, StrengthValue);
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
        }
    }

    public override string DisplayNameInItem
    {
        get
        {
            if(IsAnalyse == true)
            {
                if (StrengthValue > 0)
                {
                    return string.Format(CommonConst.Format.StrengthItemName, base.DisplayNameNormal, StrengthValue);
                }
                else
                {
                    return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
                }
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultNameWithColor, DisplayNameBefore, DisplayName, DisplayNameAfter,
                    CommonConst.Color.NotAnalyse);
            }
        }
    }

    public override string DisplayNameInMessage
    {
        get
        {
            if (IsAnalyse == true)
            {
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultNameWithColor, DisplayNameBefore, DisplayName, DisplayNameAfter,
                    CommonConst.Color.NotAnalyse);
            }
        }
    }

    /// <summary>
    /// 基本防御
    /// </summary>
    public float ShieldBaseDefense;

    /// <summary>
    /// オブジェクト実体
    /// </summary>
    public GameObject ShieldObject;

    /// <summary>
    /// 武器の見た目種類
    /// </summary>
    public ShieldAppearanceType ApType;
    
    /// <summary>
    /// 防御力
    /// オプション値も含む
    /// </summary>
    public override float ItemDefence
    {
        get
        {
            //スコア値の更新
            if (typeof(ShieldFreeHand) != this.GetType())
            {
                TotalSield++;
                if (TotalSield > ScoreInformation.Info.MostUseSieldDamage)
                {
                    ScoreInformation.Info.MostUseSieldDamage = TotalSield;
                    ScoreInformation.Info.MostUseSieldName = DisplayNameNormal;
                }
            }

            float value = ShieldBaseDefense;
            value += StrengthValue;
            return value;
        }
    }

    /// <summary>
    /// 使った回数
    /// </summary>
    public int TotalSield;

    public Vector3 _ObjectVector;
    /// <summary>
    /// プレイヤーに設定する際の位置
    /// </summary>
    public virtual Vector3 ObjectVector
    {
        get
        {
            if (CommonFunction.IsNull(_ObjectVector) == true)
            {
                return Vector3.zero;
            }
            return _ObjectVector;
        }
    }

    public Quaternion _ObjectQuaternion;
    /// <summary>
    /// プレイヤーに設定する際の回転
    /// </summary>
    public virtual Quaternion ObjectQuaternion
    {
        get
        {
            if (CommonFunction.IsNull(_ObjectQuaternion) == true)
            {
                return new Quaternion(0, 0, 0, 0);
            }
            return _ObjectQuaternion;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonShield";
        IType = ItemType.Shield;
        IsDriveProb = true;
        ApType = ShieldAppearanceType.None;
        Options = new List<BaseOption>();
        TotalSield = 0;
    }

    public override bool Equip(BaseCharacter target)
    {
        base.Equip(target);

        return ForceEquip(target);
    }

    public override bool ForceEquip(BaseCharacter target)
    {
        //ClearAnalyse();

        //インスタンスのコピーを作成
        switch (ApType)
        {
            case ShieldAppearanceType.Podlit:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldPodlit",
                    target.EquipLeft.transform);
                //ShieldObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipShieldPodlit").gameObject);
                _ObjectVector = new Vector3(-0.022f, 0.059f, 0.042f);
                _ObjectQuaternion = new Quaternion(-0.211501f, 0.5699962f, 0.5806013f, -0.5415474f);
                break;
            case ShieldAppearanceType.Wood:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldWood",
                    target.EquipLeft.transform);
                //ShieldObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipShieldWood").gameObject);
                if (PlayerInformation.Info.PType == PlayerType.OricharChan)
                {
                    _ObjectVector = new Vector3(-0.006f, 0.113f, -0.003f);
                    _ObjectQuaternion = new Quaternion(0.7836433f, -0.07938863f, -0.2394273f, 0.567693f);
                }

                break;
            case ShieldAppearanceType.Paper:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldPaper",
                    target.EquipLeft.transform);
                //ShieldObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipShieldStars").gameObject);
                _ObjectVector = new Vector3(-0.057f, 0.057f, 0.005f);
                _ObjectQuaternion = new Quaternion(0.5535138f, 0.5570804f, 0.2628616f, 0.5605245f);
                break;
            case ShieldAppearanceType.Knight:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldKnight",
                    target.EquipLeft.transform);
                //ShieldObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipShieldKnight").gameObject);
                _ObjectVector = new Vector3(-0.057f, 0.057f, 0.005f);
                _ObjectQuaternion = new Quaternion(0.5535138f, 0.5570804f, 0.2628616f, 0.5605245f);

                break;
            case ShieldAppearanceType.Empire:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldEmpire",
                    target.EquipLeft.transform);
                //ShieldObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipShieldEmpire").gameObject);
                _ObjectVector = new Vector3(-0.057f, 0.057f, 0.005f);
                _ObjectQuaternion = new Quaternion(0.5535138f, 0.5570804f, 0.2628616f, 0.5605245f);
                break;
            case ShieldAppearanceType.Stars:
                ShieldObject = ResourceInformation.GetInstance("UnityEquipShieldStars",
                    target.EquipLeft.transform);

                _ObjectVector = new Vector3(-0.022f, 0.059f, 0.042f);
                _ObjectQuaternion = new Quaternion(-0.211501f, 0.5699962f, 0.5806013f, -0.5415474f);
                break;
        }

        // 左手の子オブジェクトとして登録する
        //ShieldObject.transform.SetParent(target.EquipLeft.transform.transform);

        //位置を0クリア
        ShieldObject.transform.localPosition = ObjectVector;
        ShieldObject.transform.localRotation = ObjectQuaternion;

        //装備中にする
        IsEquip = true;


        ResetEquipOptionStatus(target);

        return true;
    }

    public override bool ForceRemoveEquip(BaseCharacter target)
    {
        RemoveEquipUpdateStatus(target);

        //GameObject.Destroy(ShieldObject);
        RemoveEquipDestroyObject();

        return true;
    }
    public override void RemoveEquipDestroyObject()
    {
        GameObject.Destroy(ShieldObject);
        ShieldObject = null;
    }


    /// <summary>
    /// 投げる開始
    /// </summary>
    public override bool ThrowStartSpecial(PlayerCharacter player,BaseCharacter target)
    {
        //隕鉄の盾だったら一定の確率で戻ってくる
        if(ApType == ShieldAppearanceType.Stars)
        {
            if(CommonFunction.IsRandom(0.95f) == true)
            {
                int damage = Mathf.CeilToInt(ItemDefence / 2);
                AttackState atState = CommonFunction.AddDamage(player.AttackInfo, player, target, damage);

                player.AttackInfo.AddMessage(string.Format(CommonConst.Message.ThrowAction, this.DisplayNameInMessage));

                //対象が死亡したら
                if (atState == AttackState.Death)
                {
                    player.AttackInfo.AddKillList(target);

                    player.AttackInfo.AddMessage(
                        target.GetMessageDeath(target.HaveExperience));

                    player.Death(target,player.AttackInfo);
                }

                EffectShieldThrow.CreateObject(target, player, damage.ToString(), AttackState.Hit).Play();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// 装備解除時に画面オブジェクトを削除する
    /// </summary>
    public override bool RemoveEquip(BaseCharacter target, bool isOutMessage = true)
    {
        bool result = base.RemoveEquip(target, isOutMessage);

        if (result == true)
        {
            ResetEquipOptionStatus(target);

            GameObject.Destroy(ShieldObject);
        }

        return result;
    }

    public override Dictionary<int, MenuItemActionType> GetItemAction()
    {
        int i = 0;
        Dictionary<int, MenuItemActionType> dic = new Dictionary<int, MenuItemActionType>();
        bool has = PlayerCharacter.HasItemPlayer(this);
        if (has == false && GameStateInformation.Info.IsThrowAway == false)
        {
            dic.Add(i++, MenuItemActionType.Get);//拾う
        }
        if (IsEquip == true)
        {
            dic.Add(i++, MenuItemActionType.RemoveEquip);//外す
        }
        else
        {
            dic.Add(i++, MenuItemActionType.Equip);//装備
        }
        //if ((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Throw);//投げる

        if (Options.Count >= 1)
        {
            dic.Add(i++, MenuItemActionType.LookOption);//オプション参照
        }
        if (has == true)
        {
            dic.Add(i++, MenuItemActionType.Put);//置く
        }

        //ドライブに空きがあれば入れるを追加
        //if (PlayerCharacter.HasDriveGap() == true)
        //{
        //    dic.Add(i++, MenuItemActionType.Putin);
        //}
        return dic;
    }

    public override string GetDefense()
    {
        if (_IsAnalyse == true)
        {
            string result = Mathf.Round(ShieldBaseDefense).ToString();
            if (StrengthValue != 0)
            {
                result += string.Format("+{0}", StrengthValue);
            }
            return result;
        }
        else
        {
            return "?";
        }
    }
}

