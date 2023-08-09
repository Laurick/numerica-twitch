using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleBehaivour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;

    private int index = 0;
    
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHover()
    {
        Vector3 pos = button.transform.position;
        button.transform.position = new Vector3(pos.x + Random.Range(0f, 800f),
            pos.y+Random.Range(0f, -800f),
            pos.z);
    }
}
