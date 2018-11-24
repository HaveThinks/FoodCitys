using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Text;

namespace FoodCityApi.Models
{
    public static class Code
    {
        public static string Sendyan(string tel, string code)
        {
            // 设置为您的apikey(https://www.yunpian.com)可查
            string apikey = "9cd2ff1452adab00e2b1c1f1db96551f";
            // 发送的手机号
            string mobile = tel;
            // 发送内容
            string text = "【今天快乐】您的验证码是" + code;
            // 智能模板发送短信url
            string url_send_sms = "https://sms.yunpian.com/v2/sms/single_send.json";
            string data_send_sms = "apikey=" + apikey + "&mobile=" + mobile + "&text=" + text;
            string res = HttpPost(url_send_sms, data_send_sms);
            return res;

        }
        public static string HttpPost(string Url, string postDataStr)
        {
            byte[] dataArray = Encoding.UTF8.GetBytes(postDataStr);
            // Console.Write(Encoding.UTF8.GetString(dataArray));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataArray.Length;
            //request.CookieContainer = cookie;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader =
                    new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                String res = reader.ReadToEnd();
                reader.Close();
                return res;
                //Console.Write("\nResponse Content:\n" + res + "\n");
            }
            catch (WebException e)
            {
                Console.Write(e.Message + e.ToString());
                Stream stream = e.Response.GetResponseStream();

                StreamReader reader =
                    new StreamReader(stream, Encoding.UTF8);
                String res = reader.ReadToEnd();
                reader.Close();
                return res;
                //Console.Write("\nResponse Content:\n" + res + "\n");
            }
        }


        public static string getRandom()
        {
            Random rd = new Random();
            //这里生成一个 6 位数的全数字验证码       
            int AuthCodeNumber = rd.Next(100000, 1000000);
            String AuthCode = AuthCodeNumber.ToString();
            return AuthCode;
        }
    }
}