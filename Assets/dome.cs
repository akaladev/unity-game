using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class dome : MonoBehaviour {
    public string webID;
    public string webAnimal;
    public string webInstruction;
    public string webinterval;
    public string webintervalInstruction;
    public string curwebAnimal;
    public string curwebInstruction;
    public string instruction;
    public string animalID;
    public string animalImage;
    public string intervalImage;
    public string intervalInstruction;
    public Animal animal;
    public List<qrcode> qrlist;
    public WWW url;
    public WWW www;
    public bool flag = true;
    public Vector2 position = new Vector2(0, 0);

    // Use this for initialization
    void Start () {
        webID = "http://70.32.108.82:3000/qrcode/findByList/";
        webAnimal = "http://70.32.108.82:3000/animal/";
        webInstruction = "http://70.32.108.82:3000/animal/";
        webinterval = "http://70.32.108.82:3000/animal/";
        webintervalInstruction = "http://70.32.108.82:3000/animal/";
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
                //flag = true;
            }

            // retrieve animalImage
            curwebAnimal = webAnimal + animalID;
            animalImage = "animalPicture" + getAnimalName(curwebAnimal);
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
                //flag = true;
            }

            // retrieve intervalImage
            //intervalImage =  getAnimalName(webinterval);
            intervalImage = "mammals";
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
                   animal = JsonUtility.FromJson<Animal>(www.text);
                   animalImage = animal.Image;
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
                    animal = JsonUtility.FromJson<Animal>(www.text);
                    instruction = animal.Description;
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
                    //animalID = dissect(www.text);
                    JSONObject j = new JSONObject(www.text);
                    animalID = accessData(j);
                    //qrlist = JsonUtility.FromJson<List<qrcode>>(www.text);
                    //animalID = qrlist[qrlist.Count - 1].AnimalId;
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

    string dissect(string obj)
    {
        string ret = "";

        ret = obj.Substring(obj.Length - 3, 1);

        return ret;
    }


    string accessData(JSONObject obj)
    {
        string ret = "";

        JSONObject json = obj.list[obj.list.Count-1];
        JSONObject field = json.list[1];
        ret = field.str;

        return ret;
    }
}
