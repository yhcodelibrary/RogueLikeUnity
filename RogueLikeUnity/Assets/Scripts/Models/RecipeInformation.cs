using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RecipeInformation
{
    public Guid Name;
    public long RecipeTargetNo;
    public ItemType RecipeTargetType;
    public string RecipeTargetName;
    public bool IsStrength;
    public BaseItem TargetStrength;
    public int Strength;
    public float Weight;

    public Dictionary<long, ItemType> RecipeMaterialsType;
    public Dictionary<long, int> RecipeMaterialsCount;
    public Dictionary<long, int> RecipeMaterialsPlus;
    public RecipeInformation()
    {
        Name = Guid.NewGuid();
        RecipeMaterialsType = new Dictionary<long, ItemType>();
        RecipeMaterialsCount = new Dictionary<long, int>();
        RecipeMaterialsPlus = new Dictionary<long, int>();
        IsStrength = false;
        Strength = 0;
        Weight = 1;
    }

    public void SetRecipeValue(long objNo,ItemType type,int count,int minPlus)
    {
        RecipeMaterialsType.Add(objNo, type);
        RecipeMaterialsCount.Add(objNo, count);
        RecipeMaterialsPlus.Add(objNo, minPlus);
    }

    public int CountHaveItem(long no)
    {
        int count;
        if (this.IsStrength == true)
        {
            if (this.RecipeTargetType == this.RecipeMaterialsType[no])
            {
                count = 1;
            }
            else
            {
                count = PlayerCharacter.ItemList.Count(i => i.ObjNo == no 
                    && i.IsDrive == false
                    && i.StrengthValue >= this.RecipeMaterialsPlus[no]);
            }
        }
        else
        {
            count = PlayerCharacter.ItemList.Count(i => i.ObjNo == no
                && i.IsDrive == false
                && i.StrengthValue == this.RecipeMaterialsPlus[no]);
        }
        return count;
    }

    /// <summary>
    /// 調合処理
    /// </summary>
    public BaseItem AtelierExecute(List<BaseItem> ItemSelected , BaseItem baseEquip = null)
    {
        //調合素材のオプションから対象とできるオプションを抜き出す
        List<BaseOption> options = new List<BaseOption>();

        //ビフォアネームを抽出
        string bname = "";

        //強化値を抽出
        int StrengthValue = 0;

        //鑑定済みかを抽出
        bool isAnalyse = true;

        foreach (BaseItem i in ItemSelected)
        {

            //素材アイテムの持つオプションを抽出
            if (CommonFunction.IsNull(i.Options) == false)
            {
                options.AddRange(i.Options);
            }
            //強化値を抽出
            if (string.IsNullOrEmpty(i.DisplayNameBefore) == false && i.IType == RecipeTargetType)
            {
                bname = i.DisplayNameBefore;
            }
            if (StrengthValue < i.StrengthValue)
            {
                StrengthValue = i.StrengthValue;
            }

            if(i.IsAnalyse == false)
            {
                isAnalyse = false;
            }
        }
        if(string.IsNullOrEmpty(bname)== true && (RecipeTargetType == ItemType.Weapon || RecipeTargetType == ItemType.Shield))
        {
            bname = "自作の";
        }

        //調合アイテムを作成
        BaseItem target = TableItemIncidence.GetItemObjNo(RecipeTargetType, RecipeTargetNo, false);

        //調合アイテムにbeforeName付与
        target.DisplayNameBefore = bname;

        //調合アイテムに強化値を反映
        if (this.IsStrength == true)
        {
            if (target.StrengthValue < StrengthValue)
            {
                target.StrengthValue = StrengthValue;
            }
        }

        //調合アイテムに鑑定フラグを付与
        target.IsAnalyse = isAnalyse;
        if(isAnalyse == true)
        {
            target.ClearAnalyse();
        }

        if (this.IsStrength == false)
        {
            if (target.StrengthValue < this.Strength)
            {
                target.StrengthValue = this.Strength;
            }
        }

        //ベースアイテムがある場合はベースアイテムの付加情報を反映
        if (CommonFunction.IsNull(baseEquip) == false)
        {
            target.DisplayNameBefore = baseEquip.DisplayNameBefore;
        }

        //調合アイテムにそのまま反映できるオプションを抽出
        target.Options = new List<BaseOption>();
        foreach (BaseOption op in options.FindAll(o => o.TargetItemType == target.IType))
        {
            BaseOption to = target.Options.Find(o => o.ObjNo == op.ObjNo && o.AbnormalStateTarget == op.AbnormalStateTarget);
            //同じオプションがすでに含まれていたらプラスの大きいほうを抽出
            if (CommonFunction.IsNull(to) == false)
            {
                if (op.Plus < to.Plus)
                {
                    op.Plus = to.Plus;
                }
            }
            else
            {
                target.Options.Add(op);
            }
        }

        //調合アイテムに反映できるかもしれないオプションを取得
        List<BaseOption> tarOps = options.FindAll(o => o.TargetItemType == ItemType.Material);

        //可能性のオプションを調合アイテム用オプションに反映
        foreach (BaseOption op in tarOps)
        {
            BaseOption newOpt = TableOptionCommon.GetValue(target.IType, op.OType, op.AbnormalStateTarget);
            
            if (CommonFunction.IsNull(newOpt) == false)
            {
                //同じオプションがすでに含まれていたらプラスの大きいほうを抽出
                BaseOption containOpt = target.Options.Find(o => o.ObjNo == newOpt.ObjNo && o.AbnormalStateTarget == newOpt.AbnormalStateTarget);
                if(CommonFunction.IsNull(containOpt) == false)
                {
                    if(containOpt.Plus < newOpt.Plus)
                    {
                        containOpt.Plus = newOpt.Plus;
                    }
                }
                //含まれていなかったら追加
                else
                {
                    newOpt.Plus = op.Plus;
                    target.Options.Add(newOpt);
                }
            }
        }

        return target;
    }
}
