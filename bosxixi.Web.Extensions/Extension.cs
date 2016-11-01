using bosxixi.Extensions;
using bosxixi.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static bosxixi.Extensions.Extension;

namespace bosxixi.Web.Extensions
{
    public static class Extension
    {
        public static IEnumerable<string> GetLogs(this string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return File.ReadLines(path, Encoding.Default).Reverse();
        }

        public static void Alert(this TempDataDictionary tempData, AlertType type, string message)
        {
            string glyphicon = string.Empty;
            switch (type)
            {
                case AlertType.success:
                    glyphicon = $"<span class='glyphicon glyphicon-ok-sign'>{message}</span>";
                    break;
                case AlertType.warning:
                    glyphicon = $"<span class='glyphicon glyphicon-alert'>{message}</span>";
                    break;
                case AlertType.info:
                    glyphicon = $"<span class='glyphicon glyphicon-info-sign'>{message}</span>";
                    break;
                case AlertType.danger:
                    glyphicon = $"<span class='glyphicon glyphicon-exclamation-sign'>{message}</span>";
                    break;
                default:
                    break;
            }

            tempData[Enum.GetName(typeof(AlertType), type)] = glyphicon;
        }

        public static bool IsFromWechat(this HttpRequestBase request)
        {
            return request.UserAgent.ToLower().Contains("micromessenger");
        }

        public static string GetUserIdIp(this HttpRequestBase request)
        {
            string result = request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
                result = request.UserHostAddress;
            if (string.IsNullOrEmpty(result) || !IsIp(result))
                return "127.0.0.1";
            return result;
        }

        public static IEnumerable<HttpFile> SaveMultiPartFileFromMvc(this HttpRequestBase request, IDictionary<string, string> paths,
                                                                 int maxCount = 12, int maxContentLength = 2048, string[] types = null,
                                                                 string tempPath = @"c:/uploads/temp", char spliter = '_',
                                                                 Func<string, string, Size, bool> imageProcessor = null, int height = 200, int width = 200)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            #region Create Directory
            foreach (var path in paths)
                if (path.Value != null && !Directory.Exists(path.Value))
                    Directory.CreateDirectory(path.Value);

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            #endregion

            #region Valid Check 
            //检查文件数量限制
            if (request.Files.Count > maxCount)
                throw new Exception("文件数量超出限制", new Exception($"文件数量超出限制,最多支持同时传输 {maxCount} 个文件"));

            //检查文件支持格式
            if (types == null)
                types = new string[] { ".jpg", ".png", ".jpeg" };

            types = types.Select(c => c.ToLower()).ToArray();

            if ((from string file in request.Files
                 where request.Files[file].ContentLength != 0
                 select Path.GetExtension(request.Files[file].FileName).ToLower()).Any(extension => !types.Contains(extension)))
                throw new Exception("文件格式不支持", new Exception($"文件格式不支持, 仅支持{types.ToString(' ')} 文件格式"));

            //检查文件大小限制
            if (request.Files.Cast<string>().Any(file => request.Files[file].ContentLength > maxContentLength * 1024))
                throw new Exception("文件大小超出限制", new Exception($"文件数量超出限制,单个文件最大支持 {maxContentLength} KB"));
            #endregion

            //保存文件
            foreach (string file in request.Files)
            {
                var fileSize = request.Files[file]?.ContentLength ?? 0;
                if (fileSize == 0) continue;

                var timeStamp = DateTime.UtcNow.Ticks.ToString();
                var extension = Path.GetExtension(request.Files[file].FileName).ToLower();

                string filename = $"{timeStamp}_{Guid.NewGuid().ToString().Substring(0, 8)}{extension}";
                //string attachmentFilename = $"{timeStamp}_{Path.GetFileNameWithoutExtension(request.Files[file].FileName)}{extension}";
                string filekey = file.Split(spliter)[0];
                if (paths.ContainsKey(filekey))
                {
                    if (imageProcessor != null && new string[] { ".jpg", ".png", ".jpeg" }.Contains(extension))
                    {
                        request.Files[file].SaveAs(Path.Combine(tempPath, filename));
                        if (!imageProcessor(Path.Combine(tempPath, filename), Path.Combine(paths[filekey], filename), new Size(width, height)))
                            throw new Exception("文件保存出现错误. 请检查图片是否已损坏.");

                        //todo add delete tempfile

                        yield return new HttpFile(filename, fileSize, filekey, paths[filekey], extension);
                    }
                    else
                    {
                        request.Files[file].SaveAs(Path.Combine(paths[filekey], filename));
                        yield return new HttpFile(filename, fileSize, filekey, paths[filekey], extension);
                    }
                }
            }
        }
    }
}
