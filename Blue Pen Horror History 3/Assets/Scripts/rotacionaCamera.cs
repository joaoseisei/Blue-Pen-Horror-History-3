using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionaCamera : MonoBehaviour{
    [SerializeField] float angulo;

    public void girar(){
        transform.eulerAngles = new Vector3(angulo, 0, 0);
    }

}
