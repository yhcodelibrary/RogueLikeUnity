using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableFood
{
    private static TableFoodData[] _table;
    private static TableFoodData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableFoodData[] {


                    new TableFoodData(23001, "コロッケ", "Croquette", FoodType.Common, 0.92f, 0, 0, 50, 0, 0, 0, "普通サイズのコロッケ。おなかがややふくれる。", "Normal size croquette. Hunger has been relaxed a lttle.")
, new TableFoodData(23002, "大きいコロッケ", "Large Croquette", FoodType.Common, 0.92f, 0, 0, 100, 0, 0, 0, "大きいコロッケ。おなかが満腹になる。", "A big croquette.  stomach will be full.")
, new TableFoodData(23003, "カレーコロッケ", "Curry Croquette", FoodType.Curry, 0.92f, 999, 0, 100, 0, 0, 2047, "特製カレーコロッケ。おなかが満腹になりHPや状態異常、Powerも回復する。", "Special curry croquette. Stomach gets full, HP, state abnormalities, Power recover.")
, new TableFoodData(23004, "ハエがたかるコロッケ", "Rotten Croquette", FoodType.Fly, 0.92f, 0, 0, 25, 0, 4, 0, "ハエがたくさんたかっているコロッケ。食べないほうがいい気がする。", "A croquette whose flies are gathered a lot. I feel that it is better not to eat.")
, new TableFoodData(23005, "手作りコロッケ", "Handmade Croquette", FoodType.Handmaid, 0.92f, 0, 0, 40, 0, 0, 0, "自分で作った手作りのコロッケ。", "Handmade croquette made by myself.")

                };

                return _table;
            }
        }
    }
    public static FoodBase GetItem(long objNo)
    {
        TableFoodData data = Array.Find(Table, i => i.ObjNo == objNo);
        FoodBase item = new FoodBase();
        item.Initialize();
        item.ObjNo = data.ObjNo;
        item.FType = data.FType;
        item.HpRecoverPoint = data.HpRecover;
        item.SatRecoverPoint = data.SatRecover;
        item.StatusBadTarget = data.StateBad;
        item.StatusRecoverTarget = data.StateRecover;
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
        return item;
    }

    private class TableFoodData
    {
        public TableFoodData(ushort objNo,
            string displayName,
            string displayNameEn,
            FoodType ftype,
            float throwDexterity,
            int hpRecover,
            int hpBad,
            int satRecover,
            int satBad,
            int stateBad,
            int stateRecover,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            FType = ftype;
            ThrowDexterity = throwDexterity;
            HpRecover = hpRecover;
            HpBad = hpBad;
            SatRecover = satRecover;
            SatBad = satBad;
            StateBad = stateBad;
            StateRecover = stateRecover;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public FoodType FType;
        public float ThrowDexterity;
        public int HpRecover;
        public int HpBad;
        public int SatRecover;
        public int SatBad;
        public int StateBad;
        public int StateRecover;

        public string Description;
        public string DescriptionEn;
    }
}