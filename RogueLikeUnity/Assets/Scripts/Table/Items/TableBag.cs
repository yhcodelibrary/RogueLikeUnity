using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TableBag
{

    private static TableBagData[] _table;
    private static TableBagData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableBagData[] {


                    new TableBagData(28001, "保存のかばん", "Save Bag", 0.92f, true, BagType.Save, 3, 0.8f, 1.1f, 9, "物を保存しておくことができるかばん。入れたものは好きな時に使える。", "A bag that can store things. Items in the bag can be used whenever you like.")
, new TableBagData(28002, "再利用かばん", "Reusable Bag", 0.92f, false, BagType.Recycle, 1, 0.8f, 1.3f, 9, "物を入れるとHPが回復する。入れた物は、バザーに出品され消えてしまう。", "HP will recover by putting things. Things put in are brought to the bazaar and disappear.")
, new TableBagData(28003, "変化のかばん", "Metamorphosis Bag", 0.92f, false, BagType.Change, 1, 0.5f, 1.3f, 9, "物を入れると別の物に変化する不思議なかばん。", "A magical bag that changes to another item when put in items.")
, new TableBagData(28004, "コロッケかばん", "Croquette Bag", 0.92f, false, BagType.Food, 1, 0.5f, 1.3f, 9, "物を入れるとコロッケに変化するかばん。", "A bag that changes into croquette after put in items.")
, new TableBagData(28005, "普通のかばん", "Normal Bag", 0.92f, false, BagType.Normal, 2, 0.8f, 1.3f, 9, "普通のかばん。物を入れておけるが取り出し口に返しが付いており、取り出すには壊さないといけない。", "A normal bag. Although you can put things in, there is a return at the take-out opening and you have to break it to take out it.")

                };
                
                return _table;
            }
        }
    }

    public static ushort[] GetObjNoList()
    {
        ushort[] datas = Array.ConvertAll(Table, i => i.ObjNo);
        return datas;
    }

    public static BagBase GetItem(long objNo)
    {
        TableBagData data = Array.Find(Table, i => i.ObjNo == objNo);
        BagBase item = new BagBase();
        item.Initialize();
        item.MaxGap = (sbyte)CommonFunction.ConvergenceRandom(data.StartGap, data.Startprob, data.Con, data.MaxGap);
        item.ObjNo = data.ObjNo;
        if (GameStateInformation.IsEnglish == false)
        {
            item.DisplayName = data.DisplayName;
            item.Description = data.Description;
        }
        else
        {
            item.DisplayName = data.DisplayNameEn;
            item.Description = data.DescriptionEn;
        }
        item.ThrowDexterity = data.ThrowDexterity;
        item.IsDisplayContents = data.IsDisplayContents;
        item.BgType = data.Bgtype;
        return item;
    }

    private class TableBagData
    {
        public TableBagData(ushort objNo,
            string displayName,
            string displayNameEn,
            float throwDexterity,
            bool isDisplayContents,
            BagType btype,
            int startGap,
            float startprob,
            float con,
            int maxGap,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            ThrowDexterity = throwDexterity;
            IsDisplayContents = isDisplayContents;
            Bgtype = btype;
            StartGap = startGap;
            Startprob = startprob;
            Con = con;
            MaxGap = maxGap;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public float ThrowDexterity;
        public bool IsDisplayContents;
        public BagType Bgtype;
        public int StartGap;
        public float Startprob;
        public float Con;
        public int MaxGap;
        public string Description;
        public string DescriptionEn;
    }
}
