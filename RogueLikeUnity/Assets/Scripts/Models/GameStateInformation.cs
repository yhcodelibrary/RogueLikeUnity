using Assets.Scripts.Models.Save;
using Assets.Scripts.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GameStateInformation
    {
        private static Guid _GameId;
        public static Guid GameId
        {
            get
            {
                if(_GameId == Guid.Empty)
                {
                    _GameId = Guid.NewGuid();
                }
                return _GameId;
            }
        }

        public static bool IsEnglish;

        private static List<BaseItem> _TempItemList;
        public static List<BaseItem> TempItemList
        {
            get
            {
                if(CommonFunction.IsNull(_TempItemList) == false)
                {
                    return _TempItemList;
                }
                _TempItemList = new List<BaseItem>();
                return _TempItemList;
            }
            set
            {
                _TempItemList = value;
            }
        }

        private static List<BaseItem> _WarehouseItems;
        public static List<BaseItem> WarehouseItems
        {
            get
            {
                if (CommonFunction.IsNull(_WarehouseItems) == false)
                {
                    return _WarehouseItems;
                }
                _WarehouseItems = new List<BaseItem>();
                return _WarehouseItems;
            }
            set
            {
                _WarehouseItems = value;
            }
        }

        private static GameStateInformation _Info;
        public static GameStateInformation Info
        {
            get
            {
                if (CommonFunction.IsNull(_Info) == false)
                {
                    return _Info;
                }
                _Info = new GameStateInformation();
                return Info;
            }
            set
            {
                _Info = value;
            }
        }

        //public List<long> MelodyNotAnalyse;
        ////public List<long> FoodNotAnalyse;
        //public List<long> CandyNotAnalyse;
        //public List<long> BallNotAnalyse;
        //public List<long> BagNotAnalyse;
        public Dictionary<long, ushort> AnalyseNames;
        private Dictionary<long, ushort> AnalyseSorts;


        public bool IsRestCount;
        public bool IsDark;
        public int Seed;
        public Guid PlayerName;

        public long EmeryTarget;
        public bool IsThrowAway;

        public string GetUnknownName(long objNo)
        {
            return TableAnalyseName.GetName(AnalyseNames[objNo]);
        }

        public long GetUnknownSort(long objNo)
        {
            return TableAnalyseName.GetSort(AnalyseNames[objNo]);
        }

        public void SetupAnalyseFirst(bool isAnalayse)
        {
            AnalyseNames = new Dictionary<long, ushort>();
            AnalyseSorts = new Dictionary<long, ushort>();

            if (isAnalayse)
            {
                //未鑑定品のマップを作る

                //旋律
                //仮名データ
                List<ushort> rnames = TableAnalyseName.GetAllTypeValue(ItemType.Melody);
                ushort[] objnos = TableMelody.GetObjNoList();
                CreateAnalyse(AnalyseNames, AnalyseSorts, rnames, objnos);

                //キャンディ
                rnames = TableAnalyseName.GetAllTypeValue(ItemType.Candy);
                objnos = TableCandy.GetObjNoList();
                CreateAnalyse(AnalyseNames, AnalyseSorts, rnames, objnos);

                //ボール
                rnames = TableAnalyseName.GetAllTypeValue(ItemType.Ball);
                objnos = TableBall.GetObjNoList();
                CreateAnalyse(AnalyseNames, AnalyseSorts, rnames, objnos);

                //バッグ
                rnames = TableAnalyseName.GetAllTypeValue(ItemType.Bag);
                objnos = TableBag.GetObjNoList();
                CreateAnalyse(AnalyseNames, AnalyseSorts, rnames, objnos);

                //リング
                rnames = TableAnalyseName.GetAllTypeValue(ItemType.Ring);
                objnos = TableRing.GetObjNoList();
                CreateAnalyse(AnalyseNames, AnalyseSorts, rnames, objnos);

            }
        }

        public void SetupSave(SavePlayingInformation save)
        {
            AnalyseNames = new Dictionary<long, ushort>();
            //RingNotAnalyseNames = new Dictionary<long, ushort>();
            //MelodyNotAnalyseNames = new Dictionary<long, ushort>();
            //CandyNotAnalyseNames = new Dictionary<long, ushort>();
            //BallNotAnalyseNames = new Dictionary<long, ushort>();
            //BagNotAnalyseNames = new Dictionary<long, ushort>();

            SetupSaveUnit(AnalyseNames, AnalyseSorts, save.anl, save.anln);
        }
        private void SetupSaveUnit(Dictionary<long, ushort> list, Dictionary<long, ushort> sorts, long[] tars,ushort[] names)
        {
            if(CommonFunction.IsNull(tars) == true)
            {
                return;
            }

            for (int i = 0; i < tars.Length; i++)
            {
                list.Add(tars[i], names[i]);
            }
        }

        private void CreateAnalyse(Dictionary<long, ushort> names, Dictionary<long, ushort> sorts, List<ushort> rnames, ushort[] objnos)
        {
            foreach (ushort objno in objnos)
            {
                //仮名データからランダムで取得
                ushort rname = rnames[UnityEngine.Random.Range(0, rnames.Count)];
                rnames.Remove(rname);
                
                //ソート順を(100,000 * type) + Rand(10000 - 99999)でランダム指定


                names.Add(objno, rname);
            }
        }


        public void Initialize()
        {

            IsRestCount = false;
            IsDark = false;
            PlayerName = Guid.Empty;
        }

        public void FloorStateInitialize()
        {
            EmeryTarget = 0;
            IsThrowAway = false;
        }
    }
}
