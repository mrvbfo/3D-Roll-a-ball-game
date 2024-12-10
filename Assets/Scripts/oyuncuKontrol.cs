using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class oyuncuKontrol : MonoBehaviour
{
    //sahnedeki oyuncunun referansý
Rigidbody oyuncuRb;
    //oyun hýzý
    public float hiz=10;
    int skor = 0;
    public Text skorText;
    public Text uyariText;
    public Text tebrikText;
    public float yatay;
    public float dikey;

    Renderer rend;

    //ses olaylarý
    AudioSource sesKaynagi;
    public AudioClip tebrikSesi;
    public AudioClip uyariSesi;
    public AudioClip carpmaSesi;

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody ref baðlantýsý
        oyuncuRb=GetComponent<Rigidbody>();
        //renderer componentini çaðýr
        rend = GetComponent<Renderer>();
        //ses kaynaðýnýn referansý
        sesKaynagi = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //Input GetAxis
            yatay = Input.GetAxis("Horizontal");
            dikey = Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            yatay = Input.acceleration.x;
            dikey = Input.acceleration.y;
        }
        else
        {
            yatay = Input.GetAxis("Horizontal");
            dikey = Input.GetAxis("Vertical");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 zipla = new Vector3(0f, 500f, 0f);
            oyuncuRb.AddForce(zipla);
        }
        //vector oluþturma
        Vector3 kuvvet = new Vector3(yatay, 0f, dikey);

        //kuvvet vektörünü uygula
        oyuncuRb.AddForce(kuvvet*hiz);
    }

    //trigger enter eventi
    //oyuncu elmasý çarpýnca elmasý yok edecek ve skor elde edecek
    
    //isTrigger true olmalý
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Elmaslar"))
        {
            // Destroy(other.gameObject); //nesneyi yok eder
            sesKaynagi.PlayOneShot(carpmaSesi);
            other.gameObject.SetActive(false); //nesneyi gizler
            skor += 10;
            Debug.Log("Skor " + skor);
            skorText.text = "Skor:"+ skor.ToString();
        }
        else if(other.CompareTag("sonElmas") && skor==90)
        {
            sesKaynagi.PlayOneShot(carpmaSesi);
            Destroy(other.gameObject);
            skor += 10;
            skorText.text = "Skor:" + skor.ToString();
            //Tebrikler mesajý
            tebrikText.text = "Tebrikler seviye tamamlandý";
            // 3 sn bekleyip sonraki level(sahne) geçiþi
            StartCoroutine(TebrikBeklet());

        }
        else
        {
            //uyarý mesajý yazdýrma
            uyariText.text = "Bu elmas en son toplanacak!!";
            //3 sn bekleyecek ve gidecek
            StartCoroutine(Beklet());

        }
    }

    IEnumerator Beklet()
    {
        sesKaynagi.PlayOneShot(uyariSesi);
        yield return new WaitForSeconds(3f);
        uyariText.text = "";

    }

    IEnumerator TebrikBeklet()
    {
        sesKaynagi.PlayOneShot(tebrikSesi);
        yield return new WaitForSeconds(3f);
        //sahne deðiþtir
        SceneManager.LoadScene("cikisSahnesi");
    }


    //fiziksel etkileþim
    //isTrigger false olmalý
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="elmas")
        {
            Destroy(collision.gameObject);
            skor += 10;
            Debug.Log("Skor " + skor);
        }
    }
    */

    public Material m1;
    public Material m2;

    bool degisti = true;
    //dinamik material deðiþimi
    private void OnMouseDown()
    {
        if(degisti)
        {
            rend.material = m1;
            degisti = false;
        }
        else
        {
            rend.material = m2;
            degisti = true;

        }
    }
}
