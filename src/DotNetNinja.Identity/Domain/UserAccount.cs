using System;

namespace DotNetNinja.Identity.Domain
{
    public class UserAccount
    {
        public int Id { get; set; }
        public Guid Subject { get; set; }
        public string SubjectId => (Subject != Guid.Empty) ? Subject.ToString() : string.Empty;
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
}