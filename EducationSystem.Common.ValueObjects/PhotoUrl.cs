using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class PhotoUrl : ValueObject<PhotoUrl>
    {
        public static PhotoUrl Empty => new PhotoUrl();

        public string Url { get; } = string.Empty;

        public static Result<PhotoUrl> Create(string url)
        {
            var regex = new Regex(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&\/\/=]*)");

            if (string.IsNullOrWhiteSpace(url))
                return Result.Success(PhotoUrl.Empty);

            if (!regex.IsMatch(url))
                return Result.Failure<PhotoUrl>("Photo url is invalid!");

            return Result.Success(new PhotoUrl(url));
        }

        protected PhotoUrl() { }

        protected PhotoUrl(string url) : this()
        {
            Url = url;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
        }
    }
}
