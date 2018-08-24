//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class AtelierInformation
//{
//    //調合対象タイプ
//    public ItemType AtelierType;

//    //調合対象No
//    public long AtelierObjNo;

//    //調合テーブル

//    /// <summary>
//    /// 調合処理
//    /// </summary>
//    public BaseItem AtelierExecute(List<Guid> ItemSelected)
//    {
//        //調合素材のオプションから対象とできるオプションを抜き出す
//        List<BaseOption> options = new List<BaseOption>();

//        //ビフォアネームを抽出
//        string bname = "";

//        //強化値を抽出
//        int strengthv = 0;

//        foreach(Guid g in ItemSelected)
//        {
//            BaseItem i = PlayerCharacter.ItemList[g];
//            //素材アイテムの持つオプションを抽出
//            options.AddRange(i.Options.Values.ToArray());

//            if(string.IsNullOrEmpty(i.DisplayNameBefore) == false && i.IType == AtelierType)
//            {
//                bname = i.DisplayNameBefore;
//            }
//            if(strengthv < i.StrengthValue)
//            {
//                strengthv = i.StrengthValue;
//            }
//        }

//        //調合アイテムを作成
//        BaseItem target = TableItemIncidence.GetItemObjNo(AtelierType, AtelierObjNo, false);

//        //調合アイテムにbeforeName付与
//        target.DisplayNameBefore = bname;

//        //調合アイテムに強化値を反映
//        target.StrengthValue = strengthv;

//        //調合アイテムにそのまま反映できるオプションを抽出
//        target.Options = options.Where(o => o.TargetItemType == target.IType).ToDictionary(o=>o.Name);

//        //調合アイテムに反映できるかもしれないオプションを取得
//        List<BaseOption> tarOps = options.Where(o => o.TargetItemType == ItemType.Material).ToList();

//        //可能性のオプションを調合アイテム用オプションに反映
//        foreach(BaseOption op in tarOps)
//        {
//            BaseOption conOp = TableOptionCommon.GetValue(target.IType, op.OType);
//            if(CommonFunction.IsNull(conOp) == false)
//            {
//                conOp.Plus = op.Plus;
//                target.Options.Add(conOp.Name, conOp);
//            }
//        }

//        return target;
//    }
//}
