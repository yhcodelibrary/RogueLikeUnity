using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class TableMaterial
{
    private static TableMaterialData[] _table;
    private static TableMaterialData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableMaterialData[]
                {

                     new TableMaterialData(30001, "塩", "Salt", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 2f, 1, 0.8f, 1.5f, "食用塩。料理からお祓いまで用途はいろいろ。","Edible salt. Uses are various from cooking to exorcising.")
, new TableMaterialData(30002, "小麦", "Wheat", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 2f, 1, 0.8f, 1.5f, "収穫したばかりの小麦。","Freshly harvested wheat.")
, new TableMaterialData(30003, "砂鉄", "Iron Sand", MaterialType.Basic, 0.92f, 0, OptionBaseType.MaterialStrength, 0, 0.1f, 2.5f, 1, 0.4f, 5f, "純度の荒い鉄の砂。製鉄して使う。","Rough iron sand of purity. It is necessary to make steel to use it.")
, new TableMaterialData(30004, "枯れ枝", "Dead Branch", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 2f, 1, 0.8f, 1.5f, "どこにでもある枯れ枝。","General dead branches.")
, new TableMaterialData(30005, "皮", "Leather", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 2f, 1, 0.8f, 1.5f, "何かの動物の皮。なめして使う。","Something's animal's skin. Tanned and used.")
, new TableMaterialData(30006, "砂糖", "Sugar", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 2f, 1, 0.8f, 1.5f, "食用砂糖。とても甘い。","Edible sugar. very sweet.")
, new TableMaterialData(30007, "銀", "Silver", MaterialType.Basic, 0.92f, 0, OptionBaseType.Material, 0, 0.8f, 1.5f, 1, 0.8f, 1.2f, "光沢を帯びた希少な金属。","A rare metal with gloss.")
, new TableMaterialData(30008, "ブラヴィニウム", "Bravinium", MaterialType.Basic, 0.92f, 0, OptionBaseType.MaterialStrength, 0, 0.8f, 1.5f, 1, 0.8f, 1.5f, "古代に降り注いだ隕石よりもたらされた希少な金属。","A rare metal brought by an ancient meteorite.")
, new TableMaterialData(30009, "玉鋼", "Steel", MaterialType.Strength, 0.92f, 1, OptionBaseType.MaterialStrength, 0, 0f, 10f, 0, 0f, 10f, "製鉄された鋼。装備の生成や強化に使える。","Iron-made steel. It can be used to generate and strengthen equipment.")


                };

                return _table;
            }
        }
    }

    public static List<MaterialBase> GetItems(MaterialType type)
    {
        TableMaterialData[] datas = Array.FindAll(Table, i => i.Mtype == type);

        List<MaterialBase> list = new List<MaterialBase>();
        foreach (TableMaterialData data in datas)
        {

            MaterialBase item = new MaterialBase();
            item.Initialize();
            AttachData(item, data, false);

            list.Add(item);
        }

        return list;
    }
    public static MaterialBase GetItem(MaterialType type)
    {
        TableMaterialData data = Array.Find(Table, i => i.Mtype == type);

        MaterialBase item = new MaterialBase();
        item.Initialize();
        AttachData(item, data, false);
        return item;

    }
    public static MaterialBase GetItem(long objNo,bool isRandomValue)
    {
        TableMaterialData data = Array.Find(Table, i => i.ObjNo == objNo);
        MaterialBase item = new MaterialBase();
        item.Initialize();
        AttachData(item, data, isRandomValue);

        return item;
    }
    private static void AttachData(MaterialBase item , TableMaterialData data,bool isRandomValue)
    {

        item.ObjNo = data.ObjNo;
        item.MType = data.Mtype;
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
        item.StrengthValue = data.Strength;

        if (isRandomValue == false)
        {
            return;
        }

        int optioncount = CommonFunction.ConvergenceRandom(data.OptionAddStart, data.OptionAddContinue, data.OptionAddReduce);
        int index = 0;
        for (int i = 0; i < optioncount; i++)
        {
            //30回回して終わらなかったら強制終了
            if (index > 30)
            {
                break;
            }
            index++;
            uint rnd = CommonFunction.GetRandomUInt32();
            BaseOption newOpt = TableOptionCommon.GetValue(data.OBType, rnd, data.OptionPowStart, data.OptionPowContinue, data.OptionPowReduce);

            //同じオプションがすでに含まれていたらもう一度算出
            if (CommonFunction.IsNull(newOpt) == true)
            {
                i--;
                continue;
            }
            BaseOption containOpt = item.Options.Find(o => o.ObjNo == newOpt.ObjNo);
            if (CommonFunction.IsNull(containOpt) == false)
            {
                i--;
                continue;
            }
            item.Options.Add(newOpt);
        }
    }

    private class TableMaterialData
    {
        public TableMaterialData(ushort objNo,
            string displayName,
            string displayNameEn,
            MaterialType mtype,
            float throwDexterity,
            int strength,
            OptionBaseType obType,
            int optionAddStart,
            float optionAddContinue,
            float optionAddReduce,
            int optionPowStart,
            float optionPowContinue,
            float optionPowReduce,
            string description,
            string descriptionEn)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            DisplayNameEn = displayNameEn;
            Mtype = mtype;
            ThrowDexterity = throwDexterity;
            Strength = strength;
            OBType = obType;
            OptionAddStart = optionAddStart;
            OptionAddContinue = optionAddContinue;
            OptionAddReduce = optionAddReduce;
            OptionPowStart = optionPowStart;
            OptionPowContinue = optionPowContinue;
            OptionPowReduce = optionPowReduce;
            Description = description;
            DescriptionEn = descriptionEn;
        }
        public ushort ObjNo;
        public string DisplayName;
        public string DisplayNameEn;
        public MaterialType Mtype;
        public float ThrowDexterity;
        public int Strength;
        public OptionBaseType OBType;
        public int OptionAddStart;
        public float OptionAddContinue;
        public float OptionAddReduce;
        public int OptionPowStart;
        public float OptionPowContinue;
        public float OptionPowReduce;
        public string Description;
        public string DescriptionEn;
    }

}