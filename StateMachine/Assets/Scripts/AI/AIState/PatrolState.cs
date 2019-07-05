using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : IAIState
{
    //巡逻点
    private Transform[] m_patrolPoints;
    //访问目标点
    private bool [] m_isVisited; 
    //目标巡逻位置
    private Transform m_targetPoint;

    //是否到达目的地
    private bool m_isArrived;

    //AI的Transform 
    private Transform m_prefabTrans;

    //巡逻视野
    //视野角度
    public float viewAngle;
    //精度   最终射线数 = 1+ 精度*2
    public int accurte;
    //视距
    public float viewDistance;
    //每条射线角度 = 视野角度/2/精度
    private float  m_subAngle;

    //目标标签
    public string targetTageName;

    //目标Transform
    public Transform targetTransform;



    public PartolState(Transform[] _partorlPoints){
        StateName = "PartolState";
        m_patrolPoints=_partorlPoints;
        m_isVisited = new bool[m_patrolPoints.Length];
        m_targetPoint = null;
        m_isArrived = false;
    }

    //状态开始动作
    public override void StateStart(){
        m_prefabTrans = ((EnemyCharacter)m_Character).MyAIController.prefab.transform;
        Debug.Log("=====>>"+StateName+" Start!!!");
        Debug.Log("===============");
        m_targetPoint = GetNextTargetPoint(true);

        //初始化视野
        viewAngle = 90;
        accurte = 6;
        viewDistance = 5;
        m_subAngle = viewAngle/2/accurte;
        targetTageName = "Player";
    }


    //状态更新动作
    public override void StateUpdate(){
        //Debug.Log("=====>>"+StateName+" Update!!!");
        //Debug.Log("===============");
        //是否看到目标
        if(canSeeObject()){
            m_Character.ChangeState(new ChaseState(targetTransform));
        }

        if(Input.GetKeyDown(KeyCode.S)){
            m_targetPoint = GetNextTargetPoint(false);
        }
       
        //检查是否到达目标点
        m_isArrived  = isArrived(m_prefabTrans.position,m_targetPoint.position,0.2f);
        //是否到达目标点
        if(m_isArrived){
            //获取下一目标点
            m_targetPoint = GetNextTargetPoint(false);
            m_isArrived = false;
        }
        else{
            //看向目标点
            m_prefabTrans.LookAt(m_targetPoint);
            //前往目标点
            m_prefabTrans.Translate(m_prefabTrans.forward*2*Time.deltaTime,Space.World);
            
        }
        
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


    //寻找目标
    private bool canSeeObject(){
        //实现扇形区域射线
        //1.中间的射线
        if(canSeeTargetObejtWithTag(m_prefabTrans.position,m_prefabTrans.forward,viewDistance,Color.red,targetTageName)){
            return true;
        }

        //2.绘制两边射线
        for(int i = 0 ;i<accurte;i++){
             if(canSeeTargetObejtWithTag(m_prefabTrans.position,Quaternion.Euler(0, -1 * m_subAngle * (i + 1), 0)*m_prefabTrans.forward,viewDistance,Color.red,targetTageName)||canSeeTargetObejtWithTag(m_prefabTrans.position,Quaternion.Euler(0, m_subAngle * (i + 1), 0)*m_prefabTrans.forward,viewDistance,Color.red,targetTageName)){
            return true;
        }
        }
        

        return false;
    }


    //绘制射线 检测是否看见目标
    private bool canSeeTargetObejtWithTag(Vector3 _start,Vector3 _dir , float _range,Color _color,string _targetName){
        //1.绘制射线
        Debug.DrawRay(_start,_dir*_range,Color.red);
        //2.实际射线
        RaycastHit hit;
        if(Physics.Raycast(_start,_dir,out hit,_range)&&hit.collider.CompareTag(_targetName)){
            //设置追踪目标
            targetTransform = hit.transform;
            Debug.Log("看到目标"+targetTransform.name);
            return true;
        }

        return false;
    }


    //寻找最近的点 参数flag = true时 被外界调用需要找最近的点作为目标点
    //            参数flag = false时 被自身类所调用找未访问过的点 作为目标点
    public Transform GetNextTargetPoint(bool flag){
        //记录目标点下标
        int index = 0;  

        //外界调用 找距离最近的点作为目标点
        if(flag){
             //当前位置
            Vector3 _currentPositon = m_prefabTrans.position;
            
            //找到最近的点
            double _minDistance = Vector3.Distance(m_patrolPoints[0].position,_currentPositon);
            
            double _tempDistance = 0 ; 
            

        for(int i = 1;i<m_patrolPoints.Length;i++){
            _tempDistance = Vector3.Distance(_currentPositon,m_patrolPoints[i].position);
            if(_minDistance> _tempDistance){
                index = i;
                _minDistance = _tempDistance;
            }
        }
        //Debug.Log("Find :"+m_patrolPoints[index].name);
        //标记访问
        m_isVisited[index]=true;

        //Debug.Log(m_isVisited[0]+" "+m_isVisited[1]+" "+m_isVisited[2]+" "+m_isVisited[3]+" "+m_isVisited[4]);

        return m_patrolPoints[index];
        }

        //检查是否所有点都访问过一遍
        int _visitedPointCount = 0;
        for(;_visitedPointCount<m_isVisited.Length;_visitedPointCount++){
            if(!m_isVisited[_visitedPointCount]){
           // Debug.Log("有未访问的点"+_visitedPointCount);
            break;
            }
        }

      
        //如果巡逻点访问过一遍 重置标记访问数组
        if(_visitedPointCount == m_isVisited.Length){
            for(int j=0;j<m_isVisited.Length;j++){
                 m_isVisited[j]=false;
            }
             //Debug.Log("重置标记访问数组");
        }

        //获取当前目标点下标
        int _currentTargetPointIndex = 0;
        for(int i = 0;i<m_patrolPoints.Length;i++){
            if(m_patrolPoints[i].Equals(m_targetPoint)){
                _currentTargetPointIndex = i;
                break;
            }
        }

        //寻找下一目标点
        while(true){
            //未被访问
            _currentTargetPointIndex=_currentTargetPointIndex%m_isVisited.Length;
            if(!m_isVisited[_currentTargetPointIndex]){
                index = _currentTargetPointIndex;
                break;
            }
            _currentTargetPointIndex++;
        }
        //Debug.Log("Find :"+m_patrolPoints[index].name);
        //标记访问
        m_isVisited[index]=true;

       // Debug.Log(m_isVisited[0]+" "+m_isVisited[1]+" "+m_isVisited[2]+" "+m_isVisited[3]+" "+m_isVisited[4]);

        return m_patrolPoints[index];


    }

    

    

    
}
