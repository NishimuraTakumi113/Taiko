using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSensor : MonoBehaviour
{
    // 最短の音符を格納する変数
    public string hitTag = "OMP";
    private GameObject hitNote = null;

    private float position;
    public float offset = 0f;

    //良、可、不可のText
    [SerializeField] private GameObject PerfectText;
    [SerializeField] private GameObject GoodText;
    [SerializeField] private GameObject MissText;


    public GameObject DonImage;
    public GameObject KaImage;


    [SerializeField] private float FindDistance = 1.0f;
    [SerializeField] private float SensorDistance = 0.5f;
    [SerializeField] private float PerfectDistance;
    
    void Start()
    {
        position = transform.position.x + offset;
        DeleteText();
    }

    // Update is called once per frame
    void Update()
    {
        hitNote = FindNearestNote();
        if(Input.GetKeyDown(KeyCode.F)||Input.GetKeyDown(KeyCode.J)){
            DonImage.SetActive(true);
            HitNote(1, hitNote);
        }else if(Input.GetKeyUp(KeyCode.F)||Input.GetKeyUp(KeyCode.J)){
            DeleteText();
            DonImage.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.K)){
            KaImage.SetActive(true);
            HitNote(2, hitNote);
        }else if(Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.K)){
            DeleteText();
            KaImage.SetActive(false);
        }
    }

    GameObject FindNearestNote()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag(hitTag);
        //対象なし
        if(notes.Length == 0){return null;}
        float minDistance = FindDistance;

        GameObject nearestNote = null;
        foreach(GameObject note in notes){
            float distance = Mathf.Abs(transform.position.x - note.transform.position.x);
            //最短の音符を更新
            if(distance < minDistance){
                minDistance = distance;
                nearestNote = note;
            }
        }
        
        return nearestNote;
    }

    private void HitNote(int ButtonType, GameObject hitNote){
        if (hitNote != null){
            int type = hitNote.GetComponent<NotesController>().type;
            bool isCorrrct = (ButtonType == type);//正しい譜面が叩かれたか
            if(isCorrrct){
                //正しい音符が叩かれた場合
                if(Mathf.Abs(hitNote.transform.position.x - position) <= SensorDistance &&Mathf.Abs(hitNote.transform.position.x - position) > PerfectDistance){
                    hitNote.GetComponent<NotesController>().GoodHit();
                    GoodText.SetActive(true);

                }else if(Mathf.Abs(hitNote.transform.position.x - position) <= PerfectDistance){
                    hitNote.GetComponent<NotesController>().PerfectHit();
                    PerfectText.SetActive(true);
                }else{
                    hitNote.GetComponent<NotesController>().MissHit();
                    MissText.SetActive(true);
                }
            }else{
                //間違った音符が叩かれた場合
                hitNote.GetComponent<NotesController>().MissHit();
                MissText.SetActive(true);
            }
        }
    }

    private void DeleteText(){
        PerfectText.SetActive(false);
        GoodText.SetActive(false);
        MissText.SetActive(false);
    }
}
