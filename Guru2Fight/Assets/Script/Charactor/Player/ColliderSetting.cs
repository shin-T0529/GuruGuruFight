using System.Linq;
using UnityEngine;

public class ColliderSetting : MonoBehaviour
{
    [SerializeField]
    private float colliderSize;

    private bool isActivated = false;
    public GameObject colliderObject;

    void Start()
    {
        // Cylinderの生成
        //colliderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //colliderObject.transform.position = transform.position;
        //colliderObject.transform.SetParent(transform);
        //colliderObject.transform.localScale = new Vector3(colliderSize, colliderSize, colliderSize);

        // Colliderオブジェクトの描画は不要なのでRendererを消す
        //Destroy(colliderObject.GetComponent<MeshRenderer>());

        // 元々存在するColliderを削除
        Collider[] colliders = colliderObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }

        // メッシュの面を逆にしてからMeshColliderを設定
        var mesh = colliderObject.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        colliderObject.AddComponent<MeshCollider>();
    }
    private void OnTriggerEnter(Collider c)
    {
        if (!isActivated && c.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            CreateInverseCollider();
        }
    }

    private void CreateInverseCollider()
    {
        // Cylinderの生成
        //colliderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //colliderObject.transform.position = transform.position;
        //colliderObject.transform.SetParent(transform);
        //colliderObject.transform.localScale = new Vector3(colliderSize, colliderSize, colliderSize);

        // Colliderオブジェクトの描画は不要なのでRendererを消す
        //Destroy(colliderObject.GetComponent<MeshRenderer>());

        // 元々存在するColliderを削除
        Collider[] colliders = colliderObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }

        // メッシュの面を逆にしてからMeshColliderを設定
        var mesh = colliderObject.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        colliderObject.AddComponent<MeshCollider>();
    }
}