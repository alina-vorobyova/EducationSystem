using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class PhotoUrl : ValueObject<PhotoUrl>
    {
        public static PhotoUrl Empty => new PhotoUrl { Url = string.Empty };

        public string Url { get; private set; }

        protected PhotoUrl() { }

        public PhotoUrl(string url)
        {
            var regex = new Regex(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&\/\/=]*)");

            if(!regex.IsMatch(url))
                throw new ArgumentException("Photo url is invalid!");

            Url = url;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
        }
    }
}
