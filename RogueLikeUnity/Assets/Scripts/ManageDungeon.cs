using Assets.Scripts.FieldObjects;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ManageDungeon : MonoBehaviour
{
    public float PosY = 2.5f;
    public float PosFloorY = 0;
    public int PositionUnit = 1;
    public bool IsRemap;
    public DungeonCreateModel Dungeon;
    //public Dictionary<Guid, BaseCharacter> Characters;
    public List<BaseCharacter> Characters;
    public List<BaseItem> Items;
    public List<BaseTrap> Traps;
    public Dictionary<Guid, int> RoomEnemys;
    public List<BaseCharacter> AddCharacters;
    public Layer2D<BaseCharacter> CharacterMap;
    public Layer2D<BaseItem> ItemMap;
    public Layer2D<BaseTrap> TrapMap;
    /// <summary>
    /// 薄ら日による可視化フラグ
    /// </summary>
    public bool IsVisible;
    /// <summary>
    /// 
    /// </summary>
    public bool IsEnemyVisible;
    public bool IsItemVisible;
    public bool IsTrapVisible;

    //ダンジョン生成のパラメータ類
    public long DungeonObjNo;
    public string DungeonName = "Test";
    public int X = 11;
    public int Y = 11;
    public float CommonProb = 0.98f;
    public int DisruptFloor = 51;
    public bool IsTimer = false;
    public bool IsAnalyze = false;
    public bool IsBringing = false;
    public int ItemS = 10;
    public float ItemP = 10;
    public float ItemC = 10;
    public int ItemM = 10;
    public int EnemyS = 10;
    public float EnemyP = 10;
    public float EnemyC = 10;
    public int EnemyM = 10;
    public int TrapS = 10;
    public float TrapP = 10;
    public float TrapC = 10;
    public int TrapM = 10;


    private GameObject Wall;
    //private GameObject Floor;
    private GameObject Floor2;
    private GameObject Water;
    //private GameObject Player;
    private GameObject MapImgUnit;
    private GameObject MapImgUnitPl;
    private GameObject MapImgUnitEn;
    private GameObject MapImgUnitSt;
    private GameObject MapImgUnitKl;
    private GameObject MapImgUnitIt;
    private GameObject MapImgUnitTr;
    private List<GameObject> Copies;
    private List<GameObject> FloorCopies;
    private List<GameObject> WaterCopies;
    private List<GameObject> MapImages;
    //private Stopwatch sw;
    private GameObject MapBase;
    private GameObject MapBaseMU;
    private GameObject MapBaseT;
    private GameObject MapBaseI;
    private GameObject MapBaseC;
    public BaseStair stair;
    public KilnObject Kiln;
    private float MapUnitX;
    private float MapUnitY;
    private Vector2 MapUnitVector;
    private List<Guid> RoomSelect;
    private Dictionary<MapPoint, RoomInformation> Visiblities;
    private Dictionary<Guid, RoomInformation> RoomVisiblities;
    private Dictionary<ObjectType, List<MaptipInformation>> MapTipPool;

    private GameObject AllFloor;
    private GameObject AllWater;

    public Material[] FloorMaterials;
    //private GameObject MapObjectBase;

    //private void Start()
    //{
    //}

    // Use this for initialization
    void Awake()
    {
        List<string> FIList = new List<string>();
        FIList.Add("Floor1");
        FIList.Add("Floor2");
        FIList.Add("Floor3");
        FIList.Add("Floor4");
        FIList.Add("Floor5");
        FIList.Add("Floor6");
        FIList.Add("Floor9");
        List<string> WIList = new List<string>();
        WIList.Add("Wall1");
        WIList.Add("Wall2");
        WIList.Add("Wall4");
        WIList.Add("Wall");
        WIList.Add("Wall5");
        WIList.Add("Wall6");
        WIList.Add("Wall9");
        string fname = "";
        string wname;

        //switch (DungeonInformation.Info.DungeonObjNo)
        //{
        //    case 50001:
        //        Floor2 = GameObject.Find("Floor1");
        //        Wall = GameObject.Find("Wall2");
        //        break;
        //    case 50002:
        //        Floor2 = GameObject.Find("Floor2");
        //        Wall = GameObject.Find("Wall4");
        //        break;
        //    case 50003:
        //        Floor2 = GameObject.Find("Floor3");
        //        Wall = GameObject.Find("Wall");
        //        break;
        //    case 50004:
        //        Floor2 = GameObject.Find("Floor4");
        //        Wall = GameObject.Find("Wall1");
        //        break;
        //    case 50005:
        //        Floor2 = GameObject.Find("Floor5");
        //        Wall = GameObject.Find("Wall5");
        //        break;
        //    case 50006:
        //        Floor2 = GameObject.Find("Floor6");
        //        Wall = GameObject.Find("Wall6");
        //        break;
        //    default:
        //        Floor2 = GameObject.Find("Floor2");
        //        Wall = GameObject.Find("Wall");
        //        break;
        //}
        AllFloor = GameObject.Find("AllFloorMat");
        AllWater = GameObject.Find("AllWater");

        switch (DungeonInformation.Info.DungeonObjNo)
        {
            case 50001:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[2];
                //fname = "Floor1";
                wname = "Wall2";
                break;
            case 50002:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[0];
                //fname = "Floor2";
                wname = "Wall4";
                break;
            case 50003:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[1];
                fname = "Floor3";
                wname = "Wall";
                break;
            case 50004:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[3];
                //fname = "Floor4";
                wname = "Wall1";
                break;
            case 50005:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[4];
                //fname = "Floor5";
                wname = "Wall5";
                break;
            case 50006:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[5];
                //fname = "Floor6";
                wname = "Wall6";
                break;
            case 50008:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[6];
                fname = "Floor9";
                wname = "Wall9";
                break;
            default:
                AllFloor.GetComponent<Renderer>().material = FloorMaterials[1];
                //fname = "Floor2";
                wname = "Wall";
                break;
        }

        if(DungeonInformation.Info.BaseLoadState == LoadStatus.Water)
        {
            AllFloor.SetActive(false);
        }
        else
        {
            AllWater.SetActive(false);
        }

        foreach (string s in FIList)
        {
            if(s == fname)
            {
                Floor2 = GameObject.Find(s);
            }
            else
            {
                //GameObject.Find(s).SetActive(false);
                Destroy(GameObject.Find(s));
            }
        }
        foreach (string s in WIList)
        {
            if (s == wname)
            {
                Wall = GameObject.Find(s);
            }
            else
            {
                //GameObject.Find(s).SetActive(false);
                Destroy(GameObject.Find(s));
            }
        }

        //Floor = GameObject.Find("Floor");
        Water = GameObject.Find("Water");
        //Player = GameObject.Find("SD_unitychan_humanoid");
        MapBase = GameObject.Find("BasePoint");
        MapBaseMU = MapBase.transform.Find("MapobjectPoint").gameObject;
        MapBaseT = MapBase.transform.Find("TrapBasePoint").gameObject;
        MapBaseI = MapBase.transform.Find("ItemBasePoint").gameObject;
        MapBaseC = MapBase.transform.Find("CharacterPoint").gameObject;
        MapImgUnit = ResourceInformation.MapPanel.transform.Find("MapImgUnit").gameObject;
        MapImgUnitPl = ResourceInformation.MapPanel.transform.Find("MapImgUnitPl").gameObject;
        MapImgUnitEn = ResourceInformation.MapPanel.transform.Find("MapImgUnitEn").gameObject;
        MapImgUnitKl = ResourceInformation.MapPanel.transform.Find("MapImgUnitKl").gameObject;
        MapImgUnitSt = ResourceInformation.MapPanel.transform.Find("MapImgUnitSt").gameObject;
        MapImgUnitIt = ResourceInformation.MapPanel.transform.Find("MapImgUnitIt").gameObject;
        MapImgUnitTr = ResourceInformation.MapPanel.transform.Find("MapImgUnitTr").gameObject;
        //MapObjectBase = GameObject.Find("BasePointObject");
        Copies = new List<GameObject>();
        FloorCopies = new List<GameObject>();
        WaterCopies = new List<GameObject>();
        Characters = new List<BaseCharacter>();
        Items = new List<BaseItem>();
        MapImages = new List<GameObject>();
        Traps = new List<BaseTrap>();
        AddCharacters = new List<BaseCharacter>();
        RoomEnemys = new Dictionary<Guid, int>();
        RoomSelect = new List<Guid>();
        Visiblities = new Dictionary<MapPoint, RoomInformation>(new MapPointComparer());
        RoomVisiblities = new Dictionary<Guid, RoomInformation>();
        MapTipPool = new Dictionary<ObjectType, List<MaptipInformation>>(new ObjectTypeComparer());

        foreach(ObjectType t in CommonFunction.EnumObjectTypes)
        {
            MapTipPool.Add(t, new List<MaptipInformation>());
        }

        //sw = new Stopwatch();
    }

    public void Setup(int x, int y)
    {
        X = x;
        Y = y;
        float unit = X > Y ?
            CommonConst.Map.Unit * (55 / (float)X)
            : CommonConst.Map.Unit * (55 / (float)Y);
        MapUnitX = unit;
        MapUnitY = unit;
        MapUnitVector = new Vector2(MapUnitX, MapUnitY);

        AllFloor.transform.localScale = new Vector3(X, AllFloor.transform.localScale.y, Y);
        AllFloor.transform.localPosition = new Vector3(X / 2, AllFloor.transform.localPosition.y, Y / 2);
        AllWater.transform.localScale = new Vector3(X, AllWater.transform.localScale.y, Y);
        AllWater.transform.localPosition = new Vector3(X / 2, AllWater.transform.localPosition.y, Y / 2);
    }

    public void Create(bool isBoss)
    {
        IsVisible = false;
        IsEnemyVisible = false;
        IsItemVisible = false;
        IsTrapVisible = false;
        //foreach (GameObject g in Copies)
        //{
        //    UnityEngine.Object.Destroy(g);
        //}
        foreach (BaseItem g in Items)
        {
            if (g.Type == ObjectType.Stair)
            {
                //UnityEngine.Object.Destroy(g.MapUnit);
                g.MapUnit.OffActive();
            }
            else
            {
                KillObjectNow(g);
                //UnityEngine.Object.Destroy(g.ThisDisplayObject);
                //g.ThisDisplayObject = null;
                ////UnityEngine.Object.Destroy(g.MapUnit);
                //g.MapUnit.OffActive();
            }
        }
        foreach (BaseCharacter g in Characters)
        {
            if (g.Type == ObjectType.Player || g.Type == ObjectType.Kiln)
            {
                //UnityEngine.Object.Destroy(g.MapUnit);
                g.MapUnit.OffActive();
            }
            else
            {
                KillObjectNow(g);
                //UnityEngine.Object.Destroy(g.ThisDisplayObject);
                //g.ThisDisplayObject = null;
                ////UnityEngine.Object.Destroy(g.MapUnit);
                //g.MapUnit.OffActive();
            }
        }
        foreach (BaseTrap g in Traps)
        {
            KillObjectNow(g);

        }
        //if (CommonFunction.IsNull(Dungeon) == false)
        //{
        //    for (int i = 0; i < Dungeon.DungeonMap.Width; i++)
        //    {
        //        for (int j = 0; j < Dungeon.DungeonMap.Height; j++)
        //        {
        //            DungeonUnitInfo info = Dungeon.DungeonMap.Get(i, j);
        //            if (CommonFunction.IsNull(info.MapImg) == false)
        //            {
        //                UnityEngine.Object.Destroy(info.MapImg);
        //            }
        //        }
        //    }
        //}
        int CopiesOldIndex = 0;
        int FloorCopiesOldIndex = 0;
        int WaterCopiesOldIndex = 0;
        List<GameObject> CopiesOld = Copies;
        List<GameObject> FloorCopiesOld = FloorCopies;
        List<GameObject> WaterCopiesOld = WaterCopies;
        Copies = new List<GameObject>();
        FloorCopies = new List<GameObject>();
        WaterCopies = new List<GameObject>();
        Items.Clear();
        Characters.Clear();
        Traps.Clear();

        Visiblities.Clear();
        RoomVisiblities.Clear();

        //迷路マップを作成
        if(CommonFunction.IsNull(Dungeon) == true)
        {
            Dungeon = new DungeonCreateModel();
        }
        Dungeon.RoomDivideProb = DungeonInformation.Info.Prob;
        
        if(isBoss == true)
        {
            Dungeon.CreateBossRoom(X, Y, true);
        }
        else
        {
            Dungeon.CreateDungeon(X, Y, true);
        }

        ItemMap = new Layer2D<BaseItem>(X, Y);
        CharacterMap = new Layer2D<BaseCharacter>(X, Y);
        TrapMap = new Layer2D<BaseTrap>(X, Y);

        RoomEnemys.Clear();
        foreach (Guid g in Dungeon.Rooms.Keys)
        {
            RoomEnemys.Add(g, 0);
        }

        //マップに従って壁を配置
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                DungeonUnitInfo unit = Dungeon.DungeonMap.Get(i, j);
                //対象が壁だった場合は壁オブジェクトを配置
                if (unit.State == LoadStatus.Wall)
                {
                    CopiesOldIndex = CreateMapObject(Wall, unit, i, j, PosY, CopiesOldIndex, CopiesOld, Copies);

                    //上と左が壁の場合は影を落とさない
                    DungeonUnitInfo t = Dungeon.DungeonMap.Get(i, j + 1);
                    DungeonUnitInfo l = Dungeon.DungeonMap.Get(i - 1, j);
                    if ((CommonFunction.IsNull(t) == true || t.State == LoadStatus.Wall)
                        && (CommonFunction.IsNull(l) == true || l.State == LoadStatus.Wall))
                    {
                        unit.FieldObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    }
                    else
                    {
                        unit.FieldObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    }
                }
                //対象が床だった場合は床オブジェクトを配置
                else if (IsFloor(unit) == true)
                {

                    if (DungeonInformation.Info.BaseLoadState == LoadStatus.Water)
                    {
                        FloorCopiesOldIndex = CreateMapObject(Floor2, unit, i, j, PosY, FloorCopiesOldIndex, FloorCopiesOld, FloorCopies);
                    }
                }
                //水だった場合は水を配置
                //else if (unit.State == LoadStatus.Water)
                //{
                //    WaterCopiesOldIndex = CreateMapObject(Water, unit, i, j, 2.3f, WaterCopiesOldIndex, WaterCopiesOld, WaterCopies);
                //}
            }

        }

        //残ったブロックは削除する
        for (int i = CopiesOldIndex; i < CopiesOld.Count; i++)
        {
            CopiesOld[i].transform.localPosition = dammyPos;
            Copies.Add(CopiesOld[i]);
        }
        //残ったブロックは削除する
        for (int i = FloorCopiesOldIndex; i < FloorCopiesOld.Count; i++)
        {
            FloorCopiesOld[i].transform.localPosition = dammyPos;
            FloorCopies.Add(FloorCopiesOld[i]);
        }
        //残ったブロックは削除する
        for (int i = WaterCopiesOldIndex; i < WaterCopiesOld.Count; i++)
        {
            WaterCopiesOld[i].transform.localPosition = dammyPos;
            WaterCopies.Add(WaterCopiesOld[i]);
        }


        IsRemap = true;
    }
    static readonly Vector3 dammyPos = new Vector3(0, 250, 0);
    static readonly Vector3 dammyPosMap = new Vector3(float.MinValue, float.MinValue, 0);
    private int CreateMapObject(GameObject parentI, DungeonUnitInfo unit, int i, int j, float posY, int copiesOldIndex, List<GameObject> copiesOld, List<GameObject> copies)
    {

        GameObject copy;
        //ブロックが残っていれば再利用する
        if (copiesOld.Count > copiesOldIndex)
        {
            copy = copiesOld[copiesOldIndex];
        }
        //ブロックが残っていなければ新規作成
        else
        {
            copy = UnityEngine.Object.Instantiate(parentI, ResourceInformation.DungeonObject.transform);
        }
        copiesOldIndex++;

        //マップ情報に参照を保存
        unit.FieldObject = copy;

        copy.transform.localPosition = new Vector3(i * PositionUnit,
            posY,
            j * PositionUnit);
        copies.Add(copy);

        return copiesOldIndex;
    }

    /// <summary>
    /// 階段の作成
    /// </summary>
    public void SetStair(GameObject g, int x, int y)
    {
        CreateStair(g, x, y);

        //階段を追加
        AddNewItem(stair);
    }

    public void CreateStair(GameObject g, int x, int y)
    {

        stair = new BaseStair();
        stair.Initialize();
        stair.ThisDisplayObject = g;
        stair.SetThisDisplayObject(x, y);
    }

    #region マップ管理
    public void UpdateMap(PlayerCharacter player)
    {
        //再描画許可がなければ描画しない
        if (IsRemap == false)
        {
            return;
        }
        IsRemap = false;
        //マップに従ってマップを描画
        for (int i = 0; i < Dungeon.DungeonMap.Width; i++)
        {
            for (int j = 0; j < Dungeon.DungeonMap.Height; j++)
            {
                DungeonUnitInfo info = Dungeon.DungeonMap.Get(i, j);

                //道と部屋の場合以外は終了
                if (info.State != LoadStatus.Load && info.State != LoadStatus.Room
                    && info.State != LoadStatus.RoomEntrance && info.State != LoadStatus.RoomExit)
                {
                    continue;
                }

                //クリアになっていればマップ描画
                if (info.IsClear == true || IsVisible == true)
                {
                    SetActiveTrue(info.MapImg);
                    //info.MapImg.SetActive(true);
                }
                else
                {
                    SetActiveFalse(info.MapImg);
                    //info.MapImg.SetActive(false);
                }
            }
        }
        //装備による可視性を取得
        bool evitem = player.IsEnemyVisible();
        bool ivitem = player.IsItemVisible();
        bool tvitem = player.IsTrapVisible();

        //マップ上にユニットを描画
        var enumerator = Characters.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseObject obj = enumerator.Current;

            //描画になっていればマップ描画
            if (obj.IsMapDisplay == true || IsEnemyVisible == true || evitem == true)
            {
                //対象位置にオブジェクトを移動
                obj.MapUnit.Maptip.transform.localPosition = new Vector3(CommonConst.Map.StartMergin + MapUnitX * obj.CurrentPoint.X,
                        CommonConst.Map.StartMergin + MapUnitY * obj.CurrentPoint.Y,
                        CommonConst.Map.Z);
                
                obj.MapUnit.SetActive(true);
                //SetActiveTrue(obj.MapUnit);
                //obj.MapUnit.SetActive(true);
            }
            else
            {
                obj.MapUnit.SetActive(false);
                //SetActiveFalse(obj.MapUnit);
                //obj.MapUnit.SetActive(false);
            }
        }


        //マップ上にアイテムを描画
        var enumerator2 = Items.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            BaseObject obj = enumerator2.Current;

            //描画になっていればマップ描画
            if (obj.IsMapDisplay == true || IsItemVisible == true || ivitem == true)
            {
                //対象位置にオブジェクトを移動
                obj.MapUnit.Maptip.transform.localPosition = new Vector3(CommonConst.Map.StartMergin + MapUnitX * obj.CurrentPoint.X,
                        CommonConst.Map.StartMergin + MapUnitY * obj.CurrentPoint.Y,
                        CommonConst.Map.Z);

                obj.MapUnit.SetActive(true);
                //SetActiveTrue(obj.MapUnit);
                //obj.MapUnit.SetActive(true);
            }
            else
            {
                obj.MapUnit.SetActive(false);
                //SetActiveFalse(obj.MapUnit);
                //obj.MapUnit.SetActive(false);
            }
        }


        //マップ上にトラップを描画
        var enumerator3 = Traps.GetEnumerator();

        while (enumerator3.MoveNext())
        {
            BaseObject obj = enumerator3.Current;
            //描画になっていればマップ描画
            if (obj.IsMapDisplay == true || IsTrapVisible == true || tvitem == true)
            {
                //対象位置にオブジェクトを移動
                obj.MapUnit.Maptip.transform.localPosition = new Vector3(CommonConst.Map.StartMergin + MapUnitX * obj.CurrentPoint.X,
                        CommonConst.Map.StartMergin + MapUnitY * obj.CurrentPoint.Y,
                        CommonConst.Map.Z);

                obj.MapUnit.SetActive(true);
                //SetActiveTrue(obj.MapUnit);
                //obj.MapUnit.SetActive(true);
            }
            else
            {
                obj.MapUnit.SetActive(false);
                //SetActiveFalse(obj.MapUnit);
                //obj.MapUnit.SetActive(false);
            }
        }

#if UNITY_EDITOR

        int flr = 0;
        for (int i = 0; i < Dungeon.DungeonMap.Width; i++)
        {
            for (int j = 0; j < Dungeon.DungeonMap.Height; j++)
            {
                if (IsFloor(Dungeon.DungeonMap.Get(i, j)))
                {
                    flr++;
                }
            }
        }
        //    GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
        //MapBaseMU.transform.childCount.ToString() + "," + MapImages.Count + "," + flr;
        //CommonFunction.CammaString(
        //Characters.Count, MapTipPool[ObjectType.Enemy].Count,
        //Items.Count, MapTipPool[ObjectType.Item].Count,
        //Traps.Count, MapTipPool[ObjectType.Trap].Count);

        //long monoUsed = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
        //long monoSize = UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
        //long totalUsed = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong(); // == Profiler.usedHeapSize
        //long totalSize = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
        //GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
        //     string.Format(
        //    "mono:{0}/{1} kb({2:f1}%)\n" +
        //    "total:{3}/{4} kb({5:f1}%)\n",
        //    monoUsed / 1024, monoSize / 1024, 100.0 * monoUsed / monoSize,
        //    totalUsed / 1024, totalSize / 1024, 100.0 * totalUsed / totalSize);
#endif
    }

    /// <summary>
    /// 対象のポイントからマップの描画対象を更新する
    /// 部屋に入った場合は部屋全体を表示対象とする
    /// </summary>
    /// <param name="point"></param>
    public void UpdateWriteTarget(MapPoint point)
    {
        //対象のマップチップを取得する
        DungeonUnitInfo unit = Dungeon.DungeonMap.Get(point.X, point.Y);

        //クリアになっていなかった場合（初めて踏み込んだ場合）
        //if(unit.IsClear == false)
        //{

        //すべてのキャラクターとアイテムの描画をオフに
        var enumerator1 = Characters.GetEnumerator();
        var enumerator2 = Items.GetEnumerator();

        while (enumerator1.MoveNext())
        {
            BaseObject c = enumerator1.Current;
            c.IsMapDisplay = false;
        }
        while (enumerator2.MoveNext())
        {
            BaseObject c = enumerator2.Current;
            c.IsMapDisplay = false;
        }
        //    foreach (BaseItem item in Items.Values)
        //{
        //    item.IsMapDisplay = false;
        //}
        //foreach (BaseCharacter c in Characters.Values)
        //{
        //    c.IsMapDisplay = false;
        //}

        MapPoint lefttop = null;
        MapPoint rightbottom = null;

        //道の場合
        if (unit.State == LoadStatus.Load || unit.State == LoadStatus.RoomExit)
        {
            //unit.IsClear = true;
            lefttop = MapPoint.Get(point.X - 1, point.Y - 1);
            rightbottom = MapPoint.Get(point.X + 1, point.Y + 1);
            //対象の区画範囲+1をクリアにする
            for (int i = lefttop.X; i <= rightbottom.X; i++)
            {
                for (int j = lefttop.Y; j <= rightbottom.Y; j++)
                {
                    Dungeon.DungeonMap.Get(i, j).IsClear = true;
                }
            }
        }
        else
        //部屋の場合
        if (unit.State == LoadStatus.Room || unit.State == LoadStatus.RoomEntrance
            )
        {
            //対象の区画情報を取得
            RoomInformation info = Dungeon.Rooms[unit.RoomName];
            lefttop = MapPoint.Get(info.Left - 1, info.Top - 1);
            rightbottom = MapPoint.Get(info.Right, info.Bottom);

            //対象の区画範囲+1をクリアにする
            for (int i = info.Left - 1; i < info.Right + 1; i++)
            {
                for (int j = info.Top - 1; j < info.Bottom + 1; j++)
                {
                    Dungeon.DungeonMap.Get(i, j).IsClear = true;
                }
            }
        }

        //アイテムの描画を最新化
        //foreach (BaseItem item in Items.Values)
        //{
        //    //if (lefttop.X < item.CurrentPoint.X && item.CurrentPoint.X < rightbottom.X
        //    //    && lefttop.Y < item.CurrentPoint.Y && item.CurrentPoint.Y < rightbottom.Y)
        //    //{
        //    //    item.IsMapDisplay = true;
        //    //}
        //    //else
        //    //{
        //    //    item.IsMapDisplay = false;
        //    //}
        //}
        //キャラクターの描画を最新化
        enumerator1 = Characters.GetEnumerator();
        while (enumerator1.MoveNext())
        {
            BaseObject c = enumerator1.Current;
            if (lefttop.X <= c.CurrentPoint.X && c.CurrentPoint.X <= rightbottom.X
                && lefttop.Y <= c.CurrentPoint.Y && c.CurrentPoint.Y <= rightbottom.Y)
            {
                c.IsMapDisplay = true;
            }
            else
            {
                c.IsMapDisplay = false;
            }
        }
        //    foreach (BaseCharacter c in Characters.Values)
        //{
        //    if (lefttop.X <= c.CurrentPoint.X && c.CurrentPoint.X <= rightbottom.X
        //        && lefttop.Y <= c.CurrentPoint.Y && c.CurrentPoint.Y <= rightbottom.Y)
        //    {
        //        c.IsMapDisplay = true;
        //    }
        //    else
        //    {
        //        c.IsMapDisplay = false;
        //    }
        //}

        //対象位置がクリアになっていれば階段をクリアにする

        if (Items.Contains(stair) &&
            Dungeon.DungeonMap.Get(stair.CurrentPoint.X, stair.CurrentPoint.Y).IsClear == true)
        {
            stair.IsMapDisplay = true;
        }
        else
        {
            stair.IsMapDisplay = false;
        }
        //対象位置がクリアになっていれば窯をクリアにする
        if (Kiln.IsActive == true &&
            Dungeon.DungeonMap.Get(Kiln.CurrentPoint).IsClear == true)
        {
            Kiln.IsMapDisplay = true;
        }
    }

    /// <summary>
    /// ダンジョン情報に対応したマップを書く
    /// </summary>
    public void WriteMap()
    {
        int CopiesOldIndex = 0;
        List<GameObject> Olds = MapImages;
        MapImages = new List<GameObject>();

        //オブジェクトを初期化
        //foreach (Image g in Maps)
        //{
        //    UnityEngine.Object.Destroy(g);
        //}


        //マップに従ってマップを描画
        for (int i = 0; i < Dungeon.DungeonMap.Width; i++)
        {
            for (int j = 0; j < Dungeon.DungeonMap.Height; j++)
            {
                DungeonUnitInfo info = Dungeon.DungeonMap.Get(i, j);
                //まだ描画されてなければマップ描画
                if (info.IsWrite == true)
                {
                    continue;
                }
                //道と部屋の場合のみ描画
                if (info.State == LoadStatus.Load || info.State == LoadStatus.Room
                    || info.State == LoadStatus.RoomEntrance || info.State == LoadStatus.RoomExit)
                {

                    GameObject copy;
                    //ブロックが残っていれば再利用する
                    if (Olds.Count > CopiesOldIndex)
                    {
                        copy = Olds[CopiesOldIndex];
                        if(Olds[i].activeSelf == false)
                        {
                            Olds[i].SetActive(true);
                        }
                    }
                    //ブロックが残っていなければ新規作成
                    else
                    {
                        copy = UnityEngine.Object.Instantiate(MapImgUnit, MapBaseMU.transform);
                    }
                    CopiesOldIndex++;

                    info.MapImg = copy;
                    MapImages.Add(info.MapImg);

                    //MapBaseの配下に配置
                    //info.MapImg.transform.SetParent(MapBase.transform, false);

                    //位置の決定
                    info.MapImg.transform.localPosition = new Vector3(CommonConst.Map.StartMergin + MapUnitX * i,
                        CommonConst.Map.StartMergin + MapUnitY * j,
                        CommonConst.Map.Z);
                    info.MapImg.GetComponent<RectTransform>().sizeDelta = MapUnitVector;
                    info.MapImg.SetActive(false);
                    //Maps.Add(info.MapImg);

                    info.IsWrite = true;
                }
            }
        }


        //残ったブロックは削除する
        for (int i = CopiesOldIndex; i < Olds.Count; i++)
        {
            //Olds[i].transform.localPosition = dammyPosMap;
            CommonFunction.SetActive(Olds[i], false);
            MapImages.Add(Olds[i]);
        }

#if UNITY_EDITOR

        int flr = 0;
        for (int i = 0; i < Dungeon.DungeonMap.Width; i++)
        {
            for (int j = 0; j < Dungeon.DungeonMap.Height; j++)
            {
                if (IsFloor(Dungeon.DungeonMap.Get(i, j)))
                {
                    flr++;
                }
            }
        }
        //    GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
        //MapBaseMU.transform.childCount.ToString() + "," + MapImages.Count + "," + flr;
        GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
            Characters.Count + "," + MapTipPool[ObjectType.Enemy].Count.ToString() + "," + flr;
#endif
        //残ったブロックは削除する
        //for (int i = CopiesOldIndex; i < Olds.Count; i++)
        //{
        //    Destroy(Olds[i]);
        //}
        IsRemap = true;
    }

    /// <summary>
    /// 暗闇状態を作る
    /// </summary>
    public void OnDark()
    {
        GameStateInformation.Info.IsDark = true;
        ResourceInformation.DungeonStruct.transform.localPosition = new Vector3(0, 250, 0);
    }

    /// <summary>
    /// 暗闇状態を切る
    /// </summary>
    public void OffDark()
    {
        GameStateInformation.Info.IsDark = false;
        ResourceInformation.DungeonStruct.transform.localPosition = Vector3.zero;
    }


    public void SetUpFieldShadow(MapPoint c)
    {

        ////マップの影
        //for (int i = 0; i < X; i++)
        //{
        //    for (int j = 0; j < Y; j++)
        //    {
        //        int dist = c.DistanceAbs(i, j);
        //        DungeonUnitInfo u = Dungeon.DungeonMap.Get(i, j);
        //        MeshRenderer m = u.FieldObject.GetComponent<MeshRenderer>();

        //        //影を付ける
        //        if (dist <= 6)
        //        {
        //            if (m.receiveShadows != true)
        //            {
        //                if (u.State == LoadStatus.Wall)
        //                {
        //                    //上と左が壁の場合は影を落とさない
        //                    DungeonUnitInfo t = Dungeon.DungeonMap.Get(i, j + 1);
        //                    DungeonUnitInfo l = Dungeon.DungeonMap.Get(i - 1, j);
        //                    if ((CommonFunction.IsNull(t) == true || t.State == LoadStatus.Wall)
        //                        && (CommonFunction.IsNull(l) == true || l.State == LoadStatus.Wall))
        //                    {
        //                    }
        //                    else
        //                    {
        //                        m.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        //                    }
        //                    m.receiveShadows = true;
        //                }
        //                else if (u.State == LoadStatus.Load || u.State == LoadStatus.Room || u.State == LoadStatus.RoomEntrance || u.State == LoadStatus.RoomExit)
        //                {
        //                    m.receiveShadows = true;
        //                }
        //            }
        //        }
        //        //影を付けない
        //        else
        //        {
        //            if (m.receiveShadows != false)
        //            {
        //                if (u.State == LoadStatus.Wall)
        //                {
        //                    m.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //                    m.receiveShadows = false;
        //                }
        //                else if (u.State == LoadStatus.Load || u.State == LoadStatus.Room || u.State == LoadStatus.RoomEntrance || u.State == LoadStatus.RoomExit)
        //                {
        //                    m.receiveShadows = false;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    #endregion マップ管理


    #region キャラクター移動管理

    public BaseCharacter GetAttackTargetCharacter(MapPoint startpoint, ushort renge, CharacterDirection dir)
    {
        //初期の調査ポイントを取得
        MapPoint target = startpoint.Add(CommonFunction.CharacterDirectionVector[dir]);

        for (int i = 0; i < renge; i++)
        {

            if (dir == CharacterDirection.BottomLeft
                || dir == CharacterDirection.BottomRight
                || dir == CharacterDirection.TopLeft
                || dir == CharacterDirection.TopRight)
            {
                //斜め方向なら対象に向かって左右の枠に壁がないかチェック
                //□□□□□□□
                //□敵■□□□□
                //□■□■□□□
                //□□■自□□□
                //□□□□□□□
                //左後ろが壁かチェック
                if (Dungeon.DungeonMap.Get(
                target.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseLeftDirectionVector[dir]]
                )).State == LoadStatus.Wall)
                {
                    return null;
                }
                //右後ろが壁かチェック
                if (Dungeon.DungeonMap.Get(
                    target.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseRightDirectionVector[dir]]
                    )).State == LoadStatus.Wall)
                {
                    return null;
                }
            }

            BaseCharacter chara = CharacterMap.Get(target.X, target.Y);

            if (CommonFunction.IsNull(chara) == false)
            {
                return chara;
            }

            //次のポイントを取得
            target = target.Add(CommonFunction.CharacterDirectionVector[dir]);
        }

        //何も取得できなかったらNull終了
        return null;
    }

    //public virtual bool CanAttack(MapPoint curPoint, MapPoint target, int fireRange, CharacterDirection dir)
    //{
    //    //自オブジェクトとの距離を取得
    //    int dist = curPoint.DistanceAbs(target);
    //    bool isLine = curPoint.IsStraightLine(target);
    //    //射程内かつ直線状にいれば攻撃可
    //    if (dist <= fireRange && isLine)
    //    {
    //        if (dir == CharacterDirection.BottomLeft
    //            || dir == CharacterDirection.BottomRight
    //            || dir == CharacterDirection.TopLeft
    //            || dir == CharacterDirection.TopRight)
    //        {
    //            //斜め方向なら対象に向かって左右の枠に壁がないかチェック
    //            //□□□□□□□
    //            //□敵■□□□□
    //            //□■□■□□□
    //            //□□■自□□□
    //            //□□□□□□□
    //            //□□□□□□□
    //            MapPoint vector = CommonFunction.CharacterDirectionVector[dir];
    //            //1マス先を取得
    //            MapPoint checkpoint = curPoint.Add(vector);
    //            for (int i = 1; i <= dist; i++)
    //            {
    //                //左後ろが壁かチェック
    //                if (Dungeon.DungeonMap.Get(
    //                    checkpoint.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseLeftDirectionVector[dir]]
    //                    )).State == LoadStatus.Wall)
    //                {
    //                    return false;
    //                }
    //                //右後ろが壁かチェック
    //                if (Dungeon.DungeonMap.Get(
    //                    checkpoint.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseRightDirectionVector[dir]]
    //                    )).State == LoadStatus.Wall)
    //                {
    //                    return false;
    //                }
    //                checkpoint = checkpoint.Add(vector);
    //            }
    //        }
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}


    public void SetUpCharacterMap()
    {
        //キャラマップを作成する
        CharacterMap.Fill(null);

        var enumerator = Characters.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseCharacter c = enumerator.Current;
            CharacterMap.Set(c.CurrentPoint.X, c.CurrentPoint.Y, c);
        }

        //現在のキャラをマップに反映する
        //foreach (BaseCharacter item in Characters.Values)
        //{
        //    CharacterMap.Set(item.CurrentPoint.X, item.CurrentPoint.Y, item);
        //}
    }

    /// <summary>
    /// 開始地点から最後の壁、ないしはキャラ
    /// </summary>
    /// <returns></returns>
    public MapPoint GetHitRangePoint(MapPoint start, CharacterDirection dir, ushort throwRange)
    {

        //チェック方向のベクトルを取得
        MapPoint vector = CommonFunction.CharacterDirectionVector[dir];

        MapPoint tarPoint = start;
        ushort distance = 0;
        while (true)
        {
            distance++;
            MapPoint next = MapPoint.Get(tarPoint.X + vector.X, tarPoint.Y + vector.Y);
            //マップ外周に達するとnullになる
            if (CommonFunction.IsNull(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
            {
                break;
            }

            //対象位置が壁の場合は終了
            if (IsWall(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
            {
                return next;
            }
            //対象位置に誰かいた場合は終了
            else if (CommonFunction.IsNull(CharacterMap.Get(next.X, next.Y)) == false)
            {
                return next;
            }
            //投擲限界距離に達したら終了
            else if (distance >= throwRange)
            {
                return next;
            }

            //ポイントを更新して続ける
            tarPoint = next;
        }

        return tarPoint;
    }

    /// <summary>
    /// 開始地点から対象の方向に向けてアイテムを投げた際にヒットしたポイントを返す
    /// 何かのキャラクター、ないしは壁
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public MapPoint GetHitItemThrow(MapPoint start, CharacterDirection dir, ushort throwRange, bool isTunnel, out bool isVanish)
    {
        isVanish = false;
        //チェック方向のベクトルを取得
        MapPoint vector = CommonFunction.CharacterDirectionVector[dir];

        MapPoint tarPoint = start;
        ushort distance = 0;
        while (true)
        {
            distance++;
            MapPoint next = MapPoint.Get(tarPoint.X + vector.X, tarPoint.Y + vector.Y);
            //マップ外周に達するとnullになる
            if (CommonFunction.IsNull(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
            {
                break;
            }

            //Tunnelの場合
            if (isTunnel == true)
            {
                //対象位置に誰かいた場合は終了
                if (CommonFunction.IsNull(CharacterMap.Get(next.X, next.Y)) == false)
                {
                    return next;
                }
            }
            //通常投擲の場合
            else
            {
                //対象位置が壁の場合は終了
                if (IsWall(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
                {
                    return tarPoint;
                }
                //対象位置に誰かいた場合は終了
                else if (CommonFunction.IsNull(CharacterMap.Get(next.X, next.Y)) == false)
                {
                    return next;
                }
                //投擲限界距離に達したら終了
                else if (distance >= throwRange)
                {
                    return tarPoint;
                }
            }

            //ポイントを更新して続ける
            tarPoint = next;
        }

        //遠投の場合は抜ける可能性あり
        isVanish = true;
        return tarPoint;
    }

    /// <summary>
    /// 開始地点から対象の方向に向けて吹き飛んだ際にヒットしたポイントを返す
    /// 何かのキャラクター、ないしは壁
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public MapPoint GetBlow(MapPoint start, CharacterDirection dir, out bool isWall, int dist = 10)
    {
        isWall = false;
        //チェック方向のベクトルを取得
        MapPoint vector = CommonFunction.CharacterDirectionVector[dir];

        int cnt = 0;

        MapPoint tarPoint = start;
        while (true)
        {
            if (cnt >= dist)
            {
                return tarPoint;
            }
            cnt++;
            MapPoint next = MapPoint.Get(tarPoint.X + vector.X, tarPoint.Y + vector.Y);
            if (CommonFunction.IsNull(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
            {
                break;
            }

            //対象位置が壁の場合は終了
            if (IsWall(Dungeon.DungeonMap.Get(next.X, next.Y)) == true)
            {
                isWall = true;
                return tarPoint;
            }
            //対象位置に誰かいた場合は終了
            else if (CommonFunction.IsNull(CharacterMap.Get(next.X, next.Y)) == false)
            {
                isWall = true;
                return tarPoint;
            }

            //ポイントを更新して続ける
            tarPoint = next;
        }

        //基本的に抜けることはない
        return null;
    }

    /// <summary>
    /// キャラクターの配置
    /// </summary>
    public void AddNewCharacter(BaseCharacter chara)
    {
        MaptipInformation img = GetMapTip(chara.Type);
        //マップへの追加
        //キャラチップによって分岐をかける

        chara.MapUnit = img;

        Characters.Add(chara);
    }

    /// <summary>
    /// キャラクターの配置
    /// </summary>
    public void AddKiln(KilnObject chara)
    {
        if (CommonFunction.IsNull(chara.MapUnit) == true)
        {
            MaptipInformation img = GetMapTip(chara.Type);
            //マップへの追加
            //キャラチップによって分岐をかける

            chara.MapUnit = img;
        }
        else
        {
            chara.MapUnit.OnActive();
        }

        Characters.Add(chara);
    }



    public void RemoveCharacter(BaseCharacter chara)
    {
        //Destroy(chara.MapUnit);
        //chara.MapUnit.OffActive();
        //chara.MapUnit = null;
        Characters.Remove(chara);
    }

    public void DeathUpdate()
    {
        //IEnumerator<BaseCharacter> list = Characters.Where(c => c.Value.IsDeath == true).Select(c => c.Value).GetEnumerator();
        //if(CommonFunction.IsNull(list) == false)
        //{
        //    while (list.MoveNext())
        //    {
        //        BaseCharacter c = list.Current;
        //        RemoveCharacter(c);
        //        UnityEngine.Debug.Log(c.Name);
        //    }
        //}
        BaseCharacter[] list = Characters.Where(c => c.IsDeath == true).ToArray();
        foreach (BaseCharacter c in list)
        {
            if (c.Type == ObjectType.Enemy)
            {
                BaseEnemyCharacter enemy = ((BaseEnemyCharacter)c);
                if (CommonFunction.IsNull(enemy.TempEnemyUseItem) == false)
                {
                    PutItem(enemy.TempEnemyUseItem, enemy.CurrentPoint, enemy.CurrentPoint);

                    enemy.TempEnemyUseItem.ResetObjectPosition();

                    enemy.TempEnemyUseItem = null;
                }
            }
            RemoveCharacter(c);
        }
    }

    /// <summary>
    /// 上下左右への移動チェック（斜めは別）
    /// </summary>
    /// <param name="dungeon"></param>
    /// <returns></returns>
    public bool CheckCharacterMoveUDLR(MapPoint target)
    {
        //移動先に誰もいない　かつ　道だったらtrue
        if (CheckNoneCharacter(target) == false
            && IsDoNotMove(Dungeon.DungeonMap.Get(target.X, target.Y)) == false)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 斜めへの移動チェック
    /// </summary>
    /// <param name="dungeon"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="rote"></param>
    /// <param name="leftside"></param>
    /// <param name="rightside"></param>
    /// <param name="isChange"></param>
    /// <returns></returns>
    public bool CheckCharacterMoveSlanting(MapPoint target, MapPoint leftside, MapPoint rightside)
    {

        //移動先に誰もいない　かつ　壁でなければtrue
        //かつ　通り抜ける角がブロックでなければtrue
        if (CheckNoneCharacter(target) == false
            && IsDoNotMove(Dungeon.DungeonMap.Get(target.X, target.Y)) == false
            && Dungeon.DungeonMap.Get(leftside.X, leftside.Y).State != LoadStatus.Wall
            && Dungeon.DungeonMap.Get(rightside.X, rightside.Y).State != LoadStatus.Wall
            )
        {
            return true;
        }
        return false;
    }

    public MapPoint MoveCharacter(MapPoint target, BaseCharacter chara)
    {
        chara.BeforePoint = chara.CurrentPoint;
        chara.CurrentPoint = target;
        chara.MoveTargetPoint = CommonConst.SystemValue.VecterDammy;
        //移動前の位置をNoneに
        //Dungeon.DungeonMap.Get(currentPoint.X, currentPoint.Y).CharInfo = CommonConst.Char.None;
        ////移動後の位置を自分に
        //Dungeon.DungeonMap.Get(target.X, target.Y).CharInfo = _charNo;
        return target;
    }

    /// <summary>
    /// ターゲットにキャラクターがいないことを確認する
    /// </summary>
    /// <param name="checkTarget"></param>
    /// <returns>いなければFalse</returns>
    private bool CheckNoneCharacter(MapPoint checkTarget)
    {

        var enumerator = Characters.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseObject obj = enumerator.Current;
            if (obj.CurrentPoint.Equal(checkTarget) == true)
            {
                return true;
            }
        }
        //    foreach (BaseCharacter a in Characters.Values)
        //{
        //    if (a.CurrentPoint.Equal(checkTarget) == true)
        //    {
        //        return true;
        //    }
        //}
        return false;
    }

    /// <summary>
    /// 対象ポイント付近にいるキャラを取得
    /// </summary>
    /// <param name="mp"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
    public List<BaseCharacter> GetNearCharacters(MapPoint mp, int dist, bool containmp = false)
    {
        List<BaseCharacter> list = new List<BaseCharacter>();
        for (int i = -dist; i <= dist; i++)
        {
            for (int j = -dist; j <= dist; j++)
            {
                if (containmp == false && (i == 0 && j == 0))
                {
                    continue;
                }
                BaseCharacter c = CharacterMap.Get(mp.X + i, mp.Y + j);

                if (CommonFunction.IsNull(c) == false)
                {
                    list.Add(c);
                }
            }
        }

        return list;
    }

    List<BaseCharacter> insightList = new List<BaseCharacter>();
    /// <summary>
    /// 視界内にいるキャラを取得
    /// </summary>
    /// <param name="mp"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
    public List<BaseCharacter> GetInsightCharacters(MapPoint mp, RoomInformation visible, bool containmp = false)
    {
        insightList.Clear();

        foreach (BaseCharacter c in Characters)
        {
            if (visible.Left <= c.CurrentPoint.X
                && c.CurrentPoint.X <= visible.Right
                && visible.Top <= c.CurrentPoint.Y
                && c.CurrentPoint.Y <= visible.Bottom)
            {
                if (containmp == false && c.CurrentPoint.Equal(mp) == true)
                {
                    continue;
                }
                insightList.Add(c);
            }
        }
        return insightList;
    }

    /// <summary>
    /// 対象ポイント内に壁があるかどうか
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool IsExistWall(MapPoint point,int dist)
    {
        for (int i = -dist; i <= dist; i++)
        {
            //対象ポイントの上下5マスをチェックする
            for (int j = -dist; j <= dist; j++)
            {
                if(CommonFunction.IsNull(Dungeon.DungeonMap.Get(point.X + i, point.Y + j)) == true
                    || Dungeon.DungeonMap.Get(point.X + i, point.Y + j).State == LoadStatus.Water
                    || Dungeon.DungeonMap.Get(point.X + i, point.Y + j).State == LoadStatus.Wall)
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 対象ポイントから最も近い陸地を取得
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public MapPoint GetNearFloor(MapPoint point)
    {
        for (int dist = 1; dist < X; dist++)
        {

            //対象ポイントの上下5マスをチェックする
            //for (int i = point.X - dist - 1; point.X + dist - 1 > i; i++)
            for (int i = 0; dist > i; i++)
            {
                int tarx = point.X + i;
                int tary = point.Y - dist;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }

                tarx = point.X + i;
                tary = point.Y + dist;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
                tarx = point.X - i;
                tary = point.Y - dist;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }

                tarx = point.X - i;
                tary = point.Y + dist;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
            }

            //対象ポイントの左右5マスをチェックする
            //for (int i = point.Y - dist - 1; point.Y + dist - 1 > i; i++)
            for (int i = 0; dist >= i; i++)
            {
                int tarx = point.X - dist;
                int tary = point.Y + i;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
                tarx = point.X + dist;
                tary = point.Y + i;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
                tarx = point.X - dist;
                tary = point.Y - i;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
                tarx = point.X + dist;
                tary = point.Y - i;
                if (IsFloor(Dungeon.DungeonMap.Get(tarx, tary)) == true)
                {
                    return MapPoint.Get(tarx, tary);
                }
            }
        }

        return null;
    }

    ///対象ポイントを中心に最も近い空いている道位置を探す
    public MapPoint GetCharacterEmptyTarget(MapPoint point)
    {
        //対象のポイントがfalseならそれで終わり
        foreach (MapPoint p in PutMapPointPri)
        {
            MapPoint temp;

            //チェック
            temp = CheckPointCharacter(point.X + p.X, point.Y + p.Y);
            if (temp != null)
            {
                return temp;
            }
        }


        return null;
    }


    private MapPoint CheckPointCharacter(int x, int y)
    {

        if (IsFloor(Dungeon.DungeonMap.Get(x, y)) == true)
        {
            //対象のポイントが空いていればそれで終わり
            if (CommonFunction.IsNull(CharacterMap.Get(x, y)) == true)
            {
                return MapPoint.Get(x, y);
            }
        }
        return null;
    }

    public BaseCharacter[] GetRoomCharacters(PlayerCharacter pl)
    {
        RoomInformation visible = GetVisibility(pl.CurrentPoint);

        //視界内のターゲットを取得(自分以外)
        BaseCharacter[] targets = this.Characters.Where(i => visible.Left <= i.CurrentPoint.X
            && i.CurrentPoint.X <= visible.Right
            && visible.Top <= i.CurrentPoint.Y
            && i.CurrentPoint.Y <= visible.Bottom
            && i.Name != pl.Name
            && i.Type != ObjectType.Kiln
            ).ToArray();

        return targets;
    }

    /// <summary>
    /// 視界を取得
    /// </summary>
    /// <returns></returns>
    public RoomInformation GetVisibility(MapPoint p)
    {
        if(Visiblities.ContainsKey(p) == true)
        {
            return Visiblities[p];
        }
        RoomInformation visible = null;

        switch (this.Dungeon.DungeonMap.Get(p.X, p.Y).State)
        {
            case LoadStatus.Room:
                Guid gname = this.Dungeon.DungeonMap.Get(p).RoomName;
                if (RoomVisiblities.ContainsKey(gname) == true)
                {
                    visible = RoomVisiblities[gname];
                }
                else
                {
                    visible = this.Dungeon.Rooms[gname].GetVisiblity();
                    RoomVisiblities.Add(gname, visible);
                }
                break;
            case LoadStatus.RoomEntrance:
                Guid gname2 = this.Dungeon.DungeonMap.Get(p).RoomName;
                if (RoomVisiblities.ContainsKey(gname2) == true)
                {
                    visible = RoomVisiblities[gname2];
                }
                else
                {
                    visible = this.Dungeon.Rooms[gname2].GetVisiblity();
                    RoomVisiblities.Add(gname2, visible);
                }
                break;
            case LoadStatus.RoomExit:
                visible = new RoomInformation(p.Y - 1, p.Y + 1, p.X + 1, p.X - 1, Guid.Empty);
                //visible = this.Dungeon.Rooms[this.Dungeon.DungeonMap.Get(p).RoomName].GetVisiblity();
                ////if (dir == CharacterDirection.Top)
                //if(this.Dungeon.DungeonMap.Get(p.Add(
                //    CommonFunction.CharacterDirectionVector[CharacterDirection.Bottom])
                //    ).State == LoadStatus.RoomEntrance)
                //{
                //    visible.Bottom += 1;
                //}
                ////else if (dir == CharacterDirection.Bottom)
                //else if (this.Dungeon.DungeonMap.Get(p.Add(
                //    CommonFunction.CharacterDirectionVector[CharacterDirection.Top])
                //    ).State == LoadStatus.RoomEntrance)
                //{
                //    visible.Top -= 1;
                //}
                //else if (this.Dungeon.DungeonMap.Get(p.Add(
                //    CommonFunction.CharacterDirectionVector[CharacterDirection.Right])
                //    ).State == LoadStatus.RoomEntrance)
                ////else if (dir == CharacterDirection.Left)
                //{
                //    visible.Left -= 1;
                //}
                //else if (this.Dungeon.DungeonMap.Get(p.Add(
                //    CommonFunction.CharacterDirectionVector[CharacterDirection.Left])
                //    ).State == LoadStatus.RoomEntrance)
                ////else if (dir == CharacterDirection.Right)
                //{
                //    visible.Right += 1;
                //}
                break;
            case LoadStatus.Load:
                visible = new RoomInformation(p.Y - 1, p.Y + 1, p.X + 1, p.X - 1, Guid.Empty);
                break;
        }
        Visiblities.Add(p, visible);

        return visible;
    }

    /// <summary>
    /// 視界内にいるオブジェクトを可視化する
    /// </summary>
    /// <param name="visible"></param>
    public void SetEnemyVisibleContainsBefore(RoomInformation visible)
    {
        //敵
        var enumerator = Characters.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseCharacter c = enumerator.Current;
            c.SetObjectActive(visible, IsVisible, true);
        }
        ////アイテム
        var enumerator2 = Items.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            BaseItem c = enumerator2.Current;
            c.SetObjectActive(visible, IsVisible, true);
        }
        ////罠
        var enumerator3 = Traps.GetEnumerator();

        while (enumerator3.MoveNext())
        {
            BaseTrap c = enumerator3.Current;
            c.SetObjectActive(visible, IsVisible, true);
        }
        //    foreach (BaseCharacter c in Characters.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, true);
        //}

        ////アイテム
        //foreach (BaseItem c in Items.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, true);
        //}
        ////罠
        //foreach (BaseTrap c in Traps.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, true);
        //}
    }

    /// <summary>
    /// 視界内にいるオブジェクトを可視化する
    /// </summary>
    /// <param name="visible"></param>
    public void SetEnemyVisible(RoomInformation visible)
    {

        //敵
        var enumerator = Characters.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseCharacter c = enumerator.Current;
            c.SetObjectActive(visible, IsVisible, false);
        }
        ////アイテム
        var enumerator2 = Items.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            BaseItem c = enumerator2.Current;
            c.SetObjectActive(visible, IsVisible, false);
        }
        ////罠
        var enumerator3 = Traps.GetEnumerator();

        while (enumerator3.MoveNext())
        {
            BaseTrap c = enumerator3.Current;
            c.SetObjectActive(visible, IsVisible, false);
        }
        //敵
        //foreach (BaseCharacter c in Characters.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, false);
        //}

        ////アイテム
        //foreach (BaseItem c in Items.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, false);
        //}
        ////罠
        //foreach (BaseTrap c in Traps.Values)
        //{
        //    c.SetObjectActive(visible, IsVisible, false);
        //}
    }

    #endregion キャラクター移動管理


    #region アイテム関連


    public void SetUpItemMap()
    {
        //アイテムマップを作成する
        ItemMap.Fill(null);

        var enumerator = Items.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseItem c = enumerator.Current;
            ItemMap.Set(c.CurrentPoint.X, c.CurrentPoint.Y, c);
        }
        ////現在のアイテムをマップに反映する
        //foreach (BaseItem item in Items.Values)
        //{
        //    if (item.IsThrowBreak == false)
        //    {
        //        ItemMap.Set(item.CurrentPoint.X, item.CurrentPoint.Y, item);
        //    }
        //}
    }

    private static MapPoint[] _PutMapPointPri;
    private static MapPoint[] PutMapPointPri
    {
        get
        {
            if (CommonFunction.IsNull(_PutMapPointPri) == false)
            {
                return _PutMapPointPri;
            }
            _PutMapPointPri = new MapPoint[]
            {
                 MapPoint.Get(0,0)
                //上下左右
                ,MapPoint.Get(-1,0)
                ,MapPoint.Get(1,0)
                ,MapPoint.Get(0,1)
                ,MapPoint.Get(0,-1)
                //斜め
                ,MapPoint.Get(1,-1)
                ,MapPoint.Get(1,1)
                ,MapPoint.Get(-1,-1)
                ,MapPoint.Get(-1,1)
                //上下左右2
                ,MapPoint.Get(-2,0)
                ,MapPoint.Get(2,0)
                ,MapPoint.Get(0,2)
                ,MapPoint.Get(0,-2)
                //上下左右1 * 8
                ,MapPoint.Get(-2,1)
                ,MapPoint.Get(2,1)
                ,MapPoint.Get(-2,-1)
                ,MapPoint.Get(2,-1)
                ,MapPoint.Get(1,2)
                ,MapPoint.Get(1,-2)
                ,MapPoint.Get(-1,2)
                ,MapPoint.Get(-1,-2)
                //斜め2
                ,MapPoint.Get(2,-2)
                ,MapPoint.Get(2,2)
                ,MapPoint.Get(-2,-2)
                ,MapPoint.Get(-2,2)

            };
            return _PutMapPointPri;
        }
    }

    ///対象ポイントを中心に最も近い空いている道位置を探す
    public MapPoint GetItemPutTarget(MapPoint point, Func<int, int, MapPoint> checkMethod)
    {
        //対象のポイントがfalseならそれで終わり
        foreach (MapPoint p in PutMapPointPri)
        {
            MapPoint temp;

            //チェック
            temp = checkMethod(point.X + p.X, point.Y + p.Y);
            if (temp != null)
            {
                return temp;
            }
        }

        return null;
    }

    private MapPoint CheckPointItem(int x, int y)
    {

        if (IsFloor(Dungeon.DungeonMap.Get(x, y)) == true)
        {
            //対象のポイントが空いていればそれで終わり
            if (CommonFunction.IsNull(ItemMap.Get(x, y)) == true)
            {
                return MapPoint.Get(x, y);
            }
        }
        return null;
    }

    private MapPoint CheckBreakOutItem(int x, int y)
    {

        if (IsWall(Dungeon.DungeonMap.Get(x, y)) == false)
        {
            //対象のポイントが空いていればそれで終わり
            if (CommonFunction.IsNull(ItemMap.Get(x, y)) == true)
            {
                return MapPoint.Get(x, y);
            }
        }
        return null;
    }


    /// <summary>
    /// アイテムの配置
    /// </summary>
    public void AddNewItem(BaseItem item)
    {
        MaptipInformation img = GetMapTip(item.Type);
        //マップへの追加
        //キャラチップによって分岐をかける
        //switch (item.Type)
        //{
        //    case ObjectType.Item:
        //        img = UnityEngine.Object.Instantiate(MapImgUnitIt, MapBaseMU.transform);
        //        break;
        //    case ObjectType.Stair:
        //        img = UnityEngine.Object.Instantiate(MapImgUnitSt, MapBaseMU.transform);
        //        break;
        //    default:
        //        break;
        //}
        //img.GetComponent<RectTransform>().sizeDelta = MapUnitVector;

        //MapBaseの配下に配置
        //img.transform.SetParent(MapBase.transform, false);

        item.MapUnit = img;

        Items.Add(item);
    }
    public bool HasItem(BaseItem item)
    {
        return Items.Contains(item);
    }

    //public void RemoveItem(Guid item)
    //{
    //    BaseItem bi = Items[item];
    //    RemoveItem(bi);
    //}
    public void RemoveItem(BaseItem item)
    {
        if( Items.Remove(item) == true)
        {
            item.MapUnit.OffActive();
            item.MapUnit = null;
            Destroy(item.ThisDisplayObject);
            item.ThisDisplayObject = null;
        }
    }

    public void PutItem(BaseItem item, MapPoint tarPoint, MapPoint current, CharacterDirection dir = CharacterDirection.Bottom)
    {
        //マップ管理のオブジェクトに追加
        SetUpItemMap();
        MapPoint pt = GetItemPutTarget(tarPoint, CheckPointItem);

        if (HasItem(item) == false)
        {
            item.SetThisDisplayObject(current.X, current.Y);
            AddNewItem(item);
        }
        else
        {
            item.SetPositionThrow(current.X, current.Y);
        }
        item.ThrowPoint = tarPoint;
        item.Direction = dir;

        //範囲内の場合（空きが遠すぎるとNULL）
        if (CommonFunction.IsNull(pt) == false)
        {
            item.CurrentPoint = pt;
            item.BeforePoint = pt;
        }
    }
    public void ThrowItem(BaseItem item, MapPoint tarPoint, MapPoint current, CharacterDirection dir = CharacterDirection.Bottom)
    {
        //マップ管理のオブジェクトに追加
        SetUpItemMap();
        MapPoint pt = GetItemPutTarget(tarPoint, CheckBreakOutItem);

        if (HasItem(item) == false)
        {
            item.SetThisDisplayObject(current.X, current.Y);
            AddNewItem(item);
        }
        else
        {
            item.SetPositionThrow(current.X, current.Y);
        }
        item.ThrowPoint = tarPoint;
        item.Direction = dir;

        //範囲内の場合（空きが遠すぎるとNULL）
        if (CommonFunction.IsNull(pt) == false)
        {
            item.CurrentPoint = pt;
            item.BeforePoint = pt;
        }
    }

    public void BreakoutItem(BaseItem item, MapPoint tarPoint, MapPoint current, CharacterDirection dir = CharacterDirection.Bottom)
    {
        //マップ管理のオブジェクトに追加
        SetUpItemMap();
        MapPoint pt = GetItemPutTarget(tarPoint, CheckBreakOutItem);

        //範囲内の場合（空きが遠すぎるとNULL）
        if (CommonFunction.IsNull(pt) == false)
        {
            if (HasItem(item) == false)
            {
                item.SetThisDisplayObject(current.X, current.Y);
                AddNewItem(item);
            }
            else
            {
                item.SetPositionThrow(current.X, current.Y);
            }
            item.ThrowPoint = tarPoint;
            item.CurrentPoint = pt;
            item.BeforePoint = pt;
            item.Direction = dir;
        }
    }

    #endregion アイテム関連
    #region トラップ関連

    public void SetUpTrapMap()
    {
        //アイテムマップを作成する
        TrapMap.Fill(null);

        var enumerator = Traps.GetEnumerator();

        while (enumerator.MoveNext())
        {
            BaseTrap c = enumerator.Current;
            TrapMap.Set(c.CurrentPoint.X, c.CurrentPoint.Y, c);
        }
        //現在のアイテムをマップに反映する
        //foreach (BaseTrap item in Traps.Values)
        //{
        //    if (item.IsThrowBreak == false)
        //    {
        //        TrapMap.Set(item.CurrentPoint.X, item.CurrentPoint.Y, item);
        //    }
        //}
    }
    /// <summary>
    /// トラップの配置
    /// </summary>
    public void AddNewTrap(BaseTrap item)
    {
        MaptipInformation img = GetMapTip(item.Type);
        //マップへの追加
        //キャラチップによって分岐をかける
        //switch (item.Type)
        //{
        //    case ObjectType.Trap:
        //        img = UnityEngine.Object.Instantiate(MapImgUnitTr, MapBaseMU.transform);
        //        break;
        //    default:
        //        break;
        //}
        //img.GetComponent<RectTransform>().sizeDelta = MapUnitVector;

        //MapBaseの配下に配置
        //img.transform.SetParent(MapBase.transform, false);

        item.MapUnit = img;

        Traps.Add(item);
    }

    private List<MapPoint> CandidatePoint = new List<MapPoint>();
    public MapPoint GetEmptyTrapPoint(MapPoint center)
    {
        CandidatePoint.Clear();
        //候補地を抽出
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                MapPoint point = center.Add(i, j);
                if (CommonFunction.IsNull(TrapMap.Get(point)) == true)
                {
                    CandidatePoint.Add(center.Add(i, j));
                }
            }
        }
        //候補地の中からランダムで選択
        if (CandidatePoint.Count > 0)
        {
            return CandidatePoint[UnityEngine.Random.Range(0, CandidatePoint.Count)];
        }

        return null;
    }

    //public void RemoveTrap(Guid item)
    //{
    //    BaseTrap bi = Traps[item];
    //    RemoveTrap(bi);
    //}
    public void RemoveTrap(BaseTrap item)
    {
        //item.MapUnit.OffActive();
        //item.MapUnit = null;
        //Destroy(item.ThisDisplayObject);
        Traps.Remove(item);
    }
    #endregion トラップ関連
    /// <summary>
    /// 壁かどうか
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool IsWall(DungeonUnitInfo info)
    {
        if (CommonFunction.IsNull(info) == true)
        {
            return false;
        }

        if (info.State == LoadStatus.Wall)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 床かどうか
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool IsFloor(DungeonUnitInfo info)
    {
        if (CommonFunction.IsNull(info) == true)
        {
            return false;
        }

        if ((info.State == LoadStatus.Room
            || info.State == LoadStatus.RoomEntrance
            || info.State == LoadStatus.Load
            || info.State == LoadStatus.RoomExit))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 部屋かどうか
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool IsRoom(int x, int y)
    {
        return IsRoom(this.Dungeon.DungeonMap.Get(x, y));
    }
    public void GetEnemyPosition(int enemycount, out int x, out int y)
    {
        //部屋数を取得
        float roomcnt = RoomEnemys.Count;

        //部屋当たりのモンスター数を取得
        int roomenm = Mathf.CeilToInt(enemycount / roomcnt);

        //まだマックス数に達していない部屋を取得
        RoomSelect.Clear();
        foreach (Guid g in RoomEnemys.Keys)
        {
            if (RoomEnemys[g] < roomenm)
            {
                RoomSelect.Add(g);
            }
        }

        //対象の中からランダムで取得
        Guid target;
        if (RoomSelect.Count > 0)
        {
            target = RoomSelect[UnityEngine.Random.Range(0, RoomSelect.Count)];
        }
        else
        {
            target = RoomEnemys.Keys.ElementAt(UnityEngine.Random.Range(0, RoomSelect.Count));
        }

        //対象の部屋に加算
        RoomEnemys[target]++;

        //対象の部屋を取得
        RoomInformation room = Dungeon.Rooms[target];

        //部屋の中からランダムで位置を取得
        x = UnityEngine.Random.Range(room.Left + 1, room.Right);
        y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        int stopper = 0;
        while (CommonFunction.IsNull(CharacterMap.Get(x, y)) == false
            || CommonFunction.IsNull(ItemMap.Get(x, y)) == false
            || Dungeon.DungeonMap.Get(x, y).State != LoadStatus.Room)
        {
            stopper++;
            if(stopper > 100)
            {
                break;
            }

            x = UnityEngine.Random.Range(room.Left + 1, room.Right);
            y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        }
        if(stopper < 100)
        {
            return;
        }

        stopper = 0;
        while (Dungeon.DungeonMap.Get(x, y).State != LoadStatus.Room)
        {
            stopper++;
            if (stopper > 100)
            {
                break;
            }

            x = GetRandomX();
            y = GetRandomY();
        }
    }
    List<RoomInformation> roomtargets = new List<RoomInformation>();
    public void GetEnemyPosition(MapPoint mp, out int x, out int y)
    {
        roomtargets.Clear();
        if (Dungeon.Rooms.Count > 1)
        {
            foreach (RoomInformation r in Dungeon.Rooms.Values)
            {
                if (r.Name != Dungeon.DungeonMap.Get(mp).RoomName)
                {
                    roomtargets.Add(r);
                }
            }
        }
        else
        {
            foreach (RoomInformation r in Dungeon.Rooms.Values)
            {
                roomtargets.Add(r);
            }
        } 
        //Guid target = RoomEnemys.Keys.ElementAt(UnityEngine.Random.Range(0, RoomSelect.Count));

        //対象の部屋を取得
        RoomInformation room;
        room = roomtargets[UnityEngine.Random.Range(0, roomtargets.Count)];

        //部屋の中からランダムで位置を取得
        x = UnityEngine.Random.Range(room.Left + 1, room.Right);
        y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        int stopper = 0;
        while (CommonFunction.IsNull(CharacterMap.Get(x, y)) == false
            || CommonFunction.IsNull(ItemMap.Get(x, y)) == false
            || mp.DistanceAbs(x,y) < 6
            || Dungeon.DungeonMap.Get(x, y).State != LoadStatus.Room)
        {
            stopper++;
            if (stopper > 100)
            {
                break;
            }

            x = UnityEngine.Random.Range(room.Left + 1, room.Right);
            y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        }

        if (stopper < 100)
        {
            return;
        }

        stopper = 0;
        x = GetRandomX();
        y = GetRandomY();
        while (Dungeon.DungeonMap.Get(x, y).State != LoadStatus.Room)
        {
            stopper++;
            if (stopper > 100)
            {
                break;
            }

            x = GetRandomX();
            y = GetRandomY();
        }
    }
    public void GetEnemyPosition(RoomInformation room, MapPoint def, out int x, out int y)
    {

        //部屋の中からランダムで位置を取得
        x = UnityEngine.Random.Range(room.Left + 1, room.Right);
        y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        int stopper = 0;
        while (CommonFunction.IsNull(CharacterMap.Get(x, y)) == false
            && def.Equal(x, y) == false)
        {
            stopper++;
            if (stopper > 100)
            {
                break;
            }
            x = UnityEngine.Random.Range(room.Left + 1, room.Right);
            y = UnityEngine.Random.Range(room.Top + 1, room.Bottom);
        }
        if (stopper < 100)
        {
            return;
        }

        x = def.X;
        y = def.Y;
    }

    /// <summary>
    /// 部屋かどうか
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool IsRoom(DungeonUnitInfo info)
    {
        if (CommonFunction.IsNull(info) == true)
        {
            return false;
        }
        else if (info.State == LoadStatus.Room)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 侵入負荷ユニットかどうか
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool IsDoNotMove(DungeonUnitInfo info)
    {

        if (info.State == LoadStatus.Wall
            || info.State == LoadStatus.Water)
        {
            return true;
        }
        return false;
    }

    private void SetActiveTrue(GameObject g)
    {
        if(g.activeSelf == false)
        {
            g.SetActive(true);
        }
    }

    private void SetActiveFalse(GameObject g)
    {
        if (g.activeSelf == true)
        {
            g.SetActive(false);
        }
    }


    public MaptipInformation GetMapTip(ObjectType t)
    {
        MaptipInformation ret;
        GameObject img = null;
        
        ret = MapTipPool[t].Find(i => i.IsActive == false);
        if(CommonFunction.IsNull(ret) == false)
        {
            ret.OnActive();
            return ret;
        }

        switch (t)
        {
            case ObjectType.Player:
                img = UnityEngine.Object.Instantiate(MapImgUnitPl, MapBaseC.transform);
                break;
            case ObjectType.Friend:
                img = UnityEngine.Object.Instantiate(MapImgUnitPl, MapBaseC.transform);
                break;
            case ObjectType.Enemy:
                img = UnityEngine.Object.Instantiate(MapImgUnitEn, MapBaseC.transform);
                break;
            case ObjectType.Kiln:
                img = UnityEngine.Object.Instantiate(MapImgUnitKl, MapBaseC.transform);
                break;
            case ObjectType.Trap:
                img = UnityEngine.Object.Instantiate(MapImgUnitTr, MapBaseT.transform);
                break;
            case ObjectType.Item:
                img = UnityEngine.Object.Instantiate(MapImgUnitIt, MapBaseI.transform);
                break;
            case ObjectType.Stair:
                img = UnityEngine.Object.Instantiate(MapImgUnitSt, MapBaseI.transform);
                break;
            default:
                break;
        }
        img.GetComponent<RectTransform>().sizeDelta = MapUnitVector;

        if (CommonFunction.IsNull(img) == false)
        {
            ret = new MaptipInformation(img);
            
            MapTipPool[t].Add(ret);
        }


        return ret;
    }

    public int GetRandomX()
    {
        return UnityEngine.Random.Range(1, this.X - 1);
    }
    public int GetRandomY()
    {
        return UnityEngine.Random.Range(1, this.Y - 1);
    }

    public static void KillObject(BaseObject o)
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(KillObjectWait(o, 1f));
    }

    public static void KillObjectNow(BaseObject o)
    {
        if (CommonFunction.IsNull(o.ThisDisplayObject) == false)
        {
            Destroy(o.ThisDisplayObject);
        }
        o.Dispose();
    }
    private static IEnumerator KillObjectWait(BaseObject o,float waittime)
    {
        //float waitcount = 0;

        //while (waitcount < waittime)
        //{
        //    waitcount += CommonFunction.GetDelta(1);
        //    yield return null;
        //}

        float waitcount = Time.time + waittime;

        //指定秒待つ
        while (waitcount >= Time.time)
        {
            yield return null;
        }

        if (CommonFunction.IsNull(o.ThisDisplayObject) == false)
        {
            Destroy(o.ThisDisplayObject);
        }
        o.Dispose();
    }

    public string CreateErrorLog()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("C:{");
        foreach(BaseCharacter c in Characters)
        {
            sb.Append("{");
            sb.Append(c.ObjNo);
            sb.Append(",");
            sb.Append(c.CharacterAbnormalState);
            sb.Append(",");
            sb.Append(c.ActType);
            sb.Append(",");
            if (CommonFunction.IsNull(c.CurrentPoint) == false)
            {
                sb.Append(c.CurrentPoint.X);
                sb.Append(",");
                sb.Append(c.CurrentPoint.Y);
            }
            sb.Append("}");
            c.ErrorInitialize();
        }
        sb.Append("}");
        sb.Append("I:{");
        foreach (BaseItem c in Items)
        {
            sb.Append("{");
            sb.Append(c.ObjNo);
            sb.Append(",");
            sb.Append(c.IType);
            sb.Append(",");
            if (CommonFunction.IsNull(c.CurrentPoint) == false)
            {
                sb.Append(c.CurrentPoint.X);
                sb.Append(",");
                sb.Append(c.CurrentPoint.Y);
            }
            sb.Append("}");
            c.ErrorInitialize();
        }
        sb.Append("}");
        sb.Append("I:{");
        foreach (BaseTrap c in Traps)
        {
            sb.Append("{");
            sb.Append(c.ObjNo);
            sb.Append(",");
            sb.Append(c.TType);
            sb.Append(",");

            if (CommonFunction.IsNull(c.CurrentPoint) == false)
            {
                sb.Append(c.CurrentPoint.X);
                sb.Append(",");
                sb.Append(c.CurrentPoint.Y);
            }
            sb.Append("}");
            c.ErrorInitialize();
        }
        sb.Append("}");

        return sb.ToString();
    }
}