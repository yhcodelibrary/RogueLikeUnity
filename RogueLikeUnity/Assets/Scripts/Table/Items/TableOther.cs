using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableOther
{

    private static TableOtherData[] _table;
    private static TableOtherData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableOtherData[1];

                _table[0] = new TableOtherData(29001, "お金", 0.92f,"お店で使えるお金。");

                return _table;
            }
        }
    }
    public static OtherBase GetItem(long objNo)
    {
        TableOtherData data = Array.Find(Table, i => i.ObjNo == objNo);
        OtherBase item = new OtherBase();
        item.Initialize();
        item.ObjNo = data.ObjNo;
        item.DisplayName = data.DisplayName;
        item.ThrowDexterity = data.ThrowDexterity;
        item.Description = data.Description;
        return item;
    }

    private class TableOtherData
    {
        public TableOtherData(long objNo,
            string displayName,
            float throwDexterity,
            string description)
        {
            ObjNo = objNo;
            DisplayName = displayName;
            ThrowDexterity = throwDexterity;
            Description = description;
        }
        public long ObjNo;
        public string DisplayName;
        public float ThrowDexterity;

        public string Description;
    }
}