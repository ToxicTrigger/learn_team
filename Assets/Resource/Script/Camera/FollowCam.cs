using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  주의 사항.
 *  ---------
 *  카메라가 한 맵에 귀속되어서는 안됌.
 *  경우에 따라 카메라를 이동시키는 로직이 자유롭게 흘러야 할 경우가 있음
 *  이는, 맵이 연결되는 구간의 공백을 어떻게 처리할 지에 대하여 고민을 해야한다고 보임.
 *  우선적으로 대상을 추적하는 코드를 사용하여 추적하도록 구현, 맵의 경계로 이동시 카메라가 추적하지 않도록 구현함
 */

public class FollowCam : MonoBehaviour
{
    public Transform target;
    // 맵의 경계를 어떻게 얻어올까? 
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
