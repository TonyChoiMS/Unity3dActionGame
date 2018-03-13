using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PooledObject 클래스를 리스트로 관리하는 매니저 클래스
public class ObjectPool : Singleton<ObjectPool> {

    public List<PooledObject> objectPool = new List<PooledObject>();

    // 오브젝트 풀을 초기화
    // 루프를 돌면서 Initialize 함수를 호출하여 초기화 실행
    void Awake()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            objectPool[i].Initialize(transform);
        }
    }
    /**
     * 사용한 객체를 ObjectPool에 반환
     * itemName : 반환할 객체의 pool 오브젝트 이름
     * item : 반환할 객체 - 게임 오브젝트
     * parent : 부모 계층 관계를 설정할 정보
     */
    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }

    /**
     * 필요한 객체를 오브젝트 풀에 요청
     * itemName : 요청할 객체의 pool 오브젝트 이름
     * parent : 부모 계층 관계를 설정할 정보
     */
    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool(parent);
    }

    /**
     * 전달된 itemName과 같은 이름을 가진 오브젝트 풀을 검색하고, 성공하면 그 결과를 리턴
     */ 
    PooledObject GetPoolItem(string itemName)
    {
        for (int i = 0; i < objectPool.Count; ++i)
        {
            if (objectPool[i].poolItemName.Equals(itemName))
                return objectPool[i];
        }
        Debug.Log("no matched pool list.");
        return null;
    }
}
