using ACMTTU.NoteSharing.Shared.DataContracts;

namespace ACMTTU.NoteSharing.SecretsService.Providers
{
    public interface IEnvironmentReader
    {
        EnvironmentVariablePayload GetEnvironmentVariableFromKeyVault(string option);
    }
}