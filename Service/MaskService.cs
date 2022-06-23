using Mask.Models;
using System.Text.Json;

namespace Mask.Service
{
    public class MaskService
    {
        public HttpClient Client { get; set; }
        public MaskService (HttpClient client)
        {
            client.BaseAddress = new Uri("https://quality.data.gov.tw/"); // 取得政府口罩連結
            client.DefaultRequestHeaders.Add("Accept", "application/json"); 
            client.DefaultRequestHeaders.Add("User-Agent", "QueryMask Sample");
            Client = client;
        }
        public async Task<IEnumerable<MaskInfo>> GetMaskInfo()
        {
            var response = await Client.GetAsync("dq_download_json.php?nid=116285&md5_url=2150b333756e64325bdbc4a5fd45fad1"); 
            response.EnsureSuccessStatusCode(); // 回應的 success、fail的例外狀況
            using var responseStream=await response.Content.ReadAsStreamAsync(); // 將 HTTP 回傳內容序列化
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaskInfo>>(responseStream); // 回傳口罩資料 並以反序列化形式 將model資料轉為物件型態
        }
    }
}
