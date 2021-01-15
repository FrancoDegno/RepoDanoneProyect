using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColors : MonoBehaviour
{
    [Range(0, 1)]
    public float redTolerance;
    [Range(0, 1)]
    public float greenTolerance;
    [Range(0, 1)]
    public float blueTolerance;


    [SerializeField]
    Color colorAtReplaceHair= new Color();
    [SerializeField]
    Color colorAtReplaceCloth = new Color();

    [SerializeField]
    Color[] hairColor = new Color[5];
    [SerializeField]
    Color[] clothsColor = new Color[5];
    List<Material> lHairmats = new List<Material>();
    List<Material> lClothsmats = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).tag=="hair")
            {
                lHairmats.Add(transform.GetChild(i).GetComponent<SpriteRenderer>().material);
            }
            if(transform.GetChild(i).tag=="cloth")
            {
                lClothsmats.Add(transform.GetChild(i).GetComponent<SpriteRenderer>().material);
            }

        }

        lHairmats.ForEach(x =>
        {
            x.SetColor("_ColorToChange", colorAtReplaceHair);
            x.SetFloat("_TolerancyRed", redTolerance);
            x.SetFloat("_TolerancyGreen", greenTolerance);
            x.SetFloat("_TolerancyBlue", blueTolerance);
        });


        lClothsmats.ForEach(x => x.SetColor("_ColorToChange",colorAtReplaceCloth));

        Color myRdmColor = hairColor[Random.Range(0, 5)];
        Color myRdmColorC = clothsColor[Random.Range(0, 5)];

        lHairmats.ForEach(x => x.SetColor("_DesiredColor", myRdmColor));
        lClothsmats.ForEach(x => x.SetColor("_DesiredColor", myRdmColorC));

    }

}
