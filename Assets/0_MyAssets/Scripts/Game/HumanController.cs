﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

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
    [SerializeField] Animator playerSkin;
    [SerializeField] Animator whiteSkin;
    [SerializeField] ParticleSystem hitPS;
    [SerializeField] MeshRenderer cube;
    [SerializeField] Material red;
    [SerializeField] Material blue;

    Transform mTargetTransform;
    NavMeshAgent agent;
    public bool isBelongToTeam { set; get; }

    Transform[] nPCSkins;

    public void OnStart(Vector3 pos)
    {
        isBelongToTeam = false;
        // animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //目標地点のどれぐらい手前で停止するかの距離
        agent.stoppingDistance = 3;
        agent.angularSpeed = 1000;
        agent.acceleration = 100;
        agent.speed = 15;
        agent.enabled = false;

        playerSkin.gameObject.SetActive(false);
        whiteSkin.gameObject.SetActive(true);

        nPCSkins = new Transform[NPCSkinSO.i.skins.Length];
        for (int i = 0; i < nPCSkins.Length; i++)
        {
            nPCSkins[i] = Instantiate(NPCSkinSO.i.skins[i], pos, Quaternion.identity, transform);
            nPCSkins[i].gameObject.SetActive(false);
        }
    }

    public void OnUpdate()
    {
        if (!isBelongToTeam) { return; }
        if (mTargetTransform == null) { return; }
        if (!agent.enabled) { return; }

        agent.SetDestination(mTargetTransform.position);

        //  Debug.Log(agent.hasPath);

        //  var uni = mTargetTransform.GetComponent<UnityChanController>();
        // if (uni) { cube.material = red; }
        // var npc = mTargetTransform.GetComponent<NPCController>();
        // if (npc) { cube.material = blue; }

        // agent.SetDestination(mTargetTransform.position);

    }

    void OnCollisionEnter(Collision other)
    {
        ColToUnityChan(other);
        ColToHuman(other);
        ColToNPC(other);
    }

    void ColToNPC(Collision other)
    {
        var npc = other.gameObject.GetComponent<NPCController>();
        if (npc == null) { return; }

        if (isBelongToTeam) { return; }
        Debug.Log("NPC");
        mTargetTransform = npc.transform;
        //  return;
        isBelongToTeam = true;

        agent.enabled = true;

        Variables.nPCHumanCounts[npc.index]++;
        whiteSkin.gameObject.SetActive(false);
        ShowNPC(npc.index);

        hitPS.Play();
        /* HumanTypeToPlayer(npc.transform, HumanType.NPC, () =>
          {
              Variables.nPCHumanCounts[npc.index]++;
              whiteSkin.gameObject.SetActive(false);
              ShowNPC(npc.index);
          });*/

    }

    void ColToUnityChan(Collision other)
    {
        var unityChan = other.gameObject.GetComponent<UnityChanController>();
        if (unityChan == null) { return; }
        HumanTypeToPlayer(unityChan.transform, () =>
       {
           Variables.humanCount++;
           playerSkin.gameObject.SetActive(true);
           whiteSkin.gameObject.SetActive(false);
       });
    }

    void ColToHuman(Collision other)
    {
        var human = other.gameObject.GetComponent<HumanController>();
        if (human == null) { return; }
        if (!human.isBelongToTeam) { return; }

        HumanTypeToPlayer(human.mTargetTransform, () =>
        {
            var npc = human.mTargetTransform.GetComponent<NPCController>();
            if (npc)
            {
                Variables.nPCHumanCounts[npc.index]++;
                whiteSkin.gameObject.SetActive(false);
                ShowNPC(npc.index);
            }
            else
            {
                Variables.humanCount++;
                playerSkin.gameObject.SetActive(true);
                whiteSkin.gameObject.SetActive(false);
            }
        });
    }


    void HumanTypeToPlayer(Transform targetTransform, Action Count)
    {
        if (isBelongToTeam) { return; }

        mTargetTransform = targetTransform;
        isBelongToTeam = true;

        agent.enabled = true;

        Count();

        hitPS.Play();
        SoundManager.i.PlayOneShot(2);
    }


    void ShowNPC(int index)
    {
        for (int i = 0; i < nPCSkins.Length; i++)
        {
            nPCSkins[i].gameObject.SetActive(index == i);
        }
    }

}