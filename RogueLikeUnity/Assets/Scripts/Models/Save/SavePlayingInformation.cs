using Assets.Scripts.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Save
{
    public class SavePlayingInformation
    {


        public bool IsLoadSave;

        public long pt;
        public string pn;

        #region プレイヤー値 
        public long pon;
        public int lv;

        public float mhc;
        public float hv;
        public float sv;
        public float sm;

        public ushort pm;
        public ushort pv;

        public int ex;

        public int asn;

        #endregion プレイヤー値

        #region ダンジョン値 
        public ushort don;

        public ushort f;
        #endregion ダンジョン値


        #region 鑑定品 
        public long[] anl;
        public ushort[] anln;
        #endregion 鑑定品

        public SavePlayingInformation()
        {
            f = 1;
            don = 50001;
            lv = 1;

            //HP
            mhc = 0;
            hv = 20;

            //ちから
            pm = 15;
            pv = 15;

            //満腹度の設定
            sv = CommonConst.Status.SatietyMax;
            sm = CommonConst.Status.SatietyMax;

            //経験値
            ex = 0;

            //経過時間
            pt = 0;

            //名前
            if (CommonFunction.IsNullOrWhiteSpace(ScoreInformation.Info.PlayerName) == true)
            {
                pn = PlayerInformation.Info.DefaultName;
            }
            else
            {
                pn = ScoreInformation.Info.PlayerName;
            }

            IsLoadSave = false;
        }

        public void SetPlayer(PlayerCharacter player)
        {
            this.lv = player.Level;
            this.hv = player.CurrentHp;
            this.mhc = player.MaxHpCorrection;

            this.ex = player.CurrentExperience;

            this.pm = player.PowerMax;
            this.pv = player.PowerValue;

            this.sm = player.SatietyMax;
            this.sv = player.SatietyValue;

            this.pon = player.ObjNo;
            this.pn = player.DisplayName;

            this.asn = player.CharacterAbnormalState;
        }

        public void SetDungeon(DungeonInformation dun,ushort floor)
        {
            this.don = (ushort)dun.DungeonObjNo;
            this.f = floor;
        }

        public void SetAnalyse()
        {
            
            //未鑑定アイテム
            List<long> names = new List<long>();
            List<ushort> maps = new List<ushort>();
            foreach (long obn in GameStateInformation.Info.AnalyseNames.Keys)
            {
                names.Add(obn);
                maps.Add(GameStateInformation.Info.AnalyseNames[obn]);
            }
            anl = names.ToArray();
            anln = maps.ToArray();
        }

        public string GetJson()
        {
            string json = JsonMapper.ToJson(this);

            return json;
        }
    }
}
