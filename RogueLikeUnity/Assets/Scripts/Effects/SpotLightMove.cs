using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpotLightMove : MonoBehaviour
{

    public int PositionUnit = 1;
    //public const int MoveNum = CommonConst.SystemValue.MoveNumberSpotLight;
    public int MoveNum = CommonConst.SystemValue.MoveNumberSpotLight;
    public int MoveNow = 0;
    public bool IsActive;
    //public GameObject left;
    //public GameObject right;
    //public GameObject top;
    //public GameObject bottom;
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;

    private Vector3 LTV;
    private Vector3 RTV;
    private Vector3 TTV;
    private Vector3 BTV;
    private float lspeed;
    private float rspeed;
    private float tspeed;
    private Vector3 tsspeed;
    private Vector3 tTargetScale;
    private float bspeed;
    private Vector3 bsspeed;
    private Vector3 bTargetScale;

    private int frameCount;
    private int targetFrame;
    private float nextTime;

    public static SpotLightMove Instance
    {
        get
        {
            return ResourceInformation.LightManage;
        }
    }

    private void Start()
    {
        //1秒後を設定
        nextTime = Time.time + 1;
        targetFrame = Mathf.CeilToInt(15 * (60 * Time.smoothDeltaTime));
    }

    private void Update()
    {
        frameCount++;

        if (Time.time >= nextTime)
        {
            // 1秒経ったらFPSを更新
            MoveNum = Mathf.CeilToInt(0.3f * frameCount);
            frameCount = 0;
            nextTime += 1;
        }

        if (IsActive == false)
        {
            return;
        }
        if(MoveNow < MoveNum)
        {
            MoveTargetH(left, LTV, lspeed);
            MoveTargetH(right, RTV, rspeed);
            MoveTargetV(top, TTV, tspeed, MoveNow, tTargetScale, tsspeed);
            MoveTargetV(bottom, BTV, bspeed, MoveNow, bTargetScale, bsspeed);
            MoveNow++;
        }
        else
        {
            left.transform.localPosition = LTV;
            right.transform.localPosition = RTV;
            top.transform.localPosition = TTV;
            top.transform.localScale = tTargetScale;
            bottom.transform.localPosition = BTV;
            bottom.transform.localScale = bTargetScale;
        }
    }

    private void MoveTargetH(Transform unit,Vector3 target,float speed)
    {
        //移動単位の取得
        Vector3 velocity = (target - unit.localPosition).normalized;

        //キャラクターを目標に移動
        unit.localPosition += (velocity * speed);
        //unit.transform.localPosition += CommonFunction.GetVelocity(velocity, speed);
    }
    private void MoveTargetV(Transform unit, Vector3 target, float speed,int movenum,Vector3 scaleTarget, Vector3 sspeed)
    {
        //移動単位の取得
        Vector3 velocity = (target - unit.localPosition).normalized;

        //キャラクターを目標に移動
        //float moveunit = (velocity * speed);
        //unit.transform.localPosition += (velocity * speed);
        unit.Translate((velocity * speed), Space.World);
        //unit.transform.localPosition += CommonFunction.GetVelocity(velocity, speed);

        if (movenum < MoveNum - 1)
        {
            //スケールを近づける
            unit.localScale += sspeed;
        }
        else
        {
            unit.localScale = scaleTarget;
        }
    }

    public void SetVisibleLight(RoomInformation visible, ManageDungeon dun, PlayerCharacter player)
    {
        //非活性なら動かさない
        if(IsActive == false)
        {
            return;
        }
        //float rate = 1 / Time.smoothDeltaTime;
        
        //MoveNum = Mathf.CeilToInt(15 * (60 * Time.smoothDeltaTime));
        //Debug.Log(MoveNum);
        //if(MoveNum > 30)
        //{
        //    MoveNum = 30;
        //}

        //左の10ユニット分空いた点を取得
        LTV = new Vector3(visible.Left - 10, 3.05f, player.CurrentPoint.Y);
        lspeed = (left.transform.localPosition - LTV).magnitude / MoveNum;

        //右
        RTV = new Vector3(visible.Right + 10, 3.05f, player.CurrentPoint.Y);
        rspeed = (right.transform.localPosition - RTV).magnitude / MoveNum;
        float dist = visible.Right - visible.Left + 1;
        float posx = (visible.Right + visible.Left) / 2;
        if (dist % 2 == 0)
        {
            posx += 0.5f;
        }

        //上
        TTV = new Vector3(posx, 3.05f, visible.Bottom + 10);
        tspeed = (top.transform.localPosition - TTV).magnitude / MoveNum;
        tsspeed = new Vector3((dist - top.transform.localScale.x) / MoveNum, 0, 0);
        tTargetScale = new Vector3(dist, top.transform.localScale.y, top.transform.localScale.z);

        //下
        BTV = new Vector3(posx, 3.05f, visible.Top - 10);
        bspeed = (bottom.transform.localPosition - BTV).magnitude / MoveNum;
        bsspeed = new Vector3((dist - bottom.transform.localScale.x) / MoveNum, 0, 0);
        bTargetScale = new Vector3(dist, bottom.transform.localScale.y, bottom.transform.localScale.z);

        MoveNow = 0;
        
    }

    public void SetInitial(bool active)
    {
        IsActive = active;
        CommonFunction.SetActive(this.left.gameObject,active);
        CommonFunction.SetActive(this.right.gameObject, active);
        CommonFunction.SetActive(this.top.gameObject, active);
        CommonFunction.SetActive(this.bottom.gameObject, active);

    }
}
