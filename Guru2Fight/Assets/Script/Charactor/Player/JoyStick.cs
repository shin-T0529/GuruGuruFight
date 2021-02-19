using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //pub.
    public Transform MoveChara;         //動かすキャラの座標.
    public RectTransform Pad;           //ジョイスティックの中.

    public float moveSpeed;             //最速度
    public float PadDeadZone = 0.5f;

    public GameObject Wa1, Wa2, Wa3, Wa4;
    public GameObject Chara;

    public Vector3 cameraForward;
    public Vector3 moveForward;

    //pri.

    //pub sta.

    //Local.
    Vector3 move;
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    float addSpeed = 0.1f;              //初速加速度.


    void Start()
    {
        rb = MoveChara.GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputHorizontal = transform.localPosition.x;
        inputVertical = transform.localPosition.y;

        //常に移動制限はかけなければならない.
        Chara.transform.position = new Vector3(
           Mathf.Clamp(Chara.transform.position.x, Wa1.transform.position.x, Wa2.transform.position.x),
            0.822f,
           Mathf.Clamp(Chara.transform.position.z, Wa4.transform.position.z, Wa3.transform.position.z ));
    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得.
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定.
        moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける.
        rb.velocity = moveForward * 0.1f + new Vector3(0, 0, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        { MoveChara.transform.rotation = Quaternion.LookRotation(moveForward); }
    }

    //ジョイスティック機能時.
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)Pad.position, Pad.rect.width * PadDeadZone);

        move = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized;
    }

    //触っていないとき.
    public void OnPointerUp(PointerEventData eventData)
    {
        moveForward = Vector3.zero;
        transform.localPosition = Vector3.zero;
        move = Vector3.zero;
        addSpeed = 0.0f;
        StopCoroutine("CharaMove");
    }

    //触っているとき.
    public void OnPointerDown(PointerEventData eventData)
    { StartCoroutine("CharaMove"); }

    //エミュレータ.
    IEnumerator CharaMove()
    {
        while (true)
        {
            Chara.transform.Translate(move * addSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    /********************************************************************************************/
}
