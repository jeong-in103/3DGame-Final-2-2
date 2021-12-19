using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private int hp; // 나무의 체력. 0 이 되면 파괴됨

    [SerializeField]
    private Animator animator; // 애니메이터

    [SerializeField]
    private CapsuleCollider col; // 구체 콜라이더. 나무 파괴시키면 비활성화시키 ㄹ것.

    [SerializeField]
    private GameObject go_tree;  // 일반 나무 오브젝트. 평소에 활성화, 나무 깨지면 비활성화

    [SerializeField]
    private GameObject[] go_tree_item_prefab; // 나무 파괴시 생성할 아이템 프리팹

    public void Mining(int damage)
    {
        hp -= damage;
        animator.SetTrigger("isMining");
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        //SoundManager.instance.PlaySE(destroy_Sound);

        col.enabled = false;

        for (int i = 0; i < go_tree_item_prefab.Length; i++)
        {
            for (int j = 0; j < Random.Range(2, 5); j++)
            {
                Instantiate(go_tree_item_prefab[i], go_tree.transform.position, Quaternion.identity);
            }
        }

        Destroy(go_tree);
    }
}
