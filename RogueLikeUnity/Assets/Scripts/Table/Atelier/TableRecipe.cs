using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TableRecipe
{
    private static TableRecipeData[] _table;
    private static TableRecipeData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableRecipeData[] {

                    new TableRecipeData(60015, 25010, ItemType.Ball, RecipeType.Normal, 0)
, new TableRecipeData(60016, 23005, ItemType.Food, RecipeType.Normal, 0)
, new TableRecipeData(60017, 24012, ItemType.Candy, RecipeType.Normal, 0)
, new TableRecipeData(60018, 30009, ItemType.Material, RecipeType.Normal, 1)
, new TableRecipeData(60019, 30009, ItemType.Material, RecipeType.Normal, 2)
, new TableRecipeData(60020, 30009, ItemType.Material, RecipeType.Normal, 3)
, new TableRecipeData(60021, 30009, ItemType.Material, RecipeType.Normal, 4)
, new TableRecipeData(60022, 30009, ItemType.Material, RecipeType.Normal, 5)
, new TableRecipeData(60023, 30009, ItemType.Material, RecipeType.Normal, 6)
, new TableRecipeData(60024, 30001, ItemType.Material, RecipeType.Normal, 0)
, new TableRecipeData(60025, 30002, ItemType.Material, RecipeType.Normal, 0)
, new TableRecipeData(60026, 30006, ItemType.Material, RecipeType.Normal, 0)
, new TableRecipeData(60027, 30004, ItemType.Material, RecipeType.Normal, 0)
, new TableRecipeData(60028, 30005, ItemType.Material, RecipeType.Normal, 0)
, new TableRecipeData(60001, 20001, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60002, 20002, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60003, 20003, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60004, 20004, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60005, 20005, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60006, 20006, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60007, 20007, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60008, 20008, ItemType.Weapon, RecipeType.Normal, 0)
, new TableRecipeData(60009, 21001, ItemType.Shield, RecipeType.Normal, 0)
, new TableRecipeData(60010, 21002, ItemType.Shield, RecipeType.Normal, 0)
, new TableRecipeData(60011, 21003, ItemType.Shield, RecipeType.Normal, 0)
, new TableRecipeData(60012, 21004, ItemType.Shield, RecipeType.Normal, 0)
, new TableRecipeData(60013, 21005, ItemType.Shield, RecipeType.Normal, 0)
, new TableRecipeData(60014, 21006, ItemType.Shield, RecipeType.Normal, 0)


                };
                return _table;
            }
        }
    }

    public static Dictionary<Guid, RecipeInformation> Result;

    public static Dictionary<Guid, RecipeInformation> GetValue(RecipeType type)
    {
        if(CommonFunction.IsNull(Result) == false)
        {
            return Result;
        }
        //TableRecipeData[] datas = Array.FindAll(Table, i => i.RType == type);

        Result = new Dictionary<Guid, RecipeInformation>();
        foreach (TableRecipeData d in Table)
        {
            RecipeInformation rec = new RecipeInformation();

            //生成対象アイテムを取得
            BaseItem item = TableItemIncidence.GetItemObjNo(d.IType,d.ObjNo, false);
            item.StrengthValue = d.Strength;
            rec.RecipeTargetName = item.DisplayNameNormal;
            rec.Weight = item.Weight;

            rec.RecipeTargetNo = d.ObjNo;
            rec.RecipeTargetType = d.IType;
            rec.Strength = d.Strength;
            TableRecipeMaterial.SetValue(rec,d.RecipeObjNo);

            Result.Add(rec.Name, rec);
        }

        return Result;
    }

    private class TableRecipeData
    {
        public TableRecipeData(
            ushort recipeTargetObjNo,
            ushort objno,
            ItemType iType,
            RecipeType rType,
            sbyte strength)
        {
            RecipeObjNo = recipeTargetObjNo;
            ObjNo = objno;
            IType = iType;
            RType = rType;
            Strength = strength;
        }
        public ushort RecipeObjNo;
        public ushort ObjNo;
        public ItemType IType;
        public RecipeType RType;
        public sbyte Strength;
    }
}
