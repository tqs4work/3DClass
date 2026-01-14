using UnityEngine;

/*
 * Tên file, class, hàm :PascalCase
 * Tên biến, tham số :camelCase
 */


public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
   public bool attackInput;
    public bool comboInput;


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        attackInput = Input.GetKeyDown(KeyCode.F);
        comboInput = Input.GetKeyDown(KeyCode.C);
    }
}
