using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Contracts;
using EducationSystem.Common.ValueObjects;

namespace EducationSystem.StudentManagement.Core
{
    public class Student : AggregateRoot<int>
    {
        public FullName FullName { get; private set; } = null!;
        public Passport Passport { get; private set; } = null!;
        public PhotoUrl PhotoUrl { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public StudentStatus Status { get; private set; }
        public int GroupId { get; private set; }

        private readonly List<Phone> _phones = new List<Phone>();
        public IReadOnlyList<Phone> Phones => _phones;

        protected Student() {}

        public Student(FullName fullName, Passport passport, PhotoUrl photoUrl, Email email) : this()
        {
            Id = default;
            FullName = fullName;
            Status = StudentStatus.New;
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
            var existedPhone = Phones.FirstOrDefault(x => x == phone);

            if (existedPhone != null!)
                throw new Exception("The provided phone already exists!");

            _phones.Add(phone);
        }

        public void RemovePhone(string phoneNumber)
        {
            var phoneToRemove = Phones.FirstOrDefault(x => x.Number == phoneNumber);

            if (phoneToRemove is null)
                throw new Exception("The provided phone to remove is not found!");

            _phones.Remove(phoneToRemove);
        }

        public void ChangePhone(Phone phoneToReplace, Phone newPhone)
        {
            var index = _phones.FindIndex(x => x.Equals(phoneToReplace));
            if (index < 0)
                throw new Exception("Phone to change not found!");

            _phones[index] = newPhone;
        }

        public void Rename(FullName newFullName)
        {
            FullName = newFullName;
        }

        public void ChangePassport(Passport newPassport)
        {
            Passport = newPassport;
        }

        public void ChangePhotoUrl(PhotoUrl newPhotoUrl)
        {
            PhotoUrl = newPhotoUrl;
        }

        public void RemovePhotoUrl()
        {
            PhotoUrl = PhotoUrl.Empty;
        }

        public void ChangeEmail(Email newEmail)
        {
            Email = newEmail;
        }

        public void RemoveEmail()
        {
            Email = Email.Empty;
        }

        public void AssignToGroup(int groupId)
        {
            if (Status == StudentStatus.Exposed || Status == StudentStatus.Graduated)
                throw new Exception("Can not assign to group exposed or graduated student!");

            if(groupId <= 0)
                throw new Exception("GroupId can not be less or equal 0");

            GroupId = groupId;

            Status = StudentStatus.Current;
        }
    }
}
