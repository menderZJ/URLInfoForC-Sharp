
using System;

namespace URLInfo
{
        class Program
        {

            static void Main(string[] args)
            {
                URLInfo x = new URLInfo(@"https://www.baidu.com/level1/level2/level3/sdgsrasfaerf.file?a=1&b=2&c&中国=中华人民共和国#asdafa");
                Console.WriteLine(x.ToString());
            Console.WriteLine("Protocol:"+x.Protocol);
            Console.WriteLine("Domain:"+x.Domain);
            Console.WriteLine("Path:"+x.Path);
            Console.WriteLine("File:"+x.File);
            Console.WriteLine("SerchStr:"+x.SearchStr);
            Console.WriteLine("Anchor:"+x.Anchor);
            Console.WriteLine("Full URL:" + x.ToString());
            Console.WriteLine("Param:");
            foreach (var item in x.URLParam)
            {
                Console.WriteLine(item.Key+":"+item.Value);
            }
            Console.WriteLine("Change param a a new value:\"ttttttttttt\"");
            x.setParam("a", "ttttttttttt");
            x.setParam("美国", "美利坚#?%合众国");

            Console.WriteLine("param a value :"+x.GetParam("a"));
            Console.WriteLine("param 美国 value :" + x.GetParam("美国"));
            Console.WriteLine("Param:");
            foreach (var item in x.URLParam)
            {
                Console.WriteLine(item.Key + ":" + item.Value);
            }
            Console.WriteLine("New Full URL:" + x.ToString());
            Console.WriteLine("------------In Route Mode--------------");
            x.Is_Route_Mode = true;
            x.Parse(@"https://www.baidu.com/level1/level2/level3/sdgsrasfaerf.file?a=1&b=2&c#asdafa");
            //also you can write like this instead of upside 2 lines
            //    x.Parse(@"https://www.baidu.com/level1/level2/level3/sdgsrasfaerf.file?a=1&b=2&c#asdafa",true);
            //Or write like this when the object initaliazing
            //    URLInfo x = new URLInfo(@"https://www.baidu.com/level1/level2/level3/sdgsrasfaerf.file?a=1&b=2&c#asdafa",true);
            Console.WriteLine("Controller:"+x.Controller);
            Console.WriteLine("Method:"+x.Method);
        }



        }//end class

    }//end namespace

