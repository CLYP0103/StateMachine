using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter:IAICharacter{

    private AIController m_AIController;
    public AIController  MyAIController{
        get{
            return m_AIController;
        }
        set{
            m_AIController =  value;
        }
    }

    public EnemyCharacter(Transform[] _patrolPoints,AIController _controller){
        
        //设置AIController
        m_AIController = _controller;
        
        //设置默认状态
        ChangeState(new PartolState(_patrolPoints));

    }
}