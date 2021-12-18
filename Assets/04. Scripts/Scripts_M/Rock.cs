using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // 바위의 체력. 0 이 되면 파괴됨

    [SerializeField]
    private int destroyTime; // 파괴된 바위의 파편들의 생명 (이 시간이 지나면 Destroy)

    [SerializeField]
    private SphereCollider col; // 구체 콜라이더. 바위 파괴시키면 비활성화시키 ㄹ것.

    [SerializeField]
    private GameObject go_rock;  // 일반 바위 오브젝트. 평소에 활성화, 바위 깨지면 비활성화
    [SerializeField]
    private GameObject go_debris;  // 깨진 바위 오브젝트. 평소에 비활성화, 바위 깨지면 활성화

    [SerializeField]
    private GameObject go_rock_item_prefab; // 바위 파괴시 생성할 아이템 프리팹

    [SerializeField]
    private int count;  // 바위 파괴시 생성할 아이템 갯수

    public void Mining()
    {
        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        col.enabled = false;

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position, Quaternion.identity);
        }

        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}