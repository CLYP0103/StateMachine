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
        getClosestPoint();
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

    //寻找最近的点
    private Transform getClosestPoint(){
        Vector3 _currentPositon = ((EnemyCharacter)m_Character).MyAIController.prefab.transform.position;
        //Debug.Log(((EnemyCharacter)m_Character).MyAIController.prefab.transform.position);
        //Debug.Log("Enemy Position: "+((EnemyCharacter)m_Character).Controller.prefab.transform.position);
        double _minDistance = Vector3.Distance(m_PatrolPoints[0].position,_currentPositon); 
        for(int i = 1;i<m_PatrolPoints.Length;i++){
            if(_minDistance>Vector3.Distance(_currentPositon,m_PatrolPoints[i].position)){
                
            }
        }
        return null;
    }

    

    
}
