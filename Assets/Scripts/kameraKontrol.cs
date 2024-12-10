using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraKontrol : MonoBehaviour
{
    //sahnedeki oyuncunun referansý
    public GameObject oyuncu;
    

    //oyuncu ve kamera arasýndaki sabit fark vektörü
    Vector3 ofset;
    // Start is called before the first frame update
    void Start()
    {
        //ofset hesabý yapýlacak
        ofset = transform.position - oyuncu.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //yeni konum hesabý
        transform.position = oyuncu.transform.position + ofset;
    }
}
