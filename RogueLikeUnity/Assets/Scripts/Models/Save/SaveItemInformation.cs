using Assets.Scripts.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Save
{
    public class SaveItemInformation
    {
        public static string GetJson(List<BaseItem> list)
        {
            List<SaveItemData> result = ToListSaveItemData(list);

            string json = JsonMapper.ToJson(result.ToArray());

            return json;
        }

        public static List<BaseItem> ToListBaseItem(SaveItemData[] list)
        {
            List<BaseItem> result = new List<BaseItem>();
            Dictionary<int, BagBase> bagMap = new Dictionary<int, BagBase>();

            //バッグを最初に処理
            foreach (SaveItemData b in Array.FindAll(list,i=>i.it == ItemType.Bag))
            {
                BaseItem item = ToBaseItem(b);
                bagMap.Add(b.hnm, (BagBase)item);
                result.Add(item);
            }

            foreach (SaveItemData b in Array.FindAll(list, i => i.it != ItemType.Bag))
            {
                BaseItem item = ToBaseItem(b);

                //バッグに入っていたらバッグに入れる
                if(b.ib != 0 && bagMap.ContainsKey(b.ib) == true)
                {
                    
                    bagMap[b.ib].BagItems.Add(item);
                    item.InDrive = bagMap[b.ib];
                }

                result.Add(item);
            }

            return result;
        }

        public static List<SaveItemData> ToListSaveItemData(List<BaseItem> list)
        {
            List<SaveItemData> result = new List<SaveItemData>();

            int index = 1;
            foreach(BaseItem b in list.OrderBy(i=>i.SortNo))
            {
                SaveItemData sd = ToSaveItemData(b);
                //鞄だけ名前を格納
                if(b.IType == ItemType.Bag)
                {
                    sd.hnm = b.Name.GetHashCode();
                }
                sd.sn = index++;
                result.Add(sd);
            }

            return result;
        }


        public static BaseItem ToBaseItem(SaveItemData d)
        {
            BaseItem t = TableItemIncidence.GetItemObjNo(d.it, d.on, false);

            QualifyInformation qi = TableQualify.GetValue(d.bnn);
            t.DisplayNameBeforeObjNo = qi.ObjNo;
            t.DisplayNameBefore = qi.Name;
            t.StrengthValue = d.sv;
            t.SortNo = d.sn;
            //t.IsBug = d.bb;
            t.IsEquip = d.be;

            if(t.IType == ItemType.Bag)
            {
                ((BagBase)t).MaxGap = d.cnt;
            }
            else if(t.IType == ItemType.Ball)
            {
                ((BallBase)t).RestCount = d.cnt;
            }

            //オプション
            if (CommonFunction.IsNull(d.ops) == false && d.ops.Length > 0)
            {
                List<BaseOption> ops = new List<BaseOption>();
                foreach (SaveOptionData o in d.ops)
                {
                    ops.Add(ToBaseOption(o));
                }
                t.Options = ops;
            }

            return t;
        }

        public static SaveItemData ToSaveItemData(BaseItem d)
        {
            SaveItemData t = new SaveItemData();
            //t.hnm = d.Name.GetHashCode();
            t.bnn = d.DisplayNameBeforeObjNo;
            t.on = d.ObjNo;
            t.it = d.IType;
            t.sv = d.StrengthValue;
            t.sn = d.SortNo;
            //t.bb = d.IsBug;
            t.be = d.IsEquip;
            if (d.IsDrive)
            {
                t.ib = d.InDrive.Name.GetHashCode();
            }

            if (d.IType == ItemType.Bag)
            {
                t.cnt = ((BagBase)d).MaxGap;
            }
            else if (d.IType == ItemType.Ball)
            {
                t.cnt = ((BallBase)d).RestCount;
            }


            //オプション
            if (CommonFunction.IsNull(d.Options) == false && d.Options.Count > 0)
            {
                List<SaveOptionData> ops = new List<SaveOptionData>();
                foreach(BaseOption o in d.Options)
                {
                    ops.Add(ToSaveOptionData(o));
                }
                t.ops = ops.ToArray();
            }

            return t;
        }
        public static SaveOptionData ToSaveOptionData(BaseOption d)
        {
            SaveOptionData t = new SaveOptionData();
            //t.hn = d.Name.GetHashCode();
            t.on = d.ObjNo;
            t.p = d.Plus;

            return t;
        }
        public static BaseOption ToBaseOption(SaveOptionData d)
        {
            BaseOption t = TableOptionCommon.GetValue(d.on);
            t.Plus = d.p;

            return t;
        }
    }

    public class SaveItemData
    {
        /// <summary>
        /// オプション
        /// </summary>
        public SaveOptionData[] ops;

        /// <summary>
        /// ボール、バッグのカウント
        /// </summary>
        public sbyte cnt;

        /// <summary>
        /// 一意番号
        /// </summary>
        public int hnm;

        /// <summary>
        /// あだ名
        /// </summary>
        public ushort bnn;

        /// <summary>
        /// オブジェクト番号
        /// </summary>
        public long on;

        /// <summary>
        /// 強化値
        /// </summary>
        public int sv;

        /// <summary>
        /// アイテムの種別
        /// </summary>
        public ItemType it;

        /// <summary>
        /// ソート順
        /// </summary>
        public long sn;

        /// <summary>
        /// バグっているかどうか（呪われているかどうか）
        /// </summary>
        //public bool bb;

        /// <summary>
        /// 装備中かどうか
        /// </summary>
        public bool be;

        /// <summary>
        /// 格納されているドライブ名
        /// </summary>
        public int ib;

    }

    public class SaveOptionData
    {

        /// <summary>
        /// 一意番号
        /// </summary>
        //public int hn;

        /// <summary>
        /// オブジェクト番号
        /// </summary>
        public long on;
        
        /// <summary>
        /// プラス値
        /// </summary>
        public int p;
    }
}
