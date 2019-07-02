using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAICharacter 
{
    //角色状态
    protected IAIState m_AIState;

    
    //设置状态
    public void ChangeState(IAIState _newAIState){
        if(_newAIState==null){
            Debug.LogError("IAICharacter.ChangeState(IAIState _newAIState): _newAIState is null!!!");
            return;
        }
           


        //退出当前状态
        m_AIState?.StateEnd();

        //赋予新的状态
        m_AIState = _newAIState;
        
        //更新(状态拥有者)
        _newAIState.SetAICharacter(this);
        
        //开始当前状态
        m_AIState.StateStart();

    }


    //更新
    public  virtual void Update(){
        m_AIState.StateUpdate();
    }
    
}
