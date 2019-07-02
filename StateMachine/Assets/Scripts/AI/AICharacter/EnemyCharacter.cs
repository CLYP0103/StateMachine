public class EnemyCharacter:IAICharacter{

    //
    public EnemyCharacter(){
        //设置默认状态
        ChangeState(new PartolState());
    }
}