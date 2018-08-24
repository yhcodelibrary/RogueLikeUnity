using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DungeonInformation
{
    private static DungeonInformation _Info;
    public static DungeonInformation Info
    {
        get
        {
            if(CommonFunction.IsNull(_Info) == false)
            {
                return _Info;
            }
            _Info = new DungeonInformation();
            return Info;
        }
        set
        {
            _Info = value;
        }
    }

    public long DungeonObjNo;
    public string Name;
    public MusicInformation.MusicType MType;
    public string CameraName;
    public LoadStatus BaseLoadState;
    public int X;
    public int Y;
    public int DisruptFloor;
    public float Prob;
    public bool IsTimer;
    public bool IsAnalyze;
    public bool IsBringing;
    public bool IsBadVisible;
    public float KilnProb;
    public float StartProbHp;
    public float StartProbAtk;
    public float StartProbExp;
    public int ItemS;
    public float ItemP;
    public float ItemC;
    public int ItemM;
    public int EnemyS;
    public float EnemyP;
    public float EnemyC;
    public int EnemyM;
    public int TrapS;
    public float TrapP;
    public float TrapC;
    public int TrapM;
    public float EnemyHpProb;
    public float EnemyAtkProb;
    public float EnemyExpProb;
    public string BossObjNo;
    public string Description;
}
