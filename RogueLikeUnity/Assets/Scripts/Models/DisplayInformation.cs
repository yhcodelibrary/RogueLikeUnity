using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class DisplayInformation
{
    public bool IsUpdate { get; set; }
    public ushort Floor { get; private set; }
    public bool IsChangeFloor(ushort vFloor)
    {
        return Floor != vFloor;
    }
    public int Level { get; private set; }
    public bool IsChangeLevel(int vLevel)
    {
        return Level != vLevel;
    }
    public int LvExpMax { get; private set; }
    public int LvExpValue { get; private set; }
    public bool IsChangeExp(int vLvExpMax, int vLvExpValue)
    {
        return (LvExpMax != vLvExpMax || LvExpValue != vLvExpValue);
    }
    public float HpMax { get; private set; }
    public float HpValue { get; private set; }
    public bool IsChangeHp(float vHpMax, float vHpValue)
    {
        return (HpMax != vHpMax || HpValue != vHpValue);
    }
    public float SatietyValue { get; private set; }
    public float SatietyMax { get; private set; }
    public bool IsChangeSat(float vSatietyMax, float vSatietyValue)
    {
        return (SatietyMax != vSatietyMax || SatietyValue != vSatietyValue);
    }
    public ushort PowerValue { get; private set; }
    public ushort PowerMax { get; private set; }
    public bool IsChangePower(ushort vPowerMax, ushort vPowerValue)
    {
        return (PowerMax != vPowerMax || PowerValue != vPowerValue);
    }
    public ushort ItemMaxCount { get; private set; }
    public int State { get; private set; }
    public bool IsUpdateMessage { get; private set; }
    /// <summary>
    /// メッセージを表示中にするかのフラグ
    /// </summary>
    public bool IsNowDisplayMessage { get; private set; }
    public List<string> DisplayMessages { get; private set; }
    public List<string> DisplayMessagesHistory { get; private set; }
    public float MessageUpdateTimestamp { get; set; }
    public bool IsMessageUpdateTimestamp { get; private set; }
    public DateTime GameStartTimestamp { get; private set; }

    private static DisplayInformation _info;
    public static DisplayInformation Info
    {
        get
        {
            if(_info != null)
            {
                return _info;
            }
            _info = new DisplayInformation();
            return _info;
        }
    }
    private static DisplayInformation _infoClone;
    public static DisplayInformation InfoClone
    {
        get
        {
            if (_infoClone != null)
            {
                return _infoClone;
            }
            _infoClone = new DisplayInformation();
            return _infoClone;
        }
        set
        {
            _infoClone = value;
        }
    }

    private DisplayInformation()
    {
        IsUpdateMessage = false;
        IsNowDisplayMessage = false;
        DisplayMessages = new List<string>();
        DisplayMessagesHistory = new List<string>();
        MessageUpdateTimestamp = 0;

    }

    public void Initialize()
    {
        Floor = ushort.MinValue;
        Level = int.MinValue;
        LvExpMax = int.MinValue;
        LvExpValue = int.MinValue;
        HpMax = float.MinValue;
        HpValue = float.MinValue;
        SatietyValue = float.MinValue;
        SatietyMax = float.MinValue;
        PowerValue = ushort.MinValue;
        PowerMax = ushort.MinValue;
        ItemMaxCount = ushort.MinValue;
        State = int.MinValue;
    }
    

    public void SetCloneValue(DisplayInformation t)
    {
        Floor = t.Floor;
        Level = t.Level;
        LvExpMax = t.LvExpMax;
        LvExpValue = t.LvExpValue;
        HpMax = t.HpMax;
        HpValue = t.HpValue;
        SatietyValue = t.SatietyValue;
        SatietyMax = t.SatietyMax;
        PowerValue = t.PowerValue;
        PowerMax = t.PowerMax;
    }

    public void SetFloorInformation(ushort floor)
    {

        this.Floor = floor;
    }

    public void SetPlayerInformation(PlayerCharacter player)
    {
        //Lv
        this.Level = player.Level;
        this.LvExpMax = player.NextLevelExperience;
        this.LvExpValue = player.CurrentExperience;
        //Hp
        this.HpMax = player.MaxHp;
        this.HpValue = player.CurrentHp;
        //満腹
        this.SatietyMax = player.SatietyMax;
        this.SatietyValue = player.SatietyValue;
        //ちから
        this.PowerMax = player.PowerMax;
        this.PowerValue = player.PowerValue;
        //状態
        this.State = player.CharacterAbnormalState;

        //アイテム所持数
        this.ItemMaxCount = PlayerCharacter.ItemMaxCount;
        this.IsUpdate = true;
    }

    public void AddMessage(string mes)
    {
        this.DisplayMessages.Add(mes);
        this.IsUpdateMessage = true;
        this.IsUpdate = true;
    }

    public void OffUpdate()
    {
        this.IsUpdate = false;
    }

    public void StartTimestamp()
    {
        if (IsMessageUpdateTimestamp == false)
        {
            IsMessageUpdateTimestamp = true;
            MessageUpdateTimestamp = 0;
        }
    }
    public void ResetTimestamp()
    {
        IsMessageUpdateTimestamp = true;
        MessageUpdateTimestamp = 0;
    }

    public void StartMessageDisplay()
    {
        this.IsUpdateMessage = false;
        this.IsNowDisplayMessage = true;
        //更新したタイムスタンプを保存
        this.MessageUpdateTimestamp = 0;
    }

    public void EndMessageDisplay()
    {
        this.MessageUpdateTimestamp = 0;
        this.IsNowDisplayMessage = false;
    }

    public string GetAllMessage()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < DisplayInformation.Info.DisplayMessages.Count - 1; i++)
        {
            sb.AppendLine(DisplayInformation.Info.DisplayMessages[i]);
        }
        if(DisplayInformation.Info.DisplayMessages.Count >= 1)
        {
            //最後の1文字は改行なしで追加
            sb.Append(DisplayInformation.Info.DisplayMessages[DisplayInformation.Info.DisplayMessages.Count - 1]);
        }

        return sb.ToString();
    }
}

