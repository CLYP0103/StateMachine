using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IAIState
{   

     //AI的Transform 
    private Transform m_prefabTrans;

    //追击目标Transform
    private Transform m_targetTransform;

    //追击速度
    public float chaseSpeed;


    //追击最大距离
    public float maxCharseDistance;

    //追到距离
    public float cathedDistance;

        public ChaseState(Transform _targetTransform){
        StateName = "ChaseState";
        m_targetTransform = _targetTransform;
    }

    //状态开始动作
    public override void StateStart(){
        Debug.Log("=====>>"+StateName+" Start!!!");
        Debug.Log("===============");

        m_prefabTrans = ((EnemyCharacter)m_Character).MyAIController.prefab.transform;
        
        //初始化追击参数    
        maxCharseDistance = 8f;
        cathedDistance = 1f;
        chaseSpeed = 3.5f;
        

    }


    //状态更新动作
    public override void StateUpdate(){
        // Debug.Log("=====>>"+StateName+" Update!!!");
        // Debug.Log("===============");

        //1.是否超出距离
        if(!isArrived(m_prefabTrans.transform.position,m_prefabTrans.transform.position,maxCharseDistance)){
            //超出最大追击距离 放弃追击
            m_Character.ChangeState(new PartolState(((EnemyCharacter)m_Character).PatrolPoints));
        }

        //2.是否追到目标点
        if(isArrived(m_prefabTrans.transform.position,m_targetTransform.transform.position,cathedDistance)){
            //追到  切换巡逻状态
            Debug.Log("Catch!!!!"+m_targetTransform.name);
            m_Character.ChangeState(new PartolState(((EnemyCharacter)m_Character).PatrolPoints));
        }
       
        //看向目标点
        //m_prefabTrans.LookAt(m_targetTransform);
        m_prefabTrans.rotation = Quaternion.Lerp(m_prefabTrans.rotation, Quaternion.LookRotation(m_targetTransform.position-m_prefabTrans.position), 0.1f);
        //前往目标点
        m_prefabTrans.Translate(m_prefabTrans.forward*chaseSpeed*Time.deltaTime,Space.World);


    }

    //状态结束动作
    public override void StateEnd(){
      Debug.Log("=====>>"+StateName+" End!!!");
      Debug.Log("===============");
    }
  

     //检查是否到达目标点
    private bool isArrived(Vector3 soucrePoint,Vector3 targetPoint,float radius){
        return Vector3.Distance(soucrePoint,targetPoint)<radius;
    }
}
