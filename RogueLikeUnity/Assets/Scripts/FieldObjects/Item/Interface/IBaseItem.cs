using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IBaseItem
{
    /// <summary>
    /// アイテムの種別
    /// </summary>
    ItemType ItemType { get; set; }

    long SortNo { get; set; }

    /// <summary>
    /// バグっているかどうか（呪われているかどうか）
    /// </summary>
    bool IsBug { get; set; }

}
