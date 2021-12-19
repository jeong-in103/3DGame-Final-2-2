using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private int hp; // ������ ü��. 0 �� �Ǹ� �ı���

    [SerializeField]
    private Animator animator; // �ִϸ�����

    [SerializeField]
    private CapsuleCollider col; // ��ü �ݶ��̴�. ���� �ı���Ű�� ��Ȱ��ȭ��Ű ����.

    [SerializeField]
    private GameObject go_tree;  // �Ϲ� ���� ������Ʈ. ��ҿ� Ȱ��ȭ, ���� ������ ��Ȱ��ȭ

    [SerializeField]
    private GameObject[] go_tree_item_prefab; // ���� �ı��� ������ ������ ������

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
