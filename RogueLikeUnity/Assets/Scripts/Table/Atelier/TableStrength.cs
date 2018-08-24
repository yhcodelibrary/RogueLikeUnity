using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableStrength
{
    private static TableStrengthData[] _table;
    private static TableStrengthData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableStrengthData[] {

//                     new TableRecipeData(60001, 20001, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60002, 20002, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60003, 20003, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60004, 20004, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60005, 20005, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60006, 20006, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60007, 20007, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60008, 20008, ItemType.Weapon, RecipeType.Normal)
//, new TableRecipeData(60009, 21001, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60010, 21002, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60011, 21003, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60012, 21004, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60013, 21005, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60014, 21006, ItemType.Shield, RecipeType.Normal)
//, new TableRecipeData(60015, 25010, ItemType.Ball, RecipeType.Normal)
//, new TableRecipeData(60016, 23005, ItemType.Food, RecipeType.Normal)
//, new TableRecipeData(60017, 24012, ItemType.Candy, RecipeType.Normal)
//, new TableRecipeData(60018, 30009, ItemType.Material, RecipeType.Normal)
//, new TableRecipeData(60019, 30010, ItemType.Material, RecipeType.Normal)
//, new TableRecipeData(60020, 30011, ItemType.Material, RecipeType.Normal)
//, new TableRecipeData(60021, 30012, ItemType.Material, RecipeType.Normal)
//, new TableRecipeData(60022, 30013, ItemType.Material, RecipeType.Normal)
//, new TableRecipeData(60023, 30014, ItemType.Material, RecipeType.Normal)


                };
                return _table;
            }
        }
    }


    public static Dictionary<Guid, RecipeInformation> GetValue(List<BaseItem> ItemList)
    {
        //持っている装備を抽出
        BaseItem[] equips = ItemList.Where(i => i.IType == ItemType.Weapon || i.IType == ItemType.Shield).ToArray();

        //持っている強化素材を抽出
        long[] materials = ItemList.Where(i => (i.GetType() == typeof(MaterialBase) && ((MaterialBase)i).MType == MaterialType.Strength)).Select(i=>i.ObjNo).Distinct().ToArray();

        //強化素材の一覧を抽出
        MaterialBase smate = TableMaterial.GetItem(MaterialType.Strength);
        //TableStrengthData[] datas = Array.FindAll(Table, i => i.RType == type);

        Dictionary<Guid, RecipeInformation> result = new Dictionary<Guid, RecipeInformation>();
        foreach (BaseItem d in equips)
        {
            int cnt = 0;
            if(materials.Length > 0)
            {
                foreach (long m in materials)
                {
                    MaterialBase mb = TableMaterial.GetItem(m, false);
                    if (d.StrengthValue < mb.StrengthValue)
                    {
                        RecipeInformation rec = new RecipeInformation();
                        rec.Weight = d.Weight;
                        rec.RecipeTargetName = d.DisplayNameNormal;
                        rec.RecipeTargetNo = d.ObjNo;
                        rec.RecipeTargetType = d.IType;
                        rec.SetRecipeValue(d.ObjNo, d.IType, 1, d.StrengthValue);
                        rec.SetRecipeValue(m, ItemType.Material, 1, d.StrengthValue + 1);

                        rec.IsStrength = true;
                        rec.TargetStrength = d;

                        result.Add(rec.Name, rec);

                        cnt++;
                    }
                }
            }
            if (cnt == 0)
            {
                RecipeInformation rec = new RecipeInformation();
                rec.RecipeTargetName = d.DisplayNameNormal;
                rec.RecipeTargetNo = d.ObjNo;
                rec.RecipeTargetType = d.IType;
                rec.SetRecipeValue(d.ObjNo, d.IType, 1, d.StrengthValue);
                rec.SetRecipeValue(smate.ObjNo, ItemType.Material, 1, d.StrengthValue + 1);

                rec.IsStrength = true;
                rec.TargetStrength = d;

                result.Add(rec.Name, rec);
            }
        }

        return result;
    }


    private class TableStrengthData
    {
        public TableStrengthData(
            ushort recipeTargetObjNo,
            ushort objno,
            ItemType iType,
            RecipeType rType)
        {
            RecipeTargetObjNo = recipeTargetObjNo;
            ObjNo = objno;
            IType = iType;
            RType = rType;
        }
        public ushort RecipeTargetObjNo;
        public ushort ObjNo;
        public ItemType IType;
        public RecipeType RType;
    }
}
