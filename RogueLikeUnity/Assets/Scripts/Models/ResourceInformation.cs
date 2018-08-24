using Assets.Scripts.Models.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ResourceInformation
{
    /// <summary>
    /// 暗転用黒テクスチャ
    /// </summary>
    //private static Texture2D blackTexture;
    //public static Texture2D BlackTexture
    //{
    //    get
    //    {
    //        if (CommonFunction.IsNull(blackTexture) == false)
    //        {
    //            return blackTexture;
    //        }
    //        //ここで黒テクスチャ作る
    //        blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
    //        blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
    //        blackTexture.SetPixel(0, 0, Color.white);
    //        blackTexture.Apply();
    //        return blackTexture;
    //    }
    //}

    private static SavePlayingInformation _saveinfo;
    public static SavePlayingInformation SaveInfo
    {
        get
        {
            return _saveinfo;
        }
        set
        {
            _saveinfo = value;
        }
    }


    private static SpotLightMove lightManage;
    public static SpotLightMove LightManage
    {
        get
        {
            if (CommonFunction.IsNull(lightManage) == false)
            {
                return lightManage;
            }
            GameObject g = new GameObject("LightManage");
            lightManage = g.AddComponent<SpotLightMove>();
            //lightManage.left = GameObject.Find("VisLeft").gameObject;
            //lightManage.top = GameObject.Find("VisTop").gameObject;
            //lightManage.right = GameObject.Find("VisRight").gameObject;
            //lightManage.bottom = GameObject.Find("VisBottom").gameObject;
            lightManage.left = GameObject.Find("VisLeft").transform;
            lightManage.top = GameObject.Find("VisTop").transform;
            lightManage.right = GameObject.Find("VisRight").transform;
            lightManage.bottom = GameObject.Find("VisBottom").transform;
            lightManage.IsActive = true;
            return lightManage;
        }
    }

    private static GameObject dammyObject;
    public static GameObject DammyObject
    {
        get
        {
            if (CommonFunction.IsNull(dammyObject) == false)
            {
                return dammyObject;
            }
            dammyObject = new GameObject();
            return dammyObject;
        }
    }

    private static GameObject commonImage;
    public static GameObject CommonImage
    {
        get
        {
            if (CommonFunction.IsNull(commonImage) == false)
            {
                return commonImage;
            }
            commonImage = GameObject.Find("CommonImage");
            return commonImage;
        }
    }

    private static GameObject _Stair;
    public static GameObject Stair
    {
        get
        {
            if (CommonFunction.IsNull(_Stair) == false)
            {
                return _Stair;
            }
            _Stair = GameObject.Find("Stair");
            return _Stair;
        }
    }

    private static GameObject _Kiln;
    public static GameObject Kiln
    {
        get
        {
            if (CommonFunction.IsNull(_Kiln) == false)
            {
                return _Kiln;
            }
            _Kiln = GameObject.Find("Kiln");
            return _Kiln;
        }
    }

    private static Dictionary<string, List<EffectBase>> _EffectPool = new Dictionary<string, List<EffectBase>>();
    public static Dictionary<string, List<EffectBase>> EffectPool
    {
        get
        {
            if (CommonFunction.IsNull(_EffectPool) == false)
            {
                return _EffectPool;
            }
            _EffectPool = new Dictionary<string, List<EffectBase>>();
            return _EffectPool;
        }
    }

    private static GameObject _Effect;
    public static GameObject Effect
    {
        get
        {
            if (CommonFunction.IsNull(_Effect) == false)
            {
                return _Effect;
            }
            _Effect = GameObject.Find("Effect");
            return _Effect;
        }
    }
    private static GameObject _MapPanel;
    public static GameObject MapPanel
    {
        get
        {
            if (CommonFunction.IsNull(_MapPanel) == false)
            {
                return _MapPanel;
            }
            _MapPanel = GameObject.Find("MapPanel");
            return _MapPanel;
        }
    }

    private static GameObject _EffectCanvas;
    public static GameObject EffectCanvas
    {
        get
        {
            if (CommonFunction.IsNull(_EffectCanvas) == false)
            {
                return _EffectCanvas;
            }
            _EffectCanvas = GameObject.Find("EffectCanvas");
            return _EffectCanvas;
        }
    }


    private static GameObject _UnityEquip;
    public static GameObject UnityEquip
    {
        get
        {
            if (CommonFunction.IsNull(_UnityEquip) == false)
            {
                return _UnityEquip;
            }
            _UnityEquip = GameObject.Find("UnityEquip");
            return _UnityEquip;
        }

    }
    private static GameObject _DungeonObject;
    public static GameObject DungeonObject
    {
        get
        {
            if (CommonFunction.IsNull(_DungeonObject) == false)
            {
                return _DungeonObject;
            }
            _DungeonObject = GameObject.Find("DungeonObject");
            return _DungeonObject;
        }
    }

    private static GameObject _DungeonStruct;
    public static GameObject DungeonStruct
    {
        get
        {
            if (CommonFunction.IsNull(_DungeonStruct) == false)
            {
                return _DungeonStruct;
            }
            _DungeonStruct = GameObject.Find("DungeonStruct");
            return _DungeonStruct;
        }
    }

    private static GameObject _DungeonDynamicObject;
    public static GameObject DungeonDynamicObject
    {
        get
        {
            if (CommonFunction.IsNull(_DungeonDynamicObject) == false)
            {
                return _DungeonDynamicObject;
            }
            _DungeonDynamicObject = GameObject.Find("DungeonDynamicObject");
            return _DungeonDynamicObject;
        }
    }


    private static Quaternion quaternionZero;
    public static Quaternion QuaternionZero
    {
        get
        {

            if (CommonFunction.IsNull(quaternionZero) == false)
            {
                return quaternionZero;
            }
            quaternionZero = new Quaternion(CommonConst.Rotation.None, CommonConst.Rotation.None, CommonConst.Rotation.None, CommonConst.Rotation.None);
            return quaternionZero;
        }
    }

    private static Dictionary<string, GameObject> _InstanceManage;
    private static Dictionary<string, GameObject> InstanceManage
    {
        get
        {

            if (CommonFunction.IsNull(_InstanceManage) == false)
            {
                return _InstanceManage;
            }
            _InstanceManage = new Dictionary<string, GameObject>();
            //プレイヤー
            AddObjectDictionary("UnityChanBody");
            AddObjectDictionary("SD_Stellachan");

            //武器
            AddObjectDictionary("UnityEquipSword");
            AddObjectDictionary("UnityEquipStaff");
            AddObjectDictionary("UnityEquipKnife");
            AddObjectDictionary("UnityEquipAxe");
            AddObjectDictionary("UnityEquipMace");
            AddObjectDictionary("UnityEquipBat");
            AddObjectDictionary("UnityEquipHockeyStick");
            AddObjectDictionary("UnityEquipPlunger");
            //盾
            AddObjectDictionary("UnityEquipShieldPodlit");
            AddObjectDictionary("UnityEquipShieldWood");
            AddObjectDictionary("UnityEquipShieldPaper");
            AddObjectDictionary("UnityEquipShieldKnight");
            AddObjectDictionary("UnityEquipShieldEmpire");
            AddObjectDictionary("UnityEquipShieldStars");

            //状態異常
            AddObjectDictionary("CommonPoison");
            AddObjectDictionary("CommonDeadlyPoison");
            AddObjectDictionary("CommonSleep");
            AddObjectDictionary("CommonPalalysis");
            AddObjectDictionary("CommonConfusion");
            AddObjectDictionary("CommonCharmed");
            AddObjectDictionary("CommonSlow");
            AddObjectDictionary("CommonReticent"); 
            AddObjectDictionary("CommonStiffShoulder");
            AddObjectDictionary("CommonMaterial");
            AddObjectDictionary("CommonAcceleration");
            //アイテム
            AddObjectDictionary("CommonWepon");
            AddObjectDictionary("CommonShield");
            AddObjectDictionary("CommonRing");
            AddObjectDictionary("CommonFood");
            AddObjectDictionary("CommonCandy");
            AddObjectDictionary("CommonMelody");
            AddObjectDictionary("CommonBall");
            AddObjectDictionary("CommonBag");
            AddObjectDictionary("CommonOther");
            AddObjectDictionary("FireBall");

            //敵
            AddObjectDictionary("Bat_1");
            AddObjectDictionary("Bat_2");
            AddObjectDictionary("EnemyDecoy");
            AddObjectDictionary("EnemyDammyItem");
            AddObjectDictionary("Ghost_1");
            AddObjectDictionary("Ghost_2");
            AddObjectDictionary("Rabbit_1");
            AddObjectDictionary("Rabbit_2");
            AddObjectDictionary("Rabbit_3");
            AddObjectDictionary("Rabbit_King");
            AddObjectDictionary("Butterfly_1");
            AddObjectDictionary("TeddyBear_1");
            AddObjectDictionary("TeddyBear_2");
            AddObjectDictionary("Slime_1");
            AddObjectDictionary("Slime_2");
            AddObjectDictionary("Slime_3");
            AddObjectDictionary("Plant_1");
            AddObjectDictionary("Plant_King");
            AddObjectDictionary("PA_Drone_1");
            AddObjectDictionary("PA_Warrior_1");
            AddObjectDictionary("SPIDER_1"); 
            AddObjectDictionary("Whale_1");
            AddObjectDictionary("Whale_King");
            AddObjectDictionary("DarkStella_1");
            AddObjectDictionary("DarkUnity_1");
            AddObjectDictionary("Alarmer_1");
            AddObjectDictionary("Drangwin_1");

            //トラップ
            AddObjectDictionary("TrapPoison");
            AddObjectDictionary("TrapDeadlyPoison");
            AddObjectDictionary("TrapFloorFire");
            AddObjectDictionary("TrapSlow");
            AddObjectDictionary("TrapBomb");
            AddObjectDictionary("TrapPhoto");
            AddObjectDictionary("TrapColorMonitor");
            AddObjectDictionary("TrapElectric");
            AddObjectDictionary("TrapCyclone");
            AddObjectDictionary("TrapSandStorm");
            AddObjectDictionary("TrapSummon");
            AddObjectDictionary("TrapFly");
            AddObjectDictionary("TrapSong");
            AddObjectDictionary("TrapRoate");
            AddObjectDictionary("TrapGas");
            AddObjectDictionary("TrapBucket");
            AddObjectDictionary("TrapPollen");

            return _InstanceManage;
        }
    }

    public static GameObject GetPlayerInstance(string name)
    {
        GameObject gm = UnityEngine.Object.Instantiate(InstanceManage[name]);

        gm.SetActive(true);

        //プレイヤーのベースインスタンスをすべて削除
        RemoveObjectDictionary("UnityChanBody");
        RemoveObjectDictionary("SD_Stellachan");
        return gm;
    }
    public static GameObject GetInstance(string name,bool isDynamic)
    {
        GameObject gm;
        if (isDynamic == true)
        {
            gm = UnityEngine.Object.Instantiate(InstanceManage[name],
                   ResourceInformation.DungeonDynamicObject.transform);
        }
        else
        {
            gm = UnityEngine.Object.Instantiate(InstanceManage[name]);
        }

        gm.SetActive(true);
        return gm;
    }
    public static GameObject GetInstance(string name, Transform t)
    {
        GameObject gm;
        gm = UnityEngine.Object.Instantiate(InstanceManage[name],
               t);

        gm.SetActive(true);
        return gm;
    }
    private static void AddObjectDictionary(string name)
    {
        GameObject gm = GameObject.Find(name);
        gm.SetActive(false);
        _InstanceManage.Add(name, gm);
    }
    private static void RemoveObjectDictionary(string name)
    {
        GameObject gm = _InstanceManage[name];
        GameObject.Destroy(gm);
        _InstanceManage.Remove(name);
    }

    public static void DungeonInit()
    {
        _EffectPool = null;
        lightManage = null;
        commonImage = null;
        _EffectCanvas = null;
        _Effect = null;
        _UnityEquip = null;
        _DungeonObject = null;
        _DungeonDynamicObject = null;
        _DungeonStruct = null;
        _MapPanel = null;
        _Stair = null;
        _Kiln = null;
        _InstanceManage = null;
        dammyObject = null;
        quaternionZero = new Quaternion(CommonConst.Rotation.None, CommonConst.Rotation.None, CommonConst.Rotation.None, CommonConst.Rotation.None);
    }
}