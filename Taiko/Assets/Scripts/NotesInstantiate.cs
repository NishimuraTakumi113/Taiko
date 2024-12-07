using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInstantiate : MonoBehaviour
{
    public GameObject NotesPrefab;//生成するprefab
    private GameObject currentInstance;//生成したprefabを動かすための箱
    

    void OnMouseDown()
    {
        // マウス位置を取得してワールド座標に変換
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // カメラからの距離を設定
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        currentInstance = Instantiate(NotesPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        currentInstance.GetComponent<NotesController>().isSelected = true;
        currentInstance.GetComponent<NotesController>().speed = EditorMelody.melodySpeed;
    }

    void Update(){
        // オブジェクトが選択されている場合
        if (Input.GetMouseButton(0) && currentInstance != null)
        {
            // マウス位置を取得してワールド座標に変換
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(currentInstance.transform.position).z; // オブジェクトのZ座標を維持
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if(worldPosition.y >= -1 && worldPosition.y <= 1){
                worldPosition.y = 0;
            }

            // オブジェクトをマウス位置に移動
            currentInstance.transform.position = worldPosition;            
        }

        // オブジェクトが選択解除された場合
        if (Input.GetMouseButtonUp(0))
        {
            if(currentInstance == null){
                return;
            }
            if(currentInstance.transform.position.y < -1 || currentInstance.transform.position.y > 1){
                Destroy(currentInstance);
                currentInstance = null;
            }else{
                // オブジェクトの参照をクリア
                currentInstance = null;
            }
            
        }
    }

}
