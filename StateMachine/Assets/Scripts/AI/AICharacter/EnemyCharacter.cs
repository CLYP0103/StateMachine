using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter:IAICharacter{


    public EnemyCharacter(Transform[] _patrolPoints){
        //设置默认状态
        ChangeState(new PartolState(_patrolPoints));
    }
}