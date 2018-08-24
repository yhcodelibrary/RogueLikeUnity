using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableEnemyIncidence
{

    private static TableEnemyIncidenceData[] _table;
    private static TableEnemyIncidenceData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableEnemyIncidenceData[]
                {

 new TableEnemyIncidenceData(1, 10001, 0, 4294967295)
, new TableEnemyIncidenceData(2, 10001, 0, 1431655765)
, new TableEnemyIncidenceData(2, 10002, 1431655765, 2863311530)
, new TableEnemyIncidenceData(2, 10021, 2863311530, 4294967295)
, new TableEnemyIncidenceData(3, 10003, 0, 1431655765)
, new TableEnemyIncidenceData(3, 10002, 1431655765, 2863311530)
, new TableEnemyIncidenceData(3, 10010, 2863311530, 4294967295)
, new TableEnemyIncidenceData(4, 10010, 0, 1073741824)
, new TableEnemyIncidenceData(4, 10004, 1073741824, 2147483648)
, new TableEnemyIncidenceData(4, 10005, 2147483648, 3221225472)
, new TableEnemyIncidenceData(4, 10006, 3221225472, 4294967295)
, new TableEnemyIncidenceData(5, 10023, 0, 1073741824)
, new TableEnemyIncidenceData(5, 10006, 1073741824, 2147483648)
, new TableEnemyIncidenceData(5, 10004, 2147483648, 3221225472)
, new TableEnemyIncidenceData(5, 10008, 3221225472, 4294967295)
, new TableEnemyIncidenceData(6, 10007, 0, 1073741824)
, new TableEnemyIncidenceData(6, 10008, 1073741824, 2147483648)
, new TableEnemyIncidenceData(6, 10009, 2147483648, 3221225472)
, new TableEnemyIncidenceData(6, 10019, 3221225472, 4294967295)
, new TableEnemyIncidenceData(7, 10009, 0, 858993459)
, new TableEnemyIncidenceData(7, 10011, 858993459, 1717986918)
, new TableEnemyIncidenceData(7, 10001, 1717986918, 2576980377)
, new TableEnemyIncidenceData(7, 10002, 2576980377, 3435973836)
, new TableEnemyIncidenceData(7, 10007, 3435973836, 4294967295)
, new TableEnemyIncidenceData(8, 10021, 0, 613566757)
, new TableEnemyIncidenceData(8, 10002, 613566757, 1227133513)
, new TableEnemyIncidenceData(8, 10023, 1227133513, 1840700270)
, new TableEnemyIncidenceData(8, 10020, 1840700270, 2454267026)
, new TableEnemyIncidenceData(8, 10012, 2454267026, 3067833783)
, new TableEnemyIncidenceData(8, 10008, 3067833783, 3681400539)
, new TableEnemyIncidenceData(8, 10010, 3681400539, 4294967295)
, new TableEnemyIncidenceData(9, 10003, 0, 536870912)
, new TableEnemyIncidenceData(9, 10012, 536870912, 1073741824)
, new TableEnemyIncidenceData(9, 10008, 1073741824, 1610612736)
, new TableEnemyIncidenceData(9, 10010, 1610612736, 2147483648)
, new TableEnemyIncidenceData(9, 10004, 2147483648, 2684354560)
, new TableEnemyIncidenceData(9, 10018, 2684354560, 3221225472)
, new TableEnemyIncidenceData(9, 10009, 3221225472, 3758096384)
, new TableEnemyIncidenceData(9, 10013, 3758096384, 4294967295)
, new TableEnemyIncidenceData(10, 10001, 0, 3303820997)
, new TableEnemyIncidenceData(10, 10003, 3303820997, 4294967295)
, new TableEnemyIncidenceData(11, 10002, 0, 2147483648)
, new TableEnemyIncidenceData(11, 10004, 2147483648, 3221225472)
, new TableEnemyIncidenceData(11, 10007, 3221225472, 4294967295)
, new TableEnemyIncidenceData(12, 10010, 0, 1717986918)
, new TableEnemyIncidenceData(12, 10012, 1717986918, 3435973836)
, new TableEnemyIncidenceData(12, 10005, 3435973836, 4294967295)
, new TableEnemyIncidenceData(13, 10009, 0, 1431655765)
, new TableEnemyIncidenceData(13, 10013, 1431655765, 2863311530)
, new TableEnemyIncidenceData(13, 10017, 2863311530, 4294967295)
, new TableEnemyIncidenceData(14, 10006, 0, 1431655765)
, new TableEnemyIncidenceData(14, 10001, 1431655765, 4294967295)
, new TableEnemyIncidenceData(15, 10007, 0, 1431655765)
, new TableEnemyIncidenceData(15, 10002, 1431655765, 2863311530)
, new TableEnemyIncidenceData(15, 10022, 2863311530, 4294967295)
, new TableEnemyIncidenceData(16, 10007, 0, 1431655765)
, new TableEnemyIncidenceData(16, 10009, 1431655765, 2863311530)
, new TableEnemyIncidenceData(16, 10019, 2863311530, 4294967295)
, new TableEnemyIncidenceData(17, 10003, 0, 1431655765)
, new TableEnemyIncidenceData(17, 10008, 1431655765, 2863311530)
, new TableEnemyIncidenceData(17, 10018, 2863311530, 4294967295)
, new TableEnemyIncidenceData(18, 10007, 0, 1431655765)
, new TableEnemyIncidenceData(18, 10017, 1431655765, 2863311530)
, new TableEnemyIncidenceData(18, 10008, 2863311530, 4294967295)
, new TableEnemyIncidenceData(19, 10009, 0, 1431655765)
, new TableEnemyIncidenceData(19, 10020, 1431655765, 2863311530)
, new TableEnemyIncidenceData(19, 10014, 2863311530, 4294967295)
, new TableEnemyIncidenceData(20, 10010, 0, 1431655765)
, new TableEnemyIncidenceData(20, 10001, 1431655765, 4294967295)
, new TableEnemyIncidenceData(21, 10017, 0, 1431655765)
, new TableEnemyIncidenceData(21, 10019, 1431655765, 2863311530)
, new TableEnemyIncidenceData(21, 10002, 2863311530, 4294967295)
, new TableEnemyIncidenceData(22, 10007, 0, 1431655765)
, new TableEnemyIncidenceData(22, 10019, 1431655765, 2863311530)
, new TableEnemyIncidenceData(22, 10004, 2863311530, 4294967295)
, new TableEnemyIncidenceData(23, 10015, 0, 1431655765)
, new TableEnemyIncidenceData(23, 10008, 1431655765, 2863311530)
, new TableEnemyIncidenceData(23, 10020, 2863311530, 4294967295)
, new TableEnemyIncidenceData(24, 10023, 0, 1431655765)
, new TableEnemyIncidenceData(24, 10017, 1431655765, 2863311530)
, new TableEnemyIncidenceData(24, 10019, 2863311530, 4294967295)
, new TableEnemyIncidenceData(25, 10009, 0, 1431655765)
, new TableEnemyIncidenceData(25, 10008, 1431655765, 2863311530)
, new TableEnemyIncidenceData(25, 10014, 2863311530, 4294967295)
, new TableEnemyIncidenceData(26, 10022, 0, 4294967295)
, new TableEnemyIncidenceData(27, 10003, 0, 1431655765)
, new TableEnemyIncidenceData(27, 10004, 1431655765, 2863311530)
, new TableEnemyIncidenceData(27, 10018, 2863311530, 4294967295)
, new TableEnemyIncidenceData(28, 10009, 0, 1073741824)
, new TableEnemyIncidenceData(28, 10021, 1073741824, 2147483648)
, new TableEnemyIncidenceData(28, 10004, 2147483648, 3221225472)
, new TableEnemyIncidenceData(28, 10023, 3221225472, 4294967295)
, new TableEnemyIncidenceData(29, 10012, 0, 1431655765)
, new TableEnemyIncidenceData(29, 10008, 1431655765, 2863311530)
, new TableEnemyIncidenceData(29, 10018, 2863311530, 4294967295)
, new TableEnemyIncidenceData(30, 10015, 0, 1073741824)
, new TableEnemyIncidenceData(30, 10017, 1073741824, 2147483648)
, new TableEnemyIncidenceData(30, 10016, 2147483648, 3221225472)
, new TableEnemyIncidenceData(30, 10009, 3221225472, 4294967295)
, new TableEnemyIncidenceData(31, 10014, 0, 1431655765)
, new TableEnemyIncidenceData(31, 10008, 1431655765, 2863311530)
, new TableEnemyIncidenceData(31, 10010, 2863311530, 4294967295)
, new TableEnemyIncidenceData(32, 10021, 0, 1431655765)
, new TableEnemyIncidenceData(32, 10017, 1431655765, 4294967295)
, new TableEnemyIncidenceData(33, 10003, 0, 1431655765)
, new TableEnemyIncidenceData(33, 10020, 1431655765, 2863311530)
, new TableEnemyIncidenceData(33, 10022, 2863311530, 4294967295)
, new TableEnemyIncidenceData(34, 10005, 0, 1431655765)
, new TableEnemyIncidenceData(34, 10018, 1431655765, 2863311530)
, new TableEnemyIncidenceData(34, 10019, 2863311530, 4294967295)
, new TableEnemyIncidenceData(35, 10007, 0, 1431655765)
, new TableEnemyIncidenceData(35, 10010, 1431655765, 2863311530)
, new TableEnemyIncidenceData(35, 10018, 2863311530, 4294967295)
, new TableEnemyIncidenceData(36, 10011, 0, 1431655765)
, new TableEnemyIncidenceData(36, 10017, 1431655765, 2863311530)
, new TableEnemyIncidenceData(36, 10002, 2863311530, 4294967295)
, new TableEnemyIncidenceData(37, 10009, 0, 1073741824)
, new TableEnemyIncidenceData(37, 10007, 1073741824, 2147483648)
, new TableEnemyIncidenceData(37, 10008, 2147483648, 3221225472)
, new TableEnemyIncidenceData(37, 10022, 3221225472, 4294967295)
, new TableEnemyIncidenceData(38, 10003, 0, 2147483648)
, new TableEnemyIncidenceData(38, 10018, 2147483648, 4294967295)
, new TableEnemyIncidenceData(39, 10017, 0, 1431655765)
, new TableEnemyIncidenceData(39, 10010, 1431655765, 2863311530)
, new TableEnemyIncidenceData(39, 10015, 2863311530, 4294967295)
, new TableEnemyIncidenceData(40, 10001, 0, 1431655765)
, new TableEnemyIncidenceData(40, 10019, 1431655765, 2863311530)
, new TableEnemyIncidenceData(40, 10002, 2863311530, 4294967295)
, new TableEnemyIncidenceData(41, 10023, 0, 1431655765)
, new TableEnemyIncidenceData(41, 10019, 1431655765, 2863311530)
, new TableEnemyIncidenceData(41, 10010, 2863311530, 4294967295)
, new TableEnemyIncidenceData(42, 10008, 0, 1431655765)
, new TableEnemyIncidenceData(42, 10010, 1431655765, 2863311530)
, new TableEnemyIncidenceData(42, 10002, 2863311530, 4294967295)
, new TableEnemyIncidenceData(43, 10009, 0, 1431655765)
, new TableEnemyIncidenceData(43, 10021, 1431655765, 2863311530)
, new TableEnemyIncidenceData(43, 10007, 2863311530, 4294967295)


                };

                return _table;
            }
        }
    }
    
    public static BaseEnemyCharacter GetEnemy(int intype, uint rand,ushort floor)
    {
        TableEnemyIncidenceData data = Array.Find(Table, i => i.IncidenceType == intype && i.StartPoint <= rand && rand <= i.EndPoint);
        if (CommonFunction.IsNull(data) == true)
        {
            return null;
        }
        return TableEnemy.GetEnemy(data.ObjNo, floor, 
            DungeonInformation.Info.EnemyHpProb, 
            DungeonInformation.Info.EnemyAtkProb, 
            DungeonInformation.Info.EnemyExpProb,
            DungeonInformation.Info.StartProbHp,
            DungeonInformation.Info.StartProbAtk,
            DungeonInformation.Info.StartProbExp);
    }

    private class TableEnemyIncidenceData
    {
        public TableEnemyIncidenceData(int incidenceType,
            ushort objNo,
            uint startPoint,
            uint endPoint)
        {
            IncidenceType = incidenceType;
            ObjNo = objNo;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
        public int IncidenceType;
        public ushort ObjNo;
        public uint StartPoint;
        public uint EndPoint;
    }
}