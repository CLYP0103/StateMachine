using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //AICharacter
    private EnemyCharacter m_EnemyCharacter;

    
    void Awake() {  
        //
        m_EnemyCharacter = new EnemyCharacter();
    }


    void Start()
    {
        
    }

    void Update()
    {
        m_EnemyCharacter.Update();
    }
}
