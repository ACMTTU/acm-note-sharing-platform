namespace ACMTTU.NoteSharing.SecretsService.Models
{
    public class EnvironmentVariablePayload
    {
        public string Development { get; set; }
        public string Staging { get; set; }
        public string Production { get; set; }
    }
}