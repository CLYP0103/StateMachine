using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : IAIState
{
    public PartolState(){
        StateName = "PartolState";
    }

    //状态开始动作
    public override void StateStart(){
        Debug.Log("=====>>"+StateName+" Start!!!");
    }


    //状态更新动作
    public override void StateUpdate(){
        Debug.Log("=====>>"+StateName+" Update!!!");
    }

    //状态结束动作
    public override void StateEnd(){
      Debug.Log("=====>>"+StateName+" End!!!");
    }
}
