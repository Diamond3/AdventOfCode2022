namespace AdventOfCode2022.Utils;

class AocClient
{
    private HttpClient client;
    private string cookie;
    public AocClient(string sessionCookie)
    {
        client = new HttpClient();
        cookie = $"session={sessionCookie}";
    }

    public async Task<string> GetDayInputAsJsonAsync(int day)
    {
        var url = $"https://adventofcode.com/2022/day/{day}/input";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Cookie", cookie);

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAnswerAsync(int day, string answer, int part)
    {
        var url = $"https://adventofcode.com/2022/day/{day}/answer";

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Cookie", cookie);

        request.Content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("level", part.ToString()),
            new KeyValuePair<string, string>("answer", answer)
        ]);

    var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        var aocResponse = await response.Content.ReadAsStringAsync();
        return aocResponse.Contains("That's not the right answer") ? "Wrong Answer!" : aocResponse.Contains("That's the right answer!") ? "Correct!" : aocResponse;
    }
}