using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ERP.Helpers
{
    public class CacheFunctions
    {
        public static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        public static void Set(string key, object data)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(120));
            _cache.Set(key, data, cacheEntryOptions);
        }
        public static object Get(string key)
        {
            return _cache.Get(key);
        }
        public static void SetSessionInt(string key, int value)
        {
            _httpContext.Session.SetInt32(key, value);
        }
        public static int GetSessionInt(string key)
        {
            return Convert.ToInt32(_httpContext.Session.GetInt32(key));
        }
        public static void SetSession(string key, string value)
        {
            _httpContext.Session.SetString(key, value);
        }
        public static string GetSession(string key)
        {
            return _httpContext.Session.GetString(key);
        }
        public static T GetComplexData<T>(string key)
        {
            var data = _httpContext.Session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static void SetComplexData(string key, object value)
        {
            _httpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static void RemoveSession(string key)
        {
            try
            {
                _httpContext.Session.Remove(key);
            }
            catch { }
        }
        public static void SetSuccess(string value)
        {
            SetSession("Success", value);
        }
        public static void SetSuccessTitle(string value)
        {
            SetSession("SuccessTitle", value);
        }
        public static string GetSuccess()
        {
            return GetSession("Success");
        }
        public static string GetSuccessTitle()
        {
            return GetSession("SuccessTitle");
        }
        public static void RemoveSuccess()
        {
            RemoveSession("Success");
        }
        public static void RemoveSuccessTitle()
        {
            RemoveSession("SuccessTitle");
        }
        public static void SetError(string value)
        {
            SetSession("Error", value);
        }
        public static string GetError()
        {
            return GetSession("Error");
        }
        public static void RemoveError()
        {
            RemoveSession("Error");
        }
        public static void SetWarning(string value)
        {
            SetSession("Warning", value);
        }
        public static string GetWarning()
        {
            return GetSession("Warning");
        }
        public static void RemoveWarning()
        {
            RemoveSession("Warning");
        }

        public static void SetCookiesList<T>(string key, List<string> ListValue)
        {
            CookieOptions option = new CookieOptions();
            option.IsEssential = true;
            option.HttpOnly = true;
            option.Expires = DateTime.Now.AddDays(360);
            if (_httpContext.Request.Cookies[key] != null)
            {
                _httpContext.Response.Cookies.Delete(key); //To update list
            }
            // Stringify your list
            var StringValues = String.Join(",", ListValue);
            _httpContext.Response.Cookies.Append(key, StringValues, option);
        }

        public static List<string> GetCookiesList(string key)
        {
            List<string> ListValue = new List<string>();
            if (_httpContext.Request.Cookies[key] != null)
            {
                // Your cookie exists - grab your value and create your List
                ListValue = _httpContext.Request.Cookies[key].Split(',').ToList();
                //ListValue = JsonConvert.DeserializeObject<List<T>>(_httpContext.Request.Cookies[key]);
            }
            return ListValue;
        }
        public static void SetSessionList<T>(string key, List<T> ListValue)
        {
            _httpContext.Session.SetString(key, JsonConvert.SerializeObject(ListValue));
        }
        public static List<T> GetSessionList<T>(string key)
        {
            var data = _httpContext.Session.GetString(key);
            if (data == null)
            {
                return default(List<T>);
            }
            return JsonConvert.DeserializeObject<List<T>>(data);
        }
    }
}
