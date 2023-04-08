using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WebApp.App_Helpers.third_party.api
{
  public class zhixinHelper
  {
    private static readonly string SUCCESS = "0000";
    private static readonly string TRADE_KEY = "b4a9e86b90654ad6a6f520a6b2b9d5b5";
    private static readonly string REQUEST_URL = "http://api.yunzhixin.com:11140/txp/sms/send";//Api地址
    public static string Send(string mobile, string tpl_id, string param = "")
    {
      //输入用户编号,即注册www.yunzhixin.com的手机号码（手动填入）
      var account = "13851406166";
      //输入需要发送的手机号（手动填入）
      //string mobile = "";
      //商户提交的订单号（商户保证其唯一性）（手动填入）
      var orderId = Guid.NewGuid().ToString().Substring(0,32);
      //用户服务器时间戳(13位)
      var time = TimeStamp() * 1000;
      //模板编号（手动填入）
      var tplId = tpl_id;
      //短信所需传入的参数,格式见文档（手动填入）
      //var param = "";
      //生成签名
      var source = new StringBuilder();
      source.Append(mobile).Append("|")
      .Append(account).Append("|")
      .Append(time).Append("|")
      .Append(tplId).Append("#")
      .Append(TRADE_KEY);
      Console.WriteLine(source.ToString());
      var sign = Md5Encrypt(source.ToString()).ToUpper();
      //组织请求参数
      var smsRequestParam = new StringBuilder();
      smsRequestParam.Append("account=").Append(account);
      smsRequestParam.Append("&mobile=").Append(mobile);
      smsRequestParam.Append("&order_id=").Append(orderId);
      smsRequestParam.Append("&time=").Append(time);
      smsRequestParam.Append("&tpl_id=").Append(tplId);
      smsRequestParam.Append("&params=").Append(param);
      smsRequestParam.Append("&sign=").Append(sign);
      Console.WriteLine(smsRequestParam);
      //进行访问
      try
      {
        var response = HttpPost(REQUEST_URL, smsRequestParam.ToString());
        Console.WriteLine(response);
        //需要导入Newtonsoft.Json;
        var result = JsonConvert.DeserializeObject<ResponseModel>(response);
        if (SUCCESS.Equals(result.return_code))
        {
           
        }
        else
        {
          //提交失败,具体状态码含义需查询文档进行判断
          Console.WriteLine("错误状态码：" + result.return_code);
          //Console.Read();

        }
        return result.return_code;
      }
      catch (Exception e)

      {

        throw;

      }
    }
    //发送短信请求方法

    private static void requestCharge(Dictionary<string, string> smsRequestParam)
    {

    }

    public static string Md5Encrypt(string input)
    {
      MD5 md5 = MD5.Create();
      // 将字符串转换成字节数组
      byte[] byteOld = Encoding.UTF8.GetBytes(input);
      // 调用加密方法
      byte[] byteNew = md5.ComputeHash(byteOld);
      // 将加密结果转换为字符串
      StringBuilder sb = new StringBuilder();
      foreach (byte b in byteNew)
      {
        // 将字节转换成16进制表示的字符串，
        sb.Append(b.ToString("x2"));
      }
      // 返回加密的字符串
      return sb.ToString();
      //var md5 = new MD5CryptoServiceProvider();
      //var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
      //var builder = new StringBuilder(32);
      //foreach (var t in buffer)
      //{
      //  builder.Append(t.ToString("x").PadLeft(2, '0'));
      //}
      //return builder.ToString();
    }



    public static long TimeStamp()
    {
      var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
      var receivetime = (long)( ( Convert.ToDateTime(DateTime.Now) ) - startTime ).TotalSeconds; // 相差秒数
      return receivetime;

    }

    private static string HttpPost(string Url, string postDataStr)
    {
      var request = (HttpWebRequest)WebRequest.Create(Url);
      request.Method = "POST";
      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
      var myRequestStream = request.GetRequestStream();
      var myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
      myStreamWriter.Write(postDataStr);
      myStreamWriter.Close();
      var response = (HttpWebResponse)request.GetResponse();
      var myResponseStream = response.GetResponseStream();
      var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
      var retString = myStreamReader.ReadToEnd();
      myStreamReader.Close();
      myResponseStream.Close();
      return retString;
    }

    public class ResponseModel
    {
      /// <summary>
      /// 返回码
      /// </summary>
      public string return_code { get; set; }
      /// <summary>
      /// 返回订单号
      /// </summary>
      public string order_id { get; set; }

    }
  }
}