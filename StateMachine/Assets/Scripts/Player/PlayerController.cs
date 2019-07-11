using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //移动速度
    public float walkSpeed;
    
    void Start()
    {
        walkSpeed = 4f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(-1*transform.right*walkSpeed*Time.deltaTime,Space.World);
        }

        if(Input.GetKey(KeyCode.D)){
            transform.Translate(transform.right*walkSpeed*Time.deltaTime,Space.World);
        } 

        if(Input.GetKey(KeyCode.W)){
            transform.Translate(transform.forward*walkSpeed*Time.deltaTime,Space.World);
        } 

        if(Input.GetKey(KeyCode.S)){
            transform.Translate(-1*transform.forward*walkSpeed*Time.deltaTime,Space.World);
        } 
    } 
}
