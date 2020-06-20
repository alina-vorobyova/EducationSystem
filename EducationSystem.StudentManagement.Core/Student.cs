using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Contracts;
using EducationSystem.Common.ValueObjects;

namespace EducationSystem.StudentManagement.Core
{
    public class Student : AggregateRoot<int>
    {
        public FullName FullName { get; private set; }
        public StudentStatus Status { get; private set; }
        public List<Phone> Phones { get; private set; }
        public Passport Passport { get; set; }
        public PhotoUrl PhotoUrl { get; set; }
        public Email Email { get; set; }

        protected Student() { }

        public Student(FullName fullName, Passport passport, PhotoUrl photoUrl, Email email)
        {
            if (fullName is null)
                throw new ArgumentException("Can't create student without a name!");

            if (passport is null)
                throw new ArgumentException("Can not create student without a passport number!");

            Id = default;
            FullName = fullName;
            Status = StudentStatus.New;
            Phones = new List<Phone>();
            Passport = passport;
            PhotoUrl = photoUrl;
            Email = email;
        }

        public void Expose()
        {
            if (Status != StudentStatus.Current)
                throw new Exception("Can't expose not current student!");

            Status = StudentStatus.Exposed;

            AddDomainEvent(new StudentExposedEvent { StudentId = Id });
        }

        public void Graduate()
        {
            if (Status != StudentStatus.Current)
                throw new Exception("Can not graduate not current student!");

            Status = StudentStatus.Graduated;
        }

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        public void RemovePhone(Phone phone)
        {
            var phoneToRemove = Phones.FirstOrDefault(x => x == phone);

            if (phoneToRemove is null)
                throw new Exception("The provided phone to remove is not found!");

            Phones.Remove(phoneToRemove);
        }

        public void ReplacePhone(Phone phoneToReplace, Phone newPhone)
        {
            if(phoneToReplace is null)
                throw new Exception("The provided phone to replace is not found!");

            if(newPhone is null)
                throw new Exception("A new phone must be provided!");

            Phones[Phones.FindIndex(x => x.Equals(phoneToReplace))] = newPhone;
        }

        public void Rename(FullName newFullName)
        {
            if(newFullName is null)
                throw new Exception("A new full name must be provided!");

            FullName = newFullName;
        }

        public void ChangePassport(Passport newPassport)
        {
           if(newPassport is null)
                throw new Exception("A new passport info must be provided!");

           Passport = newPassport;
        }

        public void ChangePhotoUrl(PhotoUrl newPhotoUrl)
        {
            if(newPhotoUrl is null)
                throw new Exception("A new photo must be provided!");

            PhotoUrl = newPhotoUrl;
        }

        public void RemovePhotoUrl()
        {
            PhotoUrl = PhotoUrl.Empty;
        }

        public void ChangeEmail(Email newEmail)
        {
            if(newEmail is null)
                throw new Exception("A new email must be provided");

            Email = newEmail;
        }

        public void RemoveEmail()
        {
            Email = Email.Empty;
        }

        public void AssignToGroup()
        {
            if (Status == StudentStatus.Exposed || Status == StudentStatus.Graduated)
                throw new Exception("Can not assign to group not current student!");

            Status = StudentStatus.Current;
        }
    }
}
