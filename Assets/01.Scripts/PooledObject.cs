using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject {

    
    public string poolItemName = string.Empty;  // 객체를 검색할 때 사용할 이름
    public GameObject prefab = null;  // 오브젝트 풀에 저장할 프리팹
    public int poolCount = 0;  // 초기화할 때 생성할 객체의 수

    private List<GameObject> poolList = new List<GameObject>();  // 생성한 객체들을 저장할 리스트

    // 초기화 함수
    public void Initialize(Transform parent = null) {
        for (int i = 0; i < poolCount; i++)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    // 사용한 객체를 다시 오브젝트 풀에 반환
    //  parent를 지정하지 않으면 기본으로 ObjectPool 게임 오브젝트의 자식으로 지정
    public void PushToPool(GameObject item, Transform parent = null) {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }

    // 객체가 필요할 때 오브젝트 풀에 요청하는 용도로 사용할 함수
    public GameObject PopFromPool(Transform parent = null) {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);

        return item;
    }

    // 객체를 생성하는데 사용할 함수
    // prefab 변수에 지정된 게임 오브젝트를 생성하는 역할
    private GameObject CreateItem(Transform parent = null) {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }
}
