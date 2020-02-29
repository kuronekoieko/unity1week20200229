using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵キャラをUnityのナビゲーション機能を使って移動させる
/// https://gametukurikata.com/navigation/navigationmove
/// ひつじコレクション - 4.フォロワーに追従機能を実装
/// https://qiita.com/YzRoid/items/75d4f8181b5f8a3ae5fa
/// NavMeshAgent
/// https://docs.unity3d.com/jp/460/ScriptReference/NavMeshAgent.html
/// 【Unity】オブジェクトが画面外かどうかを判定する方法(まとめ)
/// https://tama-lab.net/2018/08/%E3%80%90unity%E3%80%91%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%81%8C%E7%94%BB%E9%9D%A2%E5%A4%96%E3%81%8B%E3%81%A9%E3%81%86%E3%81%8B%E3%82%92%E5%88%A4%E5%AE%9A%E3%81%99%E3%82%8B/
/// </summary>
public class HumanController : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    [SerializeField] ParticleSystem hitPS;
    UnityChanController unityChan;
    float m_speed = 5;
    float m_attenuation = 0.5f;
    private Vector3 m_velocity;
    NavMeshAgent agent;
    public HumanType humanType { set; get; }

    public void OnStart(HumanType humanType, UnityChanController unityChan)
    {
        this.unityChan = unityChan;
        this.humanType = humanType;
        // animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //目標地点のどれぐらい手前で停止するかの距離
        agent.stoppingDistance = 3;
        agent.angularSpeed = 1000;
        agent.acceleration = 100;
        agent.speed = 15;
        agent.enabled = false;

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].gameObject.SetActive(i == 0);
        }
    }

    public void OnUpdate()
    {
        switch (humanType)
        {
            case HumanType.Player:

                //こっちだと重い？
                //agent.destination = player.transform.position;
                agent.SetDestination(unityChan.transform.position);
                break;
            case HumanType.None:
                //gameObject.SetActive(renderer.isVisible);
                break;
            case HumanType.Enemy:
                break;
            default:
                break;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        ColToUnityChan(other);
        ColToPlayerHuman(other);
    }

    void ColToUnityChan(Collision other)
    {
        var unityChan = other.gameObject.GetComponent<UnityChanController>();
        if (unityChan == null) { return; }
        HumanTypeToPlayer();
    }

    void ColToPlayerHuman(Collision other)
    {
        var human = other.gameObject.GetComponent<HumanController>();
        if (human == null) { return; }
        if (human.humanType != HumanType.Player) { return; }
        HumanTypeToPlayer();
    }


    void HumanTypeToPlayer()
    {
        if (humanType != HumanType.None) { return; }
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].gameObject.SetActive(i == 1);
        }
        animators[1].SetBool("Run", true);
        humanType = HumanType.Player;
        agent.enabled = true;
        Variables.playerCount++;
        hitPS.Play();
    }




}