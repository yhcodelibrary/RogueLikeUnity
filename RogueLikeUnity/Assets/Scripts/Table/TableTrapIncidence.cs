using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TableTrapIncidence
{

    private static TableTrapIncidenceData[] _table;
    private static TableTrapIncidenceData[] Table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new TableTrapIncidenceData[] {
                     new TableTrapIncidenceData(1, 30001, 0, 260301049)
, new TableTrapIncidenceData(1, 30002, 260301049, 520602097)
, new TableTrapIncidenceData(1, 30003, 520602097, 780903145)
, new TableTrapIncidenceData(1, 30004, 780903145, 937083774)
, new TableTrapIncidenceData(1, 30005, 937083774, 1457685870)
, new TableTrapIncidenceData(1, 30006, 1457685870, 1978287967)
, new TableTrapIncidenceData(1, 30007, 1978287967, 2498890063)
, new TableTrapIncidenceData(1, 30008, 2498890063, 3019492159)
, new TableTrapIncidenceData(1, 30009, 3019492159, 3149642683)
, new TableTrapIncidenceData(1, 30010, 3149642683, 3227732998)
, new TableTrapIncidenceData(1, 30011, 3227732998, 3357883522)
, new TableTrapIncidenceData(1, 30012, 3357883522, 3357883522)
, new TableTrapIncidenceData(1, 30013, 3357883522, 3357883522)
, new TableTrapIncidenceData(1, 30014, 3357883522, 3383913627)
, new TableTrapIncidenceData(1, 30015, 3383913627, 3644214675)
, new TableTrapIncidenceData(1, 30016, 3644214675, 4164816771)
, new TableTrapIncidenceData(1, 30017, 4164816771, 4294967295)
, new TableTrapIncidenceData(2, 30001, 0, 147087922)
, new TableTrapIncidenceData(2, 30002, 147087922, 441263764)
, new TableTrapIncidenceData(2, 30003, 441263764, 735439606)
, new TableTrapIncidenceData(2, 30004, 735439606, 941362695)
, new TableTrapIncidenceData(2, 30005, 941362695, 1441461627)
, new TableTrapIncidenceData(2, 30006, 1441461627, 1882725390)
, new TableTrapIncidenceData(2, 30007, 1882725390, 2471077074)
, new TableTrapIncidenceData(2, 30008, 2471077074, 3059428759)
, new TableTrapIncidenceData(2, 30009, 3059428759, 3206516680)
, new TableTrapIncidenceData(2, 30010, 3206516680, 3324187016)
, new TableTrapIncidenceData(2, 30011, 3324187016, 3471274938)
, new TableTrapIncidenceData(2, 30012, 3471274938, 3500692522)
, new TableTrapIncidenceData(2, 30013, 3500692522, 3500692522)
, new TableTrapIncidenceData(2, 30014, 3500692522, 3588945274)
, new TableTrapIncidenceData(2, 30015, 3588945274, 3853703532)
, new TableTrapIncidenceData(2, 30016, 3853703532, 4000791453)
, new TableTrapIncidenceData(2, 30017, 4000791453, 4206714543)
, new TableTrapIncidenceData(2, 30018, 4206714543, 4294967295)
, new TableTrapIncidenceData(3, 30001, 0, 127731370)
, new TableTrapIncidenceData(3, 30002, 127731370, 606724005)
, new TableTrapIncidenceData(3, 30003, 606724005, 926052428)
, new TableTrapIncidenceData(3, 30004, 926052428, 1213448009)
, new TableTrapIncidenceData(3, 30005, 1213448009, 1596642118)
, new TableTrapIncidenceData(3, 30006, 1596642118, 1915970541)
, new TableTrapIncidenceData(3, 30007, 1915970541, 2394963176)
, new TableTrapIncidenceData(3, 30008, 2394963176, 2873955811)
, new TableTrapIncidenceData(3, 30009, 2873955811, 3033620023)
, new TableTrapIncidenceData(3, 30010, 3033620023, 3193284235)
, new TableTrapIncidenceData(3, 30011, 3193284235, 3321015604)
, new TableTrapIncidenceData(3, 30012, 3321015604, 3368914868)
, new TableTrapIncidenceData(3, 30013, 3368914868, 3400847710)
, new TableTrapIncidenceData(3, 30014, 3400847710, 3496646237)
, new TableTrapIncidenceData(3, 30015, 3496646237, 3752108976)
, new TableTrapIncidenceData(3, 30016, 3752108976, 3911773187)
, new TableTrapIncidenceData(3, 30017, 3911773187, 4135303084)
, new TableTrapIncidenceData(3, 30018, 4135303084, 4294967295)
, new TableTrapIncidenceData(4, 30001, 0, 134217728)
, new TableTrapIncidenceData(4, 30002, 134217728, 637534208)
, new TableTrapIncidenceData(4, 30003, 637534208, 1040187392)
, new TableTrapIncidenceData(4, 30004, 1040187392, 1275068416)
, new TableTrapIncidenceData(4, 30005, 1275068416, 1577058304)
, new TableTrapIncidenceData(4, 30006, 1577058304, 1845493760)
, new TableTrapIncidenceData(4, 30007, 1845493760, 2248146944)
, new TableTrapIncidenceData(4, 30008, 2248146944, 2650800128)
, new TableTrapIncidenceData(4, 30009, 2650800128, 2818572288)
, new TableTrapIncidenceData(4, 30010, 2818572288, 3019898880)
, new TableTrapIncidenceData(4, 30011, 3019898880, 3087007744)
, new TableTrapIncidenceData(4, 30012, 3087007744, 3254779904)
, new TableTrapIncidenceData(4, 30013, 3254779904, 3288334336)
, new TableTrapIncidenceData(4, 30014, 3288334336, 3422552064)
, new TableTrapIncidenceData(4, 30015, 3422552064, 3590324224)
, new TableTrapIncidenceData(4, 30016, 3590324224, 3758096384)
, new TableTrapIncidenceData(4, 30017, 3758096384, 4093640704)
, new TableTrapIncidenceData(4, 30018, 4093640704, 4294967295)
, new TableTrapIncidenceData(5, 30001, 0, 118210110)
, new TableTrapIncidenceData(5, 30002, 118210110, 512243806)
, new TableTrapIncidenceData(5, 30003, 512243806, 985084242)
, new TableTrapIncidenceData(5, 30004, 985084242, 1182101091)
, new TableTrapIncidenceData(5, 30005, 1182101091, 1457924679)
, new TableTrapIncidenceData(5, 30006, 1457924679, 1773151636)
, new TableTrapIncidenceData(5, 30007, 1773151636, 2088378593)
, new TableTrapIncidenceData(5, 30008, 2088378593, 2403605551)
, new TableTrapIncidenceData(5, 30009, 2403605551, 2600622399)
, new TableTrapIncidenceData(5, 30010, 2600622399, 2876445987)
, new TableTrapIncidenceData(5, 30011, 2876445987, 2876445987)
, new TableTrapIncidenceData(5, 30012, 2876445987, 3073462835)
, new TableTrapIncidenceData(5, 30013, 3073462835, 3152269575)
, new TableTrapIncidenceData(5, 30014, 3152269575, 3349286423)
, new TableTrapIncidenceData(5, 30015, 3349286423, 3546303272)
, new TableTrapIncidenceData(5, 30016, 3546303272, 3743320120)
, new TableTrapIncidenceData(5, 30017, 3743320120, 4019143708)
, new TableTrapIncidenceData(5, 30018, 4019143708, 4294967295)
, new TableTrapIncidenceData(6, 30001, 0, 132834041)
, new TableTrapIncidenceData(6, 30002, 132834041, 309946094)
, new TableTrapIncidenceData(6, 30003, 309946094, 752726227)
, new TableTrapIncidenceData(6, 30004, 752726227, 974116294)
, new TableTrapIncidenceData(6, 30005, 974116294, 1195506361)
, new TableTrapIncidenceData(6, 30006, 1195506361, 1549730468)
, new TableTrapIncidenceData(6, 30007, 1549730468, 1815398548)
, new TableTrapIncidenceData(6, 30008, 1815398548, 2081066628)
, new TableTrapIncidenceData(6, 30009, 2081066628, 2302456695)
, new TableTrapIncidenceData(6, 30010, 2302456695, 2656680802)
, new TableTrapIncidenceData(6, 30011, 2656680802, 2656680802)
, new TableTrapIncidenceData(6, 30012, 2656680802, 2789514842)
, new TableTrapIncidenceData(6, 30013, 2789514842, 3010904908)
, new TableTrapIncidenceData(6, 30014, 3010904908, 3232294975)
, new TableTrapIncidenceData(6, 30015, 3232294975, 3453685042)
, new TableTrapIncidenceData(6, 30016, 3453685042, 3675075109)
, new TableTrapIncidenceData(6, 30017, 3675075109, 3985021202)
, new TableTrapIncidenceData(6, 30018, 3985021202, 4294967295)

                };
                return _table;
            }
        }
    }

    public static BaseTrap GetTrap(int intype, uint rand)
    {
        TableTrapIncidenceData data = Array.Find(Table, i => i.IncidenceType == intype && i.StartPoint <= rand && rand <= i.EndPoint);
        if (CommonFunction.IsNull(data) == true)
        {
            return null;
        }
        return TableTrap.GetTrap(data.ObjNo);
    }


#if UNITY_EDITOR
    public static bool GetTrapTest(int intype, uint rand)
    {
        TableTrapIncidenceData data = Array.Find(Table, i => i.IncidenceType == intype && i.StartPoint <= rand && rand <= i.EndPoint);
        if (CommonFunction.IsNull(data) == true)
        {
            return false;
        }
        return true;
    }
#endif

    public static BaseTrap GetTrap(int floor)
    {
        
        BaseTrap trap = new BaseTrap();
        trap.Initialize();
        if (CommonFunction.IsRandom(0.7f) == true)
        {
            trap = TableTrap.GetTrap(30007);
            
        }
        else
        {
            trap = TableTrap.GetTrap(30006);
        }
        return trap;
    }

    private class TableTrapIncidenceData
    {
        public TableTrapIncidenceData(int incidenceType,
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
