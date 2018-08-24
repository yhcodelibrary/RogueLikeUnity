using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ManageUI : MonoBehaviour
    {
        GameObject ClickUI;

        private void Awake()
        {
            ClickUI = GameObject.Find("ClickUI");

            SetMoveAction("Move/Right", new KeyType[] { KeyType.MoveRight });
            SetMoveAction("Move/Top", new KeyType[] { KeyType.MoveUp });
            SetMoveAction("Move/Left", new KeyType[] { KeyType.MoveLeft });
            SetMoveAction("Move/Bottom", new KeyType[] { KeyType.MoveDown });

            SetMoveAction("Move/TopRight", new KeyType[] { KeyType.MoveUp, KeyType.MoveRight });
            SetMoveAction("Move/TopLeft", new KeyType[] { KeyType.MoveUp, KeyType.MoveLeft });
            SetMoveAction("Move/BottomLeft", new KeyType[] { KeyType.MoveDown, KeyType.MoveLeft });
            SetMoveAction("Move/BottomRight", new KeyType[] { KeyType.MoveDown, KeyType.MoveRight });

            SetDirectionAction("Direction/Right", new KeyType[] { KeyType.MoveRight });
            SetDirectionAction("Direction/Top", new KeyType[] { KeyType.MoveUp });
            SetDirectionAction("Direction/Left", new KeyType[] { KeyType.MoveLeft });
            SetDirectionAction("Direction/Bottom", new KeyType[] { KeyType.MoveDown });

            SetDirectionAction("Direction/TopRight", new KeyType[] { KeyType.MoveUp, KeyType.MoveRight });
            SetDirectionAction("Direction/TopLeft", new KeyType[] { KeyType.MoveUp, KeyType.MoveLeft });
            SetDirectionAction("Direction/BottomLeft", new KeyType[] { KeyType.MoveDown, KeyType.MoveLeft });
            SetDirectionAction("Direction/BottomRight", new KeyType[] { KeyType.MoveDown, KeyType.MoveRight });

            SetMenuAction("ClickUIPanels/MenuButton", new KeyType[] { KeyType.MenuOpen });

            SetIdleAction("ClickUIPanels/IdleButton", new KeyType[] { KeyType.Idle });

            SetLogAction("ClickUIPanels/LogButton", new KeyType[] { KeyType.MessageLog });

            SetDeathBlowAction("ClickUIPanels/DeathBlowButton", new KeyType[] { KeyType.DeathBlow });
        }
        public void Start()
        {
            switch(KeyControlInformation.Info.OpMode)
            {
                case OperationMode.KeyOnly:
                    Setdown();
                    break;
                case OperationMode.UseMouse:
                    Setup();
                    break;
            }
            if(PlayerInformation.Info.DeathblowDescription == "-")
            {
                ClickUI.transform.Find("ClickUIPanels/DeathBlowButton").gameObject.SetActive(false);
            }
        }
        public void Setup()
        {
            ClickUI.SetActive(true);
        }

        public void Setdown()
        {
            ClickUI.SetActive(false);
        }
        public void OnUseMouse(GameObject target)
        {
            bool val = target.GetComponent<Toggle>().isOn;
            if (val == true)
            {
                KeyControlInformation.Info.OpMode = OperationMode.UseMouse;
                Setup();
            }
            else
            {
                KeyControlInformation.Info.OpMode = OperationMode.KeyOnly;
                Setdown();
            }
        }

        private void SetDirectionAction(string target, KeyType[] names)
        {

            GameObject tar = ClickUI.transform.Find(target).gameObject;
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushChangeDirection(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnDirectionUp(e, names));
        }
        private void SetMoveAction(string target, KeyType[] names)
        {

            GameObject tar = ClickUI.transform.Find(target).gameObject;
            //CommonFunction.AddListener(tar,
            //    EventTriggerType.UpdateSelected, e => OnSelectedMove(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushMove(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnMoveUp(e, names));
        }
        private void SetMenuAction(string target, KeyType[] names)
        {
            GameObject tar = ClickUI.transform.Find(target).gameObject;
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushMenu(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnMenuUp(e, names));
        }
        private void SetIdleAction(string target, KeyType[] names)
        {
            GameObject tar = ClickUI.transform.Find(target).gameObject;
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushIdle(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnIdleUp(e, names));
        }
        private void SetLogAction(string target, KeyType[] names)
        {
            GameObject tar = ClickUI.transform.Find(target).gameObject;
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushLog(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnLogUp(e, names));
        }
        private void SetDeathBlowAction(string target, KeyType[] names)
        {
            GameObject tar = ClickUI.transform.Find(target).gameObject;
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerDown, e => OnPushDeathBlow(e, names));
            CommonFunction.AddListener(tar,
                EventTriggerType.PointerUp, e => OnDeathBlowUp(e, names));
        }

        public void OnPushChangeDirection(BaseEventData eventData, KeyType[] type)
        {
            //左1クリックだったら方向転換
            if (CommonFunction.IsDoubleClick() == false)
            {
                KeyControlInformation.Info.SetPushKey(KeyType.ChangeDirection,true);
            }
            //左2クリックだったら攻撃
            else
            {
                KeyControlInformation.Info.SetPushKey(KeyType.Attack, true);
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.Attack, true);
                return;
            }

            //移動キー格納
            foreach (KeyType s in type)
            {
                KeyControlInformation.Info.SetPushKey(s, true);
            }
        }
        public void OnPushMove(BaseEventData eventData, KeyType[] type)
        {
            //左1クリックだったら移動
            if (CommonFunction.IsDoubleClick() == false)
            {
            }
            //左2クリックだったらダッシュ
            else
            {
                KeyControlInformation.Info.SetPushKey(KeyType.Dash, true);

            }

            //移動キー格納
            foreach (KeyType s in type)
            {
                KeyControlInformation.Info.SetPushKey(s, true);
            }
        }
        public void OnPushMenu(BaseEventData eventData, KeyType[] type)
        {

            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOpen, true);
        }
        public void OnPushIdle(BaseEventData eventData, KeyType[] type)
        {

            KeyControlInformation.Info.SetPushKey(KeyType.Idle, true);
        }
        public void OnPushLog(BaseEventData eventData, KeyType[] type)
        {

            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MessageLog, true);
        }
        public void OnPushDeathBlow(BaseEventData eventData, KeyType[] type)
        {

            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.DeathBlow, true);
        }


        public void OnDirectionUp(BaseEventData eventData, KeyType[] type)
        {

            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);

            KeyControlInformation.Info.SetPushKey(KeyType.ChangeDirection, false);
            KeyControlInformation.Info.SetPushKey(KeyType.Attack, false);
            //移動キー格納
            foreach (KeyType s in type)
            {
                KeyControlInformation.Info.SetPushKey(s, false);
            }
        }

        public void OnMoveUp(BaseEventData eventData, KeyType[] type)
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);

            KeyControlInformation.Info.SetPushKey(KeyType.Dash, false);

            //移動キー格納
            foreach (KeyType s in type)
            {
                KeyControlInformation.Info.SetPushKey(s, false);
            }
        }
        public void OnMenuUp(BaseEventData eventData, KeyType[] type)
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);
            KeyControlInformation.Info.SetPushKey(KeyType.MenuOpen, false);
        }
        public void OnIdleUp(BaseEventData eventData, KeyType[] type)
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);
            KeyControlInformation.Info.SetPushKey(KeyType.Idle, false);
        }
        public void OnLogUp(BaseEventData eventData, KeyType[] type)
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);
            KeyControlInformation.Info.SetPushKey(KeyType.MessageLog, false);
        }
        public void OnDeathBlowUp(BaseEventData eventData, KeyType[] type)
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);
            KeyControlInformation.Info.SetPushKey(KeyType.DeathBlow, false);
        }

        //public void OnPushDown(BaseEventData eventData, string type)
        //{
        //    // 選択を解除
        //    EventSystem.current.SetSelectedGameObject(null);
        //    KeyControlInformation.Info.SetPushKey(type);
        //}

        public void OnClickLog()
        {
            // 選択を解除
            EventSystem.current.SetSelectedGameObject(null);
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }

    }
}
