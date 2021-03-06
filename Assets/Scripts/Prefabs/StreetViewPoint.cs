﻿using UnityEngine;
using System.Collections;
using LitJson;

using System;
using System.Text.RegularExpressions;

public class StreetViewPoint : MonoBehaviour
{
    //해당 point의 위도, 경도, 파노라마 ID를 저장
    public float Lat;
    public float Lng;
    public string panoID;
	public bool impo;

	//정보창 여행지 정보 
	public string info_title;
	public string info_country;
	public string info_area;
	public string info_contents;

	//파일경로

	public string info_flag_path;

    //Thumbnail image URL & Streetview name URL
	public string thumbnailURL = "http://maps.google.com/cbk?output=thumbnail&panoid=";
	public string metaURL = "http://maps.google.com/cbk?output=json&panoid=";

    // SHJO ADDED
    public static string wikiURL = "http://en.wikipedia.org/w/api.php?action=query&prop=extracts&format=json&exsentences=1&exlimit=1&exintro=1&explaintext=1&exsectionformat=plain&titles=";
    public static string wikiSearchURL = "http://en.wikipedia.org/w/api.php?action=query&list=search&format=json&srprop=snippet&srsearch=";
    
    //Thumbnail image, name, ID 
	public Texture2D myThumbnailImg;
	public string myThumbnailText;
    OVRThumbnailUI thumbnailUI;

    public string searchKeyword = null;
    public string wikiText = null;
    public bool retrieveMetaData = false;

    public string region = null;
    public string country = null;
    public string description = null;


	// Use this for initialization
    IEnumerator Start() {		
		Debug.Log ("start : " + panoID);
        GameObject OVRCameraRig = GameObject.Find("LeapOVRCameraRig");
        if(OVRCameraRig != null)
        {
            thumbnailUI = OVRCameraRig.GetComponent<OVRThumbnailUI>();
        }

        yield return StartCoroutine(GetThumbnailImage(thumbnailURL));

        while (!retrieveMetaData)
        {
            yield return StartCoroutine(GetLocationText(metaURL + panoID));
        }
        print("Description Text Download Complete");

        yield return StartCoroutine(GetWikiKeyword(wikiSearchURL + searchKeyword));
        yield return StartCoroutine(GetWikiData(wikiURL + searchKeyword));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Application.LoadLevel("StreetViewer");
        }
    }
    void FadeOutEnd()
    {
        //panoID에 따라 Scene 전환(street view)
        Application.LoadLevel("StreetViewer");
    }

	public void Clicked()
	{

	}

	public void Pointed ()
	{
        EarthManager.Instance.thumbnailText = myThumbnailText;
        EarthManager.Instance.thumbnailImg = myThumbnailImg;
        Manager.Instance.SetPlaceID(panoID);

		EarthManager.Instance.title=info_title;
		EarthManager.Instance.country=info_country;
		EarthManager.Instance.area=info_area;
		EarthManager.Instance.contents=info_contents;
		EarthManager.Instance.info_flag_path = info_flag_path;
		print(EarthManager.Instance.info_flag_path);

        if (wikiText != null)
            EarthManager.Instance.wikiText = wikiText;
        else
            EarthManager.Instance.wikiText = "NULL";

        if(thumbnailUI != null)
            thumbnailUI.ShowScreen();
	}
	
    void OnMouseEnter()
	{
        Pointed();
	}

	public void PointedOut()
	{
        if(thumbnailUI != null)
            thumbnailUI.HideScreen();
	}

    void OnMouseExit()
    {
        PointedOut();
    }

	public void SetPosition(PanoramaInfo panoInfo)
    {
		Vector3 _rotation=new Vector3(panoInfo.lat, -panoInfo.lng, 0.0f);
		Vector3 _translate = new Vector3(0, 0, -37.5f);

        Lat = _rotation.x;
        Lng = -(_rotation.y);
		panoID = panoInfo.panoid;
		this.impo = panoInfo.impo;
		info_flag_path = panoInfo.info_flag_path;

		info_title = panoInfo.title;
		info_country = panoInfo.country;
		info_area = panoInfo.area;
		info_contents = panoInfo.contents;


		thumbnailURL = thumbnailURL + panoID;

		Debug.Log(Lat + " " + Lng + " " + panoID );

        transform.Rotate(_rotation);
        transform.Translate(_translate);
    }

    IEnumerator GetThumbnailImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Thumbnail Download Failed. " + www.error);
            yield break;
        }

        // 다운로드 받은 썸네일 이미지 
        myThumbnailImg = www.texture;
        EarthManager.Instance.thumbnailImg = myThumbnailImg;
    }


    IEnumerator GetLocationText(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("PanoID : " + panoID + " WWW Error [Meta Data] : " + www.error);
            retrieveMetaData = false;
            yield break;
        }
        else
        {
            retrieveMetaData = true;
        }
		
		string locationText = "";
		JsonData json = JsonMapper.ToObject(www.text);
		JsonData data = json["Data"];
		
		JsonData location = json["Location"];

        if (location.Keys.Contains("description") && !string.IsNullOrEmpty(location["description"].ToString().Trim()))
		{
			description = location["description"].ToString();
			locationText += description;

            searchKeyword = locationText;
		}
        if (location.Keys.Contains("country") && !string.IsNullOrEmpty(location["country"].ToString().Trim()))
		{
			country = location["country"].ToString();
            if (!string.IsNullOrEmpty(locationText))
                locationText += ", ";
			locationText += country;
		}
        if (location.Keys.Contains("region") && !string.IsNullOrEmpty(location["region"].ToString().Trim()))
		{
			region = location["region"].ToString();
            if (!string.IsNullOrEmpty(locationText))
                locationText += ", ";
            locationText += region;
		}
        
        if(!string.IsNullOrEmpty(description)) // 메타데이터에 세부 위치 정보가 포함되어있으면
        {
            searchKeyword = description; // 해당 장소이름을 키워드로 설정
        }
        else
        {
            searchKeyword = country; // 해당 국가를 키워드로 설정
        }

        searchKeyword = searchKeyword.Replace(" ", "%20"); // 위키피디아 검색을 위해 공백을  '_' 로 치환
		myThumbnailText = locationText;
    }

    string WikiDataNormalizeBySplit(string wikiString) // Normalize by Split
    {
        Regex regex = new Regex("\\([^)]+\\)");
        string[] words = regex.Split(wikiString);

        string result = "";
        for(int i=0; i<words.Length; i++)
        {
            result += words[i];
        }
        return result;
    }

    string WikiDataNormalizeByMatches(string wikiString) // Normalize by Matching
    {
        Regex regex = new Regex("([^()])+(?=\\(|$)");
        MatchCollection matches = regex.Matches(wikiString);

        string result = null;
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                result += match.Value;
        }
        return result;
    }

    IEnumerator GetWikiKeyword(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("WWW Error [GetWikiData] : " + www.error);
            yield break;
        }

        JsonData json = JsonMapper.ToObject(www.text);

        if(json.Keys.Contains("query"))
        {
            JsonData data = json["query"];

            int hits = Convert.ToInt32(data["searchinfo"]["totalhits"].ToString());
            Debug.Log("Hits " + searchKeyword + " : " + hits.ToString());

            if (hits > 0)
            {
                Debug.Log("Origin Search Keyword : " + searchKeyword);
                JsonData search = data["search"];
                searchKeyword = search[0]["title"].ToString();

                searchKeyword = searchKeyword.Replace(' ', '_');
                Debug.Log("New Search Keyword : " + searchKeyword);
            }  
        }
    }

    IEnumerator GetWikiData(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("WWW Error [GetWikiData] : " + www.error);
            yield break;
        }

        JsonData json = JsonMapper.ToObject(www.text);
        if(json.Keys.Contains("query"))
        {
            JsonData data = json["query"];

            JsonData pages = data["pages"];

            if (pages.Count > 0)
            {
                JsonData page = pages[0];

                if (page.Keys.Contains("extract"))
                {
                    wikiText = WikiDataNormalizeBySplit(page["extract"].ToString());
                }
            }
        }

    }
}
