using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //AICharacter
    public GameObject prefab;
    private EnemyCharacter m_enemyCharacter;



    //巡逻区域
    [Header("===== 巡逻点 =====")]
    public Transform[] patrolPoints;
    
    void Awake() {  
        //
        prefab = transform.gameObject;
        m_enemyCharacter = new EnemyCharacter(patrolPoints,this);
        
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        m_enemyCharacter.Update();
    }
}
