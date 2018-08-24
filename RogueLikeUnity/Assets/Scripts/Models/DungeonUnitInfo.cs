using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUnitInfo {

    public DungeonUnitInfo(LoadStatus state)
    {
        State = state;
        IsClear = false;
        IsWrite = false;
    }

    public void Clear()
    {
        IsClear = false;
        IsWrite = false;
        FieldObject = null;
        MapImg = null;
        RoomName = Guid.Empty;
    }

    /// <summary>
    /// フィールド上に表示しているオブジェクト
    /// </summary>
    public GameObject FieldObject { get; set; }

    /// <summary>
    /// マップタイルのイメージオブジェクト
    /// </summary>
    public GameObject MapImg { get; set; }

    /// <summary>
    /// 場所の種別
    /// </summary>
    private LoadStatus _State;
    public LoadStatus State {
        get
        {
            return _State;
        }
        set
        {
            _State = value;
        }
    }

    /// <summary>
    /// 部屋情報
    /// </summary>
    public Guid RoomName { get; set; }
    /// <summary>
    /// この場所がマップ上クリアになっているか
    /// </summary>
    public bool IsClear { get; set; }

    /// <summary>
    /// この場所がマップ上描画されたか
    /// </summary>
    public bool IsWrite { get; set; }

    ///// <summary>
    ///// この位置にいるキャラクター情報
    ///// </summary>
    //public ushort CharInfo { get; set; }

    ///// <summary>
    ///// この位置にいるアイテム情報
    ///// </summary>
    //public ushort ItemInfo { get; set; }

    /// <summary>
    /// この位置にいる罠情報
    /// </summary>
    //public ushort TrapInfo { get; set; }

}
