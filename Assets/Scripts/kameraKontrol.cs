using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraKontrol : MonoBehaviour
{
    //sahnedeki oyuncunun referans�
    public GameObject oyuncu;
    

    //oyuncu ve kamera aras�ndaki sabit fark vekt�r�
    Vector3 ofset;
    // Start is called before the first frame update
    void Start()
    {
        //ofset hesab� yap�lacak
        ofset = transform.position - oyuncu.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //yeni konum hesab�
        transform.position = oyuncu.transform.position + ofset;
    }
}
