using UnityEngine;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;

public class HttpReq: MonoBehaviour {
    
    string newURLSWEF = "https://www.htl-salzburg.ac.at/lehrerinnen-details/schweiberer-franz-prof-dipl-ing-c-205.html";
    string newURLMEES = "https://www.htl-salzburg.ac.at/index.php/lehrerinnen-details/meerwald-stadler-susanne-prof-dipl-ing-g-009.html";

   
    public string teacher1Name;
    public string teacher1Sprechstunde;
    public string teacher1Raum; 
    public string teacher2Name;
    public string teacher2Sprechstunde;
    public string teacher2Raum; 

    
    public TextMeshPro t1name;
    public TextMeshPro t2name;
    public TextMeshPro t1sprech;
    public TextMeshPro t2sprech;
    public TextMeshPro t1raum; 
    public TextMeshPro t2raum; 

    async void Start() {
        using (HttpClient client = new HttpClient()) {
            // HTML laden
            string htmlSWEF = await client.GetStringAsync(newURLSWEF);
            string htmlMEES = await client.GetStringAsync(newURLMEES);

            teacher1Name = ExtractValue(htmlSWEF, @"<div class=""field Lehrername"".*?<span class=""text"">(.*?)</span>");
            teacher1Sprechstunde = ExtractValue(htmlSWEF, @"<div class=""field SprStunde"".*?<span class=""text"">(.*?)</span>");
            teacher1Raum = ExtractValue(htmlSWEF, @"<div class=""field Raum"".*?<span class=""text"">(.*?)</span>");

            teacher2Name = ExtractValue(htmlMEES, @"<div class=""field Lehrername"".*?<span class=""text"">(.*?)</span>");
            teacher2Sprechstunde = ExtractValue(htmlMEES, @"<div class=""field SprStunde"".*?<span class=""text"">(.*?)</span>");
            teacher2Raum = ExtractValue(htmlMEES, @"<div class=""field Raum"".*?<span class=""text"">(.*?)</span>");


            Debug.Log("--- Lehrer 1 ---");
            Debug.Log("Name: " + teacher1Name);
            Debug.Log("Sprechstunde: " + teacher1Sprechstunde);
            Debug.Log("Raum: " + teacher1Raum);
            Debug.Log("--- Lehrer 2 ---");
            Debug.Log("Name: " + teacher2Name);
            Debug.Log("Sprechstunde: " + teacher2Sprechstunde);
            Debug.Log("Raum: " + teacher2Raum); 

            
            t1name.text = teacher1Name;
            t2name.text = teacher2Name;
            t1sprech.text = teacher1Sprechstunde;
            t2sprech.text = teacher2Sprechstunde;

            t1raum.text = teacher1Raum;
            t2raum.text = teacher2Raum; 
        }
    }


    private string ExtractValue(string html, string regExPattern) {
        var match = Regex.Match(html, regExPattern, RegexOptions.Singleline);
        if (match.Success) {
            return match.Groups[1].Value;
        }
        return "nicht gefunden";
    }
}