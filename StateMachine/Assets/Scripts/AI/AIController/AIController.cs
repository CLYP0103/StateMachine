using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //AICharacter
    private EnemyCharacter m_EnemyCharacter;



    //巡逻区域
    [Header("===== 巡逻点 =====")]
    public Transform[] patrolPoints;
    
    void Awake() {  
        //

        m_EnemyCharacter = new EnemyCharacter(patrolPoints);
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        m_EnemyCharacter.Update();
    }
}
