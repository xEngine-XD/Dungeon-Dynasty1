using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamageUI : MonoBehaviour
{
    //[SerializeField] private Transform damagePopUp;
    private TextMeshPro textmesh;
    private float disappearTImer;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        //Transform damagePopupTransform = Instantiate(damagePopUp, Vector2.zero, Quaternion.identity);

    }
    private void Awake()
    {
        textmesh = transform.GetComponent<TextMeshPro>();
        
    }
    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        transform.position += new Vector3(0, speed) * Time.deltaTime;
        disappearTImer -= Time.deltaTime;
        if (disappearTImer < 0)
        {
            float disSpeed = 20f;
            color.a -= disSpeed * Time.deltaTime;
            textmesh.color = color;
            if(color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public static DamageUI Create(Vector2 pos, float damage, bool isCrit, bool isPoisoned)
    {
        Transform damagePopupTransform = Instantiate(GameManager.instance.dmgPrefab, pos, Quaternion.identity);
        DamageUI damagepopup = damagePopupTransform.GetComponent<DamageUI>();
        damagepopup.Setup(damage, isCrit, isPoisoned);
        Debug.Log("sss");
        return damagepopup;
    }
    public void Setup(float damage, bool isCrit, bool isPoisoned)
    {
        textmesh.SetText(damage.ToString());
        if (!isCrit)
        {
            textmesh.fontSize = 4f;
            color = UtilsClass.GetColorFromString("FFFFFF");
        }
        if (isCrit)
        {
            textmesh.fontSize = 7f;
            color = UtilsClass.GetColorFromString("DB5656");
        }
        if (isPoisoned)
        {
            textmesh.fontSize = 4f;
            color = UtilsClass.GetColorFromString("58C862");
        }
        textmesh.color = color;
        color = textmesh.color;
        disappearTImer = 1f;
    }
}
