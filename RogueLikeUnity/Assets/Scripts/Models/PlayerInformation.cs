using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerInformation
{
    private static PlayerInformation _Info;
    public static PlayerInformation Info
    {
        get
        {
            if (CommonFunction.IsNull(_Info) == false)
            {
                return _Info;
            }
            _Info = new PlayerInformation();
            return Info;
        }
        set
        {
            _Info = value;
        }
    }

    public Guid UniqueName;
    public long ObjNo;
    public string DefaultName;
    public string InstanceName;
    public string CameraName;
    public PlayerType PType;
    public ushort ItemMaxCount;
    public bool HasDeathblow;
    public string DeathblowName;
    public string DeathblowDescription;
    public string DeathblowDescription2;

    public int DeathblowMp;

    public string AlcReasonable;
    public float AlcReasonableFloat;

}
