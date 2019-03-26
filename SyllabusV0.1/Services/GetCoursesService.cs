﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SyllabusV0._1.Services
{
    public class GetCoursesService
    {
        /// <summary>
        /// 爬取课程表
        /// 注意打不开新版树维教务系统网站时会抛出异常，需要增加判断，树维教务系统：http://219.216.96.4/eams/loginExt.action
        /// </summary>
        /// <param name="username">树维教务系统用户名</param>
        /// <param name="password">树维教务系统密码</param>
        /// <returns></returns>
        public async Task<List<Course>> LoginAndGetCoursesAsync(string username, string password)
        {
            string url1 = "http://219.216.96.4/eams/loginExt.action";// 第一次请求和第二次请求
            string url2 = "http://219.216.96.4/eams/courseTableForStd!courseTable.action";// 第三次请求
            string url3 = "http://219.216.96.4/eams/logout.action";// 第四次请求

            var handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:66.0) Gecko/20100101 Firefox/66.0");

            // 第一次请求
            HttpResponseMessage response = await httpClient.GetAsync(url1);

            // 获取响应关键内容
            string content = await response.Content.ReadAsStringAsync();
            content = content.Substring(content.IndexOf("CryptoJS.SHA1("), 80);
            content = content.Substring(15, 37);

            // 密码使用SHA1进行哈希
            password = content + password;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = Encoding.UTF8.GetBytes(password);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            password = BitConverter.ToString(bytes_out);
            password = password.Replace("-", "").ToLower();

            // 第二次请求
            var values = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password},
                {"session_locale", "zh_CN"}
            };
            var formContent = new FormUrlEncodedContent(values);

            // 发送请求
            Thread.Sleep(1000);
            response = await httpClient.PostAsync(url1, formContent);
            content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("personal-name"))
            {
                System.Diagnostics.Debug.WriteLine("error");
                return null;
            }

            // 第三次请求
            values = new Dictionary<string, string>
            {
                {"ignoreHead", "1"},
                {"showPrintAndExport", "1"},
                {"setting.kind", "std"},
                {"startWeek", ""},
                {"semester.id", "30"},
                {"ids", "39437"}
            };
            formContent = new FormUrlEncodedContent(values);

            // 发送请求
            Thread.Sleep(1000);
            response = await httpClient.PostAsync(url2, formContent);
            content = await response.Content.ReadAsStringAsync();
            if (!content.Contains("课表格式说明"))
            {
                System.Diagnostics.Debug.WriteLine("error");
                return null;
            }

            // 第四次请求
            Thread.Sleep(1000);
            await httpClient.GetAsync(url3);

            // 解析得到的课程表数据
            string pattern = @"var actTeachers = \[(?:{id:(\d+),name:""([^""]+)"",lab:(?:false|true)},?)+\];(?:\s|\S)+?TaskActivity\(actTeacherId.join\(','\),actTeacherName.join\(','\),""(.*)"",""(.*)\(.*\)"",""(.*)"",""(.*)"",""(.*)"",null,null,assistantName,"""",""""\);(\s*index =(\d+)\*unitCount\+(\d+);\s*.*\s)+";

            // 查看正则匹配结果
            foreach (Match match in Regex.Matches(content, pattern))
            {
                int groupCtr = 0;
                foreach (Group group in match.Groups)
                {
                    groupCtr++;
                    System.Diagnostics.Debug.WriteLine("   Group {0}: '{1}'", groupCtr, group.Value);
                    int captureCtr = 0;
                    foreach (Capture capture in group.Captures)
                    {
                        captureCtr++;
                        System.Diagnostics.Debug.WriteLine("      Capture {0}: '{1}'", captureCtr, capture.Value);
                    }
                }
            }

            return new List<Course>();
        }
    }
}