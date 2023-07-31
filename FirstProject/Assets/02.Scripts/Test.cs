using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour .. 
// Component 의 기본 단위 , 생성자를 직접 호출해서 생성하는게 아니고
// 해당 Script Instance 가 로드될 때 객체가 생성이 됨. 
// -> 직접 우리가 생성자를 호출하면 안됨. 
public class Test : MonoBehaviour
{
    // 씬이 로드되고나서 이 클래스를 컴포넌트로 가지는 게임오브젝트가 로드될때 이 클래스에 대한 스크립트 인스턴스도 로드됨.
    // (이 클래스를 컴포넌트로 가지는 게임오브젝트가 비활성화된 채로 씬이 로드되었다면, 스크립트인스턴스도 로드되지않음. 
    // 활성화되는 순간 스크립트 인스턴스도 로드함 )
    // 스크립트 인스턴스가 로드될 때 한번 호출
    // 생성자에서 보통 구현하는 멤버 초기화 등에대한 구현을 Awake()에다가 해주면 된다..
    private void Awake()
    {
        Debug.Log("Awake");
    }

    // 이 Component 가 활성화 될 때마다 호출 
    // (이 클래스인스턴스를 Component 로 가지는 GameObject 가 활성화 될 때마다 마찬가지로 호출)
    private void OnEnable()
    {
        Debug.Log("Enabled");
    }

    // Editor 에서만 호출, 해당 스크립트인스턴스가 GameObject 에 Add 될 때 및 개발자가 Editor 에서 직접 호출할 때 호출
    // 모든 멤버 값들을 초기값으로 되돌림
    private void Reset()
    {
        Debug.Log("Reset");
    }

    // 게임 로직 시작 직전에 딱 한번 호출
    private void Start()
    {
        Debug.Log("Start");
    }


    // 물리연산을 위한 프레임 (고정프레임, Fixed frame) 마다 호출
    private void FixedUpdate()
    {
        //Debug.Log("Fixed Update");
    }

    // trigger 옵션이 켜진 Collider 에 대한 겹침 이벤트
    private void OnTriggerEnter(Collider other)
    {
        
    }

    // trigger 옵션이 꺼진 Collider 에 대한 충돌 이벤트
    private void OnCollisionStay(Collision collision)
    {
        
    }

    // OnMouseXXX : 마우스가 이 인스턴스를 컴포넌트로가지는 게임오브젝트 위에 올라와서 특정 Action 을 취할때 호출
    private void OnMouseOver()
    {
        Debug.Log("On Mouse over");
    }

    // 매프레임마다 호출 (기기 성능에 따라 호출 주기가 달라짐)
    private void Update()
    {
        //Debug.Log("Update");
    }

    // 매 프레임마다 호출은 해야하지만, Animation 로직에 영향을 미치면 안되는 내용이나, 우선순위가 뒤로 밀려도 되는 내용들을 구현
    private void LateUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Vector3.zero, 2.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, 2.1f);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quit");
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
