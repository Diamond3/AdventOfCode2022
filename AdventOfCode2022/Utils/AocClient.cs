using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
