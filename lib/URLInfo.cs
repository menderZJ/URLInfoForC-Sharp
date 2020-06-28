using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json.Linq;
namespace URLInfo
{
    /// <summary>
    /// URLInfo C#类
    /// AUTHOR:Mender.ZJ.Yang
    /// SINCE 1.0 2020-06-28
    /// UNDER THE MIT LICENSE
    /// </summary>
    public class URLInfo
    {

        public string Protocol { get; private set; }
        public string Domain { get; private set; }
        public string Path { get; private set; }
        public string File { get; private set; }
        public string SearchStr { get; private set; }
        public string Anchor { get; private set; }

        public string Controller { get; private set; }
        public string Method { get; private set; }

        public Boolean Is_Route_Mode { get; set; }
        public Dictionary<string, string> URLParam { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="URL">URL string</param>
        /// <param name="IsRouteMode">is simply Route Mode,simply MVC mode</param>
        public URLInfo(string URL = "", Boolean IsRouteMode = false)
        {
            URLParam = new Dictionary<string, string>();
            Protocol = "";
            Domain = "";
            Path = "";
            File = "";
            SearchStr = "";
            Anchor = "";
            Parse(URL, IsRouteMode);
        }

        /// <summary>
        /// 转义URL字符串为对应的数据
        /// </summary>
        /// <param name="URL">URL string</param>
        /// <param name="IsRouteMode">is simply Route Mode,simply MVG mode</param>
        public void Parse(string URL, Boolean IsRouteMode = false)
        {
            //清空原数据
            Protocol = "";
            Domain = "";
            Path = "";
            File = "";
            SearchStr = "";
            Anchor = "";
            URLParam.Clear();
            Is_Route_Mode = Is_Route_Mode||IsRouteMode;

            Regex regExp;
            MatchCollection matchResult;
            string[] params_T;
             regExp = new Regex(@"^(?<protocol>(https|http|ftp|file)(:\/{2,4}))?(?<domain>[^\s\/\\]+)?(?<path>[\/][^?#]*)?(?<searchStrOrAnchor>(\?[^#]*))?(?<anchor>#[^#]*)?");
            matchResult = regExp.Matches(URL);
            if (matchResult.Count > 0)
            {
                Protocol = matchResult[0].Groups["protocol"].Value;
                Domain = matchResult[0].Groups["domain"].Value;
                Path = matchResult[0].Groups["path"].Value;
                File = Path.Substring(Path.LastIndexOf("/") > 0 ? Path.LastIndexOf("/") + 1 : 0);
                Path = Path.Substring(0, Path.LastIndexOf("/") == Path.Length ? Path.Length + 1 : Path.LastIndexOf("/") + 1);
                SearchStr = matchResult[0].Groups["searchStrOrAnchor"].Value;
                if (SearchStr.Substring(0, 1) == "#")
                {
                    Anchor = SearchStr;
                    SearchStr = "";
                }
                else
                {
                    Anchor = matchResult[0].Groups["anchor"].Value;
                }
                if (SearchStr != "")
                {
                    params_T = SearchStr.Replace("?", "").Split("&");
                    foreach (var p in params_T)
                    {
                        var tt = p.Split("=");
                        URLParam.Add(tt[0], tt.Length > 1 ? tt[1] : "");
                    }
                }
            }
            Controller = "";
            Method = "";
            if (Is_Route_Mode)
            {

                string tSStr = Path;
                Regex regExp1;
                MatchCollection matchResult1;
                regExp1 = new Regex(@"(?<controller>[^\/]+)\/(?<method>[^\/]+)");
                matchResult1 = regExp1.Matches(tSStr);
                Controller = matchResult1[0].Groups["controller"].Value;
                Method = matchResult1[0].Groups["method"].Value;
            }
        }
        /// <summary>
        /// 传入一个URL参数，若该参数的key存在，则 覆盖为新值，若不存在，将会新增加一个URl参数
        /// Pass into and set one urlparam,if the key exist,it's value will be covered,else if not exist,it (contains key and value) will be created
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Boolean setParam(string key, string value)
        {

            key = HttpUtility.UrlEncode(key);
            value = HttpUtility.UrlEncode(value);
            if (URLParam.ContainsKey(key))
            {
                URLParam[key] = value;
            }
            else
            {
                URLParam.Add(key, value);
            }
            SearchStr = "?";
            foreach (var item in URLParam)
            {
                SearchStr += item.Key + "=" + item.Value + "&";
            }

            if (SearchStr.LastIndexOf("&") == SearchStr.Length - 1) {
                SearchStr = SearchStr.Substring(0, SearchStr.Length - 1);
            }
            return true;
        }
        /// <summary>
        /// 传入Jobject，来设置url参数，若某个key出错将会跳过
        /// Pass into JObject object,if someone key error,the key and it's value will be aborted.
        /// </summary>
        /// <param name="params_T"></param>
        public void setParam(JObject params_T)
        {
            foreach (var item in params_T)
            {
                setParam(item.Key.ToString(), item.Value.ToString());
            }
        }
        /// <summary>
        /// 传入JSON字串来设置url参数,若JSONStr字符串有错误，返回false;
        /// Pass into JSON serialize string,JSON will be unserialize to JOject.If JSON string is error then return false.
        /// </summary>
        /// <param name="JSONStr"></param>
        public Boolean setParam(string JSONStr)
        {
            JObject params_T;
            try
            {
                params_T = JObject.Parse(JSONStr);
                foreach (var item in params_T)
                {
                    setParam(item.Key.ToString(), item.Value.ToString());
                }
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取指定的key对应的URL参数的值
        /// get the url param on key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetParam(string key)
        {
            key = HttpUtility.UrlEncode(key);
            if (URLParam.Count > 0 && URLParam.ContainsKey(key))
            {
                return HttpUtility.UrlDecode(URLParam[key]);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取一个整合后的URL字符串
        /// get a URL string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            return Protocol + Domain + Path + File + SearchStr + Anchor;
        }

    }
}
