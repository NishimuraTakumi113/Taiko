using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OMP")
        {
            collision.gameObject.GetComponent<NotesController>().DestroyInEditMode();
        }
    }
}
