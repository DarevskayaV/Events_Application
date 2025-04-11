using System;
using System.Collections.Generic;

namespace Events.Core.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        // ���������� ����������� required ��� ������������ �������
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // ������������� ��������� ��� Participants
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}