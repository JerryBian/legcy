using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Laobian.Admin.Models
{
    public class SharedFileTypeFactory
    {
        public const string Image = "img";
        public const string Package = "package";
        public const string Media = "media";
        public const string Doc = "doc";
        public const string Text = "txt";

        public static IDictionary<string, string> AllBaseTypes => new Dictionary<string, string>
        {
            {nameof(Image), Image },
            {nameof(Package), Package },
            {nameof(Media), Media },
            {nameof(Doc), Doc },
            {nameof(Text), Text }
        };

        public static List<SharedFileType> ImageFileTypes => new List<SharedFileType>
        {
            new SharedFileType(Image, "bmp"),
            new SharedFileType(Image, "jpg"),
            new SharedFileType(Image, "jpeg"),
            new SharedFileType(Image, "gif"),
            new SharedFileType(Image, "png"),
            new SharedFileType(Image, "svg"),
            new SharedFileType(Image, "ico")
        };

        public static List<SharedFileType> PackageFileTypes => new List<SharedFileType>
        {
            new SharedFileType(Package, "zip")
        };

        public static List<SharedFileType> MediaFileTypes => new List<SharedFileType>
        {
            new SharedFileType(Media, "mp3"),
            new SharedFileType(Media, "mp4"),
            new SharedFileType(Media, "avi"),
            new SharedFileType(Media, "flv"),
            new SharedFileType(Media, "m4v"),
            new SharedFileType(Media, "mpg"),
            new SharedFileType(Media, "wmv"),
            new SharedFileType(Media, "wav"),
            new SharedFileType(Media, "wma")
        };

        public static List<SharedFileType> DocFileTypes => new List<SharedFileType>
        {
            new SharedFileType(Doc, "doc"),
            new SharedFileType(Doc, "docx"),
            new SharedFileType(Doc, "ppt"),
            new SharedFileType(Doc, "pptx"),
            new SharedFileType(Doc, "xls"),
            new SharedFileType(Doc, "xlsx"),
            new SharedFileType(Doc, "pdf")
        };

        public static List<SharedFileType> TextFileTypes => new List<SharedFileType>
        {
            new SharedFileType(Text, "txt"),
            new SharedFileType(Text, "html")
        };

        public static List<SharedFileType> AllFileTypes => ImageFileTypes.Concat(PackageFileTypes)
            .Concat(MediaFileTypes).Concat(DocFileTypes).Concat(TextFileTypes).ToList();

        public static string GetBaseType(string type)
        {
            var fileType =
                AllFileTypes.FirstOrDefault(t => t.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase));
            return fileType?.BaseType;
        }

        public static string AllowedExtensions()
        {
            return JsonConvert.SerializeObject(AllFileTypes.Select(t => t.Type));
        }
    }

    public class SharedFileType
    {
        public SharedFileType(string baseType, string type)
        {
            BaseType = baseType;
            Type = type;
        }

        public string BaseType { get; set; }

        public string Type { get; set; }
    }
}