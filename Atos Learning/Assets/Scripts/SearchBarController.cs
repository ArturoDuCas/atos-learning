using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchBarController : MonoBehaviour
{
    [SerializeField]
    private GameObject searchInputObject;
    private string searchInput; 
    GameObject[] cards; 

    private GameObject NotFoundPanel; 
    private bool showNotFoundPanel;

    void Awake() {
        cards = GameObject.FindGameObjectsWithTag("ExamCard");
        NotFoundPanel = GameObject.Find("NotFoundPanel");
    }

    public void Start() {
        NotFoundPanel.SetActive(false);
    }

    public void onSearchButtonClicked() {
        searchInput = searchInputObject.GetComponent<TMP_InputField>().text.ToLower();
        makeSearch(); 
    }


    private void makeSearch() {
        showNotFoundPanel = true; 
        foreach(GameObject card in cards) {
            string cardTitle = card.transform.Find("ExamTitle").gameObject.GetComponent<TextMeshProUGUI>().text.ToLower();
            if (cardTitle.Contains(searchInput)) {
                showNotFoundPanel = false;
                card.SetActive(true);
            } else {
                card.SetActive(false);
            }
        }
        NotFoundPanel.SetActive(showNotFoundPanel);
    }

    // Start is called before the first frame update
    public void Search() {

    }
}
