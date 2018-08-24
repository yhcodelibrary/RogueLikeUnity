using Assets.Scripts.Extend;
using Assets.Scripts.Models.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SaveDataInformation
{
    public const string KeyCtrlKey = "kck2";
    public const string KeyCtrlValue = "kcv2";
    public const string SystemValueKey = "svk1";
    public const string SystemValueValue = "svv";
    public const string PlayingKey = "pvk3";
    public const string PlayingValue = "pvv3";
    public const string ItemKey = "ivk3";
    public const string ItemValue = "ivv3";
    public const string ItemWarehouseKey = "iwk1";
    public const string ItemWarehouseValue = "iwv";


    public bool IsLoadSave;
    public ushort Floor;
    public ushort FloorBreadth;
    public ushort DungeonObjNo;
    public string DungeonName;
    public int Level;
    public int LvExpMax;
    public int LvExpValue;
    public ushort PowerMax;
    public ushort PowerValue;
    public float MaxHpCorrection;
    public float HpMax;
    public float HpValue;
    public float SatietyValue;
    public float SatietyMax;
    public float SatietyReduce;
    public ushort MaxItemCount;
    public string PlayerName;
    public long PreviouslyTimestamp;
    public SaveDataInformation()
    {
        IsLoadSave = true;
        Floor = 1;
        FloorBreadth = 55;
        DungeonObjNo = 50001;
        DungeonName = "始まりの洞窟";
        Level = 1;
        //MaxItemCount = PlayerInformation.Info.ItemMaxCount;
        //HP
        MaxHpCorrection = 0;
        HpValue = 20;
        //ちから
        PowerMax = 15;
        PowerValue = 15;
        //満腹度の設定
        SatietyValue = CommonConst.Status.SatietyMax;
        SatietyMax = CommonConst.Status.SatietyMax;

        //経験値
        LvExpValue = 0;
        //経過時間
        PreviouslyTimestamp = 0;
        if(CommonFunction.IsNullOrWhiteSpace(ScoreInformation.Info.PlayerName) == true)
        {
            PlayerName = PlayerInformation.Info.DefaultName;
        }
        else
        {
            PlayerName = ScoreInformation.Info.PlayerName;
        }

        IsLoadSave = true;
    }

    public static void InitilizeSystemValue()
    {
        //データとキーを保存
        PlayerPrefs.DeleteKey(KeyCtrlKey);
        PlayerPrefs.DeleteKey(KeyCtrlValue);
        PlayerPrefs.DeleteKey(SystemValueKey);
        PlayerPrefs.DeleteKey(SystemValueValue);
        PlayerPrefs.Save();
    }

    public static void RemoveSaveData()
    {
        //データとキーを保存
        PlayerPrefs.DeleteKey(PlayingKey);
        PlayerPrefs.DeleteKey(PlayingValue);
        PlayerPrefs.DeleteKey(ItemKey);
        PlayerPrefs.DeleteKey(ItemValue);
        PlayerPrefs.Save();

    }
    #region セーブ汎用
    public static void SavePlayingValue(string json)
    {
        //データを保存
        SaveCommon(PlayingKey, PlayingValue, CommonConst.CryptKey.SavePlayingKey, json);
    }

    public static SavePlayingInformation LoadPlayingValue()
    {
        return LoadCommon<SavePlayingInformation>(PlayingKey, PlayingValue, CommonConst.CryptKey.SavePlayingKey);
    }
    #endregion セーブ汎用
    #region 倉庫アイテム
    public static void SaveItemWarehouseValue(string json)
    {
        //データを保存
        SaveCommon(ItemWarehouseKey, ItemWarehouseValue, CommonConst.CryptKey.SaveItemWarehouseKey, json);
    }

    public static SaveItemData[] LoadItemWarehouseValue()
    {
        return LoadCommon<SaveItemData[]>(ItemWarehouseKey, ItemWarehouseValue, CommonConst.CryptKey.SaveItemWarehouseKey);
    }
    #endregion 倉庫アイテム
    #region 中断アイテム
    public static void SaveItemValue(string json)
    {
        //データを保存
        SaveCommon(ItemKey, ItemValue, CommonConst.CryptKey.SaveItemKey, json);
    }

    public static SaveItemData[] LoadItemValue()
    {
        return LoadCommon<SaveItemData[]>(ItemKey, ItemValue, CommonConst.CryptKey.SaveItemKey);
    }
    #endregion 中断アイテム

    public static void SaveValue(KeyControlModel data)
    {
        //jsonの取得
        string json = data.GetJsonData();

        //データの保存
        SaveKeyControl(json);
    }
    public static void SaveKeyControl(string json)
    {
        //データを保存
        SaveCommon(KeyCtrlKey, KeyCtrlValue, CommonConst.CryptKey.KeyControlKey, json);
    }

    public static KeyControlModel LoadKeyControl()
    {
        return LoadCommon<KeyControlModel>(KeyCtrlKey, KeyCtrlValue, CommonConst.CryptKey.KeyControlKey);
    }

    public static void SaveValue(SystemInformation data)
    {
        //jsonの取得
        string json = data.GetJsonData();

        //データの保存
        SaveSystemValue(json);
    }

    public static void SaveSystemValue(string json)
    {
        //データを保存
        SaveCommon(SystemValueKey, SystemValueValue, CommonConst.CryptKey.SystemValueKey, json);
    }

    public static SystemInformation LoadSystemInformation()
    {
        return LoadCommon<SystemInformation>(SystemValueKey, SystemValueValue, CommonConst.CryptKey.SystemValueKey);
    }

    private static void SaveCommon(string keyskey,string valueskey,string encKey,string jsondata)
    {
        //保存キーの発行
        string key = Guid.NewGuid().ToString("D");

        //データを暗号化
        string value = CryptInformation.EncryptString(jsondata, key);

        //キーを暗号化
        string enckey = CryptInformation.EncryptString(key, encKey);

        //データとキーを保存
        PlayerPrefs.SetString(keyskey, enckey);
        PlayerPrefs.SetString(valueskey, value);
    }

    private static T LoadCommon<T>(string keyskey,string valueskey, string encKey)
    {
        try
        {
            //キーと値を取得
            string key = PlayerPrefs.GetString(keyskey);
            string value = PlayerPrefs.GetString(valueskey);

            if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(value) == false)
            {
                //キーを復号化
                string deckey = CryptInformation.DecryptString(key, encKey);

                //値を復号化
                string decvalue = CryptInformation.DecryptString(value, deckey);

                return JsonMapper.ToObject<T>(decvalue);
            }
            else
            {
                PlayerPrefs.DeleteKey(keyskey);
                PlayerPrefs.DeleteKey(valueskey);
                return default(T);
            }
        }
        //エラーが発生したらnull
        catch (Exception)
        {
            PlayerPrefs.DeleteKey(SystemValueKey);
            PlayerPrefs.DeleteKey(SystemValueValue);
            PlayerPrefs.Save();
            return default(T);
        }
    }


    public static void Submit()
    {
        PlayerPrefs.Save();
    }

}
