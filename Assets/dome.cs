using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class dome : MonoBehaviour {
    public string webID = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
    public string webAnimal = "http://images.earthcam.com/ec_metros/ourcams/";
    public string webInstruction = "http://images.earthcam.com/ec_metros/ourcams/";
    public string webinterval = "http://images.earthcam.com/ec_metros/ourcams/";
    public string webintervalInstruction = "http://images.earthcam.com/ec_metros/ourcams/";
    public string curwebAnimal;
    public string curwebInstruction;
    public string instruction;
    public string animalID;
    public string animalImage;
    public string intervalImage;
    public string intervalInstruction;
    public WWW url;
    public WWW www;
    public bool flag = true;
    public Vector2 position = new Vector2(0, 0);

    // Use this for initialization
    void Start () {
        //url = new WWW("");
    }
	
	// Update is called once per frame
	void Update () {

        // retrieve animal ID
        animalID = getAnimalID(webID);
        if (animalID != "")
        {
            // retrieve instruction
            curwebInstruction = webInstruction + animalID;
            instruction = getInstruction(webID);
            if (instruction != "")
            {
                flag = true;
            }

            // retrieve animalImage
            curwebAnimal = webAnimal + animalID;
            animalImage = getAnimalName(curwebAnimal);
            if (animalImage != "")
            {
                // drawSprite
                drawSprite(animalImage);
            }
        }
        else
        {
            // retrieve intervalInstruction
            intervalInstruction = getInstruction(webintervalInstruction);
            if (intervalInstruction != "")
            {
                flag = true;
            }

            // retrieve intervalImage
            intervalImage = getAnimalName(webinterval);
            if (intervalImage != "")
            {
                // drawSprite
                drawSprite(intervalImage);
            }
        } 
    }

    // DoWWW
    private IEnumerator DoWWW(string web)
    {
        www = new WWW(web);
        yield return www;
    }

    // OnGUI
    void OnGUI()
    {
        if (flag)
        {
            GUI.contentColor = Color.black;
            Rect textArea = new Rect(10, 10, 100, 100);
            GUI.Label(textArea, instruction);
        }
    }

    // getAnimalName
    public string getAnimalName(string web)
    {
        string animalImage = "";

        StartCoroutine(DoWWW(web));
        if (www != null)
        {
            try
            {
                if (www.text != null)
                {
                    animalImage = www.text;
                }
            }
            catch { }
        }

        return animalImage;
    }

    // getInstruction
    public string getInstruction(string web)
    {
        string instruction = "";

        StartCoroutine(DoWWW(web));
        if (www != null)
        {
            try
            {
                if (www.text != null)
                {
                    instruction = www.text;
                }
            }
            catch { }
        }

        return instruction;
    }

    // getAnimalID
    public string getAnimalID(string web)
    {
        string animalID = "";

        StartCoroutine(DoWWW(web));
        if (www != null)
        {
            try
            {
                if (www.text != null)
                {
                    animalID = www.text;
                }
            }
            catch {  }
        }

        return animalID;
    }

    // drawSprite
    void drawSprite(string name)
    {
        transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
        transform.localScale = new Vector2(0.5F, 0.5F);
        //transform.position = position;
    }
}
