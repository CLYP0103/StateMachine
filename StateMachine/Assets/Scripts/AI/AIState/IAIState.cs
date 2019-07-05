using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAIState 
{
    
    //状态名称
    private string m_stateName;
    public string StateName{get;set;}



    //状态拥有者
    protected IAICharacter m_Character;



    //设置状态拥有者
    public void SetAICharacter(IAICharacter _newAICharacter){
        m_Character=_newAICharacter;
    }

    //状态开始
    public abstract void StateStart();

    //状态更新
    public abstract void StateUpdate();

    //状态结束
    public abstract void StateEnd();
}
