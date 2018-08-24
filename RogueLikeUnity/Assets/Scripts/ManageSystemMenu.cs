using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ManageSystemMenu : MonoBehaviour
{
    public KeyControlModel temp;

    public bool IsClose;

    PlayerCharacter pl;

    GameObject SystemPanel;

    #region フィールド系
    GameObject ScrollViewSystemKeyField;
    GameObject SystemMoveUpHeader;
    GameObject SystemMoveDownHeader;
    GameObject SystemMoveLeftHeader;
    GameObject SystemMoveRightHeader;
    GameObject SystemMoveSlashHeader;
    GameObject SystemDirHeader; 
    GameObject SystemDeathBlowHeader;
    GameObject SystemMenuHeader;
    GameObject SystemMlogHeader;
    GameObject SystemDashHeader;
    GameObject SystemAttackHeader;
    GameObject SystemIdleHeader;
    GameObject SystemKeyDisplayHeader;

    #endregion フィールド系

    #region メニュー系
    GameObject SystemOkHeader;
    GameObject SystemCancelHeader;
    GameObject SystemOptionHeader;
    GameObject SystemSortHeader;
    GameObject SystemMultiSelectHeader;

    #endregion メニュー系

    #region 方向キー
    GameObject SystemDTopHeader;
    GameObject SystemDLeftHeader;
    #endregion 方向キー

    public void Start()
    {
        SystemPanel = GameObject.Find("SystemPanel");

        SystemPanel.transform.Find("SystemHeader").GetComponent<Text>().text = CommonConst.Message.System;
        SystemPanel.transform.Find("SystemCharacterNameHeader").GetComponent<Text>().text = CommonConst.Message.CharacterName;
        SystemPanel.transform.Find("SystemHeader").GetComponent<Text>().text = CommonConst.Message.System;

        ScrollViewSystemKeyField = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField").gameObject;
        SystemMoveUpHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMoveUpHeader").gameObject;
        SystemMoveUpHeader.GetComponent<Text>().text = CommonConst.Message.MoveUp;
        SystemMoveDownHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMoveDownHeader").gameObject;
        SystemMoveDownHeader.GetComponent<Text>().text = CommonConst.Message.MoveDown;
        SystemMoveLeftHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMoveLeftHeader").gameObject;
        SystemMoveLeftHeader.GetComponent<Text>().text = CommonConst.Message.MoveLeft;
        SystemMoveRightHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMoveRightHeader").gameObject;
        SystemMoveRightHeader.GetComponent<Text>().text = CommonConst.Message.MoveRight;
        SystemMoveSlashHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMoveSlashHeader").gameObject;
        SystemMoveSlashHeader.GetComponent<Text>().text = CommonConst.Message.XMove;
        SystemDirHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemDirHeader").gameObject;
        SystemDirHeader.GetComponent<Text>().text = CommonConst.Message.ChangeDirection;
        SystemDeathBlowHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemDeathBlowHeader").gameObject;
        SystemDeathBlowHeader.GetComponent<Text>().text = CommonConst.Message.Finisher;
        SystemMenuHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMenuHeader").gameObject;
        SystemMenuHeader.GetComponent<Text>().text = CommonConst.Message.DisplayMenu;
        SystemMlogHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemMlogHeader").gameObject;
        SystemMlogHeader.GetComponent<Text>().text = CommonConst.Message.MessageLog;
        SystemDashHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemDashHeader").gameObject;
        SystemDashHeader.GetComponent<Text>().text = CommonConst.Message.Dash;
        SystemAttackHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemAttackHeader").gameObject;
        SystemAttackHeader.GetComponent<Text>().text = CommonConst.Message.Attack;
        SystemIdleHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemIdleHeader").gameObject;
        SystemIdleHeader.GetComponent<Text>().text = CommonConst.Message.Idle;
        SystemKeyDisplayHeader = SystemPanel.transform.Find("KeyPanel/ScrollViewSystemKeyField/Viewport/Content/SystemKeyField/SystemKeyDisplayHeader").gameObject;
        SystemKeyDisplayHeader.GetComponent<Text>().text = CommonConst.Message.DisplayKeySetting;

        SystemPanel.transform.Find("KeyPanel/SystemKeyHeader").GetComponent<Text>().text = CommonConst.Message.KeyConfig;

        SystemOkHeader = SystemPanel.transform.Find("KeyPanel/SystemKeyMenu/SystemOkHeader").gameObject;
        SystemOkHeader.GetComponent<Text>().text = CommonConst.Message.MenuOk;
        SystemCancelHeader = SystemPanel.transform.Find("KeyPanel/SystemKeyMenu/SystemCancelHeader").gameObject;
        SystemCancelHeader.GetComponent<Text>().text = CommonConst.Message.MenuCancel;
        SystemOptionHeader = SystemPanel.transform.Find("KeyPanel/SystemKeyMenu/SystemOptionHeader").gameObject;
        SystemOptionHeader.GetComponent<Text>().text = CommonConst.Message.ReferOption;
        SystemSortHeader = SystemPanel.transform.Find("KeyPanel/SystemKeyMenu/SystemSortHeader").gameObject;
        SystemSortHeader.GetComponent<Text>().text = CommonConst.Message.ItemSort;
        SystemMultiSelectHeader = SystemPanel.transform.Find("KeyPanel/SystemKeyMenu/SystemMultiSelectHeader").gameObject;
        SystemMultiSelectHeader.GetComponent<Text>().text = CommonConst.Message.MultipleItemSelection;

        SystemDTopHeader = SystemPanel.transform.Find("KeyPanel/SystemDPpad/SystemDTopHeader").gameObject;
        SystemDTopHeader.GetComponent<Text>().text = CommonConst.Message.DirectionPadUp;
        SystemDLeftHeader = SystemPanel.transform.Find("KeyPanel/SystemDPpad/SystemDLeftHeader").gameObject;
        SystemDLeftHeader.GetComponent<Text>().text = CommonConst.Message.DirectionPadLeft;
        SystemPanel.SetActive(false);
    }
    
    public TurnState UpdateSystemMenu(TurnState state)
    {

        if (CommonFunction.IsNull(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject) == false)
        {
            bool isError = false;
            string target = "";
            try
            {
                target = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            }
            catch(MissingReferenceException)
            {
                isError = true;
            }

            if (isError == false)
            {
                if (target == "SystemInputFieldMoveUp")
                {
                    SetKeyInfoField(SystemMoveUpHeader, KeyType.MoveUp);
                }
                else if (target == "SystemInputFieldMoveDown")
                {
                    SetKeyInfoField(SystemMoveDownHeader, KeyType.MoveDown);
                }
                else if (target == "SystemInputFieldMoveLeft")
                {
                    SetKeyInfoField(SystemMoveLeftHeader, KeyType.MoveLeft);
                }
                else if (target == "SystemInputFieldMoveRight")
                {
                    SetKeyInfoField(SystemMoveRightHeader, KeyType.MoveRight);
                }
                else if (target == "SystemInputFieldMoveSlash")
                {
                    SetKeyInfoField(SystemMoveSlashHeader, KeyType.XMove);
                }
                else if (target == "SystemInputFieldDir")
                {
                    SetKeyInfoField(SystemDirHeader, KeyType.ChangeDirection);
                }
                else if (target == "SystemInputFieldDeathBlow")
                {
                    SetKeyInfoField(SystemDeathBlowHeader, KeyType.DeathBlow);
                }
                else if (target == "SystemInputFieldMenu")
                {
                    SetKeyInfoField(SystemMenuHeader, KeyType.MenuOpen);
                }
                else if (target == "SystemInputFieldMlog")
                {
                    SetKeyInfoField(SystemMlogHeader, KeyType.MessageLog);
                }
                else if (target == "SystemInputFieldDash")
                {
                    SetKeyInfoField(SystemDashHeader, KeyType.Dash);
                }
                else if (target == "SystemInputFieldAttack")
                {
                    SetKeyInfoField(SystemAttackHeader, KeyType.Attack);
                }
                else if (target == "SystemInputFieldIdle")
                {
                    SetKeyInfoField(SystemIdleHeader, KeyType.Idle);
                }
                else if (target == "SystemInputFieldDisplay")
                {
                    SetKeyInfoField(SystemKeyDisplayHeader, KeyType.KeyDisplay);
                }
                else if (target == "SystemInputFieldOk")
                {
                    SetKeyInfoMenu(SystemOkHeader, KeyType.MenuOk);
                }
                else if (target == "SystemInputFieldCancel")
                {
                    SetKeyInfoMenu(SystemCancelHeader, KeyType.MenuCancel);
                }
                else if (target == "SystemInputFieldOption")
                {
                    SetKeyInfoMenu(SystemOptionHeader, KeyType.LookOption);
                }
                else if (target == "SystemInputFieldSort")
                {
                    SetKeyInfoMenu(SystemSortHeader, KeyType.ItemSort);
                }
                else if (target == "SystemInputFieldMultiSelect")
                {
                    SetKeyInfoMenu(SystemMultiSelectHeader, KeyType.MenuMultiSelectOk);
                }
                else if (target == "SystemInputFieldDTop")
                {
                    SetVertical();
                }
                else if (target == "SystemInputFieldDLeft")
                {
                    SetHorizontal();
                }
            }
        }
        //閉じる
        else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        {
            IsClose = true;
        }
        if (IsClose == true)
        {
            //データを保存
            SystemInformation s = new SystemInformation();
            s.CharacterName = pl.DisplayName;
            s.IsBgm = MusicInformation.Music.IsMusicOn;
            s.BGMVolume = CommonFunction.PercentToInt(MusicInformation.Music.Volume);
            s.IsSound = SoundInformation.Sound.IsPlay;
            s.SoundVolume = CommonFunction.PercentToInt(SoundInformation.Sound.Volume);
            s.IsVoice = VoiceInformation.Voice.IsPlay;
            s.VoiceVolume = CommonFunction.PercentToInt(VoiceInformation.Voice.Volume);
            SaveDataInformation.SaveValue(KeyControlInformation.Info);
            SaveDataInformation.SaveValue(s);
            SaveDataInformation.Submit();

            //Debug.Log(temp.GetJsonData());

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);
            SystemPanel.SetActive(false);
            state = TurnState.FirstMenu;
        }

        return state;
    }


    public void Initialize(PlayerCharacter player)
    {
        pl = player;

        SystemPanel.SetActive(true);

        IsClose = false;

        SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text = "";

        SystemPanel.transform.Find("NameInputField").GetComponent<InputField>().text
            = player.DisplayName;

        SystemPanel.transform.Find("SystemBgmHeader/OnBGMCheck").GetComponent<Toggle>().isOn = MusicInformation.Music.IsMusicOn;
        SystemPanel.transform.Find("SystemBgmHeader/BGMVolumeSlider").GetComponent<Slider>().value = CommonFunction.PercentToNumber(MusicInformation.Music.Volume);
        SystemPanel.transform.Find("SystemBgmHeader/BGMVolumeInputField").GetComponent<InputField>().text = CommonFunction.PercentToNumber(MusicInformation.Music.Volume).ToString();

        SystemPanel.transform.Find("SystemSoundHeader/OnSoundCheck").GetComponent<Toggle>().isOn = SoundInformation.Sound.IsPlay;
        SystemPanel.transform.Find("SystemSoundHeader/SoundVolumeSlider").GetComponent<Slider>().value = CommonFunction.PercentToNumber(SoundInformation.Sound.Volume);
        SystemPanel.transform.Find("SystemSoundHeader/SoundVolumeInputField").GetComponent<InputField>().text = CommonFunction.PercentToNumber(SoundInformation.Sound.Volume).ToString();

        SystemPanel.transform.Find("SystemVoiceHeader/OnVoiceCheck").GetComponent<Toggle>().isOn = VoiceInformation.Voice.IsPlay;
        SystemPanel.transform.Find("SystemVoiceHeader/VoiceVolumeSlider").GetComponent<Slider>().value = CommonFunction.PercentToNumber(VoiceInformation.Voice.Volume);
        SystemPanel.transform.Find("SystemVoiceHeader/VoiceVolumeInputField").GetComponent<InputField>().text = CommonFunction.PercentToNumber(VoiceInformation.Voice.Volume).ToString();


        temp = KeyControlInformation.Info.Clone();

        SetAllkeyInfo(temp);

        SelectFieldKey();
    }
    private void SetAllkeyInfo(KeyControlModel key)
    {
        SetKeyInfo(SystemMoveUpHeader, key.MoveUp);
        SetKeyInfo(SystemMoveDownHeader, key.MoveDown);
        SetKeyInfo(SystemMoveLeftHeader, key.MoveLeft);
        SetKeyInfo(SystemMoveRightHeader, key.MoveRight);
        SetKeyInfo(SystemMoveSlashHeader, key.XMove);
        SetKeyInfo(SystemDirHeader, key.ChangeDirection);
        SetKeyInfo(SystemDeathBlowHeader, key.DeathBlow);
        SetKeyInfo(SystemMenuHeader, key.MenuOpen);
        SetKeyInfo(SystemMlogHeader, key.MessageLog);
        SetKeyInfo(SystemDashHeader, key.Dash);
        SetKeyInfo(SystemAttackHeader, key.Attack);
        SetKeyInfo(SystemIdleHeader, key.Idle);
        SetKeyInfo(SystemKeyDisplayHeader, key.KeyDisplay);

        SetKeyInfo(SystemOkHeader, key.MenuOk);
        SetKeyInfo(SystemCancelHeader, key.MenuCancel);
        SetKeyInfo(SystemOptionHeader, key.LookOption);
        SetKeyInfo(SystemSortHeader, key.ItemSort);
        SetKeyInfo(SystemMultiSelectHeader, key.MenuMultiSelectOk);

        if (KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
        {
            SystemPanel.transform.Find("SystemUseMouseHeader/OnUseMouseCheck").GetComponent<Toggle>().isOn = true;
        }
        else
        {
            SystemPanel.transform.Find("SystemUseMouseHeader/OnUseMouseCheck").GetComponent<Toggle>().isOn = false;
        }

        if (string.IsNullOrEmpty(temp.VerticalKey) == true)
        {
            SystemDTopHeader.transform.Find("SystemText").GetComponent<Text>().text = CommonConst.Message.NotSet;
        }
        else
        {
            SystemDTopHeader.transform.Find("SystemText").GetComponent<Text>().text = temp.VerticalKey;
        }
        if (string.IsNullOrEmpty(temp.HorizontalKey) == true)
        {
            SystemDLeftHeader.transform.Find("SystemText").GetComponent<Text>().text = CommonConst.Message.NotSet;
        }
        else
        {
            SystemDLeftHeader.transform.Find("SystemText").GetComponent<Text>().text = temp.HorizontalKey;
        }
    }

    private void SetKeyInfo(GameObject header, KeyCode c)
    {
        header.transform.Find("SystemText").GetComponent<Text>().text = KeyControlModel.GetName(c);
    }
    private GameObject GetSystemText(GameObject g)
    {
        return g.transform.Find("SystemText").gameObject;
    }

    public void OnChangeName()
    {
        string name = SystemPanel.transform.Find("NameInputField").GetComponent<InputField>().text;
        if(CommonFunction.IsNullOrWhiteSpace(name) == true)
        {
            name = PlayerInformation.Info.DefaultName;
        }
        pl.DisplayName = name.Trim();
    }


    public void OnBGM(GameObject target)
    {
        bool val = target.GetComponent<Toggle>().isOn;
        if (val == true)
        {
            MusicInformation.Music.IsMusicOn = true;
            MusicInformation.Music.Setup(DungeonInformation.Info.MType);

        }
        else
        {
            MusicInformation.Music.IsMusicOn = false;
        }
    }

    public void OnBGMInput(GameObject target)
    {
        string val = target.GetComponent<InputField>().text;
        float tar;
        if (float.TryParse(val, out tar) == true)
        {
            SystemPanel.transform.Find("SystemBgmHeader/BGMVolumeSlider").GetComponent<Slider>().value = tar;
            MusicInformation.Music.Volume = CommonFunction.NumberToPercent(tar);
        }
    }

    public void OnBGMSlide(GameObject target)
    {
        float val = Mathf.Round(target.GetComponent<Slider>().value);
        InputField input = SystemPanel.transform.Find("SystemBgmHeader/BGMVolumeInputField").GetComponent<InputField>();
        if (input.text != val.ToString())
        {
            input.text = val.ToString();
            MusicInformation.Music.Volume = CommonFunction.NumberToPercent(val);
        }
    }


    public void OnSound(GameObject target)
    {
        bool val = target.GetComponent<Toggle>().isOn;

        if (val == true)
        {
            SoundInformation.Sound.IsPlay = true;
            SoundInformation.Sound.Setup();
        }
        else
        {
            SoundInformation.Sound.IsPlay = false;
        }
    }

    public void OnSoundInput(GameObject target)
    {
        string val = target.GetComponent<InputField>().text;
        float tar;
        if (float.TryParse(val, out tar) == true)
        {
            SystemPanel.transform.Find("SystemSoundHeader/SoundVolumeSlider").GetComponent<Slider>().value = tar;
            SoundInformation.Sound.Volume = CommonFunction.NumberToPercent(tar);
        }
    }

    public void OnSoundSlide(GameObject target)
    {
        float val = Mathf.Round(target.GetComponent<Slider>().value);
        InputField input = SystemPanel.transform.Find("SystemSoundHeader/SoundVolumeInputField").GetComponent<InputField>();
        if (input.text != val.ToString())
        {
            input.text = val.ToString();
            SoundInformation.Sound.Volume = CommonFunction.NumberToPercent(val);
        }
    }


    public void OnVoice(GameObject target)
    {
        bool val = target.GetComponent<Toggle>().isOn;
        if (val == true)
        {
            VoiceInformation.Voice.IsPlay = true;
            VoiceInformation.Voice.Setup();
        }
        else
        {
            VoiceInformation.Voice.IsPlay = false;
        }
    }

    public void OnVoiceInput(GameObject target)
    {
        string val = target.GetComponent<InputField>().text;
        float tar;
        if (float.TryParse(val, out tar) == true)
        {
            SystemPanel.transform.Find("SystemVoiceHeader/VoiceVolumeSlider").GetComponent<Slider>().value = tar;
            VoiceInformation.Voice.Volume = CommonFunction.NumberToPercent(tar);
        }
    }

    public void OnVoiceSlide(GameObject target)
    {
        float val = Mathf.Round(target.GetComponent<Slider>().value);
        InputField input = SystemPanel.transform.Find("SystemVoiceHeader/VoiceVolumeInputField").GetComponent<InputField>();
        if (input.text != val.ToString())
        {
            input.text = val.ToString();
            VoiceInformation.Voice.Volume = CommonFunction.NumberToPercent(val);
        }
    }
    public void OnSoundTest()
    {
        SoundInformation.Sound.Play(SoundInformation.SoundType.GameStart);
    }
    public void OnVoiceTest()
    {
        VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.VoiceType.Test);
    }

    public void OnField()
    {
        SelectFieldKey();
    }
    public void OnMenu()
    {
        SelectMenuKey();
    }
    public void OnDPad()
    {
        SelectDPadKey();
    }

    public void OnOk()
    {
        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

        temp.OpMode = KeyControlInformation.Info.OpMode;

        KeyControlInformation.Info = this.temp.Clone();

        SetAllkeyInfo(KeyControlInformation.Info);

        SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text =
            CommonConst.Message.KeyInfoReflect;
    }

    public void OnInitialize()
    {
        OperationMode om = KeyControlInformation.Info.OpMode;

        KeyControlInformation.Info = new KeyControlModel();

        KeyControlInformation.Info.OpMode = om;

        temp = KeyControlInformation.Info.Clone();
        
        SetAllkeyInfo(KeyControlInformation.Info);

        SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text =
            CommonConst.Message.KeyInfoInitial;
    }

    public void OnClose()
    {
        IsClose = true;
    }

    public void OnChangeReverseV()
    {

        int target = SystemDTopHeader.transform.Find("Dropdown").GetComponent<Dropdown>().value;
        if (target == 0)
        {
            temp.VerticalDir = 1;
        }
        else
        {
            temp.VerticalDir = -1;
        }
    }
    public void OnChangeReverseH()
    {

        int target = SystemDLeftHeader.transform.Find("Dropdown").GetComponent<Dropdown>().value;
        if (target == 0)
        {
            temp.HorizontalDir = 1;
        }
        else
        {
            temp.HorizontalDir = -1;
        }
    }


    public void SelectFieldKey()
    {

        #region フィールド系
        ScrollViewSystemKeyField.SetActive(true);
        //SystemMoveUpHeader.SetActive(true);
        //SystemMoveDownHeader.SetActive(true);
        //SystemMoveLeftHeader.SetActive(true);
        //SystemMoveRightHeader.SetActive(true);
        //SystemMoveSlashHeader.SetActive(true);
        //SystemDirHeader.SetActive(true);
        //SystemDeathBlowHeader.SetActive(true);
        //SystemMenuHeader.SetActive(true);
        //SystemMlogHeader.SetActive(true);
        //SystemDashHeader.SetActive(true);
        //SystemAttackHeader.SetActive(true);
        //SystemIdleHeader.SetActive(true);
        //SystemKeyDisplayHeader.SetActive(true);

        #endregion フィールド系

        #region メニュー系
        SystemOkHeader.SetActive(false);
        SystemCancelHeader.SetActive(false);
        SystemOptionHeader.SetActive(false);
        SystemSortHeader.SetActive(false);
        SystemMultiSelectHeader.SetActive(false);

        #endregion メニュー系

        #region 方向キー
        SystemDTopHeader.SetActive(false);
        SystemDLeftHeader.SetActive(false);
        #endregion 方向キー

        SystemPanel.transform.Find("KeyPanel/SystemKeyDescription").GetComponent<Text>().text
            = CommonConst.Message.KeyDescription;
//            = @"Keyフィールドにカーソルを合わせて、設定したいキーを押してください。「OK」を押すと反映されます。
//同一カテゴリ内で、同じキーを設定することはできません。";
    }

    public void SelectMenuKey()
    {

        #region フィールド系
        ScrollViewSystemKeyField.SetActive(false);
        //SystemMoveUpHeader.SetActive(false);
        //SystemMoveDownHeader.SetActive(false);
        //SystemMoveLeftHeader.SetActive(false);
        //SystemMoveRightHeader.SetActive(false);
        //SystemMoveSlashHeader.SetActive(false);
        //SystemDirHeader.SetActive(false);
        //SystemDeathBlowHeader.SetActive(false);
        //SystemMenuHeader.SetActive(false);
        //SystemMlogHeader.SetActive(false);
        //SystemDashHeader.SetActive(false);
        //SystemAttackHeader.SetActive(false);
        //SystemIdleHeader.SetActive(false);
        //SystemKeyDisplayHeader.SetActive(false);

        #endregion フィールド系

        #region メニュー系
        SystemOkHeader.SetActive(true);
        SystemCancelHeader.SetActive(true);
        SystemOptionHeader.SetActive(true);
        SystemSortHeader.SetActive(true);
        SystemMultiSelectHeader.SetActive(true);

        #endregion メニュー系

        #region 方向キー
        SystemDTopHeader.SetActive(false);
        SystemDLeftHeader.SetActive(false);
        #endregion 方向キー

        SystemPanel.transform.Find("KeyPanel/SystemKeyDescription").GetComponent<Text>().text
            = CommonConst.Message.KeyDescription;
//            = @"Keyフィールドにカーソルを合わせて、設定したいキーを押してください。「OK」を押すと反映されます。
//同一カテゴリ内で、同じキーを設定することはできません。";
    }

    private List<KeyControlModel.HorizontalKeyList> CandidateHorizon;
    private List<KeyControlModel.VerticalKeyList> CandidateVertical;
    private string HkeyTemp;
    private string VkeyTemp;
    public void SelectDPadKey()
    {
        #region フィールド系
        ScrollViewSystemKeyField.SetActive(false);
        //SystemMoveUpHeader.SetActive(false);
        //SystemMoveDownHeader.SetActive(false);
        //SystemMoveLeftHeader.SetActive(false);
        //SystemMoveRightHeader.SetActive(false);
        //SystemMoveSlashHeader.SetActive(false);
        //SystemDirHeader.SetActive(false);
        //SystemDeathBlowHeader.SetActive(false);
        //SystemMenuHeader.SetActive(false);
        //SystemMlogHeader.SetActive(false);
        //SystemDashHeader.SetActive(false);
        //SystemAttackHeader.SetActive(false);
        //SystemIdleHeader.SetActive(false);
        //SystemKeyDisplayHeader.SetActive(false);

        #endregion フィールド系

        #region メニュー系
        SystemOkHeader.SetActive(false);
        SystemCancelHeader.SetActive(false);
        SystemOptionHeader.SetActive(false);
        SystemSortHeader.SetActive(false);
        SystemMultiSelectHeader.SetActive(false);

        #endregion メニュー系

        #region 方向キー
        SystemDTopHeader.SetActive(true);
        SystemDLeftHeader.SetActive(true);
        #endregion 方向キー

        //水平方向の候補を取得
        HkeyTemp = "";
        CandidateHorizon = new List<KeyControlModel.HorizontalKeyList>();
        foreach(KeyControlModel.HorizontalKeyList t in KeyControlModel.HorizontalKeyLists())
        {

            int dir;
            string keyname = Enum.GetName(typeof(KeyControlModel.HorizontalKeyList), t);
            bool check = CheckAxis(keyname, out dir);
            if (check == false)
            {
                CandidateHorizon.Add(t);
            }
        }

        //垂直方向の候補を取得
        VkeyTemp = "";
        CandidateVertical = new List<KeyControlModel.VerticalKeyList>();
        foreach (KeyControlModel.VerticalKeyList t in KeyControlModel.HorizontalKeyLists())
        {
            int dir;
            string keyname = Enum.GetName(typeof(KeyControlModel.VerticalKeyList), t);
            bool check = CheckAxis(keyname, out dir);
            if (check == false)
            {
                CandidateVertical.Add(t);
            }
        }


        SystemPanel.transform.Find("KeyPanel/SystemKeyDescription").GetComponent<Text>().text
            = CommonConst.Message.KeyDescriptionDPad;
//            = @"Keyフィールドにカーソルを合わせて、対応する方向キーを押してください、
//操作が逆転する場合はリストボックスから反転を選択してください。";
    }

    public void SetKeyInfoField(GameObject target, KeyType type)
    {
        GameObject label = target.transform.Find("SystemText").gameObject;
        SetKeyInfo(temp.GetFieldKeys(), target, label, type);
    }
    public void SetKeyInfoMenu(GameObject target, KeyType type)
    {
        GameObject label = target.transform.Find("SystemText").gameObject;
        SetKeyInfo(temp.GetMenuKeys(), target, label, type);
    }

    public void SetHorizontal()
    {
        bool check;
        //int target = SystemDLeftHeader.transform.FindChild("Dropdown").GetComponent<Dropdown>().value;
        //if(target == 0)
        //{
        //    temp.HorizontalKey = "";
        //    temp.HorizontalDir = 1;
        //    SystemDLeftHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "";
        //}
        //else
        //{
        //    int dir;
        //    string keyname = Enum.GetName(typeof(KeyControlModel.HorizontalKeyList), (KeyControlModel.HorizontalKeyList)target);
        //    check = CheckAxis(keyname, out dir);
        //    if (check == true)
        //    {
        //        SystemDLeftHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "反応中";
        //    }
        //    else
        //    {
        //        SystemDLeftHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "未反応";
        //    }
        //    temp.HorizontalKey = keyname;
        //    temp.HorizontalDir = dir;
        //}
        int target = SystemDLeftHeader.transform.Find("Dropdown").GetComponent<Dropdown>().value;
        if (target == 0)
        {
            temp.HorizontalDir = 1;
        }
        else
        {
            temp.HorizontalDir = -1;
        }
        if (string.IsNullOrEmpty(HkeyTemp) == true)
        {
            temp.HorizontalKey = "";
        }
        foreach (KeyControlModel.HorizontalKeyList t in CandidateHorizon)
        {

            int dir;
            string keyname = Enum.GetName(typeof(KeyControlModel.HorizontalKeyList), t);
            check = CheckAxis(keyname, out dir);
            if (check == true)
            {
                SystemDLeftHeader.transform.Find("SystemText").GetComponent<Text>().text = keyname;
                temp.HorizontalKey = keyname;
            }
        }
        if(string.IsNullOrEmpty(temp.HorizontalKey) == true)
        {
            SystemDLeftHeader.transform.Find("SystemText").GetComponent<Text>().text = "未検出";
        }
        else
        {
            HkeyTemp = temp.HorizontalKey;
        }
    }
    public void SetVertical()
    {
        bool check;
        //int target = SystemDTopHeader.transform.FindChild("Dropdown").GetComponent<Dropdown>().value;
        //if (target == 0)
        //{
        //    temp.VerticalKey = "";
        //    temp.VerticalDir = 1;
        //    SystemDTopHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "";
        //}
        //else
        //{
        //    int dir;
        //    string keyname = Enum.GetName(typeof(KeyControlModel.VerticalKeyList), (KeyControlModel.VerticalKeyList)target);
        //    check = CheckAxis(keyname, out dir);
        //    if (check == true)
        //    {
        //        SystemDTopHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "反応中";
        //    }
        //    else
        //    {
        //        SystemDTopHeader.transform.FindChild("SystemText").GetComponent<Text>().text = "未反応";
        //    }
        //    temp.VerticalKey = keyname;
        //    temp.VerticalDir = dir;
        //}
        int target = SystemDTopHeader.transform.Find("Dropdown").GetComponent<Dropdown>().value;
        if(target == 0)
        {
            temp.VerticalDir = 1;
        }
        else
        {
            temp.VerticalDir = -1;
        }
        if (string.IsNullOrEmpty(VkeyTemp) == true)
        {
            temp.VerticalKey = "";
        }
        foreach (KeyControlModel.VerticalKeyList t in CandidateVertical)
        {

            int dir;
            string keyname = Enum.GetName(typeof(KeyControlModel.VerticalKeyList), t);
            check = CheckAxis(keyname, out dir);
            if (check == true)
            {
                SystemDTopHeader.transform.Find("SystemText").GetComponent<Text>().text = keyname;
                temp.VerticalKey = keyname;
            }
        }
        if (string.IsNullOrEmpty(temp.VerticalKey) == true)
        {
            SystemDTopHeader.transform.Find("SystemText").GetComponent<Text>().text = "未検出";
        }
        else
        {
            VkeyTemp = temp.VerticalKey;
        }
    }
    private bool CheckAxis(string axisname,out int dir)
    {
        dir = 1;
        if(Input.GetAxis(axisname) == 1)
        {
            return true;
        }
        else if(Input.GetAxis(axisname) == -1)
        {
            dir = -1;
            return true;
        }
        return false;
    }

    private void SetKeyInfo(Dictionary<KeyType,KeyCode> list,GameObject target,GameObject label,KeyType type)
    {
        KeyCode key = KeyControlModel.GetInputKey();
        if(key == KeyCode.None)
        {
            SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text = 
                "";
        }
        else if (temp.CheckContainsKey(list, key, type) == true)
        {
            SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text =
                string.Format( "<color=red>「{0}」キーが重複しています。別のキーを割り当ててください。</color>", KeyControlModel.GetName(key).Trim());
        }
        else
        {
            if(list[type] != key)
            {
                label.GetComponent<Text>().text = KeyControlModel.GetName(key);
                list[type] = key;
            }
            SystemPanel.transform.Find("SystemValidation").GetComponent<Text>().text =
                "";
        }
    }
}
