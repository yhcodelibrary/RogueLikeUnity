using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EffectDamage : EffectBase
{
    private float alpha;
    private Color textColor;

    public override void Play()
    {
        base.Play();
        alpha = 1.0f;	//　透明度は最初は1.0f、つまり透明度は0
        WaitCorutine(2);
    }
    private static readonly Vector3 velocity = new Vector3(0, CommonConst.SystemValue.MoveSpeedDamageUp, 0);

    private void Update()
    {
        //transform.rotation = Camera.main.transform.rotation;
        //transform.position += new Vector3(0, 50f, 0) * Time.deltaTime;
        //transform.position += CommonFunction.GetVelocity(velocity, 1);
        transform.Translate(CommonFunction.GetVelocity(velocity, 1), Space.World);
        alpha -= CommonFunction.GetDelta(0.7f);      //　少しづつ透明にしていく

        //　テキストのcolorを設定
        GetComponent<Text>().color = new Color(textColor.r, textColor.g, textColor.b, alpha);	
    }

    public static EffectDamage CreateObject(BaseCharacter t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.EffectCanvas.transform.FindChild("Damage/DamageText").gameObject);
        //obj.transform.SetParent(ResourceInformation.EffectCanvas.transform, false);
        //EffectDamage d = obj.AddComponent<EffectDamage>();
        //d.Parent = obj;
        EffectDamage d = GetGameObject<EffectDamage>(false, "Damage/DamageText", ResourceInformation.EffectCanvas.transform, true);

        Vector3 v;
        if (CommonFunction.IsNullUnity(t.ThisDisplayObject) == false)
        {
            if (GameStateInformation.Info.IsDark == false)
            {
                v = new Vector3(t.ThisTransform.position.x,
                    t.ThisTransform.position.y,
                    t.ThisTransform.position.z);
            }
            else
            {
                ResourceInformation.DammyObject.transform.localPosition = new Vector3(t.ThisTransform.localPosition.x,
                    t.OriginalPosY,
                    t.ThisTransform.localPosition.z
                    );
                v = new Vector3(ResourceInformation.DammyObject.transform.position.x,
                    ResourceInformation.DammyObject.transform.position.y,
                    ResourceInformation.DammyObject.transform.position.z
                    );
            }
        }
        //通常入らない。保険
        else
        {
            v = Vector3.zero;
        }


        d.Parent.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main,
            v);

        //発射位置を多少ずらすS
        float locy = (float)UnityEngine.Random.Range(-100, 100) / 10;
        float locx = (float)UnityEngine.Random.Range(-100, 100) / 10;
        float locz = (float)UnityEngine.Random.Range(-100, 100) / 10;
        //d.Parent.transform.localPosition += new Vector3(locx, locy + 30, locz);
        d.Parent.transform.Translate(locx, locy + 30, locz, Space.World);
        return d;
    }

    public void SetText(string txt, AttackState state)
    {
        GetComponent<Text>().text = txt;
        switch(state)
        {
            case AttackState.Hit:
                textColor = Color.red;
                GetComponent<Outline>().effectColor = Color.white;
                break;
            case AttackState.Miss:
                textColor = Color.white;
                GetComponent<Outline>().effectColor = Color.black;
                break;
            case AttackState.Heal:
                textColor = Color.green;
                GetComponent<Outline>().effectColor = Color.white;
                break;
        }
    }
}
