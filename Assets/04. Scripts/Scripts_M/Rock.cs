using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // ������ ü��. 0 �� �Ǹ� �ı���

    [SerializeField]
    private int destroyTime; // �ı��� ������ ������� ���� (�� �ð��� ������ Destroy)

    [SerializeField]
    private SphereCollider col; // ��ü �ݶ��̴�. ���� �ı���Ű�� ��Ȱ��ȭ��Ű ����.

    [SerializeField]
    private GameObject go_rock;  // �Ϲ� ���� ������Ʈ. ��ҿ� Ȱ��ȭ, ���� ������ ��Ȱ��ȭ
    [SerializeField]
    private GameObject go_debris;  // ���� ���� ������Ʈ. ��ҿ� ��Ȱ��ȭ, ���� ������ Ȱ��ȭ

    [SerializeField]
    private GameObject go_rock_item_prefab; // ���� �ı��� ������ ������ ������

    [SerializeField]
    private int count;  // ���� �ı��� ������ ������ ����

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