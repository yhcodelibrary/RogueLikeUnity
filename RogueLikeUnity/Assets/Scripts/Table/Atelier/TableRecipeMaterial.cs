using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableRecipeMaterial
{
    private static TableRecipeMaterialData[] _table;
    private static TableRecipeMaterialData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableRecipeMaterialData[] {

                    new TableRecipeMaterialData(60015, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60015, 30005, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60016, 30001, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60016, 30002, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60016, 30004, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60017, 30001, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60017, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60018, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60019, 30009, ItemType.Material, 2, 1)
, new TableRecipeMaterialData(60020, 30009, ItemType.Material, 2, 2)
, new TableRecipeMaterialData(60021, 30009, ItemType.Material, 2, 3)
, new TableRecipeMaterialData(60022, 30009, ItemType.Material, 2, 4)
, new TableRecipeMaterialData(60023, 30009, ItemType.Material, 2, 5)
, new TableRecipeMaterialData(60024, 30005, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60024, 30004, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60024, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60025, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60025, 30001, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60025, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60026, 30004, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60026, 30002, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60026, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60027, 30002, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60027, 30001, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60027, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60028, 30007, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60028, 30003, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60001, 30009, ItemType.Material, 1, 1)
, new TableRecipeMaterialData(60001, 30005, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60001, 30004, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60002, 30009, ItemType.Material, 1, 2)
, new TableRecipeMaterialData(60002, 30004, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60003, 30009, ItemType.Material, 1, 3)
, new TableRecipeMaterialData(60003, 30005, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60003, 30001, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60004, 30009, ItemType.Material, 1, 3)
, new TableRecipeMaterialData(60004, 30004, ItemType.Material, 3, 0)
, new TableRecipeMaterialData(60004, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60005, 30009, ItemType.Material, 1, 3)
, new TableRecipeMaterialData(60005, 30007, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60006, 30009, ItemType.Material, 1, 4)
, new TableRecipeMaterialData(60007, 30009, ItemType.Material, 1, 4)
, new TableRecipeMaterialData(60007, 30001, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60007, 30005, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60008, 30009, ItemType.Material, 1, 5)
, new TableRecipeMaterialData(60008, 30004, ItemType.Material, 3, 0)
, new TableRecipeMaterialData(60008, 30007, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60008, 30008, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60009, 30009, ItemType.Material, 1, 1)
, new TableRecipeMaterialData(60009, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60010, 30009, ItemType.Material, 1, 2)
, new TableRecipeMaterialData(60010, 30004, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60011, 30009, ItemType.Material, 1, 3)
, new TableRecipeMaterialData(60011, 30006, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60011, 30007, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60012, 30009, ItemType.Material, 1, 4)
, new TableRecipeMaterialData(60013, 30009, ItemType.Material, 1, 4)
, new TableRecipeMaterialData(60013, 30005, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60013, 30004, ItemType.Material, 1, 0)
, new TableRecipeMaterialData(60013, 30007, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60014, 30009, ItemType.Material, 1, 5)
, new TableRecipeMaterialData(60014, 30004, ItemType.Material, 3, 0)
, new TableRecipeMaterialData(60014, 30007, ItemType.Material, 2, 0)
, new TableRecipeMaterialData(60014, 30008, ItemType.Material, 1, 0)



                };
                return _table;
            }
        }
    }

    //public static TableRecipeMaterialData[] GetAllValue()
    //{
    //    return Table;
    //}

    public static void SetValue(RecipeInformation rec,ushort recNo)
    {
        TableRecipeMaterialData[] data = Array.FindAll(Table, i => i.RecipeTargetObjNo == recNo);
        
        foreach(TableRecipeMaterialData d in data)
        {
            rec.SetRecipeValue(d.ObjNo, d.IType, d.Count, d.MinPlus);
        }

    }

    private class TableRecipeMaterialData
    {
        public TableRecipeMaterialData(
            ushort recipeTargetObjNo,
            ushort objno,
            ItemType iType,
            int count,
            sbyte plus)
        {
            RecipeTargetObjNo = recipeTargetObjNo;
            ObjNo = objno;
            IType = iType;
            Count = count;
            MinPlus = plus;
        }
        public ushort RecipeTargetObjNo;
        public ushort ObjNo;
        public ItemType IType;
        public int Count;
        public sbyte MinPlus;
    }
}
