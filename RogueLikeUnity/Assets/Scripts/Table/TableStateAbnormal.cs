using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TableStateAbnormal
{

    private static Dictionary<StateAbnormal, TableStateAbnormalData> _table;
    private static Dictionary<StateAbnormal, TableStateAbnormalData> table
    {
        get
        {
            if (_table != null)
            {
                return _table;
            }
            else
            {
                _table = new Dictionary<StateAbnormal, TableStateAbnormalData>(new StateAbnormalComparer());
                _table.Add(StateAbnormal.Confusion, new TableStateAbnormalData(3, 0.99f, 1.02f));
                _table.Add(StateAbnormal.Charmed, new TableStateAbnormalData(3, 0.9f, 1.1f));
                _table.Add(StateAbnormal.Dark, new TableStateAbnormalData(3, 0.99f, 1.02f));
                _table.Add(StateAbnormal.Poison, new TableStateAbnormalData(3, 0.99f, 1.02f));
                _table.Add(StateAbnormal.DeadlyPoison, new TableStateAbnormalData(10, 0.99f, 1.01f));
                _table.Add(StateAbnormal.Normal, new TableStateAbnormalData());
                _table.Add(StateAbnormal.Palalysis, new TableStateAbnormalData(4, 0.9f, 1.02f));
                _table.Add(StateAbnormal.Sleep, new TableStateAbnormalData(1, 0.99f, 1.05f));
                _table.Add(StateAbnormal.Decoy, new TableStateAbnormalData(3, 0.9f, 1.05f));
                _table.Add(StateAbnormal.Slow, new TableStateAbnormalData(3, 0.99f, 1.02f));
                _table.Add(StateAbnormal.Reticent, new TableStateAbnormalData(20, 0.99f, 1.01f));
                _table.Add(StateAbnormal.StiffShoulder, new TableStateAbnormalData(20, 0.99f, 1.01f));
                _table.Add(StateAbnormal.Acceleration, new TableStateAbnormalData(3, 0.99f, 1.02f));

                return _table;
            }
        }
    }

    /// <summary>
    /// Trueなら治る
    /// </summary>
    /// <param name="st"></param>
    /// <param name="turn"></param>
    /// <returns></returns>
    public static bool IsRecoverState(StateAbnormal st,int turn)
    {
        if(table[st].RecoverTurnStart > turn)
        {
            return false;
        }
        if(CommonFunction.IsConvergenceRandom(turn - table[st].RecoverTurnStart, table[st].ContinueState, table[st].ConiinueReducePer))
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private class TableStateAbnormalData
    {
        public TableStateAbnormalData(
            int recoverTurnStart = 3,
            float continueState = 0.9f,
            float coniinueReducePer = 1.1f)
        {
            RecoverTurnStart = recoverTurnStart;
            ContinueState = continueState;
            ConiinueReducePer = coniinueReducePer;
        }
        public int RecoverTurnStart;
        public float ContinueState;
        public float ConiinueReducePer;
    }

}

