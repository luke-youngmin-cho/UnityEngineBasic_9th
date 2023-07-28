using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // 씬이 로드되고나서 이 클래스를 컴포넌트로 가지는 게임오브젝트가 로드될때 이 클래스에 대한 스크립트 인스턴스도 로드됨.
    // (이 클래스를 컴포넌트로 가지는 게임오브젝트가 비활성화된 채로 씬이 로드되었다면, 스크립트인스턴스도 로드되지않음. 
    // 활성화되는 순간 스크립트 인스턴스도 로드함 )
    // 스크립트 인스턴스가 로드될 때 한번 호출
    private void Awake()
    {
        Debug.Log("Awake");
    }
}
