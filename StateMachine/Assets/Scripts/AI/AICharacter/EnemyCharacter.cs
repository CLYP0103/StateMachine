using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter:IAICharacter{

    //AI的Controller
    private AIController m_AIController;
    public AIController  MyAIController{
        get{
            return m_AIController;
        }
        set{
            m_AIController =  value;
        }
    }

    //巡逻点
    private Transform[] m_patrolPoints;
    public Transform[] PatrolPoints{
        get{
            return m_patrolPoints;
        }
        set{
            m_patrolPoints=value;
        }
    }

    public EnemyCharacter(Transform[] _patrolPoints,AIController _controller){
        
        //设置AIController
        m_AIController = _controller;
        
        //配置巡逻点
        PatrolPoints = _patrolPoints;

        //设置默认状态
        ChangeState(new PartolState(PatrolPoints));

    }
}