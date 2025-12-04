using UnityEngine;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

public class HttpReqV2 : MonoBehaviour {

    string webUrl = "https://www.htl-salzburg.ac.at/lehrerinnen.html";
    string expression = @"<div class=""field Lehrername"".*?<span class=""text"">(.*?)</span>";

    async void Awake() {

        HttpClient client = new HttpClient();

        string htmlResponse = await client.GetStringAsync(webUrl);

        var matches = Regex.Matches(htmlResponse, expression, RegexOptions.Singleline);


        for (int i = 0; i < 5 && i < matches.Count; i++) {
            Debug.Log(WebUtility.HtmlDecode(matches[i].Groups[1].Value.Trim()));
        }
    }
}
