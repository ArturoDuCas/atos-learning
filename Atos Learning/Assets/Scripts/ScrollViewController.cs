using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private bool isDragging = false;

    public void setDragging(bool value) {
        isDragging = value;

        GameObject[] examCards = GameObject.FindGameObjectsWithTag("ExamCard");
        foreach(GameObject examCard in examCards) {
            examCard.GetComponent<ExamCardScript>().isDragging = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        setDragging(true);
    }

    public void OnEndDrag(PointerEventData eventData) {
        setDragging(false);
    }
    
}
