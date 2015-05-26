﻿using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PanoramaInfo
{
    public float lat;
    public float lng;
    public string panoid;
	public bool impo;

    public PanoramaInfo(float lat, float lng, string panoid, bool impo = true)
    {
        this.lat = lat;
        this.lng = lng;
        this.panoid = panoid;
        this.impo = true; //  impo;
    }
}

public class CreateTarget : MonoBehaviour
{
    public GameObject obj;
    public GameObject earth;

    public List<PanoramaInfo> panoramas = new List<PanoramaInfo>();
    //public PanoramaInfo[] panoramas = new PanoramaInfo[33];

    //생성되는 객체에서 객체를 translate할 때 사용
    public static Vector3 _rotation;    //회전 vector
    public static Vector3 _translate;   //이동 vector

    //입력된 값을 float로 저장할 변수
    public float Lat;                   //위도 -90~90
    public float Lng;                  //경도 0~360
    public static string panoID;
	public bool impo;

    // Use this for initialization
    void Start()
    {
        // 남극
        panoramas.Add( new PanoramaInfo(-62.596087f, -59.901655f, "ZzuMubmHCfCGGo3ePSlpCQ", true) ); // 펭귄 서식지

        // 네덜란드
        panoramas.Add ( new PanoramaInfo(51.881662f, 4.643277f, "e1H3GgZkFLIvm2UcV0AZeA", true)); //엘슈트 풍차망
        panoramas.Add ( new PanoramaInfo(52.113498f, 4.280813f, "So0MXeaPWZXH0Dp1r-Z_Tw", false)); //스헤베닝겐

        // 네팔
        panoramas.Add ( new PanoramaInfo(27.931109f, 86.804662f, "TLc2AM_nU88AAAQpjCtyxw", false)); //Everest Base Camp
        panoramas.Add ( new PanoramaInfo(27.790246f, 86.718457f, "Cypswt1Mnvde-rgEl5jMUQ", true)); //Hillary Suspension Bridge
        panoramas.Add ( new PanoramaInfo(27.994511f, 86.828381f, "_JmXNYUY9qAZHxIyYUWhRQ", false)); //Kala Pattar
        panoramas.Add ( new PanoramaInfo(27.718398f, 86.716000f, "kFuSB9hD_6yJy8oxmjr1RQ", false)); //Mudslide Bridge
        panoramas.Add ( new PanoramaInfo(27.926364f, 86.796615f, "YJCqovBj5_EKjvQ_9tSvQQ", false)); //Taboche Mountain

        // 노르웨이
        panoramas.Add ( new PanoramaInfo(59.857361f, 6.358727f, "ZxCWoCUti1cbv7_pMLzBhg", false)); //피아에라

        // 대만
        panoramas.Add ( new PanoramaInfo(25.038821f, 121.560306f, "htCkJktfaeM2DvPEWyg3-A", false)); //선얏센 기념관
        panoramas.Add ( new PanoramaInfo(25.036556f, 121.517962f, "uM-jW1fT93Y_eJ7CPTG6jA", true)); //장개석 기념관

        // 대한민국
        panoramas.Add ( new PanoramaInfo(37.571199f, 126.968461f, "zMrHSTO0GCYAAAQINlCkXg", false)); //Gyeonghui Palace
        panoramas.Add ( new PanoramaInfo(33.459519f, 126.939750f, "IUmXlW6pRu1w9QnUdQk4vw", false)); //성산일출봉
        panoramas.Add ( new PanoramaInfo(33.527656f, 126.769749f, "qrvXxrP3D4oAAAQWtNh2dQ", true)); //만장굴

        // 러시아
        panoramas.Add ( new PanoramaInfo(56.309959f, 38.129472f, "GrFw-S4a5A6xdQWs9FhEhw", false)); //The Holy Trinity-St.Sergius Lavra
        panoramas.Add ( new PanoramaInfo(43.354961f, 42.439195f, "A5oCY2VD27HtxwTgBfI7Jw", false)); //West Summit

        // 루마니아
        panoramas.Add ( new PanoramaInfo(45.713056f, 24.151476f, "G7adRdSGuIm7K-MFvKLCUw", false)); //시스나디에 요새 교회

        // 마다가스카르
        panoramas.Add ( new PanoramaInfo(-20.250515f, 44.418992f, "88qwsWCYsJ8xXo2W1kjIXw", false)); //Baobab-Andronomea
        panoramas.Add ( new PanoramaInfo(-23.541324f, 43.779575f, "6R4EPTccvk941C4fobch5g", false)); //St. Augustin
        panoramas.Add ( new PanoramaInfo(-18.923246f, 47.532037f, "mYYUd4xWYQYgepHeFmYWxA", false)); //Antananarivo
        panoramas.Add ( new PanoramaInfo(-18.566859f, 43.887191f, "Eu_xDQX5IZPRJKK3L3zafA", false)); //Nosy Manghily
        panoramas.Add ( new PanoramaInfo(-13.480002f, 48.326310f, "0NKx1dxm9zHKuh3QLJkddQ", false)); //Nosy Komba

        // 멕시코
        panoramas.Add ( new PanoramaInfo(19.434387f, -99.141486f, "AS1HPRlUroiellKWwJ_N8w", false)); //예술의 궁전

        // 모나코
        panoramas.Add ( new PanoramaInfo(43.730309f, 7.426062f, "b6XBf_pbLLQAAAQJKij6qQ", false)); //Roche Saint Nicholas

        // 미국
        panoramas.Add ( new PanoramaInfo(38.625576f, -90.188635f, "fFRuX5QWUEISQWxwl8HGuw", false)); //게이트웨이 아치
        panoramas.Add ( new PanoramaInfo(36.065096f, -112.137107f, "Fa-wHCWazJG6bn7ZjISQCA", true)); //Grand Canyon
        panoramas.Add ( new PanoramaInfo(37.172210f, -93.322539f, "ayOK2SvpqYzlk4rm1Q4KPQ", true)); //Mizumoto Japanese Stroll Garden
        panoramas.Add ( new PanoramaInfo(-3.825792f, -32.396424f, "rfgb-QoM16gAAAQY_-2qjg", true)); //돌고래 Catlin Seaview Survey
        panoramas.Add ( new PanoramaInfo(40.688619f, -74.044125f, "cthAMoR7m9cmP-wUC8AIWA", false)); //Statue of Liberty National Monument
        panoramas.Add ( new PanoramaInfo(34.236399f, -77.953794f, "l0eKEYaRXbn6csDEH_bhPw", false)); //Battleship NORTH CAROLINA
        panoramas.Add ( new PanoramaInfo(35.071705f, -82.886928f, "gaeVY57miaQwXI2gBlIbcg", false)); //Jocassee Gorges
        panoramas.Add ( new PanoramaInfo(21.269602f, -157.695598f, "_HKq3kJG3NNXV9ElI9J8Wg", false)); //Hanauma Bay
        panoramas.Add ( new PanoramaInfo(21.277735f, -157.833614f, "27zsgCoYo3GFeBSLD3IXfw", false)); //Waikiki Beach
        panoramas.Add ( new PanoramaInfo(65.853701f, 51.403202f, "1R1EqRq4A_vnGToVxvCV-A", true)); //Byodo-In Temple
        panoramas.Add ( new PanoramaInfo(35.031650f, -111.026837f, "S2IQmPwHGhJ-YCXugFkM-Q", false)); //Meteor Crater
        panoramas.Add ( new PanoramaInfo(21.675655f, -158.039678f, "I3e6j5rY_OEMj_G8sjnxOQ", false)); //Sunset Beach
        panoramas.Add ( new PanoramaInfo(33.127904f, -117.311509f, "MSY7skkC8g5A5L2gDwWwQg", false)); //Lego Land
        panoramas.Add ( new PanoramaInfo(28.474642f, -81.466547f, "sglA5AcgqpG0sQ9o1gDOSg", false)); //Universal Studios
        panoramas.Add ( new PanoramaInfo(21.306635f, -157.859008f, "C5J9QXgPhZo-LnZvZ7FWCg", false)); //Iolani Palace
        panoramas.Add ( new PanoramaInfo(20.634702f, -156.497456f, "FUMjYMh38n_EitPWdt70RA", false)); //Maui, Hawaii, Molokini Crater
        panoramas.Add ( new PanoramaInfo(20.311091f, -87.036529f, "691D23S_viUAAAQJKigqjA", false)); //Columbia Deep
        panoramas.Add ( new PanoramaInfo(21.479702f, -86.632599f, "xq5H2tvkw_AAAAQJLmg-7A", true)); //Whale Sharks
        panoramas.Add ( new PanoramaInfo(19.419751f, -155.288205f, "NsXMzG6eODgN7UjSs9G2nA", false)); //Hawaii Volcanoes National Park
        panoramas.Add ( new PanoramaInfo(19.927064f, -155.886999f, "XTcMO38szyVGzYIDvpyIzQ", false)); //Hilton Waikoloa Village
        panoramas.Add ( new PanoramaInfo(47.620012f, -122.349345f, "TlSB0Ge9RrEPPI-oln365A", false)); //Space Needle
        panoramas.Add ( new PanoramaInfo(43.877169f, -103.456022f, "5c1XDr-37d5xdtmWQ79LDw", true)); //Mount Rushmore National Memorial
        panoramas.Add ( new PanoramaInfo(46.552484f, -86.459171f, "hodDbHroQu5nFeLgwWxtlw", false)); //Pictured Rocks National Lakeshore
        panoramas.Add ( new PanoramaInfo(-33.956262f, 151.265231f, "4p_ncfuaBCcAAAQfCZU9KA", true)); //Sydney Magic Point Sharks
        panoramas.Add ( new PanoramaInfo(-33.802112f, 151.302226f, "GFwh75SkDaEAAAQfCZXXmw", true)); //Sydney Outside Manly
        panoramas.Add ( new PanoramaInfo(-18.246795f, 147.386806f, "hvJbgfc-HtoAAAQfCaR8Mw", false)); //Myrmidon Reef
        panoramas.Add ( new PanoramaInfo(-16.427967f, 145.989965f, "U5OACYt--5UAAAQfCbAC4A", false)); //Norman Reef
        panoramas.Add ( new PanoramaInfo(-14.865306f, 145.680527f, "nNw59vW3owAAAAQfCaaiYQ", true)); //Minke Whales
        panoramas.Add ( new PanoramaInfo(-13.921229f, 144.641367f, "MGB3IbSxGxkAAAQfCa_m3w", false)); //North Broken Passage

        // 벨리즈
        panoramas.Add ( new PanoramaInfo(17.204902f, -87.544919f, "ByV4feOfB24AAAQWx3MuEw", false)); //Half Moon Caye

        // 브라질
        panoramas.Add ( new PanoramaInfo(-5.774762f, -35.197332f, "CkmCkfwvIGUAAAQW-qy0KQ", false)); //후아 카페 필류
        panoramas.Add ( new PanoramaInfo(-3.137761f, -60.493355f, "1ci-8iBT_UuG1dlrUy1vzg", false)); //Rio Negro 아마존
        panoramas.Add ( new PanoramaInfo(-3.078972f, -60.038147f, "NMd4iWDeabMAAAQXH9ow_g", false)); //Rua3

        // 스페인
        panoramas.Add ( new PanoramaInfo(41.404212f, 2.174820f, "lXSocJr_ApZrMdq_CrfUNg", false)); //사그라다 파밀리아
        panoramas.Add ( new PanoramaInfo(40.961402f, -5.666230f, "4UvBd8mYTnnAnQLOLG_AOw", false)); //쿠엥카 구 성곽도시
        panoramas.Add ( new PanoramaInfo(40.948054f, -4.116914f, "XQN5MyfKSxK9qNT1TQleJw", false)); //세고비아 구 시가지
        panoramas.Add ( new PanoramaInfo(40.419397f, -3.693281f, "1Hdng3Mj9y_OU-37-vnpkg", false)); //시벨레스 광장

        // 아랍에미리트 연합
        panoramas.Add ( new PanoramaInfo(25.195232f, 55.276428f, "eznax7JvTOXrAztKUfqj-A", true)); //Burj Khalifa
        panoramas.Add ( new PanoramaInfo(23.148599f, 53.735961f, "ZGBLHEQEe8-bc5E4tO8ekg", false)); //Liwa Desert

        // 아르헨티나
        panoramas.Add ( new PanoramaInfo(-32.653135f, -70.012024f, "W9nMFNrPp8GrTDYyVaHYBg", false)); //Aconcagua Summit

        // 안도라
        panoramas.Add ( new PanoramaInfo(42.545339f, 1.473427f, "me3DnHB6RmyGW8HEqAb5SQ", false)); //Carrer Major

        // 영국
        panoramas.Add ( new PanoramaInfo(57.145373f, -4.672888f, "QHdANWuiOm808C1fvSBVKA", false)); //Loch Ness
        panoramas.Add ( new PanoramaInfo(52.201062f, -2.458700f, "M4Nd42bBHLn7XLm3SR31Iw", false)); //Brockhampton Estate
        panoramas.Add ( new PanoramaInfo(52.457104f, -0.553142f, "Nyg5vDL24yfSxm3NqcL0sg", false)); //Lyveden New Bield
        panoramas.Add ( new PanoramaInfo(51.474628f, -0.295141f, "GQFI5Us6gc9TD0xqbBt9cg", false)); //큐 왕립식물원
        panoramas.Add ( new PanoramaInfo(51.500891f, -0.122697f, "F0cE4sooBkl9S7ERvTvwIQ", false)); //빅벤
        panoramas.Add ( new PanoramaInfo(51.506607f, -0.074633f, "4cuk-4G-vKM63svR1rsFHw", false)); //타워 브리지
        panoramas.Add ( new PanoramaInfo(51.178946f, -1.826564f, "VxzhBNNu-VGQC8HtVIaY3A", true)); //스톤헨지
        panoramas.Add ( new PanoramaInfo(50.116583f, -5.478983f, "b56yzD7fyUBnqVVxFDivBQ", false)); //St Michael's Mount
        panoramas.Add ( new PanoramaInfo(50.688888f, -1.956280f, "PbGl6pMgny7wahDD7z8AtQ", false)); //Brownsea island

        // 오스트리아
        panoramas.Add ( new PanoramaInfo(48.203845f, 16.361633f, "B01M9xj0fuo8Lbc5xtxtew", false)); //미술사 박물관

        // 우크라이나
        panoramas.Add ( new PanoramaInfo(48.522029f, 26.498246f, "ErnD9tZimEkkZ6VliXUphw", false)); //Chotin Castle
        panoramas.Add ( new PanoramaInfo(48.296993f, 25.924853f, "ydVs_ymCLcW5hmiKpHHqyw", false)); //Chernivtsi State University

        // 이집트
        panoramas.Add ( new PanoramaInfo(30.851549f, 29.664156f, "v5y4cLV08mWD7mA-BFkjoA", false)); //Monastery of Saint Mina
        panoramas.Add ( new PanoramaInfo(29.975257f, 31.138729f, "M9XnT_J1KWLORH4M-_v6sA", true)); //Great Sphinx of Giza
        panoramas.Add ( new PanoramaInfo(30.028667f, 31.261899f, "NbrCuxFkvYi3U6eQH-sT5Q", false)); //Cairo Citadel
        panoramas.Add ( new PanoramaInfo(31.213650f, 29.885435f, "TjHhwodKW_89yP5IzTGaeQ", false)); //Citadel of Qaitbay
        panoramas.Add ( new PanoramaInfo(29.978089f, 31.135899f, "-B-Q0pE9LLgmDpLK38lWCQ", true)); //Khufu Ship, Pyramids

        // 이탈리아
        panoramas.Add ( new PanoramaInfo(44.315980f, 6.174589f, "p85y89ZBexcxofkwZpkHBQ", true)); //Area marina protetta Portofino
        panoramas.Add ( new PanoramaInfo(45.432005f, 12.328098f, "XKoFzFD4LeEyJozHuqTpzg", false)); //Canali di Venezia
        
        panoramas.Add ( new PanoramaInfo(38.626372f, 15.063730f, "n9zp8zDXdpDUCDVlLRXOUg", false)); //Beaches of Panarea
        panoramas.Add ( new PanoramaInfo(40.419843f, 15.005841f, "QQaZ4ikxtC4amp8BfxNM8g", false)); //Scavi di Paestum
        panoramas.Add ( new PanoramaInfo(41.074990f, 14.327291f, "leUKewEsRlRNcA5UbHlIyQ", false)); //카세트라 왕궁
        panoramas.Add ( new PanoramaInfo(41.890072f, 12.492534f, "07gbqMWIg_HId5m7W94qHg", false)); //로마 콜로세움
        panoramas.Add ( new PanoramaInfo(43.318912f, 11.331626f, "ONVDa4I8_S4suzYciZQajA", false)); //시에나 역사지구
        panoramas.Add ( new PanoramaInfo(45.464300f, 7.189493f, "MPsvZouZ8u_7Mds8mb39QA", false)); //Milano City Center
   
        panoramas.Add ( new PanoramaInfo(45.813453f, 10.793751f, "GCV_ShhIYj0yp5QTFSnaPQ", false)); //Limone del Garda


        //panoramas.Add ( new PanoramaInfo(45.704014f, 9.662832f, "n9aCTxR7Jr_fv_bEbpGuLg", false)); //Bergamo, Piazza Duomo
        //panoramas.Add ( new PanoramaInfo(43.773372f, 11.255210f, "ioUYqKxbMWJN1lISj9KeAw", false)); //Piazza S. Giovanni
        //panoramas.Add ( new PanoramaInfo(43.761145f, 11.245873f, "W5Ok4jyYaNExJENEqXeNAQ", false)); //보볼리 정원
        //panoramas.Add ( new PanoramaInfo(45.069088f, 7.685052f, "WQjMInatpKgv5zpSZIFxRQ", false)); //Palzzo Carignano
        //panoramas.Add ( new PanoramaInfo(38.009058f, 12.329152f, "lCa2y5_6xXbnCKbfH8f_Rg", false)); //Beaches of Levanzo
        //panoramas.Add ( new PanoramaInfo(37.922971f, 12.283752f, "LqHIDwupkayHNW1llPd4Pw", false)); //Beaches of Favignana
        //panoramas.Add(new PanoramaInfo(46.153988f, 10.899672f, "1Zw4Ngm7xyVm-7orbJEGZQ", true)); //Dolomiti UNESCO
        //panoramas.Add(new PanoramaInfo(46.162368f, 10.917272f, "5_AWZhbCGuqMd0AGfOlOUw", true)); //Dolomiti UNESCO2
        //panoramas.Add ( new PanoramaInfo(46.616730f, 11.938461f, "ygDRTBjjVGqkDjkiqRPvhQ", true)); //Dolomiti UNESCO3
        //panoramas.Add ( new PanoramaInfo(46.650418f, 12.184432f, "uOJCgToPnUIqSKtxqSve_w", true)); //Dolomiti UNESCO4
        //panoramas.Add ( new PanoramaInfo(46.469498f, 12.099685f, "bxH-3FySUJJDxJuMKAjzDw", true)); //Dolomiti UNESCO5
        //panoramas.Add ( new PanoramaInfo(46.487448f, 12.105410f, "Qc_L-FGE0kogf0uLL-ffEw", true)); //Dolomiti UNESCO6
        //panoramas.Add ( new PanoramaInfo(46.383844f, 12.484926f, "1rkysmDNFGc8cQdatZW6kw", true)); //Dolomiti UNESCO7
        //panoramas.Add ( new PanoramaInfo(46.280554f, 11.812249f, "uE9kdskapFzZo06BefILfQ", true)); //Dolomiti UNESCO8
        //panoramas.Add ( new PanoramaInfo(45.624089f, 10.564648f, "WqMNGqP9fIlVWmwnwk7gHw", false)); //Vittoriale degli Italiani
        //panoramas.Add ( new PanoramaInfo(45.606710f, 10.527010f, "S6vn6ZTiexkIv-oZaU6bZw", false)); //Salo
        //panoramas.Add ( new PanoramaInfo(45.425851f, 10.602801f, "hXibr930x7SnwDx65wRYfg", false)); //San Martino della Battaglia
        //panoramas.Add ( new PanoramaInfo(45.472723f, 10.748823f, "7scKHXfK0Is69gQXDza4fw", false)); //Parco termale villa
        //panoramas.Add ( new PanoramaInfo(45.292127f, 11.727542f, "RjjJr4lYyEFuC_DXDjSH6g", false)); //Giardino di Balsanzibio

        // 인도
        panoramas.Add ( new PanoramaInfo(18.922323f, 72.834289f, "1uYAeoWlY4Pqoa6PZBDyZA", false)); //Gateway Of India Mumbai
        panoramas.Add ( new PanoramaInfo(12.305133f, 76.656141f, "ZjGqKKJpM8ofsZrw_CpAbA", false)); //Mysore Palace
        panoramas.Add ( new PanoramaInfo(10.783169f, 79.131424f, "9RWy9wBv0b7hJVk74zmrYw", false)); //Thanjavur Temple
        panoramas.Add ( new PanoramaInfo(15.949466f, 75.815568f, "oG_6nL1zn2L5SiHB7fEqBQ", false)); //Group of Mounments at Pattadakal
        panoramas.Add ( new PanoramaInfo(19.887609f, 86.095086f, "ZBH5-GhJzmP0zuQywSdVqw", false)); //Sun Temple
        panoramas.Add ( new PanoramaInfo(19.152107f, 77.317845f, "XhfJM9dq4mfrlnyX1i354g", false)); //Nanded Gurudwara
        panoramas.Add ( new PanoramaInfo(20.551901f, 75.699198f, "j7W7UotIEokAAAQWjKM7kw", false)); //Ajanta Caves
        panoramas.Add ( new PanoramaInfo(19.900922f, 75.320179f, "PxmiVxloPR5XpPOC3sDb3A", false)); //Bibi Ka Maqbara
        panoramas.Add ( new PanoramaInfo(23.479370f, 77.740076f, "s4zigh3dsnyAYGq4jJw2-w", false)); //Sanchi Buddhist Stupas
        panoramas.Add ( new PanoramaInfo(23.858889f, 72.101782f, "KmSe7jASKiBY1Rqx2aCzxg", false)); //Rani Ki Vav
        panoramas.Add ( new PanoramaInfo(26.280984f, 73.047820f, "GvNKJetyrNX5zhro7l66mw", true)); //Umaid Bhawan Palace
        panoramas.Add ( new PanoramaInfo(27.176266f, 78.021921f, "R0mRqgzFkr3ck05Kgp0BBA", false)); //Agra Fort
        panoramas.Add ( new PanoramaInfo(25.749930f, 82.688613f, "g6UlLzOBanpcMVVQyZp8gQ", false)); //Jaunpur Fort

        // 인도네시아
        panoramas.Add ( new PanoramaInfo(-8.737039f, 119.412259f, "Ri71LYeNvGsAAAQW4U3bQQ", false)); //Komodo Island

        // 일본
        panoramas.Add ( new PanoramaInfo(35.658581f, 139.746281f, "LtUmBSKdGOjeeLlhby9dVA", false)); //도쿄 타워
        panoramas.Add ( new PanoramaInfo(34.295907f, 132.318611f, "kEhefMsEl_j3fSb3YeJ-Jw", false)); //이쓰쿠시마 신사
        panoramas.Add ( new PanoramaInfo(35.012762f, 135.750338f, "QARIAkA8fLMIvXlaG9BKRg", false)); //니조성
        panoramas.Add ( new PanoramaInfo(35.015895f, 135.674029f, "owRdGfCtDxJqJSLOtMUOtw", false)); //텐류지
        panoramas.Add ( new PanoramaInfo(32.806165f, 130.706273f, "5gf4IL6Vpsx5GdU9M3Hufg", false)); //구마모토 성

        // 중국
        panoramas.Add ( new PanoramaInfo(34.557692f, 117.742510f, "rY5ZW6S3sFUAAAAGOzOdpg", true)); //Datamen Street

        // 체코
        panoramas.Add ( new PanoramaInfo(50.088037f, 14.421055f, "YO6UbAb0JdnOJYF2MODFXA", false)); //프라하 역사지구
        panoramas.Add ( new PanoramaInfo(48.810779f, 14.315088f, "l_SHzsaFFzpnRCVJv6OFAQ", false)); //체스키 그룸로프 역사지구
        panoramas.Add ( new PanoramaInfo(49.593827f, 17.250690f, "qYEGZ8uWn_pI2lUseQDyeg", false)); //올로모우츠의 성삼위일체 석주

        // 캄보디아
        panoramas.Add ( new PanoramaInfo(13.441824f, 103.858065f, "LLLgCrnde_oJztsG3uzDcQ", true)); //Bayon Temple
        panoramas.Add ( new PanoramaInfo(13.474576f, 104.229077f, "Rsfd__NlKE0c-_3rVQW4fA", false)); //Beng Mealea Temple

        // 캐나다
        panoramas.Add ( new PanoramaInfo(50.059139f, -122.958407f, "Zzl28rqGJgaL2IdkUleP8A", false)); //브리티시컬럼비아주휘슬러-블랙콤 스키 리조트
        panoramas.Add ( new PanoramaInfo(43.081493f, -79.078126f, "42SshA7tZ3JfqnY3_zu-zw", true)); //Niagara Falls
        panoramas.Add ( new PanoramaInfo(43.642097f, -79.385320f, "tNLct3NZIOciHZqwg2qKRg", false)); //CN Tower

        // 크로아티아
        panoramas.Add ( new PanoramaInfo(45.823234f, 16.019663f, "isdebLyDj-h2SPWtE0rVFQ", false)); //막시미르 공원

        // 탄자니아
        panoramas.Add ( new PanoramaInfo(-3.075645f, 37.352457f, "MXClB4d78t6qwBZDSAibPg", false)); //Kilimanjaro

        // 터키
        panoramas.Add ( new PanoramaInfo(40.049812f, 26.219028f, "k4YxYzGcIOz0lKFkaOjBBw", false)); //Canakkale Martyrs' Memorial

        // 포르투갈
        panoramas.Add ( new PanoramaInfo(41.137596f, -8.609771f, "thVBWkKMGg08eXF_uTYbQg", false)); //도우루강

        // 프랑스
        panoramas.Add ( new PanoramaInfo(48.805015f, 2.119393f, "1GWafSZa22C6hW0j1L7a8A", false)); //베르사이유 궁전
        panoramas.Add ( new PanoramaInfo(48.856306f, 2.297560f, "PRDbHicwARPmhyIq9uKeCA", true)); //에펠탑
        panoramas.Add ( new PanoramaInfo(48.853663f, 2.347951f, "pQDLtpAbzF9KG4biw7zW4w", false)); //노트르담 대성당
        panoramas.Add ( new PanoramaInfo(48.873502f, 2.295779f, "Ixp4bKuPoKGyOBtKBUHVdg", true)); //개선문
        panoramas.Add ( new PanoramaInfo(48.861489f, 2.333644f, "cMMNd74PdSBvjw3vf9PByA", false)); //루브르 박물관

        // 핀란드
        panoramas.Add ( new PanoramaInfo(68.509006f, 27.481845f, "GtnAyp0MCbYAAAQZLDcQIA", true)); //Northern Lights
        panoramas.Add ( new PanoramaInfo(66.543346f, 25.848498f, "hEuDBjnLcagAAAQZA4Rr0g", false)); //Santa Claus Office

        // 필리핀
        panoramas.Add ( new PanoramaInfo(9.085340f, 123.272577f, "nB4D43juZU6LYGYb0VIDig", false)); //Apo Island
        panoramas.Add ( new PanoramaInfo(8.936190f, 120.008698f, "J67g8IADDUUAAAQWx3Mt6g", false)); //Tubbataha

        // 헝가리
        panoramas.Add ( new PanoramaInfo(47.507172f, 19.041730f, "Pon_HbPw8a3TeuFFwfrYFA", false)); //Danube

        // 호주
        panoramas.Add ( new PanoramaInfo(-33.579289f, 151.309294f, "HrHYIKEBk4Gqden0lMxJqA", false)); //Ku-ring-gai Chase National Park
        panoramas.Add ( new PanoramaInfo(-34.137255f, 151.117934f, "_fA2Asn1hvsgqOJiJGDcTQ", false)); //Royal National Park
        panoramas.Add ( new PanoramaInfo(-28.638596f, 153.636581f, "1MWcxwH8sjiGWmyufsczzA", false)); //Cape Byron State Conservation Area
        panoramas.Add ( new PanoramaInfo(-23.303391f, 151.914955f, "TNE1NPAnEUNYhGk9kFWyJw", true)); //Wilson Island underwater
        panoramas.Add ( new PanoramaInfo(-23.442896f, 151.906584f, "CWskcsTEZBNXaD8gG-zATA", true)); //Heron Island

        for(int i=0 ; i<panoramas.Capacity; i++) 
        {
            Lat = panoramas[i].lat;
            Lng = panoramas[i].lng;
            panoID = panoramas[i].panoid;
			impo=panoramas[i].impo;

            _rotation = new Vector3(Lat, -Lng, 0.0f);
            _translate = new Vector3(0, 0, -37.5f);

            GameObject child = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;

            child.transform.parent = earth.transform;
            child.GetComponent<StreetviewPoint>().SetPosition(impo);
            
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    
}