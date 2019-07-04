using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : IAIState
{

    //巡逻点
    private Transform[] m_PatrolPoints;
    //访问目标点
    private bool [] m_IsVisited; 
    //目标巡逻位置
    private Transform m_TargetPoint;

    //是否到达目的地
    private bool m_IsArrived;

    private Transform m_prefabTrans;

    public PartolState(Transform[] _partorlPoints){
        StateName = "PartolState";
        m_PatrolPoints=_partorlPoints;
        m_IsVisited = new bool[m_PatrolPoints.Length];
        m_TargetPoint = null;
        m_IsArrived = false;
    }

    //状态开始动作
    public override void StateStart(){
        m_prefabTrans = ((EnemyCharacter)m_Character).MyAIController.prefab.transform;
        //Debug.Log("=====>>"+StateName+" Start!!!");
        //Debug.Log("巡逻点：");
        // foreach (var point in m_PatrolPoints)
        // {
        //     Debug.Log(point.name);
        // }
        
    }


    //状态更新动作
    public override void StateUpdate(){
        //Debug.Log("=====>>"+StateName+" Update!!!");
        // if(Input.GetKeyDown(KeyCode.D)){
        //     m_Character.ChangeState(new ChaseState());
        // }
        if(Input.GetKeyDown(KeyCode.S)){
            m_TargetPoint = getNextTargetPoint();
        }
       
        //检查是否到达目标点
        m_IsArrived  = isArrived(m_prefabTrans.position,m_TargetPoint.position,0.2f);
        Debug.Log("是否到达："+m_IsArrived);
        //是否到达目标点
        if(m_IsArrived){
            //获取下一目标点
            m_TargetPoint = getNextTargetPoint();
            m_IsArrived = false;
        }
        else{
            //看向目标点
            m_prefabTrans.LookAt(m_TargetPoint);
            //前往目标点
            m_prefabTrans.Translate(m_prefabTrans.forward*Time.deltaTime,Space.World);
            
        }
        
    }

    //状态结束动作
    public override void StateEnd(){
      Debug.Log("=====>>"+StateName+" End!!!");
    }

    //检查是否到达目标点
    private bool isArrived(Vector3 soucrePoint,Vector3 targetPoint,float radius){
        return Vector3.Distance(soucrePoint,targetPoint)<radius;
    }


    //寻找目标
    private bool canSeeObject(){
        return false;
    }

    //寻找最近的点
    private Transform getNextTargetPoint(){
        //检查是否所有点都访问过一遍
        int _visitedPointCount = 0;
        for(;_visitedPointCount<m_IsVisited.Length;_visitedPointCount++){
            if(!m_IsVisited[_visitedPointCount]){
           // Debug.Log("有未访问的点"+_visitedPointCount);
            break;
            }
        }

      
        
        //如果巡逻点访问过一遍 重置标记访问数组
        if(_visitedPointCount == m_IsVisited.Length){
            for(int j=0;j<m_IsVisited.Length;j++){
                 m_IsVisited[j]=false;
            }
             Debug.Log("重置标记访问数组");
        }


        //当前位置
        Vector3 _currentPositon = m_prefabTrans.position;

        int index = 0;
        //寻找目前最近的位置
        //double _minDistance = Vector3.Distance(m_PatrolPoints[0].position,_currentPositon);


        //找到最近的点
        double _minDistance = Vector3.Distance(m_PatrolPoints[0].position,_currentPositon);

        double _tempDistance = 0 ; 
        
        //找到第一个可以访问的点
        for(int k = 0;k<m_IsVisited.Length;k++){
            //如果未访问 
            if(!m_IsVisited[k]){
                _minDistance = Vector3.Distance(m_PatrolPoints[k].position,_currentPositon);
                index = k;
                break;
            }
        }

        for(int i = 0;i<m_IsVisited.Length;i++){
            //如果被访问过 跳过该点
            if(m_IsVisited[i]){
                continue;
            }
            _tempDistance = Vector3.Distance(_currentPositon,m_PatrolPoints[i].position);
            if(_minDistance> _tempDistance){
                index = i;
                _minDistance = _tempDistance;
            }
        }
        Debug.Log("Find :"+m_PatrolPoints[index].name);
        //标记访问
        m_IsVisited[index]=true;

        Debug.Log(m_IsVisited[0]+" "+m_IsVisited[1]+" "+m_IsVisited[2]+" "+m_IsVisited[3]+" "+m_IsVisited[4]);

        return m_PatrolPoints[index];
    }

    

    
}
