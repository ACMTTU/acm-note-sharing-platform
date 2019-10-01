namespace ACMTTU.NoteSharing.Shared.DataContracts
{
    /// EnvironmentVariablePayload
    /// 
    /// This stores the different environment variables
    public class EnvironmentVariablePayload
    {
        public string Development { get; set; }
        public string Staging { get; set; }
        public string Production { get; set; }
    }
}