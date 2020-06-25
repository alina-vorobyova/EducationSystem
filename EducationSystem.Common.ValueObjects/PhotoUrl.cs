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
            var regex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$");

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
