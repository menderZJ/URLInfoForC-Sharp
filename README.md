# URLInfoforC-Sharp
a class for parse protocol,domain,file,seachstring,anchor,urlparams from a URLstring.
The class file path is /lib/URLInfo.cs
Please copy the class file into you project,and use namespace URLInfo like on your code file:
```
use namespace URLInfo;
```
The solution file `URLInfo.sln` is a test demo for class  URLInfo.

## 1. APIs
###
### 1. Prototypes
#### 1. Protocol { get; private set; }
get(public) or set(private)the  URL's protocol
#### 2. Domain { get; private set; }
get(public) or set(private) URL's Domain
#### 3. Path { get; private set; }
get(public) or set(private) URL's Path
#### 4. File { get; private set; }
get(public) or set(private)  URL's File
#### 5. SearchStr { get; private set; }
get(public) or set(private) the  URL's SearchStr
#### 6. Anchor { get; private set; }
get(public) or set(private) the  URL's Anchor
#### 7. Controller { get; private set; }
get(public) or set(private) the  URL's Controller(RouteMode)
#### 8. Method { get; private set; }
get(public) or set(private) the  URL's Method(RouteMode)
#### 9. Is_Route_Mode { get; set; }
get(public) or set(public) the  URL's Is_Route_Mode
#### 10. Dictionary<string, string> URLParam { get; private set; }
get(public) or set(private) the  URL's URLParam
## 2. Methods
#### 1. public URLInfo(string URL = "", Boolean IsRouteMode = false)

is simply Route Mode,simply MVG mode，default is  false)
#### 2. public Boolean setParam(string key, string value)
传入一个URL参数，若该参数的key存在，则 覆盖为新值，若不存在，将会新增加一个URl参数
Pass into and set one urlparam,if the key exist,it's value will be covered,else if not exist,it (contains key and value) will be created
#### 3.  public void setParam(JObject params_T)
传入Jobject，来设置url参数，若某个key出错将会跳过
Pass into JObject object,if someone key error,the key and it's value will be aborted.
#### 4. public Boolean setParam(string JSONStr)
传入JSON字串来设置url参数,若JSONStr字符串有错误，返回false;
pass into JSON serialize string,JSON will be unserialize to JOject.If JSON string is error then return false.
#### 5. public override string ToString()
获取一个整合后的URL字符串