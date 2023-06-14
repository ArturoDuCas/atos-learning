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
    public GameObject[] cards; 

    GameObject NotFoundPanel; 
    private bool showNotFoundPanel;

    private bool firstSearch = true; 

    void Awake() {
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
        if (firstSearch) {
            cards = GameObject.FindGameObjectsWithTag("ExamCard");
            firstSearch = false; 
        }

        showNotFoundPanel = true; 
        // bool emptySearchInput = string.IsNullOrEmpty(searchInput);
        foreach(GameObject card in cards) {
            string cardTitle = card.transform.Find("ExamTitle").gameObject.GetComponent<TextMeshProUGUI>().text.ToLower();
            Debug.Log(cardTitle); 
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