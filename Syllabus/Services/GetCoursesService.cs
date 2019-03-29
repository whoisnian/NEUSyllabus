using Syllabus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Syllabus.Services
{
    public class GetCoursesService
    {
        /// <summary>
        /// 爬取课程表
        /// 注意打不开新版树维教务系统网站时会抛出异常，需要增加判断，树维教务系统：http://219.216.96.4/eams/loginExt.action
        /// 注意返回的Course未合并相同Name的项
        /// </summary>
        /// <param name="username">树维教务系统用户名</param>
        /// <param name="password">树维教务系统密码</param>
        /// <returns></returns>
        public async Task<List<Course>> LoginAndGetCoursesAsync(string Username, string Password)
        {
            string Url1 = "http://219.216.96.4/eams/loginExt.action";                       // 第一次请求和第二次请求
            string Url2 = "http://219.216.96.4/eams/courseTableForStd!courseTable.action";  // 第三次请求
            string Url3 = "http://219.216.96.4/eams/logout.action";                         // 第四次请求

            var Handler = new HttpClientHandler();
            HttpClient Client = new HttpClient(Handler);
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:66.0) Gecko/20100101 Firefox/66.0");

            // 第一次请求
            HttpResponseMessage Response = await Client.GetAsync(Url1);

            // 获取响应关键内容
            string Content = await Response.Content.ReadAsStringAsync();
            Content = Content.Substring(Content.IndexOf("CryptoJS.SHA1("), 80);
            Content = Content.Substring(15, 37);

            // 密码使用SHA1进行哈希
            Password = Content + Password;
            SHA1 Sha1 = new SHA1CryptoServiceProvider();
            byte[] BytesIn = Encoding.UTF8.GetBytes(Password);
            byte[] BytesOut = Sha1.ComputeHash(BytesIn);
            Sha1.Dispose();
            Password = BitConverter.ToString(BytesOut);
            Password = Password.Replace("-", "").ToLower();

            // 第二次请求
            var Values = new Dictionary<string, string>
            {
                {"username", Username},
                {"password", Password},
                {"session_locale", "zh_CN"}
            };
            var FormContent = new FormUrlEncodedContent(Values);

            // 发送请求
            Thread.Sleep(1000);
            Response = await Client.PostAsync(Url1, FormContent);
            Content = await Response.Content.ReadAsStringAsync();
            if (!Content.Contains("personal-name"))
            {
                System.Diagnostics.Debug.WriteLine("error");
                return null;
            }

            // 第三次请求
            Values = new Dictionary<string, string>
            {
                {"ignoreHead", "1"},
                {"showPrintAndExport", "1"},
                {"setting.kind", "std"},
                {"startWeek", ""},
                {"semester.id", "30"},
                {"ids", "39437"}
            };
            FormContent = new FormUrlEncodedContent(Values);

            // 发送请求
            Thread.Sleep(1000);
            Response = await Client.PostAsync(Url2, FormContent);
            Content = await Response.Content.ReadAsStringAsync();
            if (!Content.Contains("课表格式说明"))
            {
                System.Diagnostics.Debug.WriteLine("error");
                return null;
            }

            // 第四次请求
            Thread.Sleep(1000);
            await Client.GetAsync(Url3);

            // 解析得到的课程表数据
            string Pattern = @"var actTeachers = \[(?:{id:(\d+),name:""([^""]+)"",lab:(?:false|true)},?)+\];(?:\s|\S)+?TaskActivity\(actTeacherId.join\(','\),actTeacherName.join\(','\),""(.*)"",""(.*)\(.*\)"",""(.*)"",""(.*)"",""(.*)"",null,null,assistantName,"""",""""\);(\s*index =(\d+)\*unitCount\+(\d+);\s*.*\s)+";

            // 查看正则匹配结果
            List<Course> AllCourses = new List<Course>();
            foreach (Match match in Regex.Matches(Content, Pattern))
            {
                // 教师名字
                Group TempGroup = match.Groups[2];
                String TeacherName = "";
                foreach (Capture TempCapture in TempGroup.Captures)
                {
                    if (TeacherName == "")
                        TeacherName = TempCapture.Value;
                    else
                        TeacherName = TeacherName + ", " + TempCapture.Value;
                }

                // 课程名称
                String CourseName = match.Groups[4].Captures[0].Value;

                // 教室地点
                String Location = match.Groups[6].Captures[0].Value;

                // 教学周
                String Week = match.Groups[7].Captures[0].Value;

                // 周几上课
                int.TryParse(match.Groups[9].Captures[0].Value, out int WeekDay);

                // 开始于第几节
                int BeginTime = 13;
                // 结束于第几节
                int EndTime = -1;

                foreach (Capture capture in match.Groups[10].Captures)
                {
                    int.TryParse(capture.Value, out int TempInt);
                    if (TempInt < BeginTime)
                        BeginTime = TempInt;
                    if (TempInt > EndTime)
                        EndTime = TempInt;
                }
                Course TempCourse = new Course
                {
                    Name = CourseName,
                    Teacher = TeacherName
                };
                TempCourse.AddLocTime(Location, Week, WeekDay + 1, BeginTime + 1, EndTime + 1);
                AllCourses.Add(TempCourse);
                System.Diagnostics.Debug.WriteLine(CourseName + TeacherName);
            }

            return AllCourses;
        }
    }
}
