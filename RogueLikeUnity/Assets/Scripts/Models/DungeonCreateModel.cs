
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DungeonCreateModel
{

    /// <summary>
    /// 0　何もない
    /// 0以外　キャラ
    /// </summary>
    //public int[][] CharactorMap { get; set; }

    //public List<MapPoint> LoadEvenPoint { get; set; }

    public Dictionary<Guid,RoomInformation> Rooms { get; set; }

    //public MapPoint StartPoint { get; set; }
    //public MapPoint GoalPoint { get; set; }

    public float RoomDivideProb = CommonConst.Probability.CreateRoom;

    //private System.Random rnd { get; set; }


    //private int SizeX { get; set; }
    //private int SizeY { get; set; }
    //private bool isGoalSet { get; set; }



    #region 不思議のダンジョン生成

    /// <summary>
    /// 2次元配列情報
    /// </summary>
    public Layer2DDungeonUnit DungeonMap = null;
    /// <summary>
    /// 区画リスト
    /// </summary>
    List<DgDivision> _divList = null;
    
    public DungeonCreateModel()
    {
        Rooms = new Dictionary<Guid, RoomInformation>();
    }

    public void CreateBossRoom(int width, int height, bool isRecycle)
    {
        // ■1. 初期化
        // 2次元配列初期化
        if (isRecycle == true)
        {
            if (CommonFunction.IsNull(DungeonMap) == true)
            {
                DungeonMap = new Layer2DDungeonUnit(width, height);
            }
            else
            {

            }
        }
        else
        {
            DungeonMap = new Layer2DDungeonUnit(width, height);
        }
        
        Rooms.Clear();

        // 区画リスト作成
        _divList = new List<DgDivision>();

        // ■2. すべてを壁にする
        DungeonMap.Fill(LoadStatus.Wall);

        //
        DungeonMap.Fill(DungeonInformation.Info.BaseLoadState, 1, width - 1, 1, height - 1);

        //広さの1/5取得
        int dist = width / 5;

        // ■3. 最初の区画を作る
        DgDivision div = CreateDivision(1 + dist, 1 + dist, width - 1 - dist, height - 1 - dist);
        div.Room.Set(1 + dist, 1 + dist, width - 1 - dist, height - 1 - dist);
        _divList.Add(div);

        // ■5. 区画に部屋を作る
        // 部屋を作る
        CreateRoom(DungeonMap, div.Room);
    }

    public void CreateDungeon(int width, int height,bool isRecycle)
    {
        // ■1. 初期化
        // 2次元配列初期化
        if (isRecycle == true)
        {
            if(CommonFunction.IsNull(DungeonMap) == true)
            {
                DungeonMap = new Layer2DDungeonUnit(width, height);
            }
            else
            {
                
            }
        }
        else
        {
            DungeonMap = new Layer2DDungeonUnit(width, height);
        }
        
        Rooms.Clear();

        // 区画リスト作成
        _divList = new List<DgDivision>();
        
        // ■2. すべてを壁にする
        DungeonMap.Fill(LoadStatus.Wall);
        DungeonMap.Fill(DungeonInformation.Info.BaseLoadState, 1, width - 1, 1, height - 1);

        // ■3. 最初の区画を作る
        DgDivision div = CreateDivision(0, 0, width - 1, height - 1);

        // ■4. 区画を分割する
        // 垂直 or 水平分割フラグの決定
        bool bVertical = (UnityEngine.Random.Range(0, 2) == 0);
        SplitDivison(bVertical, div);

        // ■5. 区画に部屋を作る
        CreateRooms();

        // ■6. 部屋同士をつなぐ
        ConnectRooms();

    }

    DgDivision CreateDivision(int left, int top, int right, int bottom)
    {
        DgDivision div = new DgDivision();
        div.Outer.Set(left, top, right, bottom);
        return div;
    }

    /// <summary>
    /// 区画を分割する
    /// </summary>
    /// <param name="bVertical">垂直分割するかどうか</param>
    void SplitDivison(bool bVertical, DgDivision parent)
    {
        // 子となる区画を生成
        DgDivision child = new DgDivision();

        if (bVertical)
        {
            // ▼縦方向に分割する
            if (CheckDivisionSize(parent.Outer.Height) == false)
            {
                // 縦の高さが足りない
                // 親区画を戻しておしまい
                _divList.Add(parent);
                return;
            }

            // 分割ポイントを求める
            int a = parent.Outer.Top + (CommonConst.Dungeon.MinRoomSIze + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);
            int b = parent.Outer.Bottom - (CommonConst.Dungeon.MinRoomSIze + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);
            // AB間の距離を求める
            int ab = b - a;
            // 最大の部屋サイズを超えないようにする
            ab = (int)Mathf.Min(ab, CommonConst.Dungeon.MaxRoomSize + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);

            // 分割点を決める
            int p = a + UnityEngine.Random.Range(0,ab + 1);

            // 子区画に情報を設定
            child.Outer.Set(
                parent.Outer.Left, p, parent.Outer.Right, parent.Outer.Bottom);

            // 親の下側をp地点まで縮める
            parent.Outer.Bottom = child.Outer.Top;
        }
        else
        {
            // ▼横方向に分割する
            if (CheckDivisionSize(parent.Outer.Width) == false)
            {
                // 横幅が足りない
                // 親区画を戻しておしまい
                _divList.Add(parent);
                return;
            }

            // 分割ポイントを求める
            int a = parent.Outer.Left + (CommonConst.Dungeon.MinRoomSIze + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);
            int b = parent.Outer.Right - (CommonConst.Dungeon.MinRoomSIze + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);
            // AB間の距離を求める
            int ab = b - a;
            // 最大の部屋サイズを超えないようにする
            ab = Mathf.Min(ab, CommonConst.Dungeon.MaxRoomSize + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin);

            // 分割点を求める
            int p = a + UnityEngine.Random.Range(0, ab + 1);

            // 子区画に情報を設定
            child.Outer.Set(
                p, parent.Outer.Top, parent.Outer.Right, parent.Outer.Bottom);

            // 親の右側をp地点まで縮める
            parent.Outer.Right = child.Outer.Left;
        }
        
        // 分割処理を再帰呼び出し (分割方向は縦横交互にする)
        //一定確率で終了する
        //分割地点A
        if (CommonFunction.IsRandom(RoomDivideProb))
        {
            SplitDivison(!bVertical, parent);
        }
        else
        {
            //終了したら自分を加えて終了
            _divList.Add(parent);
        }

        //分割地点B
        if (CommonFunction.IsRandom(RoomDivideProb))
        {
            SplitDivison(!bVertical, child);
        }
        else
        {
            //終了したら自分を加えて終了
            _divList.Add(child);
        }
    }

    private void CreateRooms()
    {
        foreach (DgDivision div in _divList)
        {
            // 基準サイズを決める
            int dw = div.Outer.Width;//13
            int dh = div.Outer.Height;

            // 大きさをランダムに決める
            //4,9 
            int sw = UnityEngine.Random.Range(CommonConst.Dungeon.MinRoomSIze, dw - CommonConst.Dungeon.OuterMergin - CommonConst.Dungeon.OuterMergin);
            int sh = UnityEngine.Random.Range(CommonConst.Dungeon.MinRoomSIze, dh - CommonConst.Dungeon.OuterMergin - CommonConst.Dungeon.OuterMergin);


            // 空きサイズを計算 (区画 - 部屋)
            //13 - (4,9)
            //9,4
            int rw = (dw - sw);
            int rh = (dh - sh);

            // 部屋の左上位置を決める
            //左と上のマージンは無視する
            //4 => 5 , 9 => 0 
            int rx = UnityEngine.Random.Range(CommonConst.Dungeon.OuterMergin, rw - CommonConst.Dungeon.OuterMergin);// + CommonConst.Dungeon.PosMergin;
            int ry = UnityEngine.Random.Range(CommonConst.Dungeon.OuterMergin, rh - CommonConst.Dungeon.OuterMergin);// + CommonConst.Dungeon.PosMergin;

            int left = div.Outer.Left + rx;
            int right = left + sw;
            int top = div.Outer.Top + ry;
            int bottom = top + sh;
            
            // 部屋のサイズを設定
            div.Room.Set(left, top, right, bottom);

            // 部屋を作る
            CreateRoom(DungeonMap, div.Room);
        }
    }


    /// <summary>
    /// 指定のサイズを持つ区画を分割できるかどうか
    /// </summary>
    /// <param name="size">チェックする区画のサイズ</param>
    /// <returns>分割できればtrue</returns>
    bool CheckDivisionSize(int size)
    {
        // (最小の部屋サイズ + 余白(上下、左右))
        // 2分割なので x2 する
        // +1 して連絡通路用のサイズも残す
        int min = (CommonConst.Dungeon.MinRoomSIze + CommonConst.Dungeon.OuterMergin + CommonConst.Dungeon.OuterMergin) * 2 + 1;

        return size >= min;
    }


    /// <summary>
    /// 部屋同士を通路でつなぐ
    /// </summary>
    void ConnectRooms()
    {

        for (int i = 0; i < _divList.Count; i++)
        {

            int loadRightMin = int.MaxValue;
            int loadRightMax = int.MinValue;
            int loadBottomMin = int.MaxValue;
            int loadBottomMax = int.MinValue;
            DgDivision parent = _divList[i];

            //対象区画と接する右側と下側の子区画を検索
            for (int j = 0; j < _divList.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }
                DgDivision child = _divList[j];


                //右側の接触判定
                //作った道がない場合のみ実行
                if (child.hasRightRoad == false)
                {
                    //子の左が親の右であること
                    if (child.Outer.Left == parent.Outer.Right)
                    {
                        //上下が範囲内であること
                        if (child.Outer.Top >= parent.Outer.Top &&
                                child.Outer.Top <= parent.Outer.Bottom
                            || child.Outer.Bottom <= parent.Outer.Bottom &&
                                child.Outer.Bottom >= parent.Outer.Top)
                        {

                            //子側に掘る高さを決定
                            int loadTargetHeight = UnityEngine.Random.Range(child.Room.Top, child.Room.Bottom);

                            //親の境界値から子の部屋までの水平線を道で埋める
                            FillHLine(DungeonMap, parent.Outer.Right, child.Room.Left - 1, loadTargetHeight, LoadStatus.Load);

                            //掘った中から最高位置と最低位置を抽出する
                            if (loadRightMin > loadTargetHeight)
                            {
                                loadRightMin = loadTargetHeight;
                            }
                            if (loadRightMax < loadTargetHeight)
                            {
                                loadRightMax = loadTargetHeight;
                            }

                            //子部屋に入り口情報を追加
                            DungeonMap.Get(child.Room.Left, loadTargetHeight).State = LoadStatus.RoomEntrance;
                            Rooms[DungeonMap.Get(child.Room.Left, loadTargetHeight).RoomName].Entrance.Add(MapPoint.Get(child.Room.Left, loadTargetHeight));
                            DungeonMap.Get(child.Room.Left - 1, loadTargetHeight).RoomName = DungeonMap.Get(child.Room.Left, loadTargetHeight).RoomName;
                            DungeonMap.Get(child.Room.Left - 1, loadTargetHeight).State = LoadStatus.RoomExit;
                            Rooms[DungeonMap.Get(child.Room.Left, loadTargetHeight).RoomName].Exits.Add(MapPoint.Get(child.Room.Left - 1, loadTargetHeight));

                            child.hasRightRoad = true;
                        }
                    }
                }
                
                //下側の接触判定
                //作った道がすでにある場合
                //子の上が親の下＋１であること
                if (child.hasBottomRoad == false)
                {
                    if (child.Outer.Top == parent.Outer.Bottom)
                    {
                        //左右が範囲内であること
                        if ((child.Outer.Left >= parent.Outer.Left &&
                                child.Outer.Left <= parent.Outer.Right)
                            || (child.Outer.Right <= parent.Outer.Right &&
                                child.Outer.Right >= parent.Outer.Left))
                        {

                            //子側に掘る横を決定
                            int loadTargetWidth = UnityEngine.Random.Range(child.Room.Left, child.Room.Right);

                            //親の境界値から子の部屋までの垂直線を道で埋める
                            FillVLine(DungeonMap, parent.Outer.Bottom, child.Room.Top - 1, loadTargetWidth, LoadStatus.Load);
                            //掘った中から最高位置と最低位置を抽出する
                            if (loadBottomMin > loadTargetWidth)
                            {
                                loadBottomMin = loadTargetWidth;
                            }
                            if (loadBottomMax < loadTargetWidth)
                            {
                                loadBottomMax = loadTargetWidth;
                            }

                            //子部屋に入り口情報を追加
                            DungeonMap.Get(loadTargetWidth, child.Room.Top).State = LoadStatus.RoomEntrance;
                            Rooms[DungeonMap.Get(loadTargetWidth, child.Room.Top).RoomName].Entrance.Add(MapPoint.Get(loadTargetWidth, child.Room.Top));
                            DungeonMap.Get(loadTargetWidth, child.Room.Top - 1).RoomName = DungeonMap.Get(loadTargetWidth, child.Room.Top).RoomName;
                            DungeonMap.Get(loadTargetWidth, child.Room.Top - 1).State = LoadStatus.RoomExit;
                            Rooms[DungeonMap.Get(loadTargetWidth, child.Room.Top).RoomName].Exits.Add(MapPoint.Get(loadTargetWidth, child.Room.Top - 1));

                            child.hasBottomRoad = true;

                        }
                    }
                }
            }


            if (loadRightMin != int.MaxValue && loadRightMax != int.MinValue)
            {
                //親部屋から境界までの水平線を道で埋める
                int temp = UnityEngine.Random.Range(parent.Room.Top, parent.Room.Bottom);
                FillHLine(DungeonMap, parent.Room.Right, parent.Outer.Right, temp, LoadStatus.Load);

                //親部屋に入り口情報を追加
                DungeonMap.Get(parent.Room.Right - 1, temp).State = LoadStatus.RoomEntrance;
                Rooms[DungeonMap.Get(parent.Room.Right -1,temp).RoomName].Entrance.Add(MapPoint.Get(parent.Room.Right -1, temp));
                DungeonMap.Get(parent.Room.Right, temp).RoomName = DungeonMap.Get(parent.Room.Right - 1, temp).RoomName;
                DungeonMap.Get(parent.Room.Right, temp).State = LoadStatus.RoomExit;
                Rooms[DungeonMap.Get(parent.Room.Right - 1, temp).RoomName].Exits.Add(MapPoint.Get(parent.Room.Right, temp));

                //掘った中から最高位置と最低位置を抽出する
                if (loadRightMin > temp)
                {
                    loadRightMin = temp;
                }
                if (loadRightMax < temp)
                {
                    loadRightMax = temp;
                }
                ////親の部屋の境界線を道でつなぎ、親と子の道を連結する
                FillVLine(DungeonMap, loadRightMin, loadRightMax, parent.Outer.Right, LoadStatus.Load);


            }

            if (loadBottomMin != int.MaxValue && loadBottomMax != int.MinValue)
            {
                //親部屋から境界までの垂直線を道で埋める
                int temp = UnityEngine.Random.Range(parent.Room.Left, parent.Room.Right);
                FillVLine(DungeonMap, parent.Room.Bottom, parent.Outer.Bottom, temp, LoadStatus.Load);

                //親部屋に入り口情報を追加
                DungeonMap.Get(temp, parent.Room.Bottom - 1).State = LoadStatus.RoomEntrance;
                Rooms[DungeonMap.Get(temp, parent.Room.Bottom - 1).RoomName].Entrance.Add(MapPoint.Get(temp, parent.Room.Bottom - 1));
                DungeonMap.Get(temp, parent.Room.Bottom).RoomName = DungeonMap.Get(temp, parent.Room.Bottom - 1).RoomName;
                DungeonMap.Get(temp, parent.Room.Bottom ).State = LoadStatus.RoomExit;
                Rooms[DungeonMap.Get(temp, parent.Room.Bottom-1 ).RoomName].Exits.Add(MapPoint.Get(temp, parent.Room.Bottom));

                //掘った中から最高位置と最低位置を抽出する
                if (loadBottomMin > temp)
                {
                    loadBottomMin = temp;
                }
                if (loadBottomMax < temp)
                {
                    loadBottomMax = temp;
                }
                ////親の部屋の境界線を道でつなぎ、親と子の道を連結する
                FillHLine(DungeonMap, loadBottomMin, loadBottomMax, parent.Outer.Bottom, LoadStatus.Load);
            }
        }
    }

    private void CreateRoom(Layer2DDungeonUnit layer, DgDivision.DgRect r)
    {
        Guid name = Guid.NewGuid();
        Rooms.Add(name, new RoomInformation(r.Top, r.Bottom, r.Right, r.Left, name));
        layer.FillRectLTRB(r.Left, r.Top, r.Right, r.Bottom, LoadStatus.Room, name);
    }

    /// <summary>
    /// DgRectの範囲を塗りつぶす
    /// </summary>
    /// <param name="rect">矩形情報</param>
    void FillDgRect(Layer2DDungeonUnit layer, DgDivision.DgRect r, LoadStatus none)
    {
        layer.FillRectLTRB(r.Left, r.Top, r.Right, r.Bottom, none, Guid.Empty);
    }

    /// <summary>
    /// 指定した部屋の間を通路でつなぐ
    /// </summary>
    /// <param name="divA">部屋1</param>
    /// <param name="divB">部屋2</param>
    /// <param name="bGrandChild">孫チェックするかどうか</param>
    /// <returns>つなぐことができたらtrue</returns>
    //bool CreateRoad(DgDivision divA, DgDivision divB, bool bGrandChild = false)
    //{
    //    if (divA.Outer.Bottom == divB.Outer.Top || divA.Outer.Top == divB.Outer.Bottom)
    //    {
    //        // 上下でつながっている
    //        // 部屋から伸ばす通路の開始位置を決める
    //        int x1 = UnityEngine.Random.Range(divA.Room.Left, divA.Room.Right);
    //        int x2 = UnityEngine.Random.Range(divB.Room.Left, divB.Room.Right);
    //        int y = 0;

    //        if (bGrandChild)
    //        {
    //            // すでに通路が存在していたらその情報を使用する
    //            if (divA.HasRoad()) { x1 = divA.Road.Left; }
    //            if (divB.HasRoad()) { x2 = divB.Road.Left; }
    //        }

    //        if (divA.Outer.Top > divB.Outer.Top)
    //        {
    //            // B - A (Bが上側)
    //            y = divA.Outer.Top;
    //            // 通路を作成
    //            divA.CreateRoad(x1, y + 1, x1 + 1, divA.Room.Top);
    //            divB.CreateRoad(x2, divB.Room.Bottom, x2 + 1, y);
    //        }
    //        else
    //        {
    //            // A - B (Aが上側)
    //            y = divB.Outer.Top;
    //            // 通路を作成
    //            divA.CreateRoad(x1, divA.Room.Bottom, x1 + 1, y);
    //            divB.CreateRoad(x2, y, x2 + 1, divB.Room.Top);
    //        }

    //        FillDgRect(DungeonMap, divA.Road, LoadStatus.Load);
    //        FillDgRect(DungeonMap, divB.Road, LoadStatus.Load);

    //        // 通路同士を接続する
    //        FillHLine(DungeonMap, x1, x2, y, LoadStatus.Load);

    //        // 通路を作れた
    //        return true;
    //    }

    //    if (divA.Outer.Left == divB.Outer.Right || divA.Outer.Right == divB.Outer.Left)
    //    {
    //        // 左右でつながっている
    //        // 部屋から伸ばす通路の開始位置を決める
    //        int y1 = UnityEngine.Random.Range(divA.Room.Top, divA.Room.Bottom);
    //        int y2 = UnityEngine.Random.Range(divB.Room.Top, divB.Room.Bottom);
    //        int x = 0;

    //        if (bGrandChild)
    //        {
    //            // すでに通路が存在していたらその情報を使う
    //            if (divA.HasRoad()) { y1 = divA.Road.Top; }
    //            if (divB.HasRoad()) { y2 = divB.Road.Top; }
    //        }

    //        if (divA.Outer.Left > divB.Outer.Left)
    //        {
    //            // B - A (Bが左側)
    //            x = divA.Outer.Left;
    //            // 通路を作成
    //            divB.CreateRoad(divB.Room.Right, y2, x, y2 + 1);
    //            divA.CreateRoad(x + 1, y1, divA.Room.Left, y1 + 1);
    //        }
    //        else
    //        {
    //            // A - B (Aが左側)
    //            x = divB.Outer.Left;
    //            divA.CreateRoad(divA.Room.Right, y1, x, y1 + 1);
    //            divB.CreateRoad(x, y2, divB.Room.Left, y2 + 1);
    //        }

    //        FillDgRect(DungeonMap, divA.Road, LoadStatus.Load);
    //        FillDgRect(DungeonMap, divB.Road, LoadStatus.Load);

    //        // 通路同士を接続する
    //        FillVLine(DungeonMap, y1, y2, x, LoadStatus.Load);

    //        // 通路を作れた
    //        return true;
    //    }


    //    // つなげなかった
    //    return false;
    //}

    /// <summary>
    /// 水平方向に線を引く (左と右の位置は自動で反転する)
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <param name="y">Y座標</param>
    void FillHLine(Layer2DDungeonUnit layer, int left, int right, int y, LoadStatus none)
    {
        if (left > right)
        {
            // 左右の位置関係が逆なので値をスワップする
            int tmp = left;
            left = right;
            right = tmp;
        }
        layer.FillRectLTRB(left, y, right + 1, y + 1, none, Guid.Empty);
    }

    /// <summary>
    /// 垂直方向に線を引く (上と下の位置は自動で反転する)
    /// </summary>
    /// <param name="top">上</param>
    /// <param name="bottom">下</param>
    /// <param name="x">X座標</param>
    void FillVLine(Layer2DDungeonUnit layer, int top, int bottom, int x, LoadStatus none)
    {
        if (top > bottom)
        {
            // 上下の位置関係が逆なので値をスワップする
            int tmp = top;
            top = bottom;
            bottom = tmp;
        }
        layer.FillRectLTRB(x, top, x + 1, bottom + 1, none, Guid.Empty);
    }
    #endregion 不思議のダンジョン生成
}

