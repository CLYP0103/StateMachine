using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : IAIState
{

    private Transform[] m_PatrolPoints;
    
    public PartolState(Transform[] _partorlPoints){
        StateName = "PartolState";
        m_PatrolPoints=_partorlPoints;
    }

    //状态开始动作
    public override void StateStart(){
        Debug.Log("=====>>"+StateName+" Start!!!");
        Debug.Log("巡逻点：");
        foreach (var point in m_PatrolPoints)
        {
            Debug.Log(point.name);
        }
    }


    //状态更新动作
    public override void StateUpdate(){
        Debug.Log("=====>>"+StateName+" Update!!!");
        if(Input.GetKeyDown(KeyCode.D)){
            m_Character.ChangeState(new ChaseState());
        }
    }

    //状态结束动作
    public override void StateEnd(){
      Debug.Log("=====>>"+StateName+" End!!!");
    }


    //寻找目标
    private bool canSeeObject(){
        return false;
    }

    

    
}
