using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class oyuncuKontrol : MonoBehaviour
{
    //sahnedeki oyuncunun referans�
Rigidbody oyuncuRb;
    //oyun h�z�
    public float hiz=10;
    int skor = 0;
    public Text skorText;
    public Text uyariText;
    public Text tebrikText;
    public float yatay;
    public float dikey;

    Renderer rend;

    //ses olaylar�
    AudioSource sesKaynagi;
    public AudioClip tebrikSesi;
    public AudioClip uyariSesi;
    public AudioClip carpmaSesi;

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody ref ba�lant�s�
        oyuncuRb=GetComponent<Rigidbody>();
        //renderer componentini �a��r
        rend = GetComponent<Renderer>();
        //ses kayna��n�n referans�
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
        //vector olu�turma
        Vector3 kuvvet = new Vector3(yatay, 0f, dikey);

        //kuvvet vekt�r�n� uygula
        oyuncuRb.AddForce(kuvvet*hiz);
    }

    //trigger enter eventi
    //oyuncu elmas� �arp�nca elmas� yok edecek ve skor elde edecek
    
    //isTrigger true olmal�
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
            //Tebrikler mesaj�
            tebrikText.text = "Tebrikler seviye tamamland�";
            // 3 sn bekleyip sonraki level(sahne) ge�i�i
            StartCoroutine(TebrikBeklet());

        }
        else
        {
            //uyar� mesaj� yazd�rma
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
        //sahne de�i�tir
        SceneManager.LoadScene("cikisSahnesi");
    }


    //fiziksel etkile�im
    //isTrigger false olmal�
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
    //dinamik material de�i�imi
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
