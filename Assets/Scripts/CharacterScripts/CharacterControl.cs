using UnityEngine;
using System.Collections;

/// <summary>
/// キャラクタへ入力情報を渡すスクリプト
/// </summary>
[RequireComponent(typeof(CharacterAction))] //一緒に使うコンポーネントを自動で入れてくれる奴らしい
public class CharacterControl : MonoBehaviour
{
    private CharacterAction _characterAction;
    private Transform _mainCam;
    [SerializeField]
    private bool isJump;

	//一番最初に１度だけ呼ばれる
	void Start () {
	    if (Camera.main != null)
	    {
	        _mainCam = Camera.main.transform;
	    }
	    _characterAction = GetComponent<CharacterAction>();
	}
	
	//1フレームごとに呼ばれる
	void Update () {
        Vector3 camForrrwardVector3;  //カメラの前方方向ベクトル
	    Vector3 moveVector3;    //移動ベクトル
	    isJump = Input.GetButtonDown("Jump");
	    var inputHorizontal = Input.GetAxis("Horizontal");
	    var inputVertical = Input.GetAxis("Vertical");

	    if (_mainCam != null)
	    {
	        camForrrwardVector3 = Vector3.Scale(_mainCam.forward, new Vector3(1, 0, 1)).normalized;
	        moveVector3 = inputVertical*camForrrwardVector3 + inputHorizontal*_mainCam.right;
	    }
	    else
	    {
	        moveVector3 = inputVertical*Vector3.forward + inputHorizontal*Vector3.right;
	    }
        //後でキーボード用の歩くキーを決める
	    if (Input.GetKey(KeyCode.LeftShift))
	    {
	        moveVector3 *= 0.5f;
	    }

	}
}
